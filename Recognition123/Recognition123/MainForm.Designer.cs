namespace Recognition123
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonTrain = new System.Windows.Forms.Button();
            this.numericUpDownEpochs = new System.Windows.Forms.NumericUpDown();
            this.labelRecognitionResult = new System.Windows.Forms.Label();
            this.groupBoxTraining = new System.Windows.Forms.GroupBox();
            this.numericUpDownHiddenLayerSize = new System.Windows.Forms.NumericUpDown();
            this.labelHiddenLayerSize = new System.Windows.Forms.Label();
            this.labelEpochCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelProbablilities = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.drawingBox = new Recognition123.DrawingBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpochs)).BeginInit();
            this.groupBoxTraining.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHiddenLayerSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(8, 255);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonTrain
            // 
            this.buttonTrain.Location = new System.Drawing.Point(8, 85);
            this.buttonTrain.Name = "buttonTrain";
            this.buttonTrain.Size = new System.Drawing.Size(107, 23);
            this.buttonTrain.TabIndex = 8;
            this.buttonTrain.Text = "Train the ANN ...";
            this.buttonTrain.UseVisualStyleBackColor = true;
            this.buttonTrain.Click += new System.EventHandler(this.buttonTrainANN_Click);
            // 
            // numericUpDownEpochs
            // 
            this.numericUpDownEpochs.Location = new System.Drawing.Point(121, 19);
            this.numericUpDownEpochs.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownEpochs.Name = "numericUpDownEpochs";
            this.numericUpDownEpochs.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownEpochs.TabIndex = 9;
            this.numericUpDownEpochs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // labelRecognitionResult
            // 
            this.labelRecognitionResult.AutoSize = true;
            this.labelRecognitionResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRecognitionResult.Location = new System.Drawing.Point(159, 127);
            this.labelRecognitionResult.Name = "labelRecognitionResult";
            this.labelRecognitionResult.Size = new System.Drawing.Size(430, 55);
            this.labelRecognitionResult.TabIndex = 14;
            this.labelRecognitionResult.Text = "Train the ANN first!";
            // 
            // groupBoxTraining
            // 
            this.groupBoxTraining.Controls.Add(this.numericUpDownHiddenLayerSize);
            this.groupBoxTraining.Controls.Add(this.labelHiddenLayerSize);
            this.groupBoxTraining.Controls.Add(this.labelEpochCount);
            this.groupBoxTraining.Controls.Add(this.buttonTrain);
            this.groupBoxTraining.Controls.Add(this.numericUpDownEpochs);
            this.groupBoxTraining.Location = new System.Drawing.Point(10, 10);
            this.groupBoxTraining.Name = "groupBoxTraining";
            this.groupBoxTraining.Size = new System.Drawing.Size(624, 114);
            this.groupBoxTraining.TabIndex = 15;
            this.groupBoxTraining.TabStop = false;
            this.groupBoxTraining.Text = "ANN training";
            // 
            // numericUpDownHiddenLayerSize
            // 
            this.numericUpDownHiddenLayerSize.Location = new System.Drawing.Point(121, 49);
            this.numericUpDownHiddenLayerSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownHiddenLayerSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHiddenLayerSize.Name = "numericUpDownHiddenLayerSize";
            this.numericUpDownHiddenLayerSize.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownHiddenLayerSize.TabIndex = 12;
            this.numericUpDownHiddenLayerSize.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // labelHiddenLayerSize
            // 
            this.labelHiddenLayerSize.AutoSize = true;
            this.labelHiddenLayerSize.Location = new System.Drawing.Point(7, 51);
            this.labelHiddenLayerSize.Name = "labelHiddenLayerSize";
            this.labelHiddenLayerSize.Size = new System.Drawing.Size(87, 13);
            this.labelHiddenLayerSize.TabIndex = 11;
            this.labelHiddenLayerSize.Text = "Hidden layer size";
            // 
            // labelEpochCount
            // 
            this.labelEpochCount.AutoSize = true;
            this.labelEpochCount.Location = new System.Drawing.Point(7, 20);
            this.labelEpochCount.Name = "labelEpochCount";
            this.labelEpochCount.Size = new System.Drawing.Size(108, 13);
            this.labelEpochCount.TabIndex = 10;
            this.labelEpochCount.Text = "Training epoch count";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelProbablilities);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.drawingBox);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.labelRecognitionResult);
            this.groupBox1.Location = new System.Drawing.Point(10, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 401);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "123 recognition";
            // 
            // labelProbablilities
            // 
            this.labelProbablilities.AutoSize = true;
            this.labelProbablilities.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProbablilities.Location = new System.Drawing.Point(164, 211);
            this.labelProbablilities.Name = "labelProbablilities";
            this.labelProbablilities.Size = new System.Drawing.Size(0, 13);
            this.labelProbablilities.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Draw 1,2 or 3";
            // 
            // drawingBox
            // 
            this.drawingBox.Image = ((System.Drawing.Image)(resources.GetObject("drawingBox.Image")));
            this.drawingBox.Location = new System.Drawing.Point(8, 49);
            this.drawingBox.Name = "drawingBox";
            this.drawingBox.Size = new System.Drawing.Size(150, 200);
            this.drawingBox.TabIndex = 13;
            this.drawingBox.TabStop = false;
            this.drawingBox.DrawingDone += new Recognition123.DrawingBox.DrawingDoneDelegate(this.drawingBox_DrawingDone);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 544);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxTraining);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Recognition123";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpochs)).EndInit();
            this.groupBoxTraining.ResumeLayout(false);
            this.groupBoxTraining.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHiddenLayerSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonTrain;
        private System.Windows.Forms.NumericUpDown numericUpDownEpochs;
        private DrawingBox drawingBox;
        private System.Windows.Forms.Label labelRecognitionResult;
        private System.Windows.Forms.GroupBox groupBoxTraining;
        private System.Windows.Forms.NumericUpDown numericUpDownHiddenLayerSize;
        private System.Windows.Forms.Label labelHiddenLayerSize;
        private System.Windows.Forms.Label labelEpochCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelProbablilities;
    }
}

