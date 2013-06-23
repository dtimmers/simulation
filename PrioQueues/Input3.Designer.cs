namespace PrioQueues
{
    partial class Input3
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.meanArr1 = new System.Windows.Forms.TextBox();
            this.meanArr2 = new System.Windows.Forms.TextBox();
            this.meanArr3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.meanProc1 = new System.Windows.Forms.TextBox();
            this.meanProc2 = new System.Windows.Forms.TextBox();
            this.meanProc3 = new System.Windows.Forms.TextBox();
            this.submit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label8 = new System.Windows.Forms.Label();
            this.intNumWorkers = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Queue 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Queue 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Queue 3";
            // 
            // meanArr1
            // 
            this.meanArr1.Location = new System.Drawing.Point(80, 63);
            this.meanArr1.Name = "meanArr1";
            this.meanArr1.Size = new System.Drawing.Size(100, 20);
            this.meanArr1.TabIndex = 5;
            this.meanArr1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // meanArr2
            // 
            this.meanArr2.Location = new System.Drawing.Point(80, 95);
            this.meanArr2.Name = "meanArr2";
            this.meanArr2.Size = new System.Drawing.Size(100, 20);
            this.meanArr2.TabIndex = 6;
            this.meanArr2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // meanArr3
            // 
            this.meanArr3.Location = new System.Drawing.Point(80, 127);
            this.meanArr3.Name = "meanArr3";
            this.meanArr3.Size = new System.Drawing.Size(100, 20);
            this.meanArr3.TabIndex = 7;
            this.meanArr3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Interarrival times";
            // 
            // meanProc1
            // 
            this.meanProc1.Location = new System.Drawing.Point(212, 63);
            this.meanProc1.Name = "meanProc1";
            this.meanProc1.Size = new System.Drawing.Size(100, 20);
            this.meanProc1.TabIndex = 11;
            this.meanProc1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // meanProc2
            // 
            this.meanProc2.Location = new System.Drawing.Point(212, 95);
            this.meanProc2.Name = "meanProc2";
            this.meanProc2.Size = new System.Drawing.Size(100, 20);
            this.meanProc2.TabIndex = 12;
            this.meanProc2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // meanProc3
            // 
            this.meanProc3.Location = new System.Drawing.Point(212, 127);
            this.meanProc3.Name = "meanProc3";
            this.meanProc3.Size = new System.Drawing.Size(100, 20);
            this.meanProc3.TabIndex = 13;
            this.meanProc3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(157, 200);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 16;
            this.submit.Text = "submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Processing times";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Total number of workers:";
            // 
            // intNumWorkers
            // 
            this.intNumWorkers.Location = new System.Drawing.Point(157, 166);
            this.intNumWorkers.Name = "intNumWorkers";
            this.intNumWorkers.Size = new System.Drawing.Size(100, 20);
            this.intNumWorkers.TabIndex = 19;
            this.intNumWorkers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Average";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Average";
            // 
            // Input3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 239);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.intNumWorkers);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.meanProc3);
            this.Controls.Add(this.meanProc2);
            this.Controls.Add(this.meanProc1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.meanArr3);
            this.Controls.Add(this.meanArr2);
            this.Controls.Add(this.meanArr1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Input3";
            this.Text = "PrioQueues - Queue input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox meanArr1;
        private System.Windows.Forms.TextBox meanArr2;
        private System.Windows.Forms.TextBox meanArr3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox meanProc1;
        private System.Windows.Forms.TextBox meanProc2;
        private System.Windows.Forms.TextBox meanProc3;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox intNumWorkers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}