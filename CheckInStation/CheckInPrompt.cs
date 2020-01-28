using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace CheckInStation
{
    public partial class CheckInPrompt : Form
    {
        public CheckInPrompt()
        {
            InitializeComponent();
            qrPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void Initialize(QRCode code)
        {
            this.Show();
           

            Bitmap bitmap = new Bitmap(code.GetGraphic(5));

            qrPictureBox.Image = bitmap;

            try
            {

                var source = new BitmapLuminanceSource(bitmap);
                var binarizer = new HybridBinarizer(source);
                var binBitmap = new BinaryBitmap(binarizer);
                QRCodeReader qrCodeReader = new QRCodeReader();

                Result str = qrCodeReader.decode(binBitmap);
                Console.WriteLine(str.Text);

            }
            catch { }
        }


        public void Initialize(QRCode code, SkuPrompt prompt)
        {
            this.Show();


            Bitmap bitmap = new Bitmap(code.GetGraphic(5));

            qrPictureBox.Image = bitmap;

            try
            {

                var source = new BitmapLuminanceSource(bitmap);
                var binarizer = new HybridBinarizer(source);
                var binBitmap = new BinaryBitmap(binarizer);
                QRCodeReader qrCodeReader = new QRCodeReader();

                Result str = qrCodeReader.decode(binBitmap);
                Console.WriteLine(str.Text);

            }
            catch { }
        }
    }
}
