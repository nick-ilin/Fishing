using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Fishing
{
    public partial class GoogleMaps : Form
    {
        private static string _koordinates = "";
        string errPath = "errors.txt";
        public NewReport NP;
        public GoogleMaps()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(GoogleMaps_KeyDown);
            this.Shown += new EventHandler(GoogleMaps_Shown);
            this.FormClosed += new FormClosedEventHandler(GoogleMaps_FormClosed);
        }

        public static string Koordinates
        {
            get
            {
                return _koordinates;
            }
            set
            {
                _koordinates = value;
                return;
            }
        }

        void GoogleMaps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void GoogleMaps_Shown(object sender, EventArgs e)
        {
            try
            {
                string url = "gotomap.html?place=" + Koordinates;
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + url);
                //mapBrowser.Navigate(AppDomain.CurrentDomain.BaseDirectory + url, "_self", postData, "Content-Type: application/x-www-form-urlencoded\r\n");
                //mapBrowser.Navigate(AppDomain.CurrentDomain.BaseDirectory + url);
                //mapBrowser.Navigate("http://processormuseum.narod.ru/" + url);
            }
            catch (Exception ex)
            {
                writeErrors(ex.ToString());
            }
        }

        void GoogleMaps_FormClosed(object sender, FormClosedEventArgs e)
        {
            Koordinates = "";
            mapBrowser.Navigate("about:blank");
        }

        private void writeErrors(string error)
        {
            string errors = "**********************" + DateTime.Now.Date.ToString().Replace("0:00:00", "") + " " + DateTime.Now.TimeOfDay + "*********************\n";
            errors += error + "\n***********************************************************************\n\n";
            StreamWriter writer = new StreamWriter(errPath, true, Encoding.UTF8);
            writer.Write(errors);
            writer.Flush();
            writer.Close();
        }
    }
}
