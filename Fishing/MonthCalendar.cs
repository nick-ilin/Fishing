using System;
using System.Windows.Forms;

namespace Fishing
{
    public partial class MonthCalendar : Form
    {
        private static string _date = "";
        private static string _startEnd = "";
        public MonthCalendar()
        {
            InitializeComponent();
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateSelected);
            this.Shown += new EventHandler(MonthCalendar_Shown);
            this.KeyDown += new KeyEventHandler(MonthCalendar_KeyDown);
        }

        public static string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                return;
            }
        }

        public static string StartEnd
        {
            get
            {
                return _startEnd;
            }
            set
            {
                _startEnd = value;
                return;
            }
        }

        void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            Date = monthCalendar1.SelectionStart.ToShortDateString();
            this.Close();
        }

        void MonthCalendar_Shown(object sender, EventArgs e)
        {
            if (Date != "")
            {
                monthCalendar1.SelectionStart = Convert.ToDateTime(Date);
            }
            else
            {
                monthCalendar1.SelectionStart = DateTime.Now.Date;
                Date = monthCalendar1.TodayDate.ToShortDateString();
            }
        }

        void MonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
