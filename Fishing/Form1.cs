using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;

namespace Fishing
{
    public partial class Form1 : Form
    {
        private static string[] _fishnameTable;
        private static string[] _fishnameId;
        private static string[] _snastiTable;
        private static string[] _snastiId;
        private static string[] _vodoemTable;
        private static string[] _vodoemId;
        private static string[] _regionTable;
        public Directory dir;
        public NewReport nRep;
        public GoogleMaps googleMaps;
        public Foto fotoList;
        public Options opt;
        public FrmSplash frmspl;
        private OleDbConnection myOleDbConnection;
        private OleDbCommand myOleDbCommand;
        private readonly string[] months = new string[13] { "", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        private string yearFromNR = "";
        private int openedRow = 0;
        private const string ERRPATH = "errors.txt";
        private readonly string dbPath = AppDomain.CurrentDomain.BaseDirectory + "fishing.mdb";
        private readonly FileInfo dbInfo;
        private string connectionString = "";
        private Thread splashStart;

        public Form1()
        {
            InitializeComponent();
            string version = ProductName + " " + ProductVersion.Remove(ProductVersion.IndexOf('.',2));
            this.Text = version;
            dbInfo = new FileInfo(dbPath);
            if (!dbInfo.Exists)
            {
                MessageBox.Show("Отсутствует база fishing.mdb");
                myReportItem.Enabled = false;
                newReportItem.Enabled = false;
                backupItem.Enabled = false;
                spravMenu.Enabled = false;
                tabControl1.Enabled = false;
            }
            else
            {
                BaseLoad();
            }
        }

        private void BaseLoad()
        {
            splashStart = new Thread(new ThreadStart(SplashForm));
            splashStart.Start();
            Thread.Sleep(2000); //Emulate hardworking ))
            if (dbInfo.IsReadOnly)
            {
                dbInfo.Attributes = FileAttributes.Normal;
            }
            connectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=" + dbPath;
            myOleDbConnection = new OleDbConnection(connectionString);
            myOleDbCommand = myOleDbConnection.CreateCommand();
            dir = new Directory();
            nRep = new NewReport();
            googleMaps = new GoogleMaps();
            opt = new Options();
            fotoList = new Foto();

            yearListBox.SelectedIndexChanged += new EventHandler(YearListBox_SelectedIndexChanged);
            comboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            comboBox17.SelectedIndexChanged += new EventHandler(ComboBox17_SelectedIndexChanged);
            comboBox18.SelectedIndexChanged += new EventHandler(ComboBox18_SelectedIndexChanged);
            tabControl1.SelectedIndexChanged += new EventHandler(TabControl1_SelectedIndexChanged);
            reportTabPage.Enter += new EventHandler(ReportTabPage_Enter);
            recordTabPage.Enter += new EventHandler(RecordTabPage_Enter);
            chartTabPage.Enter += new EventHandler(ChartTabPage_Enter);
            dataGrid.CellMouseUp += new DataGridViewCellMouseEventHandler(DataGrid_CellMouseUp);
            recordDataGrid.CellMouseUp += new DataGridViewCellMouseEventHandler(RecordDataGrid_CellMouseUp);
            dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(DataGridView1_CellMouseUp);
            dataGrid.KeyDown += new KeyEventHandler(DataGrid_KeyDown);
            nRep.FormClosing += new FormClosingEventHandler(NRep_FormClosing);
            dir.FormClosing += new FormClosingEventHandler(Dir_FormClosing);
            fotoList.FormClosed += new FormClosedEventHandler(FotoList_FormClosed);
            showMapToolStripMenuItem.Click += new EventHandler(ShowMapToolStripMenuItem_Click);
            showAllDayFishingToolStripMenuItem.Click += new EventHandler(ShowAllDayFishingToolStripMenuItem_Click);
            showToolStripMenuItem.Click += new EventHandler(ShowToolStripMenuItem_Click);
            showAllToolStripMenuItem.Click += new EventHandler(ShowAllToolStripMenuItem_Click);
            dataGrid.ColumnAdded += new DataGridViewColumnEventHandler(DataGrid_ColumnAdded);
            radiousComboBox.SelectedIndex = 2;
            this.Shown += new EventHandler(Form1_Shown);
            GetYears();
            splashStart.Abort();
        }

        void FotoList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataGrid.RowCount > 0)
            {
                for (int h = 0; h < dataGrid.Rows.Count; h++)
                {
                    string dirp = AppDomain.CurrentDomain.BaseDirectory + "\\foto\\" + dataGrid["id2", h].Value.ToString().Remove(8);
                    DirectoryInfo dir = new DirectoryInfo(dirp);
                    if (dir.Exists)
                    {
                        dataGrid.Rows[h].HeaderCell.Value = "Ф";
                    }
                    else
                    {
                        dataGrid.Rows[h].HeaderCell.Value = "";
                    }
                }
            }
        }

