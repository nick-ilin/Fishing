using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace Fishing
{
    public partial class Foto : Form
    {
        DirectoryInfo di;
        FileInfo[] file;
        int n = 0;
        string dirPath = "";
        int currentX1 = 0;
        int currentX2 = 0;

        public Foto()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Foto_Shown);
            this.KeyDown += new KeyEventHandler(Foto_KeyDown);
            this.FormClosed += new FormClosedEventHandler(Foto_FormClosed);
            pictureBox.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
            pictureBox.MouseUp += new MouseEventHandler(pictureBox_MouseUp);
            openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
        }

        void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                currentX2 = e.X;
                if (currentX1 - currentX2 > 40)
                {
                    leftImageShow();
                }
                if (currentX1 - currentX2 < 40)
                {
                    rightImageShow();
                }
            }
        }

        void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            currentX1 = e.X;
        }

        void Foto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (file == null && di.Exists) di.Delete();
            file = null;
            pictureBox.Image = null;
        }

        void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!di.Exists) di.Create();
            for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileNames[i]);
                try
                {
                    fi.CopyTo(dirPath + "\\" + fi.Name, true);
                }
                catch
                {
                    MessageBox.Show("Файл " + fi.Name + " уже есть в папке и открыт приложением.");
                }
            }
            fotoShow(0);
        }

        void Foto_Shown(object sender, EventArgs e)
        {
            fotoShow(0);
        }

        public FileInfo[] GetFiles(DirectoryInfo dir, string searchPatterns)
        {
            ArrayList files = new ArrayList();
            string[] patterns = searchPatterns.Split(',');
            foreach (string pattern in patterns)
            {
                if (pattern.Length != 0)
                {
                    files.AddRange(dir.GetFiles(pattern));
                }
            }
            return (FileInfo[])files.ToArray(typeof(FileInfo));
        }


        private void fotoShow(int imNumber)
        {
            dirPath = AppDomain.CurrentDomain.BaseDirectory + "\\foto\\" + this.Text;
            di = new DirectoryInfo(dirPath);
            if (di.Exists)
            {
                file = GetFiles(di, "*.jpg,*.jpeg,*.gif,*.bmp,*.tiff,*.tif");
                if (imNumber == 0)
                {
                    if (file.Length > 0)
                    {
                        n = -1;
                        rightImageShow();
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Нет фотографий";
                    }
                }
                else
                {
                    leftImageShow();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Нет фотографий";
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        void Foto_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.PageDown:
                    rightImageShow();
                    break;
                case Keys.PageUp:
                    leftImageShow();
                    break;
                case Keys.Delete:
                    deleteImage();
                    break;
            }
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteImage();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightImageShow();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            rightImageShow();
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftImageShow();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            leftImageShow();
        }

        private void leftImageShow()
        {
            if (file != null)
            {
                if (n > 0)
                {
                    n--;
                }
                else
                {
                    n = file.Length - 1;
                }
                pictureBox.Image = Image.FromFile(file[n].FullName);
                toolStripStatusLabel1.Text = file[n].FullName;
            }
        }

        private void rightImageShow()
        {
            if (file != null)
            {
                if (n < file.Length - 1)
                {
                    n++;
                }
                else
                {
                    n = 0;
                }
                pictureBox.Image = Image.FromFile(file[n].FullName);
                toolStripStatusLabel1.Text = file[n].FullName;
            }
        }

        private void deleteImage()
        {
            pictureBox.Image.Dispose();
            pictureBox.Image = null;
            try
            {
                File.Delete(toolStripStatusLabel1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            fotoShow(n);
        }
    }
}
