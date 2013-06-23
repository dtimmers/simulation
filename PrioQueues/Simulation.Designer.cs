namespace PrioQueues
{
    partial class Simulation
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
            this.totalRunsBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.startSimulationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the number of runs for the simulation program";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "(more runs means higher accuracy)";
            // 
            // totalRunsBox
            // 
            this.totalRunsBox.Location = new System.Drawing.Point(283, 24);
            this.totalRunsBox.Name = "totalRunsBox";
            this.totalRunsBox.Size = new System.Drawing.Size(100, 20);
            this.totalRunsBox.TabIndex = 2;
            this.totalRunsBox.Text = "10000";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 151);
            this.progressBar.Maximum = 10000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(368, 36);
            this.progressBar.TabIndex = 3;
            // 
            // startSimulationButton
            // 
            this.startSimulationButton.Location = new System.Drawing.Point(121, 88);
            this.startSimulationButton.Name = "startSimulationButton";
            this.startSimulationButton.Size = new System.Drawing.Size(174, 36);
            this.startSimulationButton.TabIndex = 4;
            this.startSimulationButton.Text = "start simulation";
            this.startSimulationButton.UseVisualStyleBackColor = true;
            this.startSimulationButton.Click += new System.EventHandler(this.startSimulationButton_Click);
            // 
            // Simulation
            // 
            this.ClientSize = new System.Drawing.Size(422, 228);
            this.Controls.Add(this.startSimulationButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.totalRunsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Simulation";
            this.Text = "Prioqueues - Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox totalRunsBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button startSimulationButton;
    }
}