using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInStation
{
    public partial class LocationPrompt : Form
    {
        public string location = null;
        public string theCase = null;

        public LocationPrompt()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (locationInput.Text.Length != 0)
                {
                    location = locationInput.Text;
                    
                    MongoCRUD.GetInstance().ConnectToDB("Serials");
                    bool found = false;

                    List <AreaInfo> areas = MongoCRUD.GetInstance().LoadRecords<AreaInfo>("Areas",null,null);

                    foreach (AreaInfo area in areas)
                    {
                        foreach (LocationObject loc in area.locationsList)
                        {
                            if (location == loc.locName)
                            {
                                found = true;
                                break;
                            }
                        }
                    }

                    if (found)
                    {
                        theCase = caseInput.Text.Trim();
                        location = locationInput.Text.Trim();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Location Not Found","ERROR", MessageBoxButtons.OK);
                        locationInput.SelectAll();
                    }
                    
                }
            }
        }

        private void caseInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (caseInput.Text.Length != 0)
                {
                    theCase = caseInput.Text;

                    locationInput.Focus();
                }
            }
        }
    }
}
