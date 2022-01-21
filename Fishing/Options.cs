using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Fishing
{
    public partial class Options : Form
    {
        private static int _myReg;
        private static bool _sendmail;
        private static string _mail;
        private static string _pass;
        private readonly OleDbConnection myOleDbConnection;
        private readonly OleDbCommand myOleDbCommand;
        public Options()
        {
            InitializeComponent();
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + "fishing.mdb";
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=" + dbPath;
            myOleDbConnection = new OleDbConnection(connectionString);
            myOleDbCommand = myOleDbConnection.CreateCommand();
            sendmail.CheckedChanged += new EventHandler(SendToEmail_CheckedChanged);
            this.Shown += new EventHandler(Options_Shown);
            this.FormClosing += new FormClosingEventHandler(Options_FormClosing);
        }

        public static int MyRegion
        {
            get
            {
                return _myReg;
            }
            set
            {
                _myReg = value;
                return;
            }
        }

        public static bool SendMail
        {
            get
            {
                return _sendmail;
            }
            set
            {
                _sendmail = value;
                return;
            }
        }

        public static string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
                return;
            }
        }

        public static string Password
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
                return;
            }
        }

        private void Options_Shown(object sender, EventArgs e)
        {
            myregion.Text = MyRegion.ToString();
            sendmail.Checked = SendMail;
            mail.Text = Mail;
            pass.Text = Password;
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MyRegion = 0;
                if (myregion.Text.Replace(" ", "") != "") MyRegion = Convert.ToInt32(myregion.Text);
                SendMail = sendmail.Checked;
                Mail = mail.Text;
                Password = pass.Text;
                myOleDbCommand.CommandText = "UPDATE options SET [myregion] = " + MyRegion + ",[sendmail] = " + SendMail + ",[mail] = \"" + Mail + "\",[pass] = \"" + Password + "\" WHERE id = 0";
                myOleDbConnection.Open();
                myOleDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myOleDbConnection.Close();
            }
        }

        private void SendToEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (sendmail.Checked)
            {
                mail.Enabled = true;
                pass.Enabled = true;
            }
            else
            {
                mail.Enabled = false;
                pass.Enabled = false;
            }
        }
    }
}
