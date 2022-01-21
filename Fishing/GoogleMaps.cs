using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Fishing
{
    public partial class GoogleMaps : Form
    {
        private static string _koordinates = "";
        private const string ERRPATH = "errors.txt";
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

        private void GoogleMaps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void GoogleMaps_Shown(object sender, EventArgs e)
        {
            try
            {
                string url = Koordinates == "" ? "gotomap.html" : "gotomap.html?place=" + Koordinates;
                mapBrowser.Navigate(AppDomain.CurrentDomain.BaseDirectory + url);
            }
            catch (Exception ex)
            {
                WriteErrors(ex.ToString());
            }
        }

        private void GoogleMaps_FormClosed(object sender, FormClosedEventArgs e)
        {
            Koordinates = "";
            mapBrowser.Navigate("about:blank");
        }

        private void WriteErrors(string error)
        {
            string errors = "**********************" + DateTime.Now.Date.ToString().Replace("0:00:00", "") + " " + DateTime.Now.TimeOfDay + "*********************\n";
            errors += error + "\n***********************************************************************\n\n";
            StreamWriter writer = new StreamWriter(ERRPATH, true, Encoding.UTF8);
            writer.Write(errors);
            writer.Flush();
            writer.Close();
        }
    }
}