        void Dir_FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread fillDictionaries = new Thread(FillDict);
            fillDictionaries.Start();
            fillDictionaries.Join();
            FillComboBoxes();
        }

        private void SplashForm()
        {
            frmspl = new FrmSplash();
            frmspl.ShowDialog();
            frmspl.Dispose();
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            this.Activate();
            Thread fillDictionaries = new Thread(FillDict);
            fillDictionaries.Start();
            fillDictionaries.Join();
            FillComboBoxes();
        }

        public static string[] FishnameTable
        {
            get
            {
                return _fishnameTable;
            }
            set
            {
                _fishnameTable = value;
                return;
            }
        }

        public static string[] FishnameID
        {
            get
            {
                return _fishnameId;
            }
            set
            {
                _fishnameId = value;
                return;
            }
        }

        public static string[] SnastiTable
        {
            get
            {
                return _snastiTable;
            }
            set
            {
                _snastiTable = value;
                return;
            }
        }

        public static string[] SnastiID
        {
            get
            {
                return _snastiId;
            }
            set
            {
                _snastiId = value;
                return;
            }
        }

        public static string[] VodoemTable
        {
            get
            {
                return _vodoemTable;
            }
            set
            {
                _vodoemTable = value;
                return;
            }
        }

        public static string[] VodoemID
        {
            get
            {
                return _vodoemId;
            }
            set
            {
                _vodoemId = value;
                return;
            }
        }
        public static string[] RegionTable
        {
            get
            {
                return _regionTable;
            }
            set
            {
                _regionTable = value;
                return;
            }
        }

        private void ReportTabPage_Enter(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                yearListBox.Visible = true;
                ActiveControl = dataGrid;
            }
            else
            {
                yearListBox.Visible = false;
            }
        }

        private void ComboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox18.Text = "";
            textBox18_1.Text = "";
            textBox18_1.Visible = false;
            if (comboBox18.SelectedIndex == 4)
            {
                 textBox18_1.Visible = true;
            }
        }

        private void ComboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox17.Text = "";
            textBox17_1.Text = "";
            textBox17_1.Visible = false;
            if (comboBox17.SelectedIndex == 4 )
            {
                textBox17_1.Visible = true;
            }
        }

        private void DataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void ShowAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAllFishingByContextMenu(dataGridView1);
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFishingByContextMenu(dataGridView1);
        }

        private void ShowAllDayFishingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAllFishingByContextMenu(dataGrid);
        }

        private void ShowMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFishingByContextMenu(dataGrid);
        }

        private void ShowInRadiousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string coordinates = "";
            string[] splitedCoordinates;
            string[] splitedSelectedCoordinates;
            string selectedCoordinates = dataGridView1["Координаты", dataGridView1.CurrentRow.Index].Value.ToString();
            if(selectedCoordinates.Contains("p")) selectedCoordinates = selectedCoordinates.Remove(selectedCoordinates.IndexOf('p'));
            splitedSelectedCoordinates = selectedCoordinates.Split(',');
            if (selectedCoordinates != "")
            {
                double lat1 = Convert.ToDouble(splitedSelectedCoordinates[0].Replace('.', ','));
                double lon1 = Convert.ToDouble(splitedSelectedCoordinates[1].Replace('.', ','));
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if(dataGridView1["Координаты", i].Value.ToString() != "") coordinates += dataGridView1["Координаты", i].Value.ToString() + "p";
                }
                coordinates = coordinates.TrimEnd('p');
                splitedCoordinates = coordinates.Split('p');
                coordinates = "";
                for (int j = 0; j < splitedCoordinates.Length; j++)
                {
                    string[] latitudeAndLongitude;
                    latitudeAndLongitude = splitedCoordinates[j].Split(',');
                    double lat2 = Convert.ToDouble(latitudeAndLongitude[0].Replace('.', ','));
                    double lon2 = Convert.ToDouble(latitudeAndLongitude[1].Replace('.', ','));
                    double dlat = (lat2 - lat1) * (Math.PI / 180);
                    double dlon = (lon2 - lon1) * (Math.PI / 180);
                    double rlat1 = lat1 * (Math.PI / 180);
                    double rlat2 = lat2 * (Math.PI / 180);
                    double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Sin(dlon / 2) * Math.Sin(dlon / 2) * Math.Cos(rlat1) * Math.Cos(rlat2); ;
                    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                    double d = 6371 * c; //Earth Radious
                    if (d <= Convert.ToDouble(radiousComboBox.SelectedItem.ToString())) coordinates += lat2.ToString().Replace(',', '.') + "," + lon2.ToString().Replace(',', '.') + "p";
                }
                try
                {
                    IPHostEntry objIPHE = Dns.GetHostEntry("www.yandex.ru");
                    GoogleMaps.Koordinates = coordinates.TrimEnd('p');
                    googleMaps.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                    WriteErrors(ex.ToString());
                }
            }
        }

        private void ShowFishingByContextMenu(DataGridView obj)
        {
            try
            {
                IPHostEntry objIPHE = Dns.GetHostEntry("www.yandex.ru");
                GoogleMaps.Koordinates = obj["Координаты", obj.CurrentRow.Index].Value.ToString();
                googleMaps.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                WriteErrors(ex.ToString());
            }
        }

        private void ShowAllFishingByContextMenu(DataGridView obj)
        {
            string fishingDate = obj["Дата", obj.CurrentRow.Index].Value.ToString();
            string lonlat2 = "";
            for (int i = 0; i < obj.RowCount; i++)
            {
                if (obj["Дата", i].Value.ToString() == fishingDate)
                {
                    lonlat2 += obj["Координаты", i].Value.ToString() + "p";
                }
            }
            try
            {
                IPHostEntry objIPHE = Dns.GetHostEntry("www.yandex.ru");
                GoogleMaps.Koordinates = lonlat2.TrimEnd('p');
                googleMaps.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                WriteErrors(ex.ToString());
            }
        }

        private void ChartTabPage_Enter(object sender, EventArgs e)
        {
            chartTabPage.Controls.Clear();
            FileInfo zedInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ZedGraph.dll");
            if (!zedInfo.Exists)
            {
                MessageBox.Show("Отсутствует библиотека ZedGraph.dll");
            }
            else
            {
                if (dataGrid.RowCount > 0) DrawLines();
            }
        }

        private void DrawLines()
        {
            try
            {
                ZedGraphControl zgc = new ZedGraphControl();
                chartTabPage.Controls.Add(zgc);
                zgc.Dock = DockStyle.Fill;
                zgc.IsEnableZoom = false;
                GraphPane myPane = zgc.GraphPane;
                myPane.YAxis.Scale.Format = "F0";
                myPane.Title.Text = "График посещений водоемов";
                myPane.XAxis.Title.Text = "Год";
                myPane.YAxis.Title.Text = "Количество рыбалок";
                myPane.Chart.Fill = new Fill(Color.FromArgb(255, 255, 245), Color.FromArgb(255, 255, 190), 90F);
                myPane.YAxis.MajorGrid.IsVisible = true;
                myPane.XAxis.MajorGrid.IsVisible = true;
                myPane.XAxis.Title.FontSpec.Size = 10;
                myPane.YAxis.Title.FontSpec.Size = 10;
                myPane.XAxis.Scale.FontSpec.Size = 10;
                myPane.YAxis.Scale.FontSpec.Size = 10;
                myOleDbCommand.CommandText = "SELECT COUNT([Дата]) FROM (SELECT [Дата] FROM fishing GROUP BY [Дата]) GROUP BY YEAR([Дата])";
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                int years = ds.Tables[0].Rows.Count;
                double[] yval = new double[years];
                double[] mval = new double[years];
                double middle = 0;
                if(years > 0)
                {
                    for (int y = 0; y < years; y++)
                    {
                        yval[y] = Convert.ToInt32(ds.Tables[0].Rows[y].ItemArray[0]);
                        middle += yval[y];
                    }
                    double[] xval = new double[years];
                    for (int x = 0; x < years; x++)
                    {
                        xval[x] = Convert.ToInt32(yearListBox.Items[years - x - 1]);
                        mval[x] = Math.Round(middle / (years - 1));
                    }
                    if (years == 1)
                    {
                        myPane.XAxis.Scale.Min = xval[0] - 1;
                        myPane.XAxis.Scale.Max = xval[xval.Length - 1] + 1;
                    }
                    else
                    {
                        myPane.XAxis.Scale.Min = xval[0];
                        myPane.XAxis.Scale.Max = xval[xval.Length - 1];
                    }
                    LineItem myCurve = myPane.AddCurve("", xval, yval, Color.Red, SymbolType.Circle);
                    myCurve.Symbol.Size = 3;
                    LineItem middleLine = myPane.AddCurve("", xval, mval, Color.Blue, SymbolType.None);
                    for (int i = 0; i < myCurve.Points.Count; i++)
                    {
                        const double offset = 1.0;
                        PointPair pt = myCurve.Points[i];
                        TextObj text = new TextObj(pt.Y.ToString(), pt.X, pt.Y + offset, CoordType.AxisXYScale, AlignH.Left, AlignV.Center)
                        { 
                            ZOrder = ZOrder.A_InFront 
                        };
                        text.FontSpec.Size = 8;
                        text.FontSpec.Border.IsVisible = false;
                        text.FontSpec.Fill.IsVisible = false;
                        myPane.GraphObjList.Add(text);
                    }
                }
                zgc.AxisChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
            finally
            {
                myOleDbConnection.Close();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1_1.Text = "";
            textBox1.Visible = true;
            textBox1_1.Visible = false;
            comboBox1_1.Items.Clear();
            comboBox1_1.Visible = false;
            switch (comboBox1.SelectedIndex)
            {
                case 4:
                    textBox1_1.Visible = true;
                    break;
                case 5:
                    textBox1.Visible = false;
                    textBox1.Text = "";
                    for (int m = 0; m < months.Length; m++)
                    {
                        comboBox1_1.Items.Add(months[m]);
                    }
                    comboBox1_1.Visible = true;

                    break;
                case 6:
                    textBox1.Visible = false;
                    textBox1.Text = "";
                    comboBox1_1.Items.Add("");
                    for (int y = 0; y < yearListBox.Items.Count; y++)
                    {
                        comboBox1_1.Items.Add(yearListBox.Items[y]);
                    }
                    comboBox1_1.Visible = true;
                    break;
            }
        }

        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.Button == MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.Button == MouseButtons.Left) && (e.Clicks == 2))
            {
                ShowCurrentReport(dataGridView1, "");
            }
        }

        private void RecordDataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.Button == MouseButtons.Right))
            {
                recordDataGrid.CurrentCell = recordDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.ColumnIndex != 4) && (e.Clicks == 2))
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        ShowCurrentReport(recordDataGrid, "lenght");
                        break;
                    case 1:
                        ShowCurrentReport(recordDataGrid, "lenght");
                        break;
                    case 2:
                        ShowCurrentReport(recordDataGrid, "weight");
                        break;
                    case 3:
                        ShowCurrentReport(recordDataGrid, "number");
                        break;
                    case 4:
                        foreach (Control ctrl in splitContainer1.Panel1.Controls)
                        {
                            if ((ctrl.GetType() == typeof(ComboBox)) && ctrl != comboBox1_1)
                            {
                                ((ComboBox)ctrl).SelectedIndex = 0;
                            }
                        }
                        comboBox2.SelectedItem = recordDataGrid[0, e.RowIndex].Value.ToString();
                        tabControl1.SelectedTab = queryTabPage;
                        FillQuerry();
                        break;
                }
            }
        }

        private void RecordTabPage_Enter(object sender, EventArgs e)
        {
            FillRecord();
        }

        private void FillRecord()
        {
            try
            {
                myOleDbCommand.CommandText = "SELECT name AS `Вид рыбы`,MAX([Длина]) AS `Длина`,MAX([Вес]) AS `Вес`,MAX([Количество]) AS `Количество`,SUM([Количество]) AS `Всего поймано`,[Рыба] FROM fishing INNER JOIN fishnames ON fishnames.id = fishing.[Рыба] GROUP BY name,[Рыба]";
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                recordDataGrid.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
            finally
            {
                myOleDbConnection.Close();
            }
            recordDataGrid.Columns["Рыба"].Visible = false;
        }

        void RestoreItem_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "backup";
            openFileDialog.ShowDialog();
        }

        private void BackupItem_Click(object sender, EventArgs e)
        {
            string bkPath = AppDomain.CurrentDomain.BaseDirectory + "\\backup\\backup_" + DateTime.Now.ToShortDateString() + ".mdb";
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "backup");
            if(!dir.Exists) dir.Create();
            File.Copy(dbPath, bkPath, true);
            if ((Options.SendMail == true) && (Options.Mail != "") && (Options.Mail.Contains("@")) && (Options.Password != ""))
            {
                Thread tr = new Thread(SendArchToMail);
                tr.Start();
            }
            else
            {
                MessageBox.Show("Файл сохранен\n" + bkPath, "Резервное копирование");
            }
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (dataGrid.RowCount != 0))
            {
                ShowCurrentReport(dataGrid, "");
            }
        }

        private void AddSnastItem_Click(object sender, EventArgs e)
        {
            dir.Text = "Справочник спосбов ловли";
            dir.ShowDialog();
        }

        private void AddFishItem_Click(object sender, EventArgs e)
        {
            dir.Text = "Справочник видов рыб";
            dir.ShowDialog();
        }

        private void AddVodoemItem_Click(object sender, EventArgs e)
        {
            dir.Text = "Справочник мест ловли";
            dir.ShowDialog();
        }

        private void NewReportItem_Click(object sender, EventArgs e)
        {
            nRep.Text = "Отчет";
            nRep.ShowDialog();
        }

        private void MyReportItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                FillDataGrid();
            }
            else
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        private void YearListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void GetYears()
        {
            dataGrid.Visible = true;
            yearListBox.Items.Clear();
            try
            {
                myOleDbCommand.CommandText = "SELECT YEAR(Дата) FROM fishing GROUP BY YEAR(Дата) ORDER BY YEAR(Дата) DESC";
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    yearListBox.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
            finally
            {
                myOleDbConnection.Close();
            }
            if (yearListBox.Items.Count == 0)
            {
                dataGrid.Visible = false;
                noReportsLabel.Text = "Нет ни одного отчета о рыбалке";
            }
            else
            {
                noReportsLabel.Text = "";
                if (yearFromNR == "")
                {
                    yearListBox.SelectedIndex = 0;
                }
                else
                {
                    yearListBox.SelectedIndex = yearListBox.Items.IndexOf(yearFromNR);
                }
            }
            tabControl1.SelectedIndex = 0;
        }

        private void FillDataGrid()
        {
            string year = DateTime.Today.Year.ToString();
            if (yearListBox.SelectedIndex != -1) year = yearListBox.SelectedItem.ToString();
            string query = "SELECT [Дата],fishnames.name AS `Рыба`,[Количество],[Длина],[Вес],[Остальные],[Общий вес],snasti.name AS `Способ ловли`,places.name AS `Место ловли`,id2,[Координаты] " +
                           "FROM fishing,fishnames,snasti,places " +
                            "WHERE fishing.[Рыба] = fishnames.id AND fishing.[Снасть] = snasti.id AND fishing.[Водоем] = places.id AND YEAR([Дата])= " + year + " ORDER BY id2";
            try
            {
                myOleDbCommand.CommandText = query;
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                dataGrid.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
            finally
            {
                myOleDbConnection.Close();
            }
            dataGrid.Columns["Координаты"].Visible = false;
            dataGrid.Columns["id2"].Visible = false;
            if (dataGrid.RowCount > 0)
            {
                for (int h = 0; h < dataGrid.Rows.Count; h++)
                {
                    string dirp = AppDomain.CurrentDomain.BaseDirectory + "\\foto\\" + dataGrid["id2", h].Value.ToString().Remove(8);
                    DirectoryInfo dir = new DirectoryInfo(dirp);
                    if (dir.Exists) dataGrid.Rows[h].HeaderCell.Value = "Ф";
                    if (h != dataGrid.Rows.Count - 1)
                    {
                        if ((dataGrid["Дата", h + 1].Value.ToString() == dataGrid["Дата", h].Value.ToString()))
                        {
                            for (int j = 0; j < dataGrid.Columns.Count; j++)
                            {
                                dataGrid.Rows[h + 1].Cells[j].Style.BackColor = dataGrid.Rows[h].Cells[j].Style.BackColor;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < dataGrid.Columns.Count; j++)
                            {
                                dataGrid.Rows[0].Cells[j].Style.BackColor = Color.White;
                                if (dataGrid.Rows[h].Cells[j].Style.BackColor == Color.Gainsboro)
                                {
                                    dataGrid.Rows[h + 1].Cells[j].Style.BackColor = Color.White;
                                }
                                else
                                {
                                    dataGrid.Rows[h + 1].Cells[j].Style.BackColor = Color.Gainsboro;
                                }
                            }
                        }
                    }
                }
                dataGrid.Focus();
            }
        }

        private void DifferentColors(DataGridView obj2)
        {
            //Выделение отчетов разным цветом
            for (int h = 0; h < obj2.Rows.Count; h++)
            {
                string dirp = AppDomain.CurrentDomain.BaseDirectory + "\\foto\\" + obj2["id2", h].Value.ToString().Remove(8);
                DirectoryInfo dir = new DirectoryInfo(dirp);
                if (dir.Exists) obj2.Rows[h].HeaderCell.Value = "Ф";
                if (h != obj2.Rows.Count - 1)
                {
                    if ((obj2["Дата", h + 1].Value.ToString() == obj2["Дата", h].Value.ToString()))
                    {
                        for (int j = 0; j < obj2.Columns.Count; j++)
                        {
                            obj2.Rows[h + 1].Cells[j].Style.BackColor = obj2.Rows[h].Cells[j].Style.BackColor;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < obj2.Columns.Count; j++)
                        {
                            obj2.Rows[0].Cells[j].Style.BackColor = Color.White;
                            if (obj2.Rows[h].Cells[j].Style.BackColor == Color.Gainsboro)
                            {
                                obj2.Rows[h + 1].Cells[j].Style.BackColor = Color.White;
                            }
                            else
                            {
                                obj2.Rows[h + 1].Cells[j].Style.BackColor = Color.Gainsboro;
                            }
                        }
                    }
                }
            }
            this.ActiveControl = obj2;
        }

        private void DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.Button == MouseButtons.Right))
            {
                dataGrid.CurrentCell = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string dirp = AppDomain.CurrentDomain.BaseDirectory + "\\foto\\" + dataGrid["id2",dataGrid.CurrentRow.Index].Value.ToString().Remove(8);
                DirectoryInfo dir = new DirectoryInfo(dirp);
                if (dir.Exists)
                {
                    fotoToolStripMenuItem.Enabled = true;
                }
                else
                {
                    fotoToolStripMenuItem.Enabled = false;
                }
            }
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.Button == MouseButtons.Left) && (e.Clicks == 2))
            {
                ShowCurrentReport(dataGrid, "");
            }
        }

        private void NRep_FormClosing(object sender, FormClosingEventArgs e)
        {
            yearFromNR = NewReport.CurrentDate;
            if (yearFromNR != "") yearFromNR = Convert.ToDateTime(NewReport.CurrentDate).Year.ToString();
            DataGridView obj = null;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    GetYears();
                    obj = dataGrid;
                    break;
                case 1:
                    FillQuerry();
                    obj = dataGridView1;
                    break;
                case 2:
                    FillRecord();
                    obj = recordDataGrid;
                    break;
            }
            if (tabControl1.SelectedIndex != 3) SetCurrentCell(obj);
        }

        private void SetCurrentCell(DataGridView obj)
        {
            int rowCount = obj.Rows.Count;
            if (rowCount != 0)
            {
                if (rowCount <= openedRow)
                {
                    obj.CurrentCell = obj[0, rowCount - 1];
                }
                else
                {
                    obj.CurrentCell = obj[0, openedRow];
                }
            }
        }

        private void ShowCurrentReport(DataGridView obj, string records)
        {
            if (records == "")
            {
                openedRow = obj.CurrentRow.Index;
                NewReport.CurrentDate = obj["Дата", obj.CurrentRow.Index].Value.ToString().Remove(10);
                NewReport.CurrentID = obj["id2", obj.CurrentRow.Index].Value.ToString();
                nRep.ShowDialog();
            }
            else
            {
                myOleDbCommand.CommandText = "";
                switch (records)
                {
                    case "lenght":
                        if (recordDataGrid[1, recordDataGrid.CurrentCell.RowIndex].Value.ToString() != "")
                        {
                            myOleDbCommand.CommandText = "SELECT [Дата] FROM fishing WHERE Рыба = (SELECT id FROM fishnames WHERE name = \"" + recordDataGrid[0, recordDataGrid.CurrentCell.RowIndex].Value.ToString() + "\") AND Длина = " + recordDataGrid[1, recordDataGrid.CurrentCell.RowIndex].Value.ToString();
                        }
                        break;
                    case "weight":
                        if (recordDataGrid[2, recordDataGrid.CurrentCell.RowIndex].Value.ToString() != "")
                        {
                            myOleDbCommand.CommandText = "SELECT [Дата] FROM fishing WHERE Рыба = (SELECT id FROM fishnames WHERE name = \"" + recordDataGrid[0, recordDataGrid.CurrentCell.RowIndex].Value.ToString() + "\") AND Вес = " + recordDataGrid[2, recordDataGrid.CurrentCell.RowIndex].Value.ToString();
                        }
                        break;
                    case "number":
                        if (recordDataGrid[3, recordDataGrid.CurrentCell.RowIndex].Value.ToString() != "")
                        {
                            myOleDbCommand.CommandText = "SELECT [Дата] FROM fishing WHERE Рыба = (SELECT id FROM fishnames WHERE name = \"" + recordDataGrid[0, recordDataGrid.CurrentCell.RowIndex].Value.ToString() + "\") AND Количество = " + recordDataGrid[3, recordDataGrid.CurrentCell.RowIndex].Value.ToString();
                        }
                        break;
                }
                if (myOleDbCommand.CommandText != "")
                {
                    try
                    {
                        myOleDbConnection.Open();
                        DataSet ds = new DataSet();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                        adapter.Fill(ds);
                        NewReport.CurrentDate = ds.Tables[0].Rows[0].ItemArray[0].ToString().Remove(10);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        WriteErrors(ex.ToString());
                    }
                    finally
                    {
                        myOleDbConnection.Close();
                    }
                    nRep.ShowDialog();
                }
            }
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

        private void AddReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nRep.Text = "Отчет";
            nRep.ShowDialog();
        }

        private void DeleteReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openedRow = dataGrid.CurrentRow.Index;
            if (MessageBox.Show("Действительно хотите удалить отчет?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    myOleDbCommand.CommandText = "DELETE FROM fishing WHERE id2 = \"" + dataGrid["id2", dataGrid.CurrentRow.Index].Value.ToString() + "\"";
                    myOleDbConnection.Open();
                    myOleDbCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    WriteErrors(ex.ToString());
                }
                finally
                {
                    myOleDbConnection.Close();
                    if (dataGrid.RowCount == 1)
                    {
                        if (yearListBox.Items.Count == 1)
                        {
                            yearListBox.Items.Clear();
                            dataGrid.Visible = false;
                            noReportsLabel.Text = "Нет ни одного отчета о рыбалке";
                        }
                        else
                        {
                            GetYears();
                        }
                    }
                    else
                    {
                        FillDataGrid();
                        SetCurrentCell(dataGrid);
                    }
                }
            }
        }

        private void SendArchToMail()
        {
            string archBackupPath = AppDomain.CurrentDomain.BaseDirectory + "\\backup\\backup_" + DateTime.Now.ToShortDateString() + ".7z";
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\7z.exe";
                proc.StartInfo.Arguments = " a \"" + archBackupPath + "\" \"" + dbPath +"\"";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
                string[] loginsmtp = Options.Mail.Split('@');
                SmtpClient Smtp = new SmtpClient("smtp." + loginsmtp[1])
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };
                NetworkCredential NetworkCred = new NetworkCredential
                {
                    UserName = loginsmtp[0],
                    Password = Options.Password
                };
                Smtp.Credentials = NetworkCred;
                Smtp.Port = 25;
                Smtp.EnableSsl = true;
                MailMessage Message = new MailMessage(Options.Mail, Options.Mail, "Backup программы Fishing от " + DateTime.Now.ToShortDateString(), "Сообщение");
                Attachment attach = new Attachment(archBackupPath, MediaTypeNames.Application.Octet);
                Message.Attachments.Add(attach);
                Smtp.Send(Message);
                MessageBox.Show("Архив резервной копии успешно\nотправлен на ящик " + Options.Mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string restorePath = openFileDialog.FileName;
            FileInfo fl2 = new FileInfo(restorePath);
            if (restorePath != dbPath)
            {
                fl2.CopyTo(dbPath, true);
                myReportItem.Enabled = true;
                newReportItem.Enabled = true;
                backupItem.Enabled = true;
                spravMenu.Enabled = true;
                tabControl1.Enabled = true;
                BaseLoad();
            }
            else
            {
                MessageBox.Show("Это рабочая база. Укажите BACKUP!");
            }
        }
        private void FillDict()
        {
            try
            {
                myOleDbCommand.CommandText = "SELECT id,name FROM fishnames ORDER BY name";
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                int fnt = ds.Tables[0].Rows.Count;
                FishnameTable = new string[fnt];
                FishnameID = new string[fnt];
                for (int i = 0; i < fnt; i++)
                {
                    FishnameID.SetValue(ds.Tables[0].Rows[i].ItemArray[0].ToString(), i);
                    FishnameTable.SetValue(ds.Tables[0].Rows[i].ItemArray[1].ToString(), i);
                }
                myOleDbCommand.CommandText = "SELECT id,name FROM snasti ORDER BY name";
                ds.Clear();
                adapter.Fill(ds);
                int st = ds.Tables[0].Rows.Count;
                SnastiTable= new string[st];
                SnastiID = new string[st];
                for (int i = 0; i < st; i++)
                {
                    SnastiID.SetValue(ds.Tables[0].Rows[i].ItemArray[0].ToString(), i);
                    SnastiTable.SetValue(ds.Tables[0].Rows[i].ItemArray[1].ToString(), i);
                }
                myOleDbCommand.CommandText = "SELECT name FROM region";
                ds.Clear();
                adapter.Fill(ds);
                int rt = ds.Tables[0].Rows.Count;
                RegionTable = new string[rt];
                for (int i = 0; i < rt; i++)
                {
                    RegionTable.SetValue(ds.Tables[0].Rows[i].ItemArray[1].ToString(), i);
                }
                myOleDbCommand.CommandText = "SELECT id,name FROM places ORDER BY name";
                ds.Clear();
                adapter.Fill(ds);
                int vt = ds.Tables[0].Rows.Count;
                VodoemTable = new string[vt];
                VodoemID = new string[vt];
                for (int i = 0; i < vt; i++)
                {
                    VodoemID.SetValue(ds.Tables[0].Rows[i].ItemArray[0].ToString(), i);
                    VodoemTable.SetValue(ds.Tables[0].Rows[i].ItemArray[1].ToString(), i);
                }
                myOleDbCommand.CommandText = "SELECT * FROM options";
                DataSet ds2 = new DataSet();
                OleDbDataAdapter adapter2 = new OleDbDataAdapter(myOleDbCommand);
                adapter2.Fill(ds2);
                Options.MyRegion = Convert.ToInt32(ds2.Tables[0].Rows[0].ItemArray[1]);
                Options.SendMail = Convert.ToBoolean(ds2.Tables[0].Rows[0].ItemArray[2]);
                Options.Mail = ds2.Tables[0].Rows[0].ItemArray[3].ToString();
                Options.Password = ds2.Tables[0].Rows[0].ItemArray[4].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
            finally
            {
                myOleDbConnection.Close();
            }
        }

        private void FillComboBoxes()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("");
            comboBox2.Items.AddRange(FishnameTable);
            comboBox7.Items.Clear();
            comboBox7.Items.Add("");
            comboBox7.Items.AddRange(SnastiTable);
            comboBox8.Items.Clear();
            comboBox8.Items.Add("");
            comboBox8.Items.AddRange(RegionTable);
            comboBox9.Items.Clear();
            comboBox9.Items.Add("");
            comboBox9.Items.AddRange(VodoemTable);
        }

        private void ShowByQueryButton_Click(object sender, EventArgs e)
        {
            FillQuerry();
        }

        private void FillQuerry()
        {
            string megaQuery = "SELECT TOP 500 [Дата],fishnames.name AS `Рыба`,[Количество],[Длина],[Вес],[Общий вес],snasti.name AS `Снасть`,places.name AS `Водоем`,[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],id2,[Координаты] " +
                "FROM fishing,fishnames,snasti,places " +
                "WHERE fishing.[Рыба] = fishnames.id AND fishing.[Снасть] = snasti.id AND fishing.[Водоем] = places.id AND ";
            string q1 = "";
            string q2 = "";
            string q3 = "";
            string q4 = "";
            string q5 = "";
            string q6 = "";
            string q7 = "";
            string q8 = "";
            string q9 = "";
            string q10 = "";
            string q11 = "";
            string q12 = "";
            string q13 = "";
            string q14 = "";
            string q15 = "";
            string q16 = "";
            string q17 = "";
            string q18 = "";
            string tb1 = textBox1.Text.Replace(" ", "");
            string tb2 = textBox1_1.Text.Replace(" ", "");
            if (tb1 != "..")
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        q1 = "";
                        break;
                    case 1:
                        q1 = "[Дата] = DATEVALUE('" + textBox1.Text + "') ";
                        break;
                    case 2:
                        q1 = "[Дата] >= DATEVALUE('" + textBox1.Text + "') ";
                        break;
                    case 3:
                        q1 = "[Дата] <= DATEVALUE('" + textBox1.Text + "') ";
                        break;
                    case 4:
                        if (tb2 != "..")
                        {
                            q1 = "[Дата] BETWEEN DATEVALUE('" + textBox1.Text + "') AND DATEVALUE('" + textBox1_1.Text + "') ";
                        }
                        else
                        {
                            q1 = "[Дата] BETWEEN DATEVALUE('" + textBox1.Text + "') AND DATEVALUE(NOW) ";
                        }
                        break;
                }
            }
            else
            {
                if (comboBox1_1.Text != "")
                {
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            q1 = "";
                            break;
                        case 5:
                            q1 = "MONTH([Дата]) = " + comboBox1_1.SelectedIndex + " ";
                            break;
                        case 6:
                            q1 = "YEAR([Дата]) = " + comboBox1_1.Text + " ";
                            break;
                    }
                }
            }
            if (comboBox20.SelectedIndex > 0)
            {
                if (q1 == "")
                {
                    q1 += "MONTH([Дата]) = " + comboBox20.SelectedIndex + " ";
                }
                else
                {
                    q1 += "AND MONTH([Дата]) = " + comboBox20.SelectedIndex + " ";
                }
            }
            if (comboBox2.Text != "")
            {
                q2 = "[Рыба] = (SELECT id FROM fishnames WHERE name = \"" + comboBox2.SelectedItem + "\") ";
            }
            if (textBox3.Text != "")
            {
                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        q3 = "";
                        break;
                    case 1:
                        q3 = "[Количество] >= " + textBox3.Text + " ";
                        break;
                    case 2:
                        q3 = "[Количество] <= " + textBox3.Text + " ";
                        break;
                    case 3:
                        q3 = "[Количество] = " + textBox3.Text + " ";
                        break;
                }
            }
            if (textBox4.Text != "")
            {
                switch (comboBox4.SelectedIndex)
                {
                    case 0:
                        q4 = "";
                        break;
                    case 1:
                        q4 = "[Длина] >= " + textBox4.Text + " ";
                        break;
                    case 2:
                        q4 = "[Длина] <= " + textBox4.Text + " ";
                        break;
                    case 3:
                        q4 = "[Длина] = " + textBox4.Text + " ";
                        break;
                }
            }
            if (textBox5.Text != "")
            {
                switch (comboBox5.SelectedIndex)
                {
                    case 0:
                        q5 = "";
                        break;
                    case 1:
                        q5 = "[Вес] >= " + textBox5.Text + " ";
                        break;
                    case 2:
                        q5 = "[Вес] <= " + textBox5.Text + " ";
                        break;
                    case 3:
                        q5 = "[Вес] = " + textBox5.Text + " ";
                        break;
                }
            }
            if (textBox6.Text != "")
            {
                switch (comboBox6.SelectedIndex)
                {
                    case 0:
                        q6 = "";
                        break;
                    case 1:
                        q6 = "[Общий вес] >= " + textBox6.Text + " ";
                        break;
                    case 2:
                        q6 = "[Общий вес] <= " + textBox6.Text + " ";
                        break;
                    case 3:
                        q6 = "[Общий вес] = " + textBox6.Text + " ";
                        break;
                }
            }
            if (comboBox7.Text != "")
            {
                q7 = "[Снасть] = (SELECT id FROM snasti WHERE name = \"" + comboBox7.SelectedItem + "\") ";
            }
            if (comboBox8.Text != "")
            {
                q8 = "[Регион] = (SELECT id FROM region WHERE name = \"" + comboBox8.SelectedItem + "\") ";  
            }
            if (comboBox9.Text != "")
            {
                q9 = "[Водоем] = (SELECT id FROM places WHERE name = \"" + comboBox9.SelectedItem + "\") ";
            }
            if (comboBox10.Text != "")
            {
                q10 = "[База] = \"" + comboBox10.SelectedItem + "\" ";
            }
            if (comboBox11.Text != "")
            {
                q11 = "[У других] = \"" + comboBox11.SelectedItem + "\" ";
            } 
            if (comboBox12.Text != "")
            {
                q12 = "[Луна] = \"" + comboBox12.SelectedItem + "\" ";
            }
            if (comboBox13.Text != "")
            {
                q13 = "[Время дня] = \"" + comboBox13.SelectedItem + "\" ";
            }
            if (comboBox14.Text != "")
            {
                q14 = "[Осадки] = \"" + comboBox14.SelectedItem + "\" ";
            }
            if (comboBox15.Text != "")
            {
                q15 = "[Ветер] = \"" + comboBox15.SelectedItem + "\" ";
            }
            if (comboBox16.Text != "")
            {
                q16 = "[Направление] = \"" + comboBox16.SelectedItem + "\" ";
            }
            if (textBox17.Text.Replace(" ", "") != "" || textBox17_1.Text.Replace(" ", "") != "")
            {
                string t1 = textBox17.Text;
                string t2 = textBox17_1.Text;
                if (textBox17.Text.Replace(" ", "") == "") t1 = "0";
                if (textBox17_1.Text.Replace(" ", "") == "") t2 = "60";
                switch (comboBox17.SelectedIndex)
                {
                    case 0:
                        q17 = "";
                        break;
                    case 1:
                        q17 = "[Температура] >= " + t1 + " ";
                        break;
                    case 2:
                        q17 = "[Температура] <= " + t1 + " ";
                        break;
                    case 3:
                        q17 = "[Температура] = " + t1 + " ";
                        break;
                    case 4:
                        q17 = "[Температура] >= " + t1 + " AND [Температура] <= " + t2 + " ";
                        break;
                }
            }
            if (textBox18.Text.Replace(" ", "") != "" || textBox18_1.Text.Replace(" ", "") != "")
            {
                string p1 = textBox18.Text.Replace(" ","");
                string p2 = textBox18_1.Text.Replace(" ", "");
                if (textBox18.Text.Replace(" ", "") == "") p1 = "700";
                if (textBox18_1.Text.Replace(" ", "") == "") p2 = "800";
                switch (comboBox18.SelectedIndex)
                {
                    case 0:
                        q18 = "";
                        break;
                    case 1:
                        q18 = "[Давление] >= " + p1 + " ";
                        break;
                    case 2:
                        q18 = "[Давление] <= " + p1 + " ";
                        break;
                    case 3:
                        q18 = "[Давление] = " + p1 + " ";
                        break;
                    case 4:
                        q18 = "[Давление] >= " + p1 + " AND [Давление] <= " + p2 + " ";
                        break;
                }
            }
            object[] array = new object[18] { q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12 ,q13, q14, q15, q16, q17, q18 };
            for (int i = 0; i <= array.Length - 1; i++)
            {
                if (array[i].ToString() != "")
                {
                    megaQuery += array[i].ToString() + "AND ";
                }
            }
            int lastIndexAND = megaQuery.LastIndexOf("AND ");
            if (lastIndexAND != -1)
            {
                megaQuery = megaQuery.Remove(lastIndexAND);
            }
            try
            {
                myOleDbCommand.CommandText = megaQuery + " ORDER BY id2 DESC";
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["Координаты"].Visible = true;
                dataGridView1.Columns["id2"].Visible = false;
                findLabel.Text = "Найдено: " + ds.Tables[0].Rows.Count;
                ShowHideByChecked();
                DifferentColors(dataGridView1);
                if (ds.Tables[0].Rows.Count == 500) MessageBox.Show("Объем данных слишком велик, показаны последние 500 записей");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrors(ex.ToString());
            }
            finally
            {
                myOleDbConnection.Close();
            }
        }

        void ShowHideByChecked()
        {
            foreach (Control ctrl in splitContainer1.Panel1.Controls)
            {
                if ((ctrl.GetType() == typeof(CheckBox)) && (ctrl.Name != "checkBox20") && (ctrl.Name != "checkBox19") && (ctrl.Name != "checkBox10"))
                {
                    if (((CheckBox)ctrl).Checked)
                    {
                        dataGridView1.Columns[ctrl.Text].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[ctrl.Text].Visible = false;
                    }
                }
            }
        }

        private void CheckBox19_CheckedChanged(object sender, EventArgs e)
        {
            Control.ControlCollection ctrl = splitContainer1.Panel1.Controls;
            for (int i = 0; i < ctrl.Count; i++)
            {
                if (checkBox19.Checked)
                {
                    if (ctrl[i].GetType() == typeof(CheckBox) && ctrl[i].TabIndex != 129)
                    {
                        ((CheckBox)ctrl[i]).Checked = true;
                    }
                }
                else
                {
                    if (ctrl[i].GetType() == typeof(CheckBox) && ctrl[i].TabIndex > 105)
                    {
                        ((CheckBox)ctrl[i]).Checked = false;
                    }
                }
            }
        }

        private void FotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FotoShow(dataGrid);
        }

        private void FotoShow(DataGridView obj)
        {
            string etad = obj["id2", obj.CurrentCell.RowIndex].Value.ToString().Remove(8);
            fotoList.Text = etad;
            fotoList.ShowDialog();
        }

        private void AboutItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ProductName + " версия " + ProductVersion + "\nДанная версия является бесплатной\n\u00a9 2008-2022 " + CompanyName + "\nВсе права защищены\ne-mail: fishingsetup@yandex.ru", "О программе " + ProductName);
        }

        private void OptionsItem_Click(object sender, EventArgs e)
        {
            opt.ShowDialog();
        }

        private void HelpItem_Click(object sender, EventArgs e)
        {
            Process proc2 = new Process();
            proc2.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\help.chm";
            proc2.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            proc2.Start();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nRep.Text = "Отчет";
            NewReport.CurrentID = "";
            NewReport.Vodoem = dataGrid["Место ловли", dataGrid.CurrentRow.Index].Value.ToString();
            NewReport.Fish = dataGrid["Рыба", dataGrid.CurrentRow.Index].Value.ToString();
            NewReport.Snasti = dataGrid["Способ ловли", dataGrid.CurrentRow.Index].Value.ToString();
            ((GroupBox)nRep.Controls["mainFishGroupBox"]).Controls["place_0"].Text = dataGrid["Координаты", dataGrid.CurrentRow.Index].Value.ToString();
            nRep.ShowDialog();
        }

        private void RemakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurrentReport(dataGrid, "");
        }

        private void FotoOnQueryStripMenuItem_Click(object sender, EventArgs e)
        {
            FotoShow(dataGridView1);
        }
    }
}
