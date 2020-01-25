using System;

namespace Recognition123
{
    public class Neuron
    {
        public double[] Weights { set; get; } = null;

        public double Bias { set; get; } = double.NaN;

        public Neuron(int size)
        {
            InitRandom(size);
        }

        public void InitRandom(int numberOfInputs)
        {
            Weights = new double[numberOfInputs];

            Random r = new Random();
            for (int i = 0; i < Weights.Length; ++i)
            {
                Weights[i] = (RollDouble() % 1.0) - 2.0;
                Weights[i] /= Weights.Length;
            }

            Bias = RollDouble() % 10;
        }

        public double CalcOutput(double[] inputs)
        {
            if (inputs.Length != Weights.Length) throw new Exception("Vector sizes doesn't match");

            double output = 0;

            for (int i = 0; i < Weights.Length; ++i)
            {
                output += Weights[i] * inputs[i];
            }

            output = Sigmoid(output);

            return output;
        }

        static public double Sigmoid(double x)
        {
            return 1.0 / (1 + Math.Exp(-x));
        }

        public static int RollInt()
        {
            return _rand.Next();
        }
        public static double RollDouble()
        {
            return _rand.NextDouble();
        }

        private static Random _rand = new Random();
    }
}
