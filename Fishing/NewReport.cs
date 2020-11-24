using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Reflection;
using Microsoft.Win32;

namespace Fishing
{
    public partial class NewReport : Form
    {
        private static string _CurrentDate = "";
        private static string _CurrentID = "";
        private static string _Vodoem = "";
        private static string _Fish = "";
        private static string _Snasti = "";
        private static string _PlaceClicked = "";
        public GoogleMaps GM;
        public MonthCalendar monCal;
        public Foto fotoList;
        private OleDbConnection myOleDbConnection;
        private OleDbCommand myOleDbCommand;
        string errPath = "errors.txt";
        string dbPath = AppDomain.CurrentDomain.BaseDirectory + "fishing.mdb";
        //string curID = "";
        int preC = 0;
        int c = 0;
        int r = 30;
        Control con = new Control();

        public NewReport()
        {
            InitializeComponent();
            GM = new GoogleMaps();
            monCal = new MonthCalendar();
            fotoList = new Foto();
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=" + dbPath;
            myOleDbConnection = new OleDbConnection(connectionString);
            myOleDbCommand = myOleDbConnection.CreateCommand();
            saveReportButton.Click += new EventHandler(saveReportButton_Click);
            date.Click += new EventHandler(date_Click);
            endDate.Click += new EventHandler(endDate_Click);
            monCal.FormClosing += new FormClosingEventHandler(monCal_FormClosing);
            place_0.DoubleClick += new EventHandler(place_0_DoubleClick);
            place_0.Click += new EventHandler(place_0_Click);
            other.LostFocus += new EventHandler(other_LostFocus);
            this.FormClosed += new FormClosedEventHandler(NewReport_FormClosed);
            this.Shown += new EventHandler(NewReport_Shown);
            this.ActiveControl = date;
            this.KeyDown += new KeyEventHandler(NewReport_KeyDown);
        }

        void other_LostFocus(object sender, EventArgs e)
        {
            string[] text = other.Text.Split(',','.');
            lenght.Text = text[text.Length - 1];
        }

        public static string Vodoem
        {
            get
            {
                return _Vodoem;
            }
            set
            {
                _Vodoem = value;
                return;
            }
        }

        public static string Fish
        {
            get
            {
                return _Fish;
            }
            set
            {
                _Fish = value;
                return;
            }
        }

        public static string Snasti
        {
            get
            {
                return _Snasti;
            }
            set
            {
                _Snasti = value;
                return;
            }
        }
        
        public static string CurrentDate
        {
            get
            {
                return _CurrentDate;
            }
            set
            {
                _CurrentDate = value;
                return;
            }
        }

        public static string CurrentID
        {
            get
            {
                return _CurrentID;
            }
            set
            {
                _CurrentID = value;
                return;
            }
        }

        public static string PlaceClicked
        {
            get
            {
                return _PlaceClicked;
            }
            set
            {
                _PlaceClicked = value;
                return;
            }
        }
        
        void place_0_Click(object sender, EventArgs e)
        {
            place_0.SelectAll();
        }

        void endDate_Click(object sender, EventArgs e)
        {
            if (endFishingDate.Checked)
            {
                monCal.Location = endDate.PointToScreen(new Point(0, date.Size.Height));
                MonthCalendar.StartEnd = "end";
                MonthCalendar.Date = endDate.Text;
                monCal.ShowDialog();
            }
        }

        private string defaultBrowser()
        {
            string regkey = @"http\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(regkey, false);
            string browserPath = ((string)registryKey.GetValue(null, null)).Split('"')[1];
            return browserPath;
        }

        void place_0_DoubleClick(object sender, EventArgs e)
        {
            string koordinates = "";
            if (place_0.Text != "")
            {
                koordinates = "?place=" + place_0.Text;
            }


            string url = "gotomap.html?place=" + place_0.Text;
            Process p = new Process();
            p.StartInfo.FileName = defaultBrowser();
            //p.StartInfo.Arguments = "D:\\" + url;
            p.StartInfo.Arguments = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, url);
            p.Start();

