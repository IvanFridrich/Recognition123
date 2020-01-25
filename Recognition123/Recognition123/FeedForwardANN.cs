using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recognition123
{

    public delegate void TrainingProgressDelgate(int perc, TimeSpan eta);
    public class FeedForwardANNOutput
    {
        public double[] values;
        public double[] hiddenLayerOutput;

        public void Normalize()
        {
            double sum = values.Sum();

            for (int index = 0; index < values.Length; index++)
            {
                values[index] /= sum;
            }
        }

        public string Output()
        {
            if (values.All(x => x < 0.7)) return "Not sure";

            switch (MaxOutputIndex())
            {
                case 0: { return "One"; }
                case 1: { return "Two"; }
                case 2: { return "Three"; }
            }

            return "Internal Error";
        }

        public override string ToString()
        {
            string one = string.Format("{0:0.00}", Math.Round(values[0], 2));
            string two = string.Format("{0:0.00}", Math.Round(values[1], 2));
            string three = string.Format("{0:0.00}", Math.Round(values[2], 2));

            return $"One={one} Two={two} Three={three}";
        }

        public int MaxOutputIndex()
        {
            double max = double.MinValue;
            int maxIndex = -1;

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] > max)
                {
                    max = values[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }

    public class FeedForwardANN
    {
        public Neuron[] hiddenLayer;
        public Neuron[] outputLayer;
        bool stopTraining;

        public FeedForwardANN(int inputSize, int hiddenLayerSize, int outputLayerSize)
        {
            hiddenLayer = new Neuron[hiddenLayerSize];

            for (int i = 0; i < hiddenLayer.Length; ++i)
                hiddenLayer[i] = new Neuron(inputSize);

            outputLayer = new Neuron[outputLayerSize];
            for (int i = 0; i < outputLayer.Length; ++i)
                outputLayer[i] = new Neuron(hiddenLayerSize);

        }

        public FeedForwardANNOutput CalcOutput(double[] inputVector)
        {
            FeedForwardANNOutput output = new FeedForwardANNOutput();

            output.hiddenLayerOutput = new double[hiddenLayer.Length];
            for (int i = 0; i < hiddenLayer.Length; ++i)
                output.hiddenLayerOutput[i] = hiddenLayer[i].CalcOutput(inputVector);

            output.values = new double[outputLayer.Length];
            for (int i = 0; i < outputLayer.Length; ++i)
                output.values[i] = outputLayer[i].CalcOutput(output.hiddenLayerOutput);

            return output;
        }

        public void StopTraining()
        {
            stopTraining = true;
        }

        public void Train(List<double[]> inputs, List<double[]> expectedOutputs, int epochs, TrainingProgressDelgate trainingProgressDelgate)
        {
            var indexes = Enumerable.Range(0, inputs.Count).ToList();

            Random r = new Random();

            int totalIters = epochs * inputs.Count;
            int iters = 0;
            DateTime start = DateTime.Now;

            for (int i = 0; i < epochs; ++i)
            {
                var indexesClone = new List<int>(indexes);

                for (int j = 0; j < inputs.Count; ++j)
                {
                    // take random input and coresponding expected output and train the ANN
                    var x = r.Next() % indexesClone.Count;
                    int ind = indexesClone[x];
                    indexesClone.RemoveAt(x);
                    Train(inputs[ind], expectedOutputs[ind]);
                    ++iters;

                    if (iters % 50 == 0 && trainingProgressDelgate != null)
                    {
                        double progress = (double)iters/(double)totalIters;
                        DateTime now = DateTime.Now;
                        double totalSeconds = (now - start).TotalSeconds / progress * (1 - progress);

                        trainingProgressDelgate?.Invoke((int)(progress * 100.0), TimeSpan.FromSeconds(totalSeconds));
                    }

                    if (stopTraining)
                    {
                        stopTraining = false;
                        return;
                    }
                }
            }
        }

        private void Train(double[] input, double[] expectedOutput)
        {
            var output = CalcOutput(input);

            double[,] gradWout = new double[output.values.Length, output.hiddenLayerOutput.Length];
            double[] biasesOut = new double[output.values.Length];

            // output stage
            for (int i = 0; i < output.values.Length; ++i)
            {
                double derE_derOut = output.values[i] - expectedOutput[i];
                double derOut_derNet = output.values[i] * (1.0 - output.values[i]);

                // weights
                for (int j = 0; j < output.hiddenLayerOutput.Length; ++j)
                {
                    // chain rule
                    gradWout[i, j] = derE_derOut * derOut_derNet * output.hiddenLayerOutput[j];
                }


                // chain rule
                biasesOut[i] = derE_derOut * derOut_derNet;
            }

            // hidden layer
            double[,] hidLayerWeights = new double[output.hiddenLayerOutput.Length, input.Length];
            double[] biasesHid = new double[output.hiddenLayerOutput.Length];

            for (int j = 0; j < output.hiddenLayerOutput.Length; ++j)
            {
                double grad = 0;
                for (int k = 0; k < output.values.Length; ++k)
                {
                    double derE_derOut = output.values[k] - expectedOutput[k];
                    double derOut_derNet = output.values[k] * (1.0 - output.values[k]);
                    double derNeto1_derOutH1 = outputLayer[k].Weights[j];

                    grad += derE_derOut * derOut_derNet * derNeto1_derOutH1;
                }

                double derOutH_derNetH = output.hiddenLayerOutput[j] * (1.0 - output.hiddenLayerOutput[j]);

                // weights
                for (int i = 0; i < input.Length; ++i)
                {
                    hidLayerWeights[j, i] = grad * derOutH_derNetH * input[i];
                }

                // biases
                biasesHid[j] = grad * derOutH_derNetH;
            }

            // update neural network
            // output layer
            for (int i = 0; i < outputLayer.Length; ++i)
            {
                for (int j = 0; j < hiddenLayer.Length; ++j)
                    outputLayer[i].Weights[j] -= 0.5 * gradWout[i, j];

                outputLayer[i].Bias -= 0.5 * biasesOut[i];
            }

            // hidden layer
            for (int i = 0; i < hiddenLayer.Length; ++i)
            {
                for (int j = 0; j < input.Length; ++j)
                    hiddenLayer[i].Weights[j] -= 0.5 * hidLayerWeights[i, j];

                hiddenLayer[i].Bias -= 0.5 * biasesHid[i];
            }
        }
    }
}
