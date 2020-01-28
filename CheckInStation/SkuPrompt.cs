using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckInStation;
using QRCoder;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace CheckInStation
{
    public partial class SkuPrompt : Form
    {

        public string carrierString;
        public string locationString;
        public string caseIDString;
        public string skuString;
        public Dictionary<Image,int> deviceImages;
        bool skuOpButtonClicked;

        public SkuPrompt()
        {
            InitializeComponent();

            skuOpButtonClicked = false;

            deviceImages = new Dictionary<Image,int>();
            this.skuDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.skuDataGridView.RowHeadersVisible = false;

            //string s = SKUDBMgr.ExecuteQuery("SELECT Data FROM Pics WHERE ID=20", null);

            //byte[] bit = Encoding.Default.GetBytes(s);
            try
            {
                byte[] imageData = DownloadData("https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-11/Green/Apple-iPhone-11-Green-frontimage.jpg");
                Image returnImage = (Bitmap)((new ImageConverter()).ConvertFrom(imageData));
                byte[] imageData2 = DownloadData("https://media.giphy.com/media/Ekoti3WwzXr0Y/giphy.gif");
                Image returnImage2 = (Bitmap)((new ImageConverter()).ConvertFrom(imageData2));
                deviceImages.Add(returnImage, 0);
                deviceImages.Add(returnImage2, 1);
                this.devicePictureBox.Image = returnImage;
            }
            catch (Exception e)
            {

            }

            
            
            this.devicePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.skuDataGridView.AllowUserToAddRows = false;
            this.skuDataGridView.RowTemplate.Height = 45;
            this.skuDataGridView.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.skuOptionInputBox.Enabled = false;
            skuString = null;
            carrierString = null;
        }

        private byte[] DownloadData(string url)
        {
            byte[] by = null;

            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    by = ms.ToArray();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return by;
            
        }

        private void SkuPrompt_Shown(object sender, EventArgs e)
        {
            this.skuPromptInputBox.Focus();
        }

        public void Initialize(List<string> info)
        {
            
            foreach (string row in info)
            {
                deviceSpecsBox.AppendText(row + "\n");
                Console.WriteLine(row);
            }


            this.ShowDialog();

        }

        private void skuPromptInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string result = SKUDBMgr.ExecuteQuery($"SELECT F3 FROM SmartphoneTable WHERE F9='{skuPromptInputBox.Text.Trim()}' OR F10='{skuPromptInputBox.Text.Trim()}' OR F11='{skuPromptInputBox.Text.Trim()}'",skuDataGridView);

                if (result!="")
                {
                    skuDataGridView.ClearSelection();
                    skuDataGridView.CurrentCell = null;
                    
                    skuOptionInputBox.Enabled = true;
                    skuOptionInputBox.Focus();
                }
                else {

                    result = SKUDBMgr.ExecuteQuery($"SELECT F2 FROM TabletsTable WHERE F8='{skuPromptInputBox.Text.Trim()}' OR F9='{skuPromptInputBox.Text.Trim()}' OR F10='{skuPromptInputBox.Text.Trim()}'", skuDataGridView);

                    if (result != "")
                    {
                        skuDataGridView.ClearSelection();
                        skuDataGridView.CurrentCell = null;

                        skuOptionInputBox.Enabled = true;
                        skuOptionInputBox.Focus();
                    }
                    else
                    {

                        result = SKUDBMgr.ExecuteQuery($"SELECT F2 FROM MP3Table WHERE F8='{skuPromptInputBox.Text.Trim()}' OR F9='{skuPromptInputBox.Text.Trim()}' OR F10='{skuPromptInputBox.Text.Trim()}'", skuDataGridView);

                        if (result != "")
                        {
                            skuDataGridView.ClearSelection();
                            skuDataGridView.CurrentCell = null;

                            skuOptionInputBox.Enabled = true;
                            skuOptionInputBox.Focus();
                        }
                        else
                        {

                            skuPromptInputBox.SelectAll();
                            MessageBox.Show("Cannot Find SKU");

                        }

                    }

                }

            }
        }

        private void skuOptionInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            skuOpButtonClicked = false;
            if (e.KeyCode == Keys.Enter)
            {
                skuOpButtonClicked = true;
                if (skuOptionInputBox.Text.Trim().ToUpper() == "VALIDATE")
                {

                    string s = skuDataGridView.Rows[0].Cells[0].Value != null ? skuDataGridView.Rows[0].Cells[0].Value.ToString() : null;

                    Console.WriteLine(s);

                    if (s != null) {

                        skuString = s;
                        bool isValid = true;
                        StringBuilder falseString = new StringBuilder();


                        if (skuString.Contains("("))
                        {
                            int length = s.IndexOf(")") - s.IndexOf("(") - 1;
                            string[] halfString = s.Split('(');
                            string[] splitHalfString = halfString[0].Split(' ');
                            
                            
                            s = s.Substring(s.IndexOf("(") + 1, length);
                            carrierString = s;


                            foreach (string st in splitHalfString)
                            {

                                if (st.Length != 0)
                                {
                                    if (!deviceSpecsBox.Text.ToUpper().Contains(st.ToUpper().Trim()))
                                    {
                                        isValid = false;
                                        falseString.AppendLine(st);
                                    }
                                }

                            }
                        }
                        else
                        {
                            string[] splitSkuString = s.Split(' ');


                            foreach (string st in splitSkuString)
                            {

                                if (st.Length != 0)
                                {
                                    if (!deviceSpecsBox.Text.ToUpper().Contains(st.ToUpper().Trim()))
                                    {
                                        isValid = false;
                                        falseString.AppendLine(st);
                                    }
                                }

                            }
                        }


                        if (skuString.Contains("-"))
                        {
                            for (int i = 0; i < skuString.Count(c => c == '-'); i++)
                            {
                                skuString = skuString.Replace('-', ' ');
                            }
                        }

                        if (carrierString != null)
                        {
                            if (carrierString.Contains("-"))
                            {
                                for (int i = 0; i < carrierString.Count(c => c == '-'); i++)
                                {
                                    carrierString = carrierString.Replace('-', ' ');
                                }
                            }
                        }

                        if (!isValid)
                        {
                            if (MessageBox.Show($"Specs Don't Match\n\n{falseString}\nContinue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                LocationPrompt locs = new LocationPrompt();
                                locationString = locs.location;
                                caseIDString = locs.theCase;

                                if (locationString !=null && caseIDString !=null)
                                {
                                    this.Close();
                                }
                                else
                                {
                                    skuOptionInputBox.Clear();
                                    skuOptionInputBox.Enabled = false;
                                    skuPromptInputBox.Focus();
                                    skuPromptInputBox.SelectAll();
                                    skuOpButtonClicked = false;
                                }
                                
                            }
                            else
                            {
                                skuOptionInputBox.Clear();
                                skuOptionInputBox.Enabled = false;
                                skuPromptInputBox.Focus();
                                skuPromptInputBox.SelectAll();
                                skuOpButtonClicked = false;
                            }
                        }
                        else
                        {
                            LocationPrompt locs = new LocationPrompt();

                            StringBuilder builder = new StringBuilder();

                            builder.AppendLine(deviceSpecsBox.Lines[1]);
                            builder.AppendLine(skuPromptInputBox.Text);
                            builder.AppendLine(locs.location);

                            Console.WriteLine(builder.ToString());

                            QRCodeGenerator generator = new QRCodeGenerator();
                            QRCodeData data = generator.CreateQrCode(builder.ToString(), QRCodeGenerator.ECCLevel.L);
                            QRCode code = new QRCode(data);

                            locationString = locs.location;
                            caseIDString = locs.theCase;
                            this.Close();
                            //CheckInPrompt cp = new CheckInPrompt();
                            //cp.Initialize(code);

                        }

                    }

                    

                    

                }
                else if (skuOptionInputBox.Text.Trim().ToUpper() == "CLEAR")
                {
                    skuPromptInputBox.Clear();
                    skuPromptInputBox.Focus();
                    skuOptionInputBox.Clear();
                    skuOptionInputBox.Enabled = false;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index;

            if (deviceImages.ContainsKey(devicePictureBox.Image))
            {
                index = deviceImages[devicePictureBox.Image];

                Console.WriteLine("INDEX: " +index);

                foreach (KeyValuePair<Image, int> kv in deviceImages)
                {
     
                    if (kv.Value == index+1)
                    {
                        devicePictureBox.Image = kv.Key;
                        index = kv.Value;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index;

            if (deviceImages.ContainsKey(devicePictureBox.Image))
            {
                index = deviceImages[devicePictureBox.Image];

                Console.WriteLine("INDEX: " + index);

                foreach (KeyValuePair<Image, int> kv in deviceImages)
                {

                    if (kv.Value == index - 1)
                    {
                        devicePictureBox.Image = kv.Key;
                        index = kv.Value;
                    }
                }
            }
        }

        private void SkuPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!skuOpButtonClicked)
            {
                skuString = null;
            }
        
        }
    }

    
}
