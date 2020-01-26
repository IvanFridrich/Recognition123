using System;

namespace Recognition123
{
    public class Neuron
    {
        /// <summary>
        /// Neuron weights
        /// </summary>
        public double[] Weights { set; get; } = null;

        /// <summary>
        /// Bias (offset)
        /// </summary>
        public double Bias { set; get; } = double.NaN;

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random Rand { get; } = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputVectorSize">Size of the input vector</param>
        public Neuron(int inputVectorSize)
        {
            InitRandom(inputVectorSize);
        }

        /// <summary>
        /// Initialize neuron weights by random numbers
        /// </summary>
        /// <param name="inputVectorSize">Size of the input vector</param>
        private void InitRandom(int inputVectorSize)
        {
            Weights = new double[inputVectorSize];

            for (int i = 0; i < Weights.Length; ++i)
            {
                Weights[i] = (Rand.NextDouble() % 1.0) - 2.0;
                Weights[i] /= Weights.Length;
            }

            Bias = Rand.NextDouble() % 10;
        }

        /// <summary>
        /// Calculate output value - sigmoid(inputs * weights + bias)
        /// </summary>
        /// <param name="inputs">Input vector</param>
        public double CalcOutput(double[] inputs)
        {
            if (inputs.Length != Weights.Length) throw new Exception("Vector sizes doesn't match");

            double output = 0;

            for (int i = 0; i < Weights.Length; ++i)
            {
                output += Weights[i] * inputs[i];
            }

            output += Bias;

            output = Sigmoid(output);

            return output;
        }

        /// <summary>
        /// Activation function
        /// </summary>
        /// <param name="x">Input value</param>
        /// <returns>Output value (0 - 1)</returns>
        static private double Sigmoid(double x)
        {
            return 1.0 / (1 + Math.Exp(-x));
        }
    }
}
