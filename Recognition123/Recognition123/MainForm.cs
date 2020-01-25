using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace Recognition123
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Instance of the Artifitial Nerual Network
        /// </summary>
        private FeedForwardANN ANN { get; set; }
        
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clear button click handler.
        /// </summary>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            drawingBox.Clear();

            if (ANN == null) labelRecognitionResult.Text = "Train the ANN first!";
            else labelRecognitionResult.Text = "None";

            labelProbablilities.Text = "";
        }

        /// <summary>
        /// Train ANN button click handler.
        /// </summary>
        private void buttonTrainANN_Click(object sender, EventArgs e)
        {
            const int imageWidth = 15;
            const int imageHeight = 20;

            var ann  = new FeedForwardANN(imageWidth * imageHeight, (int)numericUpDownHiddenLayerSize.Value, 3);

            TrainingForm trainingForm = new TrainingForm(ann, (int)numericUpDownEpochs.Value);
            trainingForm.ShowDialog();
            trainingForm.Close();

            ANN = ann;

            labelRecognitionResult.Text = "None";
        }

        /// <summary>
        /// Drawing done event handler
        /// </summary>
        private void drawingBox_DrawingDone()
        {
            if (ANN == null)
            {
                labelRecognitionResult.Text = "Train the ANN first!";
                return;
            }
            
            Bitmap input = Utils.Utils.MovePictureContentToUpperLeftCorner(drawingBox.GetBitmap());
            var inputVector = Utils.Utils.BitmapToVector(input);
            var output = ANN.CalcOutput(inputVector);
            output.Normalize();
            labelRecognitionResult.Text = output.Output();
            labelProbablilities.Text = output.ToString();
        }
    }
}
