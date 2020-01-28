namespace CheckInStation
{
    partial class SkuPrompt
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
            this.devicePictureBox = new System.Windows.Forms.PictureBox();
            this.skuDataGridView = new System.Windows.Forms.DataGridView();
            this.skuPromptInputBox = new System.Windows.Forms.TextBox();
            this.skuOptionInputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deviceSpecsBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.devicePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skuDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // devicePictureBox
            // 
            this.devicePictureBox.Location = new System.Drawing.Point(12, 51);
            this.devicePictureBox.Name = "devicePictureBox";
            this.devicePictureBox.Size = new System.Drawing.Size(212, 221);
            this.devicePictureBox.TabIndex = 0;
            this.devicePictureBox.TabStop = false;
            // 
            // skuDataGridView
            // 
            this.skuDataGridView.AllowUserToDeleteRows = false;
            this.skuDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skuDataGridView.Location = new System.Drawing.Point(230, 207);
            this.skuDataGridView.Name = "skuDataGridView";
            this.skuDataGridView.ReadOnly = true;
            this.skuDataGridView.Size = new System.Drawing.Size(209, 65);
            this.skuDataGridView.TabIndex = 2;
            // 
            // skuPromptInputBox
            // 
            this.skuPromptInputBox.Location = new System.Drawing.Point(107, 348);
            this.skuPromptInputBox.Name = "skuPromptInputBox";
            this.skuPromptInputBox.Size = new System.Drawing.Size(212, 20);
            this.skuPromptInputBox.TabIndex = 3;
            this.skuPromptInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.skuPromptInputBox_KeyDown);
            // 
            // skuOptionInputBox
            // 
            this.skuOptionInputBox.Location = new System.Drawing.Point(107, 394);
            this.skuOptionInputBox.Name = "skuOptionInputBox";
            this.skuOptionInputBox.Size = new System.Drawing.Size(212, 20);
            this.skuOptionInputBox.TabIndex = 4;
            this.skuOptionInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.skuOptionInputBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.MaximumSize = new System.Drawing.Size(500, 0);
            this.label1.MinimumSize = new System.Drawing.Size(200, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Device Overview";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // deviceSpecsBox
            // 
            this.deviceSpecsBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceSpecsBox.Location = new System.Drawing.Point(230, 51);
            this.deviceSpecsBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deviceSpecsBox.Name = "deviceSpecsBox";
            this.deviceSpecsBox.Size = new System.Drawing.Size(209, 150);
            this.deviceSpecsBox.TabIndex = 6;
            this.deviceSpecsBox.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 278);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Prev";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(149, 278);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SkuPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deviceSpecsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skuOptionInputBox);
            this.Controls.Add(this.skuPromptInputBox);
            this.Controls.Add(this.skuDataGridView);
            this.Controls.Add(this.devicePictureBox);
            this.Name = "SkuPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SkuPrompt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SkuPrompt_FormClosing);
            this.Shown += new System.EventHandler(this.SkuPrompt_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.devicePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skuDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox devicePictureBox;
        private System.Windows.Forms.DataGridView skuDataGridView;
        private System.Windows.Forms.TextBox skuPromptInputBox;
        private System.Windows.Forms.TextBox skuOptionInputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox deviceSpecsBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}