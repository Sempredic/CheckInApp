namespace CheckInStation
{
    partial class Form1
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
            this.printButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.deviceList = new System.Windows.Forms.DataGridView();
            this.DeviceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMEI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewImageColumn();
            this.Carrier = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.clearButton = new System.Windows.Forms.Button();
            this.numSelectedLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.numSelectedLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.databaseLocation = new System.Windows.Forms.TextBox();
            this.skuCheckBox = new System.Windows.Forms.CheckBox();
            this.selectOptionCB = new System.Windows.Forms.ComboBox();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.modelCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelerLocation = new System.Windows.Forms.TextBox();
            this.dataLocation = new System.Windows.Forms.TextBox();
            this.printQRCButton = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.serialInputBox = new System.Windows.Forms.TextBox();
            this.locInputBox = new System.Windows.Forms.TextBox();
            this.skuInputBox = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.printOnlyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.deviceList)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(660, 375);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(127, 39);
            this.printButton.TabIndex = 1;
            this.printButton.Text = "CheckIn/Print";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Clicked);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(394, 375);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(127, 39);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Clicked);
            // 
            // deviceList
            // 
            this.deviceList.AllowUserToAddRows = false;
            this.deviceList.AllowUserToDeleteRows = false;
            this.deviceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deviceList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceCol,
            this.Serial,
            this.IMEI,
            this.MEI,
            this.Model,
            this.Color,
            this.Carrier});
            this.deviceList.Location = new System.Drawing.Point(5, 6);
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(925, 351);
            this.deviceList.TabIndex = 4;
            this.deviceList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.deviceList_CellClick);
            this.deviceList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.deviceList_CellMouseUp);
            this.deviceList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvFeesHead_KeyDown);
            this.deviceList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.deviceList_MouseUp);
            // 
            // DeviceCol
            // 
            this.DeviceCol.HeaderText = "Device";
            this.DeviceCol.Name = "DeviceCol";
            this.DeviceCol.ReadOnly = true;
            this.DeviceCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeviceCol.Width = 180;
            // 
            // Serial
            // 
            this.Serial.HeaderText = "Serial";
            this.Serial.Name = "Serial";
            this.Serial.ReadOnly = true;
            this.Serial.Width = 120;
            // 
            // IMEI
            // 
            this.IMEI.HeaderText = "IMEI";
            this.IMEI.Name = "IMEI";
            this.IMEI.ReadOnly = true;
            this.IMEI.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IMEI.Width = 120;
            // 
            // MEI
            // 
            this.MEI.HeaderText = "MEI";
            this.MEI.Name = "MEI";
            this.MEI.ReadOnly = true;
            this.MEI.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MEI.Width = 120;
            // 
            // Model
            // 
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Model.Width = 120;
            // 
            // Color
            // 
            this.Color.HeaderText = "Color";
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            this.Color.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Color.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Carrier
            // 
            this.Carrier.HeaderText = "Carrier";
            this.Carrier.Items.AddRange(new object[] {
            "ATT",
            "Verizon",
            "TMobile",
            "Sprint",
            "Other"});
            this.Carrier.Name = "Carrier";
            this.Carrier.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Carrier.Width = 120;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(944, 446);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.printOnlyButton);
            this.tabPage1.Controls.Add(this.clearButton);
            this.tabPage1.Controls.Add(this.numSelectedLabel);
            this.tabPage1.Controls.Add(this.statusLabel);
            this.tabPage1.Controls.Add(this.numSelectedLbl);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.deviceList);
            this.tabPage1.Controls.Add(this.loadButton);
            this.tabPage1.Controls.Add(this.printButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(936, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(527, 375);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(127, 39);
            this.clearButton.TabIndex = 9;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Clicked);
            // 
            // numSelectedLabel
            // 
            this.numSelectedLabel.AutoSize = true;
            this.numSelectedLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSelectedLabel.Location = new System.Drawing.Point(280, 383);
            this.numSelectedLabel.Name = "numSelectedLabel";
            this.numSelectedLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numSelectedLabel.Size = new System.Drawing.Size(17, 20);
            this.numSelectedLabel.TabIndex = 8;
            this.numSelectedLabel.Text = "0";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(24, 388);
            this.statusLabel.MinimumSize = new System.Drawing.Size(250, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(250, 13);
            this.statusLabel.TabIndex = 7;
            // 
            // numSelectedLbl
            // 
            this.numSelectedLbl.AutoSize = true;
            this.numSelectedLbl.Location = new System.Drawing.Point(358, 388);
            this.numSelectedLbl.Name = "numSelectedLbl";
            this.numSelectedLbl.Size = new System.Drawing.Size(0, 13);
            this.numSelectedLbl.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(304, 385);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Selected";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(936, 420);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Controls.Add(this.databaseLocation);
            this.tabPage2.Controls.Add(this.skuCheckBox);
            this.tabPage2.Controls.Add(this.selectOptionCB);
            this.tabPage2.Controls.Add(this.saveSettingsButton);
            this.tabPage2.Controls.Add(this.modelCheckBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.labelLocation);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.labelerLocation);
            this.tabPage2.Controls.Add(this.dataLocation);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(936, 420);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(270, 325);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Prompt Case";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(462, 93);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(210, 21);
            this.comboBox1.TabIndex = 11;
            // 
            // databaseLocation
            // 
            this.databaseLocation.Location = new System.Drawing.Point(701, 175);
            this.databaseLocation.Name = "databaseLocation";
            this.databaseLocation.Size = new System.Drawing.Size(198, 20);
            this.databaseLocation.TabIndex = 10;
            // 
            // skuCheckBox
            // 
            this.skuCheckBox.AutoSize = true;
            this.skuCheckBox.Checked = true;
            this.skuCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skuCheckBox.Location = new System.Drawing.Point(152, 325);
            this.skuCheckBox.Name = "skuCheckBox";
            this.skuCheckBox.Size = new System.Drawing.Size(84, 17);
            this.skuCheckBox.TabIndex = 9;
            this.skuCheckBox.Text = "Prompt SKU";
            this.skuCheckBox.UseVisualStyleBackColor = true;
            // 
            // selectOptionCB
            // 
            this.selectOptionCB.FormattingEnabled = true;
            this.selectOptionCB.Items.AddRange(new object[] {
            "Normal",
            "Prompt via Serial Select"});
            this.selectOptionCB.Location = new System.Drawing.Point(701, 93);
            this.selectOptionCB.Name = "selectOptionCB";
            this.selectOptionCB.Size = new System.Drawing.Size(151, 21);
            this.selectOptionCB.TabIndex = 8;
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(38, 375);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.saveSettingsButton.TabIndex = 7;
            this.saveSettingsButton.Text = "button1";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Clicked);
            // 
            // modelCheckBox
            // 
            this.modelCheckBox.AutoSize = true;
            this.modelCheckBox.Checked = true;
            this.modelCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modelCheckBox.Location = new System.Drawing.Point(38, 325);
            this.modelCheckBox.Name = "modelCheckBox";
            this.modelCheckBox.Size = new System.Drawing.Size(93, 17);
            this.modelCheckBox.TabIndex = 6;
            this.modelCheckBox.Text = "Include Model";
            this.modelCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Label File Location";
            // 
            // labelLocation
            // 
            this.labelLocation.Location = new System.Drawing.Point(38, 258);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(285, 20);
            this.labelLocation.TabIndex = 4;
            this.labelLocation.Text = "C:\\Program Files\\Microsoft Office\\root\\Office16\\WINWORD.EXE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Labeler Program Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data Source Location";
            // 
            // labelerLocation
            // 
            this.labelerLocation.Location = new System.Drawing.Point(38, 175);
            this.labelerLocation.Name = "labelerLocation";
            this.labelerLocation.Size = new System.Drawing.Size(285, 20);
            this.labelerLocation.TabIndex = 1;
            this.labelerLocation.Text = "C:\\Program Files\\Microsoft Office\\root\\Office16\\WINWORD.EXE";
            // 
            // dataLocation
            // 
            this.dataLocation.Location = new System.Drawing.Point(38, 94);
            this.dataLocation.Name = "dataLocation";
            this.dataLocation.Size = new System.Drawing.Size(285, 20);
            this.dataLocation.TabIndex = 0;
            // 
            // printQRCButton
            // 
            this.printQRCButton.Enabled = false;
            this.printQRCButton.Location = new System.Drawing.Point(80, 353);
            this.printQRCButton.Name = "printQRCButton";
            this.printQRCButton.Size = new System.Drawing.Size(75, 23);
            this.printQRCButton.TabIndex = 8;
            this.printQRCButton.Text = "button1";
            this.printQRCButton.UseVisualStyleBackColor = true;
            this.printQRCButton.Click += new System.EventHandler(this.printQRCButton_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(216, 397);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Locate";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.serialInputBox);
            this.tabPage4.Controls.Add(this.locInputBox);
            this.tabPage4.Controls.Add(this.skuInputBox);
            this.tabPage4.Controls.Add(this.printQRCButton);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(216, 397);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "CheckIn/Print";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // serialInputBox
            // 
            this.serialInputBox.Location = new System.Drawing.Point(17, 175);
            this.serialInputBox.Name = "serialInputBox";
            this.serialInputBox.Size = new System.Drawing.Size(181, 20);
            this.serialInputBox.TabIndex = 11;
            // 
            // locInputBox
            // 
            this.locInputBox.Location = new System.Drawing.Point(17, 263);
            this.locInputBox.Name = "locInputBox";
            this.locInputBox.Size = new System.Drawing.Size(181, 20);
            this.locInputBox.TabIndex = 10;
            // 
            // skuInputBox
            // 
            this.skuInputBox.Location = new System.Drawing.Point(17, 89);
            this.skuInputBox.Name = "skuInputBox";
            this.skuInputBox.Size = new System.Drawing.Size(181, 20);
            this.skuInputBox.TabIndex = 9;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(962, 76);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(224, 423);
            this.tabControl2.TabIndex = 6;
            // 
            // printOnlyButton
            // 
            this.printOnlyButton.Location = new System.Drawing.Point(793, 375);
            this.printOnlyButton.Name = "printOnlyButton";
            this.printOnlyButton.Size = new System.Drawing.Size(127, 39);
            this.printOnlyButton.TabIndex = 10;
            this.printOnlyButton.Text = "Print";
            this.printOnlyButton.UseVisualStyleBackColor = true;
            this.printOnlyButton.Click += new System.EventHandler(this.printOnlyButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 511);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.deviceList)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.DataGridView deviceList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox labelerLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label numSelectedLbl;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox labelLocation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button printQRCButton;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox serialInputBox;
        private System.Windows.Forms.TextBox locInputBox;
        private System.Windows.Forms.TextBox skuInputBox;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serial;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMEI;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewImageColumn Color;
        private System.Windows.Forms.DataGridViewComboBoxColumn Carrier;
        private System.Windows.Forms.CheckBox modelCheckBox;
        private System.Windows.Forms.Label numSelectedLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveSettingsButton;
        private System.Windows.Forms.ComboBox selectOptionCB;
        private System.Windows.Forms.CheckBox skuCheckBox;
        public System.Windows.Forms.TextBox dataLocation;
        private System.Windows.Forms.TextBox databaseLocation;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button printOnlyButton;
    }
}

