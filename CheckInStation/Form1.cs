using BarcodeLib;
using iMobileDevice;
using iMobileDevice.Afc;
using iMobileDevice.iDevice;
using iMobileDevice.Lockdown;
using iMobileDevice.Plist;
using QRCoder;
//using Squirrel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using iMobileDevice.InstallationProxy;
using iMobileDevice.Service;
using System.Runtime.Serialization.Formatters.Binary;
using iMobileDevice.MobileSync;
using iMobileDevice.Misagent;
using System.Data.OleDb;
using System.Management;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CheckInStation
{
    public partial class Form1 : Form
    {
       
        BackgroundWorker updateThread = null;

        bool _keepRunning = false;
        List<SettingModel> settings;
        List<object> settingFields;
        StringBuilder mainString;
        StringBuilder mainString2;

        public Form1()
        {
           
            InitializeComponent();

            deviceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            updateThread = new BackgroundWorker();

            updateThread.DoWork += UpdateThread_DoWork;
            updateThread.RunWorkerCompleted += UpdateThread_RunUpdateCompleted;
            updateThread.ProgressChanged += UpdateThread_ProgressChanged;
            updateThread.WorkerReportsProgress = true;
            updateThread.WorkerSupportsCancellation = true;


            settingFields = new List<object>();

            settingFields.Add(dataLocation);
            settingFields.Add(labelerLocation);
            settingFields.Add(labelLocation);
            settingFields.Add(databaseLocation);
            mainString = new StringBuilder();
            mainString2 = new StringBuilder();

            selectOptionCB.SelectedIndex = 0;
            this.KeyPreview = true;

            LoadSettings();

            SKUDBMgr.ConnectToDatabase(databaseLocation.Text.Trim());

            object[] row = { "iphone 5s 16gb", "1234asf1234df","11111333333444", "111113333334445555", "M123 LL/A 2345", null };
            object[] row2 = { "iphone 7s 32gb", "345gfg43tt3vv", "111155333444", "1111553334447777", "M456 LL/A rwrt", null };
            object[] row3 = { "iphone 11 pro 128gb", "7u7u7u7u7u7u", "00000088677774", "000006555333", "M765 LL/A feef", null };
            object[] row4 = { "iphone 6s 64gb", "8i8i8i8i8i8i", "222223333445", "6664446464646", "M353 LL/A f33g", null };
            object[] row5 = { "iphone 8 16gb", "9o9o6u6y5t4t4t", "57575757575757", "5757575757", "M634 LL/A aa4g", null };
            //var listViewItem = new ListViewItem(row);

            deviceList.Rows.Add(row);
            deviceList.Rows.Add(row2);
            deviceList.Rows.Add(row3);
            deviceList.Rows.Add(row4);
            deviceList.Rows.Add(row5);

            //ProcessLabels("THIS IS TITLE ENJOY", "whats going on man", null, @"C:\Users\Vince\Documents\QrCodeLabelTemp2.dotx", true);



            //doc.Close(ref saveChanges, ref missing, ref missing);
            //AndroidMgr.GetConnectedDevices();

            /*
            ImageConverter conv = new ImageConverter();
            System.Drawing.Image im = Bitmap.FromFile(@"C:\Users\Vince\Pictures\maxresdefault.jpg");
            byte[] xByte = (byte[])conv.ConvertTo(im, typeof(byte[]));



            //SKUDBMgr.ExecuteQuery($"INSERT INTO Pics (Data) VALUES ('{"efe"}')",null);

            //SKUDBMgr.ExecuteQuery("SELECT F1 FROM Table1 WHERE F2='23456'");
            
            string qry = "INSERT INTO Pics (Data) VALUES(@Data)";

            using (SKUDBMgr.conn)
            {
                OleDbCommand cmd = new OleDbCommand(qry, SKUDBMgr.conn);
                cmd.Parameters.AddWithValue("@Data", xByte);

                //SKUDBMgr.conn.Open();
                cmd.ExecuteNonQuery();
            }
            
            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();

            values.Add(new KeyValuePair<string, object>("F9", "00500"));
            values.Add(new KeyValuePair<string, object>("F11", "44444"));

            SKUDBMgr.ExecuteQuery(SKUDBMgr.SKUDBOPTIONS.SELECT, "Smartphones", new List<string> { "F3","F9","F11"}, SKUDBMgr.SKUDBOPTIONS.FROM, values,SKUDBMgr.SKUDBOPTIONS.WHERE,SKUDBMgr.SKUDBOPTIONS.OR);
            

            var usbDevices = GetUSBDevices();

            foreach (var usbDevice in usbDevices)
            {
                Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}",
                    usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description);
            }

            Console.Read();
            */


            AndroidMgr.InitiateADBService();

            List<string> dev = AndroidMgr.GetConnectedDevices();

            foreach (string devs in dev) {
                Console.WriteLine(devs);
            }

            foreach(aDevice ad in AndroidMgr.LoadDevices(dev))
            {
                Console.WriteLine($"DEVICE: {ad.deviceName} MODEL: {ad.model} SERIAL: {ad.serialNo} IMEI: {ad.imeiNo} MANUFACTURER: {ad.manufacturer}");
            }

        }

        private async Task ProcessLabels(List<LabelObject> labels, string saveLocation,bool open)
        {
            object missing = System.Reflection.Missing.Value;
            object readOnly = true;
            object visible = false;
            object saveChanges = false;
            List<string> lookupKeys = new List<string> { "<title>", "<content>", "<image>" };
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(@"C:\Users\Vince\Documents\QrCodeLabelTemp.dotx", ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing
                , ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing);
            // Activate document
            doc.Activate();

            try
            {
                //----------------------Replace--------------------------------
                Microsoft.Office.Interop.Word.Find fnd = app.ActiveWindow.Selection.Find;
                fnd.ClearFormatting();
                fnd.Replacement.ClearFormatting();
                fnd.Forward = true;
                fnd.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;

                foreach(LabelObject label in labels){

                    Console.WriteLine($"{label.title} {label.content} {label.qrCodeLocation} {saveLocation}");

                    string imagePath = label.qrCodeLocation;

                    QRCodeGenerator generator = new QRCodeGenerator();
                    QRCodeData data = generator.CreateQrCode(label.uuid,QRCodeGenerator.ECCLevel.L);
                    QRCode code = new QRCode(data);
                    Bitmap image = new Bitmap(code.GetGraphic(5));
                    image.Save(label.qrCodeLocation);


                    foreach (string key in lookupKeys)
                    {
                        var keyword = key;
                        var sel = app.Selection;
                        sel.Find.Text = string.Format("[{0}]", keyword);
                        app.Selection.Find.Execute(keyword);

                        int count = doc.Paragraphs.Count;

                        for (int i = 1; i < count; i++)
                        {
                            Microsoft.Office.Interop.Word.Range range = doc.Paragraphs[i].Range;


                            if (range.Text.Trim().Contains(keyword.Trim()))
                            {
                                //gets desired range here it gets last character to make superscript in range 
                                Microsoft.Office.Interop.Word.Range temprange = doc.Range(range.End - key.Length + 1, range.End);//keyword is of 4 charecter range.End - 4
                                temprange.Select();
                                sel = app.Selection;
                                //currentSelection.Font.Superscript = 1;
                                sel.Find.Execute(Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceOne);
                                sel.Range.Select();


                                if (key == "<image>")
                                {

                                    var imagePath1 = Path.GetFullPath(string.Format(imagePath, keyword));

                                    Microsoft.Office.Interop.Word.InlineShape shape = sel.InlineShapes.AddPicture(FileName: imagePath1, LinkToFile: false, SaveWithDocument: true);

                                    shape.Width = 125f;
                                    shape.Height = 125f;
                                    break;
                                }
                                else
                                {
                                    if (key == "<title>")
                                    {
                                        sel.Text = label.title;
                                        break;
                                    }
                                    else
                                    {
                                        sel.Text = label.content;
                                        break;
                                    }

                                }

                            }
                        }
                    }


                }

                Task t = Task.Run(() => SaveFile(doc, saveLocation));
                await t.ContinueWith((t1) =>
                {
                    if (t.IsCompleted)
                    {

                        if (open)
                        {
                            Console.WriteLine("IM CALLED");
                            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
                            ap.Visible = true;

                            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(saveLocation, ConfirmConversions: false, ReadOnly: true);

                        }
                    }

                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            finally
            {

                if (doc != null)
                {
                    ((Microsoft.Office.Interop.Word._Document)doc).Close(ref saveChanges, ref missing, ref missing);
                    Marshal.FinalReleaseComObject(doc);



                }
                if (app != null)
                {
                    ((Microsoft.Office.Interop.Word._Application)app).Quit();
                    Marshal.FinalReleaseComObject(app);
                }

            }

        }


        private async Task ProcessLabels(string title, string content, QRCode qrCode,string saveLocation,bool open)
        {
            object missing = System.Reflection.Missing.Value;
            object readOnly = true;
            object visible = false;
            object saveChanges = false;
            List<string> lookupKeys = new List<string> {"<title>","<content>","<image>"};
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(@"C:\Users\Vince\Documents\QrCodeLabelTemp.dotx", ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing
                , ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing);
            // Activate document
            doc.Activate();

            try
            {
                //----------------------Replace--------------------------------
                Microsoft.Office.Interop.Word.Find fnd = app.ActiveWindow.Selection.Find;
                fnd.ClearFormatting();
                fnd.Replacement.ClearFormatting();
                fnd.Forward = true;
                fnd.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;

                string imagePath = "code.png";

                foreach (string key in lookupKeys)
                {
                    var keyword = key;
                    var sel = app.Selection;
                    sel.Find.Text = string.Format("[{0}]", keyword);
                    app.Selection.Find.Execute(keyword);

                    int count = doc.Paragraphs.Count;

                    for (int i = 1; i < count; i++)
                    {
                        Microsoft.Office.Interop.Word.Range range = doc.Paragraphs[i].Range;
                        

                        if (range.Text.Trim().Contains(keyword.Trim()))
                        {
                            //gets desired range here it gets last character to make superscript in range 
                            Microsoft.Office.Interop.Word.Range temprange = doc.Range(range.End - key.Length+1, range.End);//keyword is of 4 charecter range.End - 4
                            temprange.Select();
                            sel = app.Selection;
                            //currentSelection.Font.Superscript = 1;
                            sel.Find.Execute(Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceOne);
                            sel.Range.Select();


                            if (key == "<image>") {

                                var imagePath1 = Path.GetFullPath(string.Format(imagePath, keyword));
                                Microsoft.Office.Interop.Word.InlineShape shape = sel.InlineShapes.AddPicture(FileName: imagePath1, LinkToFile: false, SaveWithDocument: true);
                                shape.Width = 125f;
                                shape.Height = 125f;
                            }
                            else
                            {
                                if (key=="<title>")
                                {
                                    sel.Text = title;
                                }
                                else
                                {
                                    sel.Text = content;
                                }
                                
                            }

                        }
                    }
                }


                Task t = Task.Run(() => SaveFile(doc,saveLocation));
                await t.ContinueWith((t1) =>
                {
                    if (t.IsCompleted)
                    {
                       
                        if (open)
                        {
                            Console.WriteLine("IM CALLED");
                            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
                            ap.Visible = true;

                            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(saveLocation, ConfirmConversions: false, ReadOnly: true);

                        }
                    }
                   
                });

            }
            catch (Exception e){
                Console.WriteLine(e.ToString());
            }

            finally
            {

                if (doc != null)
                {
                    ((Microsoft.Office.Interop.Word._Document)doc).Close(ref saveChanges, ref missing, ref missing);
                    Marshal.FinalReleaseComObject(doc);



                }
                if (app != null)
                {
                    ((Microsoft.Office.Interop.Word._Application)app).Quit();
                    Marshal.FinalReleaseComObject(app);
                }

            }

        }


        private void SaveFile(Microsoft.Office.Interop.Word.Document doc,string saveLocation)
        {
            Console.WriteLine("STARTED");
            object missing = System.Reflection.Missing.Value;


            doc.SaveAs(saveLocation, ref missing, ref missing, ref missing, ref missing);

        }


        private void LoadSettings()
        {
            settings = SqliteDataMgr.LoadSettings();

            List<object> tbl = settingFields.FindAll(f => f.GetType() == typeof(TextBox));

            foreach (TextBox tb in tbl)
            {
                SettingModel sm = settings.Find(s => s.SettingName == tb.Name);

                if (sm != null)
                {
                    tb.Text = sm.SettingValue;
                }
            }
        }

        private void UpdateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            _keepRunning = true;
            DateTime startTime = DateTime.Now;

            while (_keepRunning)
            {
                Thread.Sleep(1000);

                string timeElapsedInstring = (DateTime.Now - startTime).ToString(@"hh\:mm\:ss");

                updateThread.ReportProgress(0, timeElapsedInstring);

                if (updateThread.CancellationPending)
                {
                    // this is important as it set the cancelled property of RunWorkerCompletedEventArgs to true
                    e.Cancel = true;
                    break;
                }
                else
                {
                    try
                    {
                        if (serialInputBox.Text.Length != 0 && skuInputBox.Text.Length != 0 && locInputBox.Text.Length != 0)
                        {
                            this.Invoke((MethodInvoker)delegate {
                                // Running on the UI thread
                                this.printQRCButton.Enabled = true;
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate {
                                // Running on the UI thread
                                this.printQRCButton.Enabled = false;
                            });
                        }



                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }

                }

            }
        }

        private void UpdateThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Trace.WriteLine(e.UserState.ToString());
        }

        private void UpdateThread_RunUpdateCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Trace.WriteLine("THREAD CANCLEED");
            }
            else
            {
                Trace.WriteLine("THREAD STOPPED");
            }
        }

        private void clearButton_Clicked(object sender, EventArgs e)
        {

            deviceList.Rows.Clear();

        }

        

        private void dgvFeesHead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (deviceList.SelectedRows.Count!=0)
                {
                    //e.SuppressKeyPress = true;
                }
                else
                {
                    //e.SuppressKeyPress = false;
                }
                
            }
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            

            if (selectOptionCB.Text == "Prompt via Serial Select")
            {
                mainString2.Append(e.KeyChar);
                Console.WriteLine(mainString2);
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    Console.WriteLine("ENTER PRESSED");
                    foreach (DataGridViewRow v in deviceList.Rows)
                    {
                        string serial = (string)v.Cells[1].Value;

                        int numChars = serial.Length;

                        int positionOfNewLine = mainString2.ToString().IndexOf(e.KeyChar);

                        if (positionOfNewLine >= numChars)
                        {
                            int startIndex = positionOfNewLine - numChars;

                            string partBefore = mainString2.ToString().Substring(startIndex, numChars);

                            Console.WriteLine("PARTFEFE: " + partBefore);

                            if (partBefore.Trim() == serial.Trim())
                            {
                                
                                deviceList.ClearSelection();
                                v.Selected = true;
                                Console.WriteLine(partBefore.Trim() + " == " + serial.Trim());
                                break;
                            }


                        }

                    }

                    mainString2.Clear();
                }
                else if (e.KeyChar == (char)Keys.Return){
                    e.Handled = true;
                    Console.WriteLine("ENTER PRESSED");
                    foreach (DataGridViewRow v in deviceList.Rows)
                    {
                        string serial = (string)v.Cells[1].Value;

                        int numChars = serial.Length;

                        int positionOfNewLine = mainString2.ToString().IndexOf(e.KeyChar);

                        if (positionOfNewLine >= numChars)
                        {
                            int startIndex = positionOfNewLine - numChars;

                            string partBefore = mainString2.ToString().Substring(startIndex, numChars);

                            Console.WriteLine("PARTFEFE: " + partBefore);

                            if (partBefore.Trim() == serial.Trim())
                            {

                                deviceList.ClearSelection();
                                v.Selected = true;
                                Console.WriteLine(partBefore.Trim() + " == " + serial.Trim());
                                break;
                            }


                        }

                    }

                    mainString2.Clear();
                }

                {

                }
            }
            
        }

        private void saveSettingsButton_Clicked(object sender, EventArgs e)
        {
            
            foreach (object field in settingFields)
            {
                SettingModel setting = new SettingModel();

                if (field.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)field;
                    setting.SettingName = tb.Name;
                    setting.SettingValue = tb.Text;

                    if (!SqliteDataMgr.SettingExists(tb.Name))
                    {
                        SqliteDataMgr.AddSetting(setting);
                    }
                    else
                    {
                        SqliteDataMgr.UpdateSetting(setting);
                    }
                    
                }
            }

            SKUDBMgr.ConnectToDatabase(databaseLocation.Text.Trim());

        }


        private void loadButton_Clicked(object sender, EventArgs e)
        {

            ReadOnlyCollection<string> udids;
            ReadOnlyCollection<string> info;
            NativeLibraries.Load();
            int count = 0;

            var idevice = LibiMobileDevice.Instance.iDevice;
            var lockdown = LibiMobileDevice.Instance.Lockdown;
            var shit = LibiMobileDevice.Instance.Plist;

            var ret = idevice.idevice_get_device_list(out udids, ref count);

            if (ret == iDeviceError.NoDevice)
            {
                // Not actually an error in our case
                statusLabel.Text = "NO DEVICE ERROR";
                return;
            }

            ret.ThrowOnError();

            if (udids.Count == 0)
            {
                statusLabel.Text = "NO DEVICE DETECTED";
            }

            try
            {

                // Get the device name
                foreach (var udid in udids)
                {

                    string t1;
                    string t2;
                    string t3;
                    string t4;
                    string t5;
                    string t6;
                    string t7;
                    string t8;
                    string t9;
                    string t10;
                    string t11;
                    string t20;
                    string totalSize="0";

                    PlistHandle tested1;
                    PlistHandle tested2;
                    PlistHandle tested3;
                    PlistHandle tested4;
                    PlistHandle tested5;
                    PlistHandle tested6;
                    PlistHandle tested7;
                    PlistHandle tested8;
                    PlistHandle tested9;
                    PlistHandle tested10;
                    PlistHandle tested11;
                    

                    LockdownClientHandle lockdownHandle;
                    LockdownServiceDescriptorHandle mcLdsHandle;
                    iDeviceHandle deviceHandle;
                    LockdownServiceDescriptorHandle ldsHandle;
                    AfcClientHandle afcClient;
                    ReadOnlyCollection<string> deviceInfo;

                    int va = 100;
                    uint u = 0;

                    idevice.idevice_new(out deviceHandle, udid).ThrowOnError();

                    lockdown.lockdownd_client_new_with_handshake(deviceHandle, out lockdownHandle, "Quamotion").ThrowOnError();
                    
                    lockdown.lockdownd_start_service(lockdownHandle, "com.apple.afc", out ldsHandle);

                    ldsHandle.Api.Afc.afc_client_new(deviceHandle, ldsHandle, out afcClient);

                    ldsHandle.Api.Afc.afc_get_device_info_key(afcClient, "FSTotalBytes", out totalSize);

                    //Find serial number in plist
                    lockdown.lockdownd_get_value(lockdownHandle, null, "SerialNumber", out
                    tested1);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "InternationalMobileEquipmentIdentity", out
                     tested2);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "ModelNumber", out
                     tested3);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "MobileEquipmentIdentifier", out
                    tested4);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "MobileSubscriberNetworkCode", out
                    tested5);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "IntegratedCircuitCardIdentity", out
                    tested6);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "RegulatoryModelNumber", out
                    tested7);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "MarketingName", out tested8);

                    //lockdown.lockdownd_get_value(lockdownHandle, "com.apple.disk_usage", "TotalDiskCapacity", out tested9);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "RegionInfo", out tested11);

                    lockdown.lockdownd_get_value(lockdownHandle, null, "DeviceEnclosureColor", out tested10);

                    //Get string values from plist
                    tested1.Api.Plist.plist_get_string_val(tested1, out t1);
                    tested2.Api.Plist.plist_get_string_val(tested2, out t2);
                    tested3.Api.Plist.plist_get_string_val(tested3, out t3);
                    tested4.Api.Plist.plist_get_string_val(tested4, out t4);
                    tested5.Api.Plist.plist_get_string_val(tested5, out t5);
                    tested6.Api.Plist.plist_get_string_val(tested6, out t6);
                    tested7.Api.Plist.plist_get_string_val(tested7, out t7);
                    tested8.Api.Plist.plist_get_string_val(tested8, out t8);
                    //tested9.Api.Plist.plist_get_string_val(tested9, out t9);
                    tested10.Api.Plist.plist_get_string_val(tested10, out t10);
                    tested11.Api.Plist.plist_get_string_val(tested11, out t11);

                    if (totalSize != null)
                    {
                        if (totalSize.Length != 0)
                        {
                            double si = Math.Round(double.Parse(totalSize) / 1000000000);

                            var bitmap = new Bitmap(16, 16);
                            using (var g = Graphics.FromImage(bitmap))
                            {
                                var colorCode = t10;

                                if (colorCode!=null)
                                {
                                    var color = ColorTranslator.FromHtml(colorCode);
                                    using (var brush = new SolidBrush(color))
                                    {
                                        g.FillRectangle(brush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                    }
                                }
                                
                            }

                            object[] row = { t8 + " " + si + "GB", t1, t2, t4, t3 + " " + t11 + " " + t7, bitmap };
                            //var listViewItem = new ListViewItem(row);

                            deviceList.Rows.Add(row);


                        }
                    }


                    deviceHandle.Dispose();
                    lockdownHandle.Dispose();

                }

                //////////////////////////////////////////
                foreach(aDevice ad in AndroidMgr.LoadDevices(AndroidMgr.GetConnectedDevices())){

                    object[] row = {ad.manufacturer + " " + ad.deviceName + " GB", ad.serialNo,ad.imeiNo, " ", ad.model , null };
                    //var listViewItem = new ListViewItem(row);

                    deviceList.Rows.Add(row);
                }

                
                //////////////////////////////////////////
                deviceList.ClearSelection();
                serialInputBox.Text = "";

                UpdateNumSelected(deviceList);

            } catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }



        }

        private void CallbackBitch(IntPtr ptr1, IntPtr ptr2, IntPtr ptr3)
        {
            Console.WriteLine(ptr1 + " " + ptr2 + " " + ptr3);
        }

        private void deviceList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            UpdateNumSelected((DataGridView)sender);

        }

        private void deviceList_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateNumSelected((DataGridView)sender);
        }

        private void UpdateNumSelected(DataGridView grv)
        {
            DataGridView dgv = grv;
            int counter = 0;


            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Selected)
                {
                    counter++;
                }
            }

            numSelectedLabel.Text = counter.ToString();
        }

        private void deviceList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

  
            if (dgv == null)
                return;
            if (dgv.CurrentRow.Selected)
            {

                Console.WriteLine(dgv.CurrentRow.Cells[6].Value);
                UpdateDeviceInfo(dgv.CurrentRow);
                dgv.CurrentRow.Selected = true;
            }


        }

        private void UpdateDeviceInfo(DataGridViewRow row)
        {
            string serial = row.Cells[1].Value == null ? " " : row.Cells[1].Value.ToString();

            serialInputBox.Text = serial;
        }

        private void printOnlyButton_Click(object sender, EventArgs e)
        {
            List<LabelObject> labels = new List<LabelObject>();

            try
            {
                foreach (DataGridViewRow v in deviceList.SelectedRows)
                {
                    StringBuilder builder = new StringBuilder();

                    LabelObject label = new LabelObject();

                    Console.WriteLine("THIS IS VALIUE: " + v.Cells[0].Value);

                    label.title = v.Cells[0].Value == null ? " " : v.Cells[0].Value.ToString();
                    builder.AppendLine(v.Cells[1].Value == null ? " " : v.Cells[1].Value.ToString());
                    builder.AppendLine(v.Cells[2].Value == null ? " " : v.Cells[2].Value.ToString());
                    builder.AppendLine(v.Cells[3].Value == null ? " " : v.Cells[3].Value.ToString());
                    builder.AppendLine(v.Cells[4].Value == null ? " " : v.Cells[4].Value.ToString());
                    builder.AppendLine(v.Cells[6].Value == null ? " " : v.Cells[6].Value.ToString());
                    label.content = builder.ToString();
                    label.qrCodeLocation = AppDomain.CurrentDomain.BaseDirectory + "code.png";
                    label.uuid = v.Cells[1].Value == null ? " " : v.Cells[1].Value.ToString();

                    labels.Add(label);
                }

                Console.WriteLine(labels.Count);
                ProcessLabels(labels, @"C:\Users\Vince\Documents\QrCodeLabelTemp2.dotx", true);


            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

            }
        }

        private async void printButton_Clicked(object sender, EventArgs e)
        {
            List<LabelObject> labels = new List<LabelObject>();
            List<DeviceObject> devices = new List<DeviceObject>();

            try
            {
                foreach (DataGridViewRow v in deviceList.SelectedRows)
                {
                    StringBuilder builder = new StringBuilder();

                    LabelObject label = new LabelObject();

                    Task<DeviceObject> t1 = Task.Run(() => RunDataGridView(v));
                    await t1;

                    

                    Console.WriteLine("THIS IS VALIUE: " +v.Cells[0].Value);
                    
                    label.title = v.Cells[0].Value == null ? " " : v.Cells[0].Value.ToString();
                    builder.AppendLine(v.Cells[1].Value == null ? " " : v.Cells[1].Value.ToString());
                    builder.AppendLine(v.Cells[2].Value == null ? " " : v.Cells[2].Value.ToString());
                    builder.AppendLine(v.Cells[3].Value == null ? " " : v.Cells[3].Value.ToString());
                    builder.AppendLine(v.Cells[4].Value == null ? " " : v.Cells[4].Value.ToString());
                    builder.AppendLine(v.Cells[6].Value == null ? " " : v.Cells[6].Value.ToString());
                    label.content = builder.ToString();
                    label.qrCodeLocation = "code.png";
                    label.uuid = v.Cells[1].Value == null ? " " : v.Cells[1].Value.ToString();

                    labels.Add(label);

                    if (!skuCheckBox.Checked)
                    {
                        LocationPrompt locs = new LocationPrompt();

                        DeviceObject device = new DeviceObject();

                        device.serialID = v.Cells[1].Value == null ? " " : v.Cells[1].Value.ToString();
                        device.location = locs.location;
                        device.caseID = locs.theCase;

                        devices.Add(device);
                    }
                    else
                    {
                        if (t1.Result!=null)
                        {
                            devices.Add(t1.Result);
                        }
                        
                    }
                    
                }

                if (devices.Count!=0)
                {
                    Console.WriteLine(labels.Count);
                    await ProcessLabels(labels, @"C:\Users\Vince\Documents\QrCodeLabelTemp2.dotx", true);
                    CheckInDevices(devices);
                }
                
                

            }
            catch (Exception ex) {
                Trace.WriteLine(ex.ToString());

            }
            
        }

        private void CheckInDevices(List<DeviceObject> devices)
        {

            foreach (DeviceObject device in devices)
            {

                SerialInfo si = new SerialInfo();

                si.serial = device.serialID;

                LocationData d = new LocationData();
                d.curCase = device.caseID;
                d.date = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
                d.time = DateTime.Now.ToString("h:mm:ss tt");
                d.location = device.location;
                d.lastLocation = true;
                d.userID = "311015";


                si.locationData.Add(d);



                if (MongoCRUD.GetInstance().RecordExists<SerialInfo>("Serial", device.serialID, "serial"))
                {
                    MongoCRUD.GetInstance().AppendRecord<SerialInfo>("Serial", device.serialID, d);
                }
                else
                {


                    MongoCRUD.GetInstance().InsertRecord("Serial", si, device.serialID, device.caseID);
                }

                CaseInfo ci = new CaseInfo();
                ci.caseID = device.caseID;
                ci.curLoc = device.location;
                ci.ageInfo = DateTime.Now.ToString("MM-dd-yyyy hh: mm tt");


                MongoCRUD.GetInstance().InsertRecord("Cases", ci, device.caseID, null);

                AreaInfo a = null;

                List<AreaInfo> areas = MongoCRUD.GetInstance().LoadRecords<AreaInfo>("Areas", null, null);

                foreach (AreaInfo area in areas)
                {
                    foreach (LocationObject loc in area.locationsList)
                    {
                        if (device.location == loc.locName)
                        {
                            a = area;
                            break;
                        }
                    }
                }

                if (a!=null)
                {
                    UpdateAreaLocCases(ci, a);
                }

                
            }
            


        }

        private void UpdateAreaLocCases(CaseInfo ci, AreaInfo area)
        {

            foreach (LocationObject lo in area.locationsList)
            {
                if (lo.locName == ci.curLoc)
                {
                    if (!lo.casesList.Contains(ci))
                    {
                        MongoCRUD.GetInstance().UpdateLocationCases(lo, area, ci);
                    }
                }

            }

        }

        private DeviceObject RunDataGridView(DataGridViewRow v)
        {
            Bitmap bitmap = (Bitmap)v.Cells[5].Value;
            DeviceObject deviceObj = null;

            string device = v.Cells[0].Value == null ? " " : v.Cells[0].Value.ToString();
            string serial = v.Cells[1].Value == null ? " " : v.Cells[1].Value.ToString();
            string imei = v.Cells[2].Value == null ? " " : v.Cells[2].Value.ToString();
            string mei = v.Cells[3].Value == null ? " " : v.Cells[3].Value.ToString();
            string model = v.Cells[4].Value == null ? " " : v.Cells[4].Value.ToString();
            string color = v.Cells[5].Value == null ? " " : ColorTranslator.ToHtml(bitmap.GetPixel(0, 0));
            string carrier = v.Cells[6].Value == null ? " " : v.Cells[6].Value.ToString();

            List<string> fields = new List<string>() { device, serial, mei, imei, model, color, carrier };

            if (skuCheckBox.Checked)
            {

                SkuPrompt skuPrompt = new SkuPrompt();

                skuPrompt.Initialize(fields);

                if (skuPrompt.skuString != null)
                {
                    StreamWriter writer = new StreamWriter(dataLocation.Text, false);
                    deviceObj = new DeviceObject();

                    if (!File.Exists(dataLocation.Text))
                    {
                        File.Create(dataLocation.Text);
                    }

                    writer.WriteLine("DEVICE-SERIAL-IMEI-MEI-MODEL-COLOR-CARRIER");

                    writer.WriteLineAsync($"{skuPrompt.skuString}-{serial}-{imei}-{mei}-{model}-{color}-{skuPrompt.carrierString}");

                    writer.Close();

                    Task.Run(() => PrintProcess());

                    deviceObj.serialID = serial;
                    deviceObj.location = skuPrompt.locationString;
                    deviceObj.caseID = skuPrompt.caseIDString;

                }

            }
            else
            {


                Console.WriteLine(device + " " + serial + " " + imei + " " + mei + " " + model + " " + color + " " + carrier);

                StreamWriter writer = new StreamWriter(dataLocation.Text, false);

                if (!File.Exists(dataLocation.Text))
                {
                    File.Create(dataLocation.Text);
                }

                writer.WriteLine("DEVICE-SERIAL-IMEI-MEI-MODEL-COLOR-CARRIER");

                if (carrier.Length > 1)
                {
                    if (modelCheckBox.Checked)
                    {
                        writer.WriteLineAsync(device + " (" + carrier + ") {" + model + "}-" + serial + "-" + imei + "-" + mei + "-" + model + "-" + color + "-" + carrier);
                    }
                    else
                    {
                        writer.WriteLineAsync(device + " (" + carrier + ")-" + serial + "-" + imei + "-" + mei + "-" + model + "-" + color + "-" + carrier);
                    }

                }
                else
                {
                    if (modelCheckBox.Checked)
                    {
                        writer.WriteLineAsync(device + " {" + model + "}-" + serial + "-" + imei + "-" + mei + "-" + model + "-" + color);
                    }
                    else
                    {
                        writer.WriteLineAsync(device + "-" + serial + "-" + imei + "-" + mei + "-" + model + "-" + color);
                    }

                }


                writer.Close();

                Task.Run(() => PrintProcess());

            }
            return deviceObj;
        }


        public void PrintProcess()
        {
            /*
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = labelerLocation.Text;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "\"" + labelLocation.Text+"\"" + " -P -Q -L 1 -U";
           



            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
            */
        }

        private void printQRCButton_Click(object sender, EventArgs e)
        {

            ScannableObject so = new ScannableObject();

            so.sku = skuInputBox.Text;
            so.serialID = serialInputBox.Text;

            string output = JsonConvert.SerializeObject(so);
            output = JValue.Parse(output).ToString(Newtonsoft.Json.Formatting.Indented);

            StringBuilder st = new StringBuilder();
            st.AppendLine(output);
            st.AppendLine(output);
            st.AppendLine(output);
            st.AppendLine(output);
            st.AppendLine(output);
            st.AppendLine(output);
            /////////////////////////////////////////////////////////////////
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(output,QRCodeGenerator.ECCLevel.L);
            QRCode code = new QRCode(data);

            CheckInPrompt prompt = new CheckInPrompt();
            prompt.Initialize(code);


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            switch (MessageBox.Show(this, "Are you sure?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {

                //Stay on this form
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    
                    updateThread.CancelAsync();
                    _keepRunning = false;
                    AndroidMgr.KillADBService();
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateThread.RunWorkerAsync();
        }

        
    }

    public class LabelObject
    {
        public string title { get; set; }
        public string content { get; set; }
        public string qrCodeLocation { get; set; }
        public string saveLocation { get; set; }
        public string uuid { get; set; }
    }

    public class DeviceObject
    {
        public string location { get; set; }
        public string caseID { get; set; }
        public string serialID { get; set; }
    }
}


