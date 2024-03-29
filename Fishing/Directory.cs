﻿using System;
using System.IO;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;
using System.Threading;

namespace Fishing
{
    public partial class Directory : Form
    {
        private readonly OleDbConnection myOleDbConnection;
        private readonly OleDbCommand myOleDbCommand;
        private string queryDirectory = "";
        private string addToQuery = "";
        private string dataBase = "";
        private const string ERRPATH = "errors.txt";
        private readonly string editValue = "";
        private readonly string dbPath = AppDomain.CurrentDomain.BaseDirectory + "fishing.mdb";

        public Directory()
        {
            InitializeComponent();
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=" + dbPath;
            myOleDbConnection = new OleDbConnection(connectionString);
            myOleDbCommand = myOleDbConnection.CreateCommand();
            dataGridFishes.CellEndEdit += new DataGridViewCellEventHandler(DataGridFishes_CellEndEdit);
            dataGridFishes.CellMouseUp += new DataGridViewCellMouseEventHandler(DataGridFishes_CellMouseUp);
            delToolStripMenuItem.Click += new EventHandler(DelToolStripMenuItem_Click);
            this.Shown += new EventHandler(Directory_Shown);
            this.KeyDown += new KeyEventHandler(Directory_KeyDown);
        }

        private void Directory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void DataGridFishes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            TextInfo ti = Thread.CurrentThread.CurrentCulture.TextInfo;
            int id;
            string currentValue = "";
            string currentID = "";
            try
            {
                if (dataGridFishes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    currentID = dataGridFishes.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                    if (this.Text == "Справочник мест ловли")
                    {
                        currentValue = dataGridFishes.CurrentCell.Value.ToString();
                    }
                    else
                    {
                        currentValue = ti.ToTitleCase(dataGridFishes.CurrentCell.Value.ToString());
                    }
                }
                if (currentID == "")
                {
                    if (currentValue != "")
                    {
                        queryDirectory = "INSERT INTO " + dataBase + " ([name]) VALUES (\"" + currentValue + "\")";
                        OpenConnection();
                    }
                }
                else
                {
                    if (currentValue == "")
                    {
                        dataGridFishes.CurrentCell.Value = editValue;
                        MessageBox.Show("Для удаления нажмите правой кнопкой мыши");
                    }
                    else
                    {
                        queryDirectory = "UPDATE " + dataBase + " SET name = \"" + currentValue + "\" WHERE id = " + currentID;
                        OpenConnection();
                        for (int n = 0; n < dataGridFishes.RowCount - 1; n++)
                        {
                            if (dataGridFishes[1, n].Value.ToString() == currentValue) id = n;
                        }
                    }
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
                Renew();
            }
        }

        private void Directory_Shown(object sender, EventArgs e)
        {
            switch (this.Text)
            {
                case "Справочник спосбов ловли":
                    dataBase = "snasti";
                    addToQuery = "Снасть";
                    break;
                case "Справочник видов рыб":
                    dataBase = "fishnames";
                    addToQuery = "Рыба";
                    break;
                case "Справочник рыболовных баз":
                    dataBase = "fishbase";
                    addToQuery = "База";
                    break;
                case "Справочник мест ловли":
                    dataBase = "places";
                    addToQuery = "Водоем";
                    break;
            }
            Renew();
        }

        private void Renew()
        {
            try
            {
                queryDirectory = "SELECT id,name AS Название FROM " + dataBase + " ORDER BY name";
                myOleDbCommand.CommandText = queryDirectory;
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                dataGridFishes.DataSource = ds.Tables[0];
                dataGridFishes.Columns["id"].Visible = false;
                for (int i = 0; i < dataGridFishes.Rows.Count - 1; i++)
                {
                    dataGridFishes.Rows[i].HeaderCell.Value = (i + 1).ToString();
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
        }

        private void DataGridFishes_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1) && (e.Button == MouseButtons.Right))
            {
                dataGridFishes.CurrentCell = dataGridFishes.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentID = dataGridFishes[0, dataGridFishes.CurrentRow.Index].Value.ToString();
            int countofIds = CheckIDinFishing(currentID);
            if (countofIds > 0)
            {
                MessageBox.Show("Данная запись имеется в " + countofIds + " отчетах о рыбалке.\nУдалите отчеты, содержащие эту запись");
            }
            else
            {
                queryDirectory = "DELETE FROM " + dataBase + " WHERE id = " + currentID;
                OpenConnection();
            }
            dataGridFishes.CurrentCell = dataGridFishes[1, dataGridFishes.RowCount - 1];
        }

        private int CheckIDinFishing(string id)
        {
            int countOfIDs = 999999;
            try
            {
                myOleDbCommand.CommandText = "SELECT COUNT (id2) FROM fishing WHERE " + addToQuery + " = " + id;
                myOleDbConnection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(myOleDbCommand);
                adapter.Fill(ds);
                countOfIDs = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
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
            return countOfIDs;
        }

        private void OpenConnection()
        {
            try
            {
                myOleDbCommand.CommandText = queryDirectory;
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
                Renew();
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
    }
}
