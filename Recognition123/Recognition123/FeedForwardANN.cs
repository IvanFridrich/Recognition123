using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognition123
{

    /// <summary>
    /// Delegate for reporting progress of training of the ANN.
    /// </summary>
    /// <param name="perc">Percentual progress (0-100)</param>
    /// <param name="eta">Estimated time to finish</param>
    public delegate void TrainingProgressDelgate(int perc, TimeSpan eta);

    /// <summary>
    /// Structure for holding results of recognition by ANN.
    /// </summary>
    public class FeedForwardANNOutput
    {
        /// <summary>
        /// Output vector
        /// </summary>
        public double[] outputValues;

        /// <summary>
        /// Hidden layer outputs vector
        /// </summary>
        public double[] hiddenLayerOutput;

        /// <summary>
        /// Normalizes output so the sum of the outputs will be aprox. 1.
        /// </summary>
        public void Normalize()
        {
            double sum = outputValues.Sum();

            for (int index = 0; index < outputValues.Length; index++)
            {
                outputValues[index] /= sum;
            }
        }

        /// <summary>
        /// Converts output vector to a simplified string. 
        /// </summary>
        /// <returns>String describing the output vector</returns>
        public string SimplifiedOutputAsString()
        {
            if (outputValues.All(x => x < 0.7)) return "Not sure";

            switch (MaxOutputIndex())
            {
                case 0: { return "One"; }
                case 1: { return "Two"; }
                case 2: { return "Three"; }
            }

            // The program shouldn't get here!
            return "Internal Error";
        }

        /// <summary>
        /// Converts output vector to a string.
        /// </summary>
        /// <returns>Output vector as a string</returns>
        public override string ToString()
        {
            string one = string.Format("{0:0.00}", Math.Round(outputValues[0], 2));
            string two = string.Format("{0:0.00}", Math.Round(outputValues[1], 2));
            string three = string.Format("{0:0.00}", Math.Round(outputValues[2], 2));

            return $"One={one} Two={two} Three={three}";
        }

        /// <summary>
        /// Returns index of the highest value in the output vector.
        /// </summary>
        /// <returns>The highest value index in output values</returns>
        private int MaxOutputIndex()
        {
            double max = double.MinValue;
            int maxIndex = -1;

            for (int i = 0; i < outputValues.Length; ++i)
            {
                if (outputValues[i] > max)
                {
                    max = outputValues[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }

    public class FeedForwardANN
    {
        /// <summary>
        /// Hiddel layer neurons
        /// </summary>
        public Neuron[] hiddenLayer;

        /// <summary>
        /// Output layer neurons
        /// </summary>
        public Neuron[] outputLayer;

        /// <summary>
        /// Stop flag for stopping the training routine
        /// </summary>
        bool stopTrainingFlag;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputSize">Input vector size</param>
        /// <param name="hiddenLayerSize">Hidden layer neuron count</param>
        /// <param name="outputLayerSize">Size of the output vector</param>
        public FeedForwardANN(int inputSize, int hiddenLayerSize, int outputLayerSize)
        {
            hiddenLayer = new Neuron[hiddenLayerSize];

            for (int i = 0; i < hiddenLayer.Length; ++i)
                hiddenLayer[i] = new Neuron(inputSize);

            outputLayer = new Neuron[outputLayerSize];
            for (int i = 0; i < outputLayer.Length; ++i)
                outputLayer[i] = new Neuron(hiddenLayerSize);
        }

        /// <summary>
        /// Calculate output vector based on input vector.
        /// </summary>
        /// <param name="inputVector">Input vector</param>
        /// <returns>Output vector</returns>
        public FeedForwardANNOutput CalcOutput(double[] inputVector)
        {
            FeedForwardANNOutput output = new FeedForwardANNOutput();

            // Calculate outputs of the hidden layer
            output.hiddenLayerOutput = new double[hiddenLayer.Length];
            for (int i = 0; i < hiddenLayer.Length; ++i)
                output.hiddenLayerOutput[i] = hiddenLayer[i].CalcOutput(inputVector);

            // Calculate outputs of the output layer
            output.outputValues = new double[outputLayer.Length];
            for (int i = 0; i < outputLayer.Length; ++i)
                output.outputValues[i] = outputLayer[i].CalcOutput(output.hiddenLayerOutput);

            return output;
        }

        /// <summary>
        /// Stops training
        /// </summary>
        public void StopTraining()
        {
            stopTrainingFlag = true;
        }

        /// <summary>
        /// Trains the ANN - Gradient Descent method
        /// </summary>
        /// <param name="inputs">List of input vectors</param>
        /// <param name="expectedOutputs">List of expected output vectors</param>
        /// <param name="epochs">Number of training epochs</param>
        /// <param name="trainingProgressDelgate">Report progress callback</param>
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
                        // report progress
                        double progress = (double)iters / (double)totalIters;
                        DateTime now = DateTime.Now;
                        double totalSeconds = (now - start).TotalSeconds / progress * (1 - progress);
                        trainingProgressDelgate?.Invoke((int)(progress * 100.0), TimeSpan.FromSeconds(totalSeconds));
                    }

                    // If stop flag is true - stop training
                    if (stopTrainingFlag)
                    {
                        stopTrainingFlag = false;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Single input training - Gradient Descent method
        /// </summary>
        /// <param name="input">Input vector</param>
        /// <param name="expectedOutput">Expected output vector</param>
        private void Train(double[] input, double[] expectedOutput)
        {
            var output = CalcOutput(input);

            double[,] gradWout = new double[output.outputValues.Length, output.hiddenLayerOutput.Length];
            double[] biasesOut = new double[output.outputValues.Length];

            // output stage
            for (int i = 0; i < output.outputValues.Length; ++i)
            {
                double derE_derOut = output.outputValues[i] - expectedOutput[i];
                double derOut_derNet = output.outputValues[i] * (1.0 - output.outputValues[i]);

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
                for (int k = 0; k < output.outputValues.Length; ++k)
                {
                    double derE_derOut = output.outputValues[k] - expectedOutput[k];
                    double derOut_derNet = output.outputValues[k] * (1.0 - output.outputValues[k]);
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
