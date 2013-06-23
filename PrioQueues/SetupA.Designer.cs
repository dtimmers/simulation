namespace PrioQueues
{
    partial class SetupA
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
            this.submit = new System.Windows.Forms.Button();
            this.nrQueuesBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(181, 61);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(52, 23);
            this.submit.TabIndex = 4;
            this.submit.Text = "submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // nrQueuesBox
            // 
            this.nrQueuesBox.FormattingEnabled = true;
            this.nrQueuesBox.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.nrQueuesBox.Location = new System.Drawing.Point(181, 25);
            this.nrQueuesBox.Name = "nrQueuesBox";
            this.nrQueuesBox.Size = new System.Drawing.Size(52, 21);
            this.nrQueuesBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Enter the number of queues:";
            // 
            // SetupA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 106);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nrQueuesBox);
            this.Controls.Add(this.submit);
            this.Name = "SetupA";
            this.Text = "PrioQueues - Queue number";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.ComboBox nrQueuesBox;
        private System.Windows.Forms.Label label1;

    }
}

