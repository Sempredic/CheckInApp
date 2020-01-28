namespace CheckInStation
{
    partial class LocationPrompt
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
            this.locationInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.caseInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // locationInput
            // 
            this.locationInput.Location = new System.Drawing.Point(34, 170);
            this.locationInput.MaximumSize = new System.Drawing.Size(50, 30);
            this.locationInput.MinimumSize = new System.Drawing.Size(300, 30);
            this.locationInput.Name = "locationInput";
            this.locationInput.Size = new System.Drawing.Size(300, 30);
            this.locationInput.TabIndex = 0;
            this.locationInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 125);
            this.label1.MinimumSize = new System.Drawing.Size(300, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Location";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 27);
            this.label2.MinimumSize = new System.Drawing.Size(300, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter Case";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // caseInput
            // 
            this.caseInput.Location = new System.Drawing.Point(37, 72);
            this.caseInput.MaximumSize = new System.Drawing.Size(50, 30);
            this.caseInput.MinimumSize = new System.Drawing.Size(300, 30);
            this.caseInput.Name = "caseInput";
            this.caseInput.Size = new System.Drawing.Size(300, 30);
            this.caseInput.TabIndex = 2;
            this.caseInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.caseInput_KeyDown);
            // 
            // LocationPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 232);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.caseInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.locationInput);
            this.Name = "LocationPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LocationPrompt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox locationInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox caseInput;
    }
}