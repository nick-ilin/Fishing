using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Fishing
{
    public partial class NewReport : Form
    {
        private static string _CurrentDate = "";
        private static string _CurrentID = "";
        private static string _Vodoem = "";
        private static string _Fish = "";
        private static string _Snasti = "";
        public GoogleMaps googleMaps;
        public Foto fotoList;
        private readonly OleDbConnection myOleDbConnection;
        private readonly OleDbCommand myOleDbCommand;
        private readonly string dbPath = AppDomain.CurrentDomain.BaseDirectory + "fishing.mdb";
        private const string ERRPATH = "errors.txt";
        private const int ADDHEIGHT = 30;
        private int previousCountOfLines = 0;
        private int countOfLines = 0;

        public NewReport()
        {
            InitializeComponent();
            googleMaps = new GoogleMaps();
            fotoList = new Foto();
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=" + dbPath;
            myOleDbConnection = new OleDbConnection(connectionString);
            myOleDbCommand = myOleDbConnection.CreateCommand();
            saveReportButton.Click += new EventHandler(SaveReportButton_Click);
            place_0.DoubleClick += new EventHandler(Place_0_DoubleClick);
            place_0.Click += new EventHandler(Place_0_Click);
            other.LostFocus += new EventHandler(Other_LostFocus);
            this.FormClosed += new FormClosedEventHandler(NewReport_FormClosed);
            this.Shown += new EventHandler(NewReport_Shown);
            this.ActiveControl = date;
            this.KeyDown += new KeyEventHandler(NewReport_KeyDown);
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

        private void Other_LostFocus(object sender, EventArgs e)
        {
            string[] text = other.Text.Split(',', '.');
            lenght.Text = text[text.Length - 1];
        }

        private void Place_0_Click(object sender, EventArgs e)
        {
            place_0.SelectAll();
        }

        private void Place_0_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.yandex.ru");
                GoogleMaps.Koordinates = place_0.Text;
                googleMaps.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                WriteErrors(ex.ToString());
            }
        }

        private void NewReport_Shown(object sender, EventArgs e)
        {
            FillComboBoxes();
            GetControlsAndData();
        }

        private void GetControlsAndData()
        {
            if (CurrentDate != "")
            {
                this.Text = "Отчет " + CurrentDate;
                number.Name = "number_0";
                lenght.Name = "lenght_0";
                weight.Name = "weight_0";
                other.Name = "other_0";
                try
                {
                    myOleDbCommand.CommandText = "SELECT [Дата],fishnames.name,[Количество],[Длина],[Вес],[Остальные],[Общий вес],snasti.name,places.name,[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],[Заметки],[Координаты],id2 " +
                        "FROM fishing,fishnames,snasti,places WHERE fishing.[Рыба] = fishnames.id AND fishing.[Снасть] = snasti.id AND fishing.[Водоем] = places.id AND [Дата] = DATEVALUE('" + CurrentDate + "') ORDER BY id2";
                    myOleDbConnection.Open();
                    DataSet ds = new DataSet();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                    adapter.Fill(ds);
                    previousCountOfLines = ds.Tables[0].Rows.Count - 1;
                    for (int i = 0; i <= previousCountOfLines; i++)
                    {
                        if (i != 0) AddControlsFunc();
                        ComboBox fish_ = (ComboBox)mainFishGroupBox.Controls["fish_" + i];
                        Control lenght_ = mainFishGroupBox.Controls["lenght_" + i];
                        Control number_ = mainFishGroupBox.Controls["number_" + i];
                        Control weight_ = mainFishGroupBox.Controls["weight_" + i];
                        Control other_ = mainFishGroupBox.Controls["other_" + i];
                        ComboBox snasti_ = (ComboBox)mainFishGroupBox.Controls["snasti_" + i];
                        Control place_ = mainFishGroupBox.Controls["place_" + i];
                        fish_.SelectedItem = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                        number_.Text = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                        lenght_.Text = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                        weight_.Text = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                        other_.Text = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                        snasti_.SelectedItem = ds.Tables[0].Rows[i].ItemArray[7].ToString();
                        place_.Text = ds.Tables[0].Rows[i].ItemArray[19].ToString();
                    }
                    date.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString().Remove(10);
                    int reg = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[9]);
                    if (reg > 20)
                    {
                        region.SelectedIndex = reg - 2;
                    }
                    else
                    {
                        region.SelectedIndex = reg - 1;
                    }
                    vodoem.SelectedItem = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                    timeofDay.SelectedItem = ds.Tables[0].Rows[0].ItemArray[10];
                    rain.SelectedItem = ds.Tables[0].Rows[0].ItemArray[11];
                    wind.SelectedItem = ds.Tables[0].Rows[0].ItemArray[12];
                    direction.SelectedItem = ds.Tables[0].Rows[0].ItemArray[13];
                    temperature.Text = ds.Tables[0].Rows[0].ItemArray[14].ToString();
                    pressure.Text = ds.Tables[0].Rows[0].ItemArray[15].ToString();
                    moon.SelectedItem = ds.Tables[0].Rows[0].ItemArray[16];
                    otherFishers.SelectedItem = ds.Tables[0].Rows[0].ItemArray[17];
                    totalWeight.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                    notesTextBox.Text = ds.Tables[0].Rows[0].ItemArray[18].ToString();
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

        private void NewReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            Vodoem = "";
            Snasti = "";
            Fish = "";
            CurrentDate = "";
            CurrentID = "";
            date.Text = "";
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
            vodoem.SelectedIndex = -1;
            timeofDay.SelectedIndex = -1;
            rain.SelectedIndex = -1;
            wind.SelectedIndex = -1;
            moon.SelectedIndex = -1;
            otherFishers.SelectedIndex = -1;
            previousCountOfLines = 0;
            int z = countOfLines;
            if (z > 0)
            {
                for (int i = 0; i < z; i++)
                {
                    DeleteControlsFunc();
                }
            }
        }

        private void NewReport_KeyDown(object sender, KeyEventArgs e)
        {
            Control control = NewReport.ActiveForm.ActiveControl;
            if ((e.KeyCode == Keys.Enter) && (control != null) && (control != notesTextBox))
            {
                this.SelectNextControl(control, true, true, true, true);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Oemplus))
            {
                AddControlsFunc();
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.OemMinus))
            {
                DeleteControlsFunc();
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S))
            {
                SaveFunc();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void SaveReportButton_Click(object sender, EventArgs e)
        {
            SaveFunc();
        }

        private void FillComboBoxes()
        {
            region.Items.Clear();
            vodoem.Items.Clear();
            fish_0.Items.Clear();
            snasti_0.Items.Clear();
            region.Items.AddRange(Form1.RegionTable);
            vodoem.Items.AddRange(Form1.VodoemTable);
            fish_0.Items.AddRange(Form1.FishnameTable);
            snasti_0.Items.AddRange(Form1.SnastiTable);
        }
        private void AddControlsFunc()
        {
            if (countOfLines < 10)
            {
                countOfLines++;
                ComboBox fishfish = new ComboBox
                {
                    AllowDrop = true,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(fish_0.Location.X, fish_0.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "fish_" + countOfLines,
                    Size = new Size(fish_0.Size.Width, fish_0.Size.Height),
                    TabIndex = 20 + 7 * countOfLines
                };
                mainFishGroupBox.Controls.Add(fishfish);
                for (int i = 0; i < fish_0.Items.Count; i++)
                {
                    fishfish.Items.Add(fish_0.Items[i]);
                }

                ComboBox snastisnasti = new ComboBox
                {
                    AllowDrop = true,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(snasti_0.Location.X, snasti_0.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "snasti_" + countOfLines,
                    Size = new Size(snasti_0.Size.Width, snasti_0.Size.Height),
                    TabIndex = 21 + 7 * countOfLines
                };
                mainFishGroupBox.Controls.Add(snastisnasti);
                for (int i = 0; i < snasti_0.Items.Count; i++)
                {
                    snastisnasti.Items.Add(snasti_0.Items[i]);
                }
                if (snasti_0.SelectedIndex != -1) { snastisnasti.SelectedIndex = snasti_0.SelectedIndex; }

                MaskedTextBox numbernumber = new MaskedTextBox
                {
                    CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                    TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                    HidePromptOnLeave = true,
                    HideSelection = false,
                    Mask = "000",
                    PromptChar = ' ',
                    SkipLiterals = false,
                    Location = new Point(number.Location.X, number.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "number_" + countOfLines,
                    Size = new Size(number.Size.Width, number.Size.Height),
                    TabIndex = 22 + 7 * countOfLines
                };
                mainFishGroupBox.Controls.Add(numbernumber);

                TextBox otherother = new TextBox
                {
                    Location = new Point(other.Location.X, other.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "other_" + countOfLines,
                    Size = new Size(other.Size.Width, other.Size.Height),
                    TabIndex = 23 + 7 * countOfLines
                };
                mainFishGroupBox.Controls.Add(otherother);

                MaskedTextBox lenghtlenght = new MaskedTextBox
                {
                    CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                    TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                    HidePromptOnLeave = true,
                    HideSelection = false,
                    Mask = "000",
                    PromptChar = ' ',
                    SkipLiterals = false,
                    Location = new Point(lenght.Location.X, lenght.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "lenght_" + countOfLines,
                    Size = new Size(lenght.Size.Width, lenght.Size.Height),
                    TabIndex = 24 + 7 * countOfLines
                };
                mainFishGroupBox.Controls.Add(lenghtlenght);

                MaskedTextBox weightweight = new MaskedTextBox
                {
                    CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                    TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                    HidePromptOnLeave = true,
                    HideSelection = false,
                    Mask = "00000",
                    PromptChar = ' ',
                    SkipLiterals = false,
                    Location = new Point(weight.Location.X, weight.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "weight_" + countOfLines,
                    Size = new Size(weight.Size.Width, weight.Size.Height),
                    TabIndex = 25 + 7 * countOfLines
                };
                mainFishGroupBox.Controls.Add(weightweight);

                TextBox mapmap = new TextBox
                {
                    Location = new Point(place_0.Location.X, place_0.Location.Y + ADDHEIGHT * countOfLines),
                    Name = "place_" + countOfLines,
                    Size = new Size(place_0.Size.Width, place_0.Size.Height),
                    TabIndex = 26 + 7 * countOfLines
                };
                mapmap.DoubleClick += new EventHandler(Mapmap_DoubleClick);
                mapmap.Click += new EventHandler(Mapmap_Click);
                mainFishGroupBox.Controls.Add(mapmap);

                if (place_0.Text != "") { mapmap.Text = place_0.Text; }

                mainFishGroupBox.Size = new Size(mainFishGroupBox.Size.Width, mainFishGroupBox.Size.Height + ADDHEIGHT);
                deleteControls.Visible = true;
                deleteControls.Location = new Point(addControls.Location.X, mapmap.Location.Y + ADDHEIGHT - deleteControls.Height);
            }
        }

        private void Mapmap_Click(object sender, EventArgs e)
        {
            ((TextBox)this.ActiveControl).SelectAll();
        }

        private void Mapmap_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.yandex.ru");
                TextBox tb = (TextBox)sender;
                GoogleMaps.Koordinates = tb.Text;
                googleMaps.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер Яндекс не отвечает, проверьте интернет-соединение.");
                WriteErrors(ex.ToString());
            }
        }

        private void DeleteControlsFunc()
        {
            if (countOfLines > 0)
            {
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["fish_" + countOfLines]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["number_" + countOfLines]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["lenght_" + countOfLines]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["weight_" + countOfLines]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["other_" + countOfLines]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["snasti_" + countOfLines]);
                mainFishGroupBox.Controls.Remove(mainFishGroupBox.Controls["place_" + countOfLines]);
                mainFishGroupBox.Size = new Size(mainFishGroupBox.Size.Width, mainFishGroupBox.Size.Height - ADDHEIGHT);
                deleteControls.Location = new Point(addControls.Location.X, deleteControls.Location.Y - ADDHEIGHT);
                countOfLines--;
                if (countOfLines == 0) deleteControls.Visible = false;
            }
        }

        private bool CheckValid()
        {
            bool valid;
            string messageText = "";
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

        private void SaveFunc()
        {
            if (CheckValid())
            {
                bool close = true;
                string dateFishing = date.Text;
                string[] dateSplited = dateFishing.Split('.');
                string id_ = dateSplited[2] + dateSplited[1] + dateSplited[0];
                string fishTotalWeight = "NULL";
                string fishTemperature = "NULL";
                string fishPressure = "NULL";
                string fishWind = "";
                string fishDirection = "";
                string fishMoon = "";
                string fishOtherFishers = "";
                string fishTime = timeofDay.SelectedItem.ToString();
                string fishRain = rain.SelectedItem.ToString();
                if (wind.SelectedIndex > 0) fishWind = wind.SelectedItem.ToString();
                if (direction.SelectedIndex > 0) fishDirection = direction.SelectedItem.ToString();
                if (moon.SelectedIndex > 0) fishMoon = moon.SelectedItem.ToString();
                if (otherFishers.SelectedIndex > 0) fishOtherFishers = otherFishers.SelectedItem.ToString();
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
                if (countOfLines < previousCountOfLines)
                {
                    for (int i = 0; i <= previousCountOfLines; i++)
                    {
                        if (i <= countOfLines)
                        {
                            ComboBox fish_ = (ComboBox)mainFishGroupBox.Controls["fish_" + i];
                            ComboBox snasti_ = (ComboBox)mainFishGroupBox.Controls["snasti_" + i];
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
                                string fishId = Form1.FishnameID[fish_.SelectedIndex];
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
                                "SET [Дата] = \"" + dateFishing + "\",[Рыба] = " + fishId + ",[Количество] = " + fishNumber + ",[Длина] = " + fishLenght + ",[Вес] = " + fishWeight + ",[Остальные] = \"" + otherFish + "\",[Общий вес] = "
                                + fishTotalWeight + ",[Снасть] = " + fishSnasti + ",[Водоем] = " + fishVodoem + ",[Регион] = " + fishRegion + ",[Время дня] = \"" + fishTime + "\",[Осадки] = \"" + fishRain + "\",[Ветер] = \"" + fishWind + "\",[Направление] = \""
                                + fishDirection + "\",[Температура] = " + fishTemperature + ",[Давление] = " + fishPressure + ",[Луна] = \"" + fishMoon + "\",[У других] = \"" + fishOtherFishers + "\",[Заметки] = \"" + fishNotes + "\"" + ",[Координаты] = \"" + fishPlace + "\",[id2] =\"" + id2 + "\" " +
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
                            WriteErrors(ex.ToString());
                        }
                        finally
                        {
                            myOleDbConnection.Close();
                        }
                    }
                }
                if (countOfLines >= previousCountOfLines)
                {
                    for (int i = 0; i <= countOfLines; i++)
                    {
                        ComboBox fish_ = (ComboBox)mainFishGroupBox.Controls["fish_" + i];
                        ComboBox snasti_ = (ComboBox)mainFishGroupBox.Controls["snasti_" + i];
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
                            string fishId = Form1.FishnameID[fish_.SelectedIndex];
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
                                myOleDbCommand.CommandText = "INSERT INTO fishing ([Дата],[Рыба],[Количество],[Длина],[Вес],[Остальные],[Общий вес],[Снасть],[Водоем],[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],[Заметки],[id2],[Координаты]) "
                                                                + "VALUES (\"" + dateFishing + "\"," + fishId + "," + fishNumber + "," + fishLenght + "," + fishWeight + ",\"" + otherFish + "\"," + fishTotalWeight + "," + fishSnasti + "," + fishVodoem + "," + fishRegion
                                                                + ",\"" + fishTime + "\",\"" + fishRain + "\",\"" + fishWind + "\",\"" + fishDirection + "\"," + fishTemperature + "," + fishPressure + ",\"" + fishMoon + "\",\"" + fishOtherFishers + "\",\"" + fishNotes + "\",\"" + id2 + "\",\"" + fishPlace + "\")";
                            }
                            else
                            {
                                if (i <= previousCountOfLines)
                                {
                                    myOleDbCommand.CommandText = "UPDATE fishing " +
                                    "SET [Дата] = \"" + dateFishing + "\",[Рыба] = " + fishId + ",[Количество] = " + fishNumber + ",[Длина] = " + fishLenght + ",[Вес] = " + fishWeight + ",[Остальные] = \"" + otherFish + "\",[Общий вес] = "
                                    + fishTotalWeight + ",[Снасть] = " + fishSnasti + ",[Водоем] = " + fishVodoem + ",[Регион] = " + fishRegion + ",[Время дня] = \"" + fishTime + "\",[Осадки] = \"" + fishRain + "\",[Ветер] = \"" + fishWind + "\",[Направление] = \""
                                    + fishDirection + "\",[Температура] = " + fishTemperature + ",[Давление] = " + fishPressure + ",[Луна] = \"" + fishMoon + "\",[У других] = \"" + fishOtherFishers + "\",[Заметки] = \"" + fishNotes + "\"" + ",[Координаты] = \"" + fishPlace + "\",[id2] =\"" + id2 + "\" " +
                                    "WHERE id2 = \"" + id_ + i + "\"";
                                }
                                else
                                {
                                    myOleDbCommand.CommandText = "INSERT INTO fishing ([Дата],[Рыба],[Количество],[Длина],[Вес],[Остальные],[Общий вес],[Снасть],[Водоем],[Регион],[Время дня],[Осадки],[Ветер],[Направление],[Температура],[Давление],[Луна],[У других],[Заметки],[id2],[Координаты]) "
                                + "VALUES (\"" + dateFishing + "\"," + fishId + "," + fishNumber + "," + fishLenght + "," + fishWeight + ",\"" + otherFish + "\"," + fishTotalWeight + "," + fishSnasti + "," + fishVodoem + "," + fishRegion
                                + ",\"" + fishTime + "\",\"" + fishRain + "\",\"" + fishWind + "\",\"" + fishDirection + "\"," + fishTemperature + "," + fishPressure + ",\"" + fishMoon + "\",\"" + fishOtherFishers + "\",\"" + fishNotes + "\",\"" + id2 + "\",\"" + fishPlace + "\")";
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
                                WriteErrors(ex.ToString());
                            }
                            finally
                            {
                                myOleDbConnection.Close();
                            }
                        }
                    }
                }
                FormClose(close);
            }
        }

        private void FormClose(bool close1)
        {
            if (close1) this.Close();
        }


        private void AddControls_Click(object sender, EventArgs e)
        {
            AddControlsFunc();
        }

        private void DeleteControls_Click(object sender, EventArgs e)
        {
            DeleteControlsFunc();
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

        private void FotoButton_Click(object sender, EventArgs e)
        {
            if (date.Text != "")
            {
                string[] dateSplited;
                dateSplited = date.Text.Split('.');
                string etad = dateSplited[2] + dateSplited[1] + dateSplited[0];
                fotoList.Text = etad;
                fotoList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите дату", "Ошибка ввода");
            }
        }
    }
}
