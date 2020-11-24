namespace Fishing
{
    partial class NewReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.date = new System.Windows.Forms.TextBox();
            this.saveReportButton = new System.Windows.Forms.Button();
            this.dateLabel = new System.Windows.Forms.Label();
            this.addControls = new System.Windows.Forms.Button();
            this.deleteControls = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.place_0 = new System.Windows.Forms.TextBox();
            this.other = new System.Windows.Forms.TextBox();
            this.number = new System.Windows.Forms.MaskedTextBox();
            this.weight = new System.Windows.Forms.MaskedTextBox();
            this.lenght = new System.Windows.Forms.MaskedTextBox();
            this.temperature = new System.Windows.Forms.TextBox();
            this.totalWeight = new System.Windows.Forms.MaskedTextBox();
            this.totalweightLabel = new System.Windows.Forms.Label();
            this.regionLabel = new System.Windows.Forms.Label();
            this.fishBaseLabel = new System.Windows.Forms.Label();
            this.region = new System.Windows.Forms.ComboBox();
            this.fishBase = new System.Windows.Forms.ComboBox();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.snasti_0 = new System.Windows.Forms.ComboBox();
            this.placeLabel = new System.Windows.Forms.Label();
            this.snastiLabel = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.lenghtLabel = new System.Windows.Forms.Label();
            this.numberLabel = new System.Windows.Forms.Label();
            this.fishLabel = new System.Windows.Forms.Label();
            this.fish_0 = new System.Windows.Forms.ComboBox();
            this.notesLabel = new System.Windows.Forms.Label();
            this.timeofDay = new System.Windows.Forms.ComboBox();
            this.timeofDayLabel = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.wind = new System.Windows.Forms.ComboBox();
            this.windLabel = new System.Windows.Forms.Label();
            this.moon = new System.Windows.Forms.ComboBox();
            this.mainFishGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pressureLabel = new System.Windows.Forms.Label();
            this.pressure = new System.Windows.Forms.MaskedTextBox();
            this.rain = new System.Windows.Forms.ComboBox();
            this.rainLabel = new System.Windows.Forms.Label();
            this.otherFishers = new System.Windows.Forms.ComboBox();
            this.direction = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.moonLabel = new System.Windows.Forms.Label();
            this.vodoem = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fotoButton = new System.Windows.Forms.Button();
            this.endDate = new System.Windows.Forms.TextBox();
            this.endFishingDate = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mainFishGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // date
            // 
            this.date.BackColor = System.Drawing.SystemColors.Window;
            this.date.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.date.ForeColor = System.Drawing.SystemColors.WindowText;
            this.date.Location = new System.Drawing.Point(6, 19);
            this.date.Margin = new System.Windows.Forms.Padding(2);
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Size = new System.Drawing.Size(152, 19);
            this.date.TabIndex = 1;
            // 
            // saveReportButton
            // 
            this.saveReportButton.Location = new System.Drawing.Point(671, 414);
            this.saveReportButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveReportButton.Name = "saveReportButton";
            this.saveReportButton.Size = new System.Drawing.Size(99, 28);
            this.saveReportButton.TabIndex = 999;
            this.saveReportButton.Text = "Сохранить";
            this.saveReportButton.UseVisualStyleBackColor = true;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.dateLabel.Location = new System.Drawing.Point(41, 4);
            this.dateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(84, 13);
            this.dateLabel.TabIndex = 7;
            this.dateLabel.Text = "Дата рыбалки*";
            // 
            // addControls
            // 
            this.addControls.Location = new System.Drawing.Point(778, 38);
            this.addControls.Margin = new System.Windows.Forms.Padding(2);
            this.addControls.Name = "addControls";
            this.addControls.Size = new System.Drawing.Size(20, 20);
            this.addControls.TabIndex = 1000;
            this.addControls.TabStop = false;
            this.addControls.Text = "+";
            this.addControls.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.addControls, "Добавить строку (Ctrl + +)");
            this.addControls.UseVisualStyleBackColor = true;
            this.addControls.Click += new System.EventHandler(this.addControls_Click);
            // 
            // deleteControls
            // 
            this.deleteControls.AccessibleDescription = "";
            this.deleteControls.Location = new System.Drawing.Point(778, 16);
            this.deleteControls.Margin = new System.Windows.Forms.Padding(2);
            this.deleteControls.Name = "deleteControls";
            this.deleteControls.Size = new System.Drawing.Size(20, 20);
            this.deleteControls.TabIndex = 1001;
            this.deleteControls.TabStop = false;
            this.deleteControls.Tag = "";
            this.deleteControls.Text = "-";
            this.toolTip.SetToolTip(this.deleteControls, "Удалить строку (Ctrl + -)");
            this.deleteControls.UseVisualStyleBackColor = true;
            this.deleteControls.Visible = false;
            this.deleteControls.Click += new System.EventHandler(this.deleteControls_Click);
            // 
            // place_0
            // 
            this.place_0.AllowDrop = true;
            this.place_0.BackColor = System.Drawing.SystemColors.Window;
            this.place_0.Location = new System.Drawing.Point(507, 28);
            this.place_0.Margin = new System.Windows.Forms.Padding(2);
            this.place_0.Name = "place_0";
            this.place_0.Size = new System.Drawing.Size(93, 20);
            this.place_0.TabIndex = 26;
            this.toolTip.SetToolTip(this.place_0, "Двойной клик для открытия Google Maps");
            // 
            // other
            // 
            this.other.Location = new System.Drawing.Point(315, 28);
            this.other.Margin = new System.Windows.Forms.Padding(2);
            this.other.Name = "other";
            this.other.Size = new System.Drawing.Size(76, 20);
            this.other.TabIndex = 23;
            this.toolTip.SetToolTip(this.other, "Длины рыб, которые чаще всего попадались, через запятую");
            // 
            // number
            // 
            this.number.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.number.HidePromptOnLeave = true;
            this.number.HideSelection = false;
            this.number.Location = new System.Drawing.Point(247, 28);
            this.number.Margin = new System.Windows.Forms.Padding(2);
            this.number.Mask = "000";
            this.number.Name = "number";
            this.number.PromptChar = ' ';
            this.number.Size = new System.Drawing.Size(63, 20);
            this.number.SkipLiterals = false;
            this.number.TabIndex = 22;
            this.number.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.toolTip.SetToolTip(this.number, "Количество выловленной рыбы данного вида");
            // 
            // weight
            // 
            this.weight.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.weight.HidePromptOnLeave = true;
            this.weight.Location = new System.Drawing.Point(449, 28);
            this.weight.Margin = new System.Windows.Forms.Padding(2);
            this.weight.Mask = "00000";
            this.weight.Name = "weight";
            this.weight.PromptChar = ' ';
            this.weight.Size = new System.Drawing.Size(54, 20);
            this.weight.SkipLiterals = false;
            this.weight.TabIndex = 25;
            this.weight.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.toolTip.SetToolTip(this.weight, "Вес самой большой рыбы данного вида");
            // 
            // lenght
            // 
            this.lenght.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.lenght.HidePromptOnLeave = true;
            this.lenght.Location = new System.Drawing.Point(395, 28);
            this.lenght.Margin = new System.Windows.Forms.Padding(2);
            this.lenght.Mask = "000";
            this.lenght.Name = "lenght";
            this.lenght.PromptChar = ' ';
            this.lenght.Size = new System.Drawing.Size(50, 20);
            this.lenght.SkipLiterals = false;
            this.lenght.TabIndex = 24;
            this.lenght.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.toolTip.SetToolTip(this.lenght, "Длина самой большой рыбы данного вида");
            // 
            // temperature
            // 
            this.temperature.Location = new System.Drawing.Point(6, 318);
            this.temperature.Margin = new System.Windows.Forms.Padding(2);
            this.temperature.Name = "temperature";
            this.temperature.Size = new System.Drawing.Size(72, 20);
            this.temperature.TabIndex = 11;
            // 
            // totalWeight
            // 
            this.totalWeight.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.totalWeight.HidePromptOnLeave = true;
            this.totalWeight.HideSelection = false;
            this.totalWeight.Location = new System.Drawing.Point(6, 395);
            this.totalWeight.Margin = new System.Windows.Forms.Padding(2);
            this.totalWeight.Mask = "000000";
            this.totalWeight.Name = "totalWeight";
            this.totalWeight.PromptChar = ' ';
            this.totalWeight.Size = new System.Drawing.Size(152, 20);
            this.totalWeight.SkipLiterals = false;
            this.totalWeight.TabIndex = 14;
            this.totalWeight.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // totalweightLabel
            // 
            this.totalweightLabel.AutoSize = true;
            this.totalweightLabel.Location = new System.Drawing.Point(29, 379);
            this.totalweightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalweightLabel.Name = "totalweightLabel";
            this.totalweightLabel.Size = new System.Drawing.Size(109, 13);
            this.totalweightLabel.TabIndex = 30;
            this.totalweightLabel.Text = "Общий вес улова,  г";
            // 
            // regionLabel
            // 
            this.regionLabel.AutoSize = true;
            this.regionLabel.Location = new System.Drawing.Point(60, 80);
            this.regionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(47, 13);
            this.regionLabel.TabIndex = 33;
            this.regionLabel.Text = "Регион*";
            // 
            // fishBaseLabel
            // 
            this.fishBaseLabel.AutoSize = true;
            this.fishBaseLabel.Location = new System.Drawing.Point(35, 154);
            this.fishBaseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fishBaseLabel.Name = "fishBaseLabel";
            this.fishBaseLabel.Size = new System.Drawing.Size(97, 13);
            this.fishBaseLabel.TabIndex = 34;
            this.fishBaseLabel.Text = "Рыболовная база";
            // 
            // region
            // 
            this.region.AllowDrop = true;
            this.region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region.DropDownWidth = 200;
            this.region.Location = new System.Drawing.Point(6, 95);
            this.region.Margin = new System.Windows.Forms.Padding(2);
            this.region.Name = "region";
            this.region.Size = new System.Drawing.Size(152, 21);
            this.region.TabIndex = 4;
            // 
            // fishBase
            // 
            this.fishBase.AllowDrop = true;
            this.fishBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fishBase.Location = new System.Drawing.Point(6, 169);
            this.fishBase.Margin = new System.Windows.Forms.Padding(2);
            this.fishBase.Name = "fishBase";
            this.fishBase.Size = new System.Drawing.Size(152, 21);
            this.fishBase.TabIndex = 6;
            // 
            // notesTextBox
            // 
            this.notesTextBox.Location = new System.Drawing.Point(166, 384);
            this.notesTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesTextBox.Size = new System.Drawing.Size(471, 66);
            this.notesTextBox.TabIndex = 997;
            // 
            // snasti_0
            // 
            this.snasti_0.AllowDrop = true;
            this.snasti_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.snasti_0.Location = new System.Drawing.Point(126, 28);
            this.snasti_0.Margin = new System.Windows.Forms.Padding(2);
            this.snasti_0.Name = "snasti_0";
            this.snasti_0.Size = new System.Drawing.Size(117, 21);
            this.snasti_0.TabIndex = 21;
            // 
            // placeLabel
            // 
            this.placeLabel.AutoSize = true;
            this.placeLabel.Location = new System.Drawing.Point(516, 12);
            this.placeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.placeLabel.Name = "placeLabel";
            this.placeLabel.Size = new System.Drawing.Size(70, 13);
            this.placeLabel.TabIndex = 1015;
            this.placeLabel.Text = "Google Maps";
            // 
            // snastiLabel
            // 
            this.snastiLabel.AutoSize = true;
            this.snastiLabel.Location = new System.Drawing.Point(145, 13);
            this.snastiLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.snastiLabel.Name = "snastiLabel";
            this.snastiLabel.Size = new System.Drawing.Size(81, 13);
            this.snastiLabel.TabIndex = 1014;
            this.snastiLabel.Text = "Способ ловли*";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(458, 13);
            this.weightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(37, 13);
            this.weightLabel.TabIndex = 1012;
            this.weightLabel.Text = "Вес, г";
            // 
            // lenghtLabel
            // 
            this.lenghtLabel.AutoSize = true;
            this.lenghtLabel.Location = new System.Drawing.Point(392, 12);
            this.lenghtLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lenghtLabel.Name = "lenghtLabel";
            this.lenghtLabel.Size = new System.Drawing.Size(60, 13);
            this.lenghtLabel.TabIndex = 1011;
            this.lenghtLabel.Text = "Длина, см";
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Location = new System.Drawing.Point(245, 13);
            this.numberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(70, 13);
            this.numberLabel.TabIndex = 1010;
            this.numberLabel.Text = "Количество*";
            // 
            // fishLabel
            // 
            this.fishLabel.AutoSize = true;
            this.fishLabel.Location = new System.Drawing.Point(20, 13);
            this.fishLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fishLabel.Name = "fishLabel";
            this.fishLabel.Size = new System.Drawing.Size(92, 13);
            this.fishLabel.TabIndex = 1008;
            this.fishLabel.Text = "Название рыбы*";
            // 
            // fish_0
            // 
            this.fish_0.AllowDrop = true;
            this.fish_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fish_0.Location = new System.Drawing.Point(5, 28);
            this.fish_0.Margin = new System.Windows.Forms.Padding(2);
            this.fish_0.Name = "fish_0";
            this.fish_0.Size = new System.Drawing.Size(117, 21);
            this.fish_0.TabIndex = 20;
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(256, 368);
            this.notesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(302, 13);
            this.notesLabel.TabIndex = 1016;
            this.notesLabel.Text = "Прикорм, насадки, приманки и другие заметки рыболова";
            // 
            // timeofDay
            // 
            this.timeofDay.AllowDrop = true;
            this.timeofDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeofDay.Items.AddRange(new object[] {
            "Утро",
            "День",
            "Вечер",
            "Ночь"});
            this.timeofDay.Location = new System.Drawing.Point(6, 206);
            this.timeofDay.Margin = new System.Windows.Forms.Padding(2);
            this.timeofDay.Name = "timeofDay";
            this.timeofDay.Size = new System.Drawing.Size(152, 21);
            this.timeofDay.TabIndex = 7;
            // 
            // timeofDayLabel
            // 
            this.timeofDayLabel.AutoSize = true;
            this.timeofDayLabel.Location = new System.Drawing.Point(52, 190);
            this.timeofDayLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeofDayLabel.Name = "timeofDayLabel";
            this.timeofDayLabel.Size = new System.Drawing.Size(65, 13);
            this.timeofDayLabel.TabIndex = 1018;
            this.timeofDayLabel.Text = "Время дня*";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(5, 303);
            this.temperatureLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(74, 13);
            this.temperatureLabel.TabIndex = 1020;
            this.temperatureLabel.Text = "Температура";
            // 
            // wind
            // 
            this.wind.AllowDrop = true;
            this.wind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wind.Items.AddRange(new object[] {
            "",
            "Штиль",
            "1-3 м/с",
            "3-5 м/с",
            "5-7 м/с",
            "7-10 м/с",
            "Ураган"});
            this.wind.Location = new System.Drawing.Point(6, 279);
            this.wind.Margin = new System.Windows.Forms.Padding(2);
            this.wind.Name = "wind";
            this.wind.Size = new System.Drawing.Size(72, 21);
            this.wind.TabIndex = 9;
            // 
            // windLabel
            // 
            this.windLabel.AutoSize = true;
            this.windLabel.Location = new System.Drawing.Point(22, 264);
            this.windLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.windLabel.Name = "windLabel";
            this.windLabel.Size = new System.Drawing.Size(37, 13);
            this.windLabel.TabIndex = 1022;
            this.windLabel.Text = "Ветер";
            // 
            // moon
            // 
            this.moon.AllowDrop = true;
            this.moon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moon.Items.AddRange(new object[] {
            "",
            "Новолуние",
            "Первая четверть",
            "Полнолуние",
            "Последняя четверть"});
            this.moon.Location = new System.Drawing.Point(6, 356);
            this.moon.Margin = new System.Windows.Forms.Padding(2);
            this.moon.Name = "moon";
            this.moon.Size = new System.Drawing.Size(152, 21);
            this.moon.TabIndex = 13;
            // 
            // mainFishGroupBox
            // 
            this.mainFishGroupBox.Controls.Add(this.label6);
            this.mainFishGroupBox.Controls.Add(this.label5);
            this.mainFishGroupBox.Controls.Add(this.place_0);
            this.mainFishGroupBox.Controls.Add(this.label1);
            this.mainFishGroupBox.Controls.Add(this.placeLabel);
            this.mainFishGroupBox.Controls.Add(this.other);
            this.mainFishGroupBox.Controls.Add(this.lenght);
            this.mainFishGroupBox.Controls.Add(this.number);
            this.mainFishGroupBox.Controls.Add(this.lenghtLabel);
            this.mainFishGroupBox.Controls.Add(this.numberLabel);
            this.mainFishGroupBox.Controls.Add(this.fish_0);
            this.mainFishGroupBox.Controls.Add(this.weight);
            this.mainFishGroupBox.Controls.Add(this.fishLabel);
            this.mainFishGroupBox.Controls.Add(this.weightLabel);
            this.mainFishGroupBox.Controls.Add(this.snasti_0);
            this.mainFishGroupBox.Controls.Add(this.snastiLabel);
            this.mainFishGroupBox.Location = new System.Drawing.Point(166, 10);
            this.mainFishGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.mainFishGroupBox.Name = "mainFishGroupBox";
            this.mainFishGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.mainFishGroupBox.Size = new System.Drawing.Size(608, 55);
            this.mainFishGroupBox.TabIndex = 17;
            this.mainFishGroupBox.TabStop = false;
            this.mainFishGroupBox.Text = "Пойманные рыбы";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(327, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1011;
            this.label1.Text = "Длина, см";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 415);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1031;
            this.label2.Text = "У других рыбаков";
            // 
            // pressureLabel
            // 
            this.pressureLabel.AutoSize = true;
            this.pressureLabel.Location = new System.Drawing.Point(94, 303);
            this.pressureLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pressureLabel.Name = "pressureLabel";
            this.pressureLabel.Size = new System.Drawing.Size(58, 13);
            this.pressureLabel.TabIndex = 1034;
            this.pressureLabel.Text = "Давление";
            // 
            // pressure
            // 
            this.pressure.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.pressure.HidePromptOnLeave = true;
            this.pressure.Location = new System.Drawing.Point(86, 318);
            this.pressure.Margin = new System.Windows.Forms.Padding(2);
            this.pressure.Mask = "000";
            this.pressure.Name = "pressure";
            this.pressure.PromptChar = ' ';
            this.pressure.Size = new System.Drawing.Size(72, 20);
            this.pressure.SkipLiterals = false;
            this.pressure.TabIndex = 12;
            this.pressure.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // rain
            // 
            this.rain.AllowDrop = true;
            this.rain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rain.Items.AddRange(new object[] {
            "Ясно",
            "Слабая облачность",
            "Средняя облачность",
            "Сильная облачность",
            "Туман",
            "Слабый дождь",
            "Средний дождь",
            "Сильный дождь",
            "Гроза",
            "Слабый снегопад",
            "Средний снегопад",
            "Сильный снегопад",
            "Метель"});
            this.rain.Location = new System.Drawing.Point(6, 242);
            this.rain.Margin = new System.Windows.Forms.Padding(2);
            this.rain.Name = "rain";
            this.rain.Size = new System.Drawing.Size(152, 21);
            this.rain.TabIndex = 8;
            // 
            // rainLabel
            // 
            this.rainLabel.AutoSize = true;
            this.rainLabel.Location = new System.Drawing.Point(61, 227);
            this.rainLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rainLabel.Name = "rainLabel";
            this.rainLabel.Size = new System.Drawing.Size(48, 13);
            this.rainLabel.TabIndex = 1037;
            this.rainLabel.Text = "Погода*";
            // 
            // otherFishers
            // 
            this.otherFishers.AllowDrop = true;
            this.otherFishers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.otherFishers.Items.AddRange(new object[] {
            "",
            "Полный ноль",
            "Лучше, чем у меня",
            "Как у меня",
            "Хуже, чем у меня",
            "Рыбачил один"});
            this.otherFishers.Location = new System.Drawing.Point(6, 430);
            this.otherFishers.Margin = new System.Windows.Forms.Padding(2);
            this.otherFishers.Name = "otherFishers";
            this.otherFishers.Size = new System.Drawing.Size(152, 21);
            this.otherFishers.TabIndex = 15;
            // 
            // direction
            // 
            this.direction.AllowDrop = true;
            this.direction.DropDownHeight = 140;
            this.direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.direction.IntegralHeight = false;
            this.direction.Items.AddRange(new object[] {
            "",
            "С",
            "Ю",
            "В",
            "З",
            "С-В",
            "С-З",
            "Ю-В",
            "Ю-З"});
            this.direction.Location = new System.Drawing.Point(86, 279);
            this.direction.Margin = new System.Windows.Forms.Padding(2);
            this.direction.Name = "direction";
            this.direction.Size = new System.Drawing.Size(72, 21);
            this.direction.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 264);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 1040;
            this.label3.Text = "Направление";
            // 
            // moonLabel
            // 
            this.moonLabel.AutoSize = true;
            this.moonLabel.Location = new System.Drawing.Point(67, 340);
            this.moonLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.moonLabel.Name = "moonLabel";
            this.moonLabel.Size = new System.Drawing.Size(32, 13);
            this.moonLabel.TabIndex = 1041;
            this.moonLabel.Text = "Луна";
            // 
            // vodoem
            // 
            this.vodoem.AllowDrop = true;
            this.vodoem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vodoem.DropDownWidth = 152;
            this.vodoem.Location = new System.Drawing.Point(6, 132);
            this.vodoem.Margin = new System.Windows.Forms.Padding(2);
            this.vodoem.Name = "vodoem";
            this.vodoem.Size = new System.Drawing.Size(152, 21);
            this.vodoem.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 116);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 1043;
            this.label4.Text = "Место ловли, водоем*";
            // 
            // fotoButton
            // 
            this.fotoButton.Location = new System.Drawing.Point(671, 384);
            this.fotoButton.Margin = new System.Windows.Forms.Padding(2);
            this.fotoButton.Name = "fotoButton";
            this.fotoButton.Size = new System.Drawing.Size(99, 28);
            this.fotoButton.TabIndex = 2000;
            this.fotoButton.Text = "Фотографии";
            this.fotoButton.UseVisualStyleBackColor = true;
            this.fotoButton.Click += new System.EventHandler(this.fotoButton_Click);
            // 
            // endDate
            // 
            this.endDate.BackColor = System.Drawing.SystemColors.Control;
            this.endDate.Enabled = false;
            this.endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.endDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.endDate.Location = new System.Drawing.Point(6, 60);
            this.endDate.Margin = new System.Windows.Forms.Padding(2);
            this.endDate.Name = "endDate";
            this.endDate.ReadOnly = true;
            this.endDate.Size = new System.Drawing.Size(152, 19);
            this.endDate.TabIndex = 3;
            // 
            // endFishingDate
            // 
            this.endFishingDate.AutoSize = true;
            this.endFishingDate.Location = new System.Drawing.Point(32, 41);
            this.endFishingDate.Margin = new System.Windows.Forms.Padding(2);
            this.endFishingDate.Name = "endFishingDate";
            this.endFishingDate.Size = new System.Drawing.Size(104, 17);
            this.endFishingDate.TabIndex = 2;
            this.endFishingDate.Text = "Конец рыбалки";
            this.endFishingDate.UseVisualStyleBackColor = true;
            this.endFishingDate.CheckedChanged += new System.EventHandler(this.endFishingDate_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(504, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 1044;
            this.label5.Text = "Координаты";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(392, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 1044;
            this.label6.Text = "Самая большая";
            // 
            // NewReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 454);
            this.Controls.Add(this.endFishingDate);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.fotoButton);
            this.Controls.Add(this.temperature);
            this.Controls.Add(this.vodoem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.moonLabel);
            this.Controls.Add(this.direction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.otherFishers);
            this.Controls.Add(this.rain);
            this.Controls.Add(this.rainLabel);
            this.Controls.Add(this.pressure);
            this.Controls.Add(this.pressureLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mainFishGroupBox);
            this.Controls.Add(this.moon);
            this.Controls.Add(this.wind);
            this.Controls.Add(this.windLabel);
            this.Controls.Add(this.temperatureLabel);
            this.Controls.Add(this.timeofDay);
            this.Controls.Add(this.timeofDayLabel);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.fishBase);
            this.Controls.Add(this.region);
            this.Controls.Add(this.fishBaseLabel);
            this.Controls.Add(this.regionLabel);
            this.Controls.Add(this.totalweightLabel);
            this.Controls.Add(this.totalWeight);
            this.Controls.Add(this.deleteControls);
            this.Controls.Add(this.addControls);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.saveReportButton);
            this.Controls.Add(this.date);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.mainFishGroupBox.ResumeLayout(false);
            this.mainFishGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox date;
        private System.Windows.Forms.Button saveReportButton;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Button addControls;
        private System.Windows.Forms.Button deleteControls;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MaskedTextBox totalWeight;
        private System.Windows.Forms.Label totalweightLabel;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.Label fishBaseLabel;
        private System.Windows.Forms.ComboBox region;
        private System.Windows.Forms.ComboBox fishBase;
        private System.Windows.Forms.TextBox notesTextBox;
        private System.Windows.Forms.TextBox place_0;
        private System.Windows.Forms.ComboBox snasti_0;
        private System.Windows.Forms.Label placeLabel;
        private System.Windows.Forms.Label snastiLabel;
        private System.Windows.Forms.TextBox other;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.MaskedTextBox weight;
        private System.Windows.Forms.MaskedTextBox lenght;
        private System.Windows.Forms.MaskedTextBox number;
        private System.Windows.Forms.Label lenghtLabel;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Label fishLabel;
        private System.Windows.Forms.ComboBox fish_0;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.ComboBox timeofDay;
        private System.Windows.Forms.Label timeofDayLabel;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.ComboBox wind;
        private System.Windows.Forms.Label windLabel;
        private System.Windows.Forms.ComboBox moon;
        private System.Windows.Forms.GroupBox mainFishGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pressureLabel;
        private System.Windows.Forms.MaskedTextBox pressure;
        private System.Windows.Forms.ComboBox rain;
        private System.Windows.Forms.Label rainLabel;
        private System.Windows.Forms.ComboBox otherFishers;
        private System.Windows.Forms.ComboBox direction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label moonLabel;
        private System.Windows.Forms.ComboBox vodoem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox temperature;
        private System.Windows.Forms.Button fotoButton;
        private System.Windows.Forms.TextBox endDate;
        private System.Windows.Forms.CheckBox endFishingDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}