            //Process.Start(Path.Combine("D:\\", "gotomap.html"));
            //Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gotomap.html?"));

            /*try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.yandex.ru");
                GoogleMaps.Koordinates = place_0.Text;
                GM.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                writeErrors(ex.ToString());
            }*/
        }

        void NewReport_Shown(object sender, EventArgs e)
        {
            fillComboBoxes();
            getControlsAndData();
        }

        void monCal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MonthCalendar.StartEnd == "start")
            {
                date.Text = MonthCalendar.Date;
            }
            if (MonthCalendar.StartEnd == "end")
            {
                endDate.Text = MonthCalendar.Date;
            }
        }

        void date_Click(object sender, EventArgs e)
        {
            monCal.Location = date.PointToScreen(new Point(0, date.Size.Height));
            MonthCalendar.StartEnd = "start";
            MonthCalendar.Date = date.Text;
            monCal.ShowDialog();
        }

        private void getControlsAndData()
        {
            if (CurrentDate != "")
            {
                /*if (CurrentID == "")
                {
                    string[] str = CurrentDate.Split('.');
                    curID = str[2] + str[1] + str[0];
                }*/
                this.Text = "Отчет " + CurrentDate;
                number.Name = "number_0";
                lenght.Name = "lenght_0";
                weight.Name = "weight_0";
                other.Name = "other_0";
                try
                {
                    myOleDbCommand.CommandText = "SELECT [Дата],[Дата2],fishnames.name,[Количество],[Длина],[Вес],[Остальные],[Общий вес],snasti.name,places.name,[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],[Заметки],[Координаты],[База],id2 " +
                        "FROM fishing,fishnames,snasti,places WHERE fishing.[Рыба] = fishnames.id AND fishing.[Снасть] = snasti.id AND fishing.[Водоем] = places.id AND [Дата] = DATEVALUE('" + CurrentDate + "') ORDER BY id2";
                    myOleDbConnection.Open();
                    DataSet ds = new DataSet();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                    adapter.Fill(ds);
                    preC = ds.Tables[0].Rows.Count - 1;
                    for (int i = 0; i <= preC; i++)
                    {
                        if (i != 0) addControlsFunc();
                        ComboBox fish_ = (ComboBox)mainFishGroupBox.Controls["fish_" + i];
                        Control lenght_ = mainFishGroupBox.Controls["lenght_" + i];
                        Control number_ = mainFishGroupBox.Controls["number_" + i];
                        Control weight_ = mainFishGroupBox.Controls["weight_" + i];
                        Control other_ = mainFishGroupBox.Controls["other_" + i];
                        ComboBox snasti_ = (ComboBox)mainFishGroupBox.Controls["snasti_" + i];
                        Control place_ = mainFishGroupBox.Controls["place_" + i];
                        fish_.SelectedItem = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                        number_.Text = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                        lenght_.Text = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                        weight_.Text = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                        other_.Text = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                        snasti_.SelectedItem = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                        place_.Text = ds.Tables[0].Rows[i].ItemArray[20].ToString();
                    }
                    date.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString().Remove(10);
                    if (ds.Tables[0].Rows[0].ItemArray[1].ToString() != "")
                    {
                        endDate.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString().Remove(10);
                        endFishingDate.Checked = true;
                    }
                    int reg = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[10]);
                    if (reg > 20)
                    {
                        region.SelectedIndex = reg - 2;
                    }
                    else
                    {
                        region.SelectedIndex = reg - 1;
                    }
                    vodoem.SelectedItem = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                    fishBase.SelectedItem = ds.Tables[0].Rows[0].ItemArray[21].ToString();
                    timeofDay.SelectedItem = ds.Tables[0].Rows[0].ItemArray[11];
                    rain.SelectedItem = ds.Tables[0].Rows[0].ItemArray[12];
                    wind.SelectedItem = ds.Tables[0].Rows[0].ItemArray[13];
                    direction.SelectedItem = ds.Tables[0].Rows[0].ItemArray[14];
                    temperature.Text = ds.Tables[0].Rows[0].ItemArray[15].ToString();
                    pressure.Text = ds.Tables[0].Rows[0].ItemArray[16].ToString();
                    moon.SelectedItem = ds.Tables[0].Rows[0].ItemArray[17];
                    otherFishers.SelectedItem = ds.Tables[0].Rows[0].ItemArray[18];
                    totalWeight.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                    notesTextBox.Text = ds.Tables[0].Rows[0].ItemArray[19].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    writeErrors(ex.ToString());
                }
                finally
                {
                    myOleDbConnection.Close();
                }
            }
            else
            {
                CurrentID = "";
                if (Options.MyRegion > 20)
                {
                    region.SelectedIndex = Options.MyRegion - 2;
                }
                else
                {
                    region.SelectedIndex = Options.MyRegion - 1;
                }
                if (Vodoem != "") vodoem.SelectedItem = Vodoem;
                if (Fish != "") fish_0.SelectedItem = Fish;
                if (Snasti != "") snasti_0.SelectedItem = Snasti;
            }
        }

        void NewReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ActiveControl = date;
            Vodoem = "";
            Snasti = "";
            Fish = "";
            CurrentDate = "";
            CurrentID = "";
            date.Text = "";
            endDate.Text = "";
            number.Text = "";
            lenght.Text = "";
            weight.Text = "";
            other.Text = "";
            totalWeight.Text = "";
            place_0.Text = "";
            notesTextBox.Text = "";
            pressure.Text = "";
            temperature.Text = "";
            fish_0.SelectedIndex = -1;
            snasti_0.SelectedIndex = -1;
            region.SelectedIndex = -1;
            fishBase.SelectedIndex = -1;
            vodoem.SelectedIndex = -1;
            timeofDay.SelectedIndex = -1;
            rain.SelectedIndex = -1;
            wind.SelectedIndex = -1;
            moon.SelectedIndex = -1;
            otherFishers.SelectedIndex = -1;
            endFishingDate.Checked = false;
            endDate.Enabled = false;
            preC = 0;
            int z = c;
            if (z > 0)
            {
                for (int i = 0; i < z; i++)
                {
                    deleteControlsFunc();
                }
            }
        }

        void NewReport_KeyDown(object sender, KeyEventArgs e)
        {
            Control control = NewReport.ActiveForm.ActiveControl;
            if ((e.KeyCode == Keys.Enter) && (control != null) && (control != notesTextBox))
            {
                this.SelectNextControl(control, true, true, true, true);
            }
            if ((e.Modifiers == Keys.Control) && ((e.KeyCode == Keys.Oemplus) || (e.KeyCode == Keys.Add)))
            {
                addControlsFunc();
            }
            if ((e.Modifiers == Keys.Control) && ((e.KeyCode == Keys.OemMinus) || (e.KeyCode == Keys.Subtract)))
            {
                deleteControlsFunc();
            }
            if ((e.Modifiers == Keys.Control) && ((e.KeyCode == Keys.S)))
            {
                saveFunc();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void saveReportButton_Click(object sender, EventArgs e)
        {
            saveFunc();
        }

        private void fillComboBoxes()
        {
            region.Items.Clear();
            vodoem.Items.Clear();
            fish_0.Items.Clear();
            snasti_0.Items.Clear();
            fishBase.Items.Clear();
            region.Items.AddRange(Form1.RegionTable);
            vodoem.Items.AddRange(Form1.VodoemTable);
            fish_0.Items.AddRange(Form1.FishnamesTable);
            snasti_0.Items.AddRange(Form1.SnastiTable);
            fishBase.Items.Add("");
            fishBase.Items.AddRange(Form1.FishbaseTable);
        }

        private void addControlsFunc()
        {
            if (c < 10)
            {
                c++;
                ComboBox fishfish = new ComboBox();
                fishfish.AllowDrop = true;
                fishfish.DropDownStyle = ComboBoxStyle.DropDownList;
                fishfish.Location = new System.Drawing.Point(fish_0.Location.X, fish_0.Location.Y + r * c);
                fishfish.Name = "fish_" + c;
                fishfish.Size = new System.Drawing.Size(fish_0.Size.Width, fish_0.Size.Height);
                mainFishGroupBox.Controls.Add(fishfish);
                fishfish.TabIndex = 20 + 7 * c;
                for (int i = 0; i < fish_0.Items.Count; i++)
                {
                    fishfish.Items.Add(fish_0.Items[i]);
                }

                ComboBox snastisnasti = new ComboBox();
                snastisnasti.AllowDrop = true;
                snastisnasti.DropDownStyle = ComboBoxStyle.DropDownList;
                snastisnasti.Location = new System.Drawing.Point(snasti_0.Location.X, snasti_0.Location.Y + r * c);
                snastisnasti.Name = "snasti_" + c;
                snastisnasti.Size = new System.Drawing.Size(snasti_0.Size.Width, snasti_0.Size.Height);
                mainFishGroupBox.Controls.Add(snastisnasti);
                snastisnasti.TabIndex = 21 + 7 * c;
                for (int i = 0; i < snasti_0.Items.Count; i++)
                {
                    snastisnasti.Items.Add(snasti_0.Items[i]);
                }
                if (snasti_0.SelectedIndex != -1) { snastisnasti.SelectedIndex = snasti_0.SelectedIndex; }

                MaskedTextBox numbernumber = new MaskedTextBox();
                numbernumber.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                numbernumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                numbernumber.HidePromptOnLeave = true;
                numbernumber.HideSelection = false;
                numbernumber.Mask = "000";
                numbernumber.PromptChar = ' ';
                numbernumber.SkipLiterals = false;
                numbernumber.Location = new Point(number.Location.X, number.Location.Y + r * c);
                numbernumber.Name = "number_" + c;
                numbernumber.Size = new Size(number.Size.Width, number.Size.Height);
                numbernumber.TabIndex = 22 + 7 * c;
                mainFishGroupBox.Controls.Add(numbernumber);

                TextBox otherother = new TextBox();
                otherother.Location = new Point(other.Location.X, other.Location.Y + r * c);
                otherother.Name = "other_" + c;
                otherother.Size = new Size(other.Size.Width, other.Size.Height);
                otherother.TabIndex = 23 + 7 * c;
                mainFishGroupBox.Controls.Add(otherother);

                MaskedTextBox lenghtlenght = new MaskedTextBox();
                lenghtlenght.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                lenghtlenght.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                lenghtlenght.HidePromptOnLeave = true;
                lenghtlenght.HideSelection = false;
                lenghtlenght.Mask = "000";
                lenghtlenght.PromptChar = ' ';
                lenghtlenght.SkipLiterals = false;
                lenghtlenght.Location = new Point(lenght.Location.X, lenght.Location.Y + r * c);
                lenghtlenght.Name = "lenght_" + c;
                lenghtlenght.Size = new Size(lenght.Size.Width, lenght.Size.Height);
                lenghtlenght.TabIndex = 24 + 7 * c;
                mainFishGroupBox.Controls.Add(lenghtlenght);

                MaskedTextBox weightweight = new MaskedTextBox();
                weightweight.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                weightweight.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                weightweight.HidePromptOnLeave = true;
                weightweight.HideSelection = false;
                weightweight.Mask = "00000";
                weightweight.PromptChar = ' ';
                weightweight.SkipLiterals = false;
                weightweight.Location = new Point(weight.Location.X, weight.Location.Y + r * c);
                weightweight.Name = "weight_" + c;
                weightweight.Size = new Size(weight.Size.Width, weight.Size.Height);
                weightweight.TabIndex = 25 + 7 * c;
                mainFishGroupBox.Controls.Add(weightweight);

                TextBox mapmap = new TextBox();
                mapmap.DoubleClick += new EventHandler(mapmap_DoubleClick);
                mapmap.Click += new EventHandler(mapmap_Click);
                mapmap.Location = new System.Drawing.Point(place_0.Location.X, place_0.Location.Y + r * c);
                mapmap.Name = "place_" + c;
                mapmap.Size = new System.Drawing.Size(place_0.Size.Width, place_0.Size.Height);
                mapmap.TabIndex = 26 + 7 * c;
                mainFishGroupBox.Controls.Add(mapmap);
                if (place_0.Text != "") { mapmap.Text = place_0.Text; }

                mainFishGroupBox.Size = new Size(mainFishGroupBox.Size.Width, mainFishGroupBox.Size.Height + r);
                deleteControls.Visible = true;
                deleteControls.Location = new Point(addControls.Location.X, mapmap.Location.Y + r - deleteControls.Height);
            }
        }

        void mapmap_Click(object sender, EventArgs e)
        {
            ((TextBox)this.ActiveControl).SelectAll();
        }

        void mapmap_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //TextBox tb = (TextBox)sender;
                //string url = "gotomap.html?place=" + tb.Text;
                string url = "gotomap.html";
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + url);
                /*System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.yandex.ru");

                TextBox tb = (TextBox)sender;
                GoogleMaps.Koordinates = tb.Text;
                GM.FormClosed += new FormClosedEventHandler(GM_FormClosed);
                PlaceClicked = tb.Name;
                GM.ShowDialog();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                writeErrors(ex.ToString());
            }
        }

        void GM_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void deleteControlsFunc()
        {
            if (c > 0)
            {
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["fish_" + c]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["number_" + c]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["lenght_" + c]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["weight_" + c]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["other_" + c]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["snasti_" + c]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["place_" + c]);
                mainFishGroupBox.Size = new Size(mainFishGroupBox.Size.Width, mainFishGroupBox.Size.Height - r);
                deleteControls.Location = new Point(addControls.Location.X, deleteControls.Location.Y - r);
                c--;
                if (c == 0) deleteControls.Visible = false;
            }
        }

        bool checkValid()
        {
            bool valid = false;
            string messageText = "";
            if (endDate.Text != "")
            {
                if (Convert.ToDateTime(date.Text) >= Convert.ToDateTime(endDate.Text))
                {
                    messageText += "Дата окончания не должна быть меньше или равна дате рыбалки\n";
                }
            }
            if (date.Text == "")
            {
                messageText += "Введите дату\n";
            }
            if (fish_0.Text == "")
            {
                messageText += "Введите название рыбы\n";
            }
            if (snasti_0.Text == "")
            {
                messageText += "Введите способ ловли\n";
            }
            if (number.Text == "")
            {
                messageText += "Введите количество\n";
            }
            if (region.Text == "")
            {
                messageText += "Введите регион\n";
            }
            if (vodoem.Text == "")
            {
                messageText += "Введите водоем\n";
            }
            if (timeofDay.Text == "")
            {
                messageText += "Введите время дня\n";
            }
            if (rain.Text == "")
            {
                messageText += "Введите погоду\n";
            }
            if (place_0.Text != "")
            {
                if (!place_0.Text.Contains(",") || !place_0.Text.Contains("."))
                {
                    messageText += "Неправильный формат ввода координат\n";
                }
            }
            if (temperature.Text != "")
            {
                try
                {
                    int t = Convert.ToInt32(temperature.Text);
                    if (Math.Abs(t) > 70) messageText += "Неправильный формат ввода температуры\n";
                }
                catch
                {
                    messageText += "Неправильный формат ввода температуры\n";
                }
            }
            if (pressure.Text != "")
            {
                try
                {
                    int p = Convert.ToInt32(pressure.Text);
                    if (p > 900 || p < 600) messageText += "Неправильный формат ввода давления\n";
                }
                catch
                {
                    messageText += "Неправильный формат ввода давления\n";
                }
            }
            if (messageText != "")
            {
                MessageBox.Show(messageText, "Ошибка ввода");
                valid = false;
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        private void saveFunc()
        {
            if (checkValid())
            {
                bool close = true;
                string dateFishing = date.Text;
                string endDateFishing = "NULL";
                if (endFishingDate.Checked && endDate.Text != "") endDateFishing = "\"" + endDate.Text + "\"";
                string[] dateSplited = new string[3];
                dateSplited = dateFishing.Split('.');
                string id_ = dateSplited[2] + dateSplited[1] + dateSplited[0];
                string fishTotalWeight = "NULL";
                string fishTemperature = "NULL";
                string fishPressure = "NULL";
                string fishWind = "";
                string fishDirection = "";
                string fishMoon = "";
                string fishOtherFishers = "";
                string fishBazaOtdyha = "";
                string fishTime = timeofDay.SelectedItem.ToString();
                string fishRain = rain.SelectedItem.ToString();
                if (wind.SelectedIndex > 0) fishWind = wind.SelectedItem.ToString();
                if (direction.SelectedIndex > 0) fishDirection = direction.SelectedItem.ToString();
                if (moon.SelectedIndex > 0) fishMoon = moon.SelectedItem.ToString();
                if (otherFishers.SelectedIndex > 0) fishOtherFishers = otherFishers.SelectedItem.ToString();
                if (fishBase.SelectedIndex > 0) fishBazaOtdyha = fishBase.SelectedItem.ToString();
                if (temperature.Text != "") fishTemperature = temperature.Text;
                if (pressure.Text != "") fishPressure = pressure.Text;
                if (totalWeight.Text != "") fishTotalWeight = totalWeight.Text;
                string fishNotes = notesTextBox.Text.Replace('"', '\'');
                string fishRegion = region.SelectedItem.ToString().Remove(2).TrimEnd(' ');
                string fishVodoem = Form1.VodoemID[vodoem.SelectedIndex];
                number.Name = "number_0";
                lenght.Name = "lenght_0";
                weight.Name = "weight_0";
                other.Name = "other_0";
                //if (CurrentID != "") curID = CurrentID.Remove(8);
                if (c < preC)
                {
                    for (int i = 0; i <= preC; i++)
                    {
                        if (i <= c)
                        {
                            ComboBox fish_ = new ComboBox();
                            ComboBox snasti_ = new ComboBox();
                            fish_ = (ComboBox)mainFishGroupBox.Controls["fish_" + i];
                            snasti_ = (ComboBox)mainFishGroupBox.Controls["snasti_" + i];
                            Control number_ = mainFishGroupBox.Controls["number_" + i];
                            Control lenght_ = mainFishGroupBox.Controls["lenght_" + i];
                            Control weight_ = mainFishGroupBox.Controls["weight_" + i];
                            Control other_ = mainFishGroupBox.Controls["other_" + i];
                            Control place_ = mainFishGroupBox.Controls["place_" + i];
                            if ((fish_.SelectedIndex == -1) || (snasti_.SelectedIndex == -1) || (number_.Text == ""))
                            {
                                MessageBox.Show("Введены не все данные", "Ошибка ввода");
                                close = false;
                            }
                            else
                            {
                                string fishId = Form1.FishnamesID[fish_.SelectedIndex];
                                string fishSnasti = Form1.SnastiID[snasti_.SelectedIndex];
                                string fishPlace = place_.Text;
                                string id2 = id_ + i;
                                string fishNumber = "NULL";
                                if (number_.Text != "") fishNumber = number_.Text;
                                string fishLenght = "NULL";
                                if (lenght_.Text != "") fishLenght = lenght_.Text;
                                string fishWeight = "NULL";
                                if (weight_.Text != "") fishWeight = weight_.Text;
                                string otherFish = other_.Text.Replace('.', ',').Replace('/', ',');
                                myOleDbCommand.CommandText = "UPDATE fishing " +
                                "SET [Дата] = \"" + dateFishing + "\",[Дата2] = " + endDateFishing + ",[Рыба] = " + fishId + ",[Количество] = " + fishNumber + ",[Длина] = " + fishLenght + ",[Вес] = " + fishWeight + ",[Остальные] = \"" + otherFish + "\",[Общий вес] = "
                                + fishTotalWeight + ",[Снасть] = " + fishSnasti + ",[Водоем] = " + fishVodoem + ",[Регион] = " + fishRegion + ",[Время дня] = \"" + fishTime + "\",[Осадки] = \"" + fishRain + "\",[Ветер] = \"" + fishWind + "\",[Направление] = \""
                                + fishDirection + "\",[Температура] = " + fishTemperature + ",[Давление] = " + fishPressure + ",[Луна] = \"" + fishMoon + "\",[У других] = \"" + fishOtherFishers + "\",[База] = \"" + fishBazaOtdyha + "\",[Заметки] = \"" + fishNotes + "\"" + ",[Координаты] = \"" + fishPlace + "\",[id2] =\"" + id2 + "\" " +
                                "WHERE id2 = \"" + id_ + i + "\"";
                            }
                        }
                        else
                        {
                            myOleDbCommand.CommandText = "DELETE FROM fishing WHERE id2 = \"" + id_ + i + "\"";
                        }
                        try
                        {
                            myOleDbConnection.Open();
                            myOleDbCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("повторяющиеся"))
                            {
                                MessageBox.Show("Запись с такой датой уже существует", "Ошибка ввода");
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                            }
                            writeErrors(ex.ToString());
                        }
                        finally
                        {
                            myOleDbConnection.Close();
                        }
                    }
                }
                if (c >= preC)
                {
                    for (int i = 0; i <= c; i++)
                    {
                        ComboBox fish_ = new ComboBox();
                        ComboBox snasti_ = new ComboBox();
                        fish_ = (ComboBox)mainFishGroupBox.Controls["fish_" + i];
                        snasti_ = (ComboBox)mainFishGroupBox.Controls["snasti_" + i];
                        Control number_ = mainFishGroupBox.Controls["number_" + i];
                        Control lenght_ = mainFishGroupBox.Controls["lenght_" + i];
                        Control weight_ = mainFishGroupBox.Controls["weight_" + i];
                        Control other_ = mainFishGroupBox.Controls["other_" + i];
                        Control place_ = mainFishGroupBox.Controls["place_" + i];
                        if ((fish_.SelectedIndex == -1) || (snasti_.SelectedIndex == -1) || (number_.Text == ""))
                        {
                            MessageBox.Show("Введены не все данные", "Ошибка ввода");
                            close = false;
                        }
                        else
                        {
                            string fishId = Form1.FishnamesID[fish_.SelectedIndex];
                            string fishSnasti = Form1.SnastiID[snasti_.SelectedIndex];
                            string fishPlace = place_.Text;
                            string fishNumber = "NULL";
                            if (number_.Text != "") fishNumber = number_.Text;
                            string fishLenght = "NULL";
                            if (lenght_.Text != "") fishLenght = lenght_.Text;
                            string fishWeight = "NULL";
                            if (weight_.Text != "") fishWeight = weight_.Text;
                            string otherFish = other_.Text.Replace('.', ',').Replace('/', ',');
                            string id2 = id_ + i;
                            if (this.Text == "Отчет")
                            {
                                //Для модификации базы нажать кнопку сохранить.//
                                //myOleDbCommand.CommandText = "UPDATE fishing set [Водоем] = 10001 WHERE [Водоем] in (10002,10003,10004,10005,10006,10007,10008,10009,10010,10011,10012,10013,10015,10016,10018,10019,10020,10023,10027,10028,10029,10031,10032,10033,10035)";
                                myOleDbCommand.CommandText = "INSERT INTO fishing ([Дата],[Дата2],[Рыба],[Количество],[Длина],[Вес],[Остальные],[Общий вес],[Снасть],[Водоем],[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],[База],[Заметки],[id2],[Координаты]) "
                                                                + "VALUES (\"" + dateFishing + "\"," + endDateFishing + "," + fishId + "," + fishNumber + "," + fishLenght + "," + fishWeight + ",\"" + otherFish + "\"," + fishTotalWeight + "," + fishSnasti + "," + fishVodoem + "," + fishRegion
                                                                + ",\"" + fishTime + "\",\"" + fishRain + "\",\"" + fishWind + "\",\"" + fishDirection + "\"," + fishTemperature + "," + fishPressure + ",\"" + fishMoon + "\",\"" + fishOtherFishers + "\",\"" + fishBazaOtdyha + "\",\"" + fishNotes + "\",\"" + id2 + "\",\"" + fishPlace + "\")";
                            }
                            else
                            {
                                if (i <= preC)
                                {
                                    myOleDbCommand.CommandText = "UPDATE fishing " +
                                    "SET [Дата] = \"" + dateFishing + "\",[Дата2] = " + endDateFishing + ",[Рыба] = " + fishId + ",[Количество] = " + fishNumber + ",[Длина] = " + fishLenght + ",[Вес] = " + fishWeight + ",[Остальные] = \"" + otherFish + "\",[Общий вес] = "
                                    + fishTotalWeight + ",[Снасть] = " + fishSnasti + ",[Водоем] = " + fishVodoem + ",[Регион] = " + fishRegion + ",[Время дня] = \"" + fishTime + "\",[Осадки] = \"" + fishRain + "\",[Ветер] = \"" + fishWind + "\",[Направление] = \""
                                    + fishDirection + "\",[Температура] = " + fishTemperature + ",[Давление] = " + fishPressure + ",[Луна] = \"" + fishMoon + "\",[У других] = \"" + fishOtherFishers + "\",[База] = \"" + fishBazaOtdyha + "\",[Заметки] = \"" + fishNotes + "\"" + ",[Координаты] = \"" + fishPlace + "\",[id2] =\"" + id2 + "\" " +
                                    "WHERE id2 = \"" + id_ + i + "\"";
                                }
                                else
                                {
                                    myOleDbCommand.CommandText = "INSERT INTO fishing ([Дата],[Дата2],[Рыба],[Количество],[Длина],[Вес],[Остальные],[Общий вес],[Снасть],[Водоем],[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],[База],[Заметки],[id2],[Координаты]) "
                                + "VALUES (\"" + dateFishing + "\"," + endDateFishing + "," + fishId + "," + fishNumber + "," + fishLenght + "," + fishWeight + ",\"" + otherFish + "\"," + fishTotalWeight + "," + fishSnasti + "," + fishVodoem + "," + fishRegion
                                + ",\"" + fishTime + "\",\"" + fishRain + "\",\"" + fishWind + "\",\"" + fishDirection + "\"," + fishTemperature + "," + fishPressure + ",\"" + fishMoon + "\",\"" + fishOtherFishers + "\",\"" + fishBazaOtdyha + "\",\"" + fishNotes + "\",\"" + id2 + "\",\"" + fishPlace + "\")";
                                }
                            }
                            try
                            {
                                myOleDbConnection.Open();
                                myOleDbCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("повторяющиеся"))
                                {
                                    MessageBox.Show("Запись с такой датой уже существует", "Ошибка ввода");
                                    ActiveForm.Close();
                                }
                                else
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                writeErrors(ex.ToString());
                            }
                            finally
                            {
                                myOleDbConnection.Close();
                            }
                        }
                    }
                }
                formClose(close);
            }
        }

        void formClose(bool close1)
        {
            if (close1) this.Close();
        }


        private void addControls_Click(object sender, EventArgs e)
        {
            addControlsFunc();
        }

        private void deleteControls_Click(object sender, EventArgs e)
        {
            deleteControlsFunc();
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

        private void fotoButton_Click(object sender, EventArgs e)
        {
            if (date.Text != "")
            {
                string[] dateSplited = new string[3];
                dateSplited = date.Text.Split('.');
                string etad = dateSplited[2] + dateSplited[1] + dateSplited[0];
                string dirPath = AppDomain.CurrentDomain.BaseDirectory + "\\foto\\" + etad;
                fotoList.Text = etad;
                fotoList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите дату", "Ошибка ввода");
            }
        }

        private void endFishingDate_CheckedChanged(object sender, EventArgs e)
        {
            if (endFishingDate.Checked)
            {
                endDate.Enabled = true;
                endDate.BackColor = SystemColors.Window;
            }
            else
            {
                endDate.Enabled = false;
                endDate.Text = "";
                endDate.BackColor = SystemColors.Control;
            }
        }
    }
}
