namespace PI_AF.Sinus
{
    partial class RandomConfigEditor
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
            this.cbCounter = new System.Windows.Forms.CheckBox();
            this.numFreq = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCounter
            // 
            this.cbCounter.AutoSize = true;
            this.cbCounter.Location = new System.Drawing.Point(12, 12);
            this.cbCounter.Name = "cbCounter";
            this.cbCounter.Size = new System.Drawing.Size(185, 17);
            this.cbCounter.TabIndex = 0;
            this.cbCounter.Text = "Value above 10 is counter of gets";
            this.cbCounter.UseVisualStyleBackColor = true;
            // 
            // numFreq
            // 
            this.numFreq.Location = new System.Drawing.Point(94, 40);
            this.numFreq.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFreq.Name = "numFreq";
            this.numFreq.Size = new System.Drawing.Size(103, 20);
            this.numFreq.TabIndex = 1;
            this.numFreq.ThousandsSeparator = true;
            this.numFreq.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Frequency [Hz]";
            // 
            // bOK
            // 
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(197, 114);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // RandomConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 152);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numFreq);
            this.Controls.Add(this.cbCounter);
            this.Name = "RandomConfigEditor";
            this.Text = "RandomConfigEditor";
            ((System.ComponentModel.ISupportInitialize)(this.numFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbCounter;
        private System.Windows.Forms.NumericUpDown numFreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
    }
}