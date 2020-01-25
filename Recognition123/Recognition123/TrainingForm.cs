using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Recognition123
{
    public partial class TrainingForm : Form
    {
        private int Epochs { get; }
        private FeedForwardANN ANN { get; }

        public TrainingForm(FeedForwardANN ann, int epochs)
        {
            ANN = ann;
            Epochs = epochs;

            InitializeComponent();

            ThreadPool.QueueUserWorkItem(TrainTheAnn);
        }

        public void TrainTheAnn(Object stateInfo)
        {
            // load input data
            var files = Directory.GetFiles("../../trainingData");

            var inputs = new List<double[]>();
            var expected = new List<double[]>();

            double[] expected1 = new double[] { 1.0, 0, 0 };
            var data1Files = files.Where(x => new FileInfo(x).Name.StartsWith("1"));
            foreach (var f in data1Files)
            {
                var d1 = Utils.Utils.BitmapToVector((Bitmap)Image.FromFile(f));
                inputs.Add(d1);
                expected.Add(expected1);
            }

            double[] expected2 = new double[] { 0, 1.0, 0 };
            var data2Files = files.Where(x => new FileInfo(x).Name.StartsWith("2"));
            foreach (var f in data2Files)
            {
                var d2 = Utils.Utils.BitmapToVector((Bitmap)Image.FromFile(f));
                inputs.Add(d2);
                expected.Add(expected2);
            }

            double[] expected3 = new double[] { 0, 0, 1.0 };
            var data3Files = files.Where(x => new FileInfo(x).Name.StartsWith("3"));
            foreach (var f in data3Files)
            {
                var d3 = Utils.Utils.BitmapToVector((Bitmap)Image.FromFile(f));
                inputs.Add(d3);
                expected.Add(expected3);
            }

            ANN.Train(inputs, expected, Epochs, OnTrainingProgress);
            FormClosing -= TrainingForm_FormClosing;

            Invoke(new Action(() =>
            {
                Close();
            }));
        }

        void OnTrainingProgress(int perc, TimeSpan eta)
        {
            Invoke(new Action(() =>
            {
                progressBar1.Value = perc;
                labeETA.Text = "ETA " + eta.ToString();
            }));
        }

        private void TrainingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ANN.StopTraining();
            FormClosing -= TrainingForm_FormClosing;
        }
    }
}
