namespace BelferCS
{
    partial class dlgWait
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbrProgress = new System.Windows.Forms.ProgressBar();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.pbrProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 135);
            this.panel1.TabIndex = 0;
            // 
            // pbrProgress
            // 
            this.pbrProgress.Location = new System.Drawing.Point(12, 86);
            this.pbrProgress.MarqueeAnimationSpeed = 15;
            this.pbrProgress.Maximum = 1;
            this.pbrProgress.Name = "pbrProgress";
            this.pbrProgress.Size = new System.Drawing.Size(362, 23);
            this.pbrProgress.Step = 1;
            this.pbrProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbrProgress.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label2.Location = new System.Drawing.Point(12, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(362, 23);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Proszę czekać!";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInfo.Location = new System.Drawing.Point(12, 25);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(362, 23);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Przygotowywanie wydruku ...";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dlgWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 135);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "dlgWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Przygotowywanie wydruku ...";
            this.Text = "dlgWait";
            this.Load += new System.EventHandler(this.dlgWait_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ProgressBar pbrProgress;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lblInfo;
    }
}