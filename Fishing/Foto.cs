using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace Fishing
{
    public partial class Foto : Form
    {
        private DirectoryInfo di;
        private FileInfo[] file;
        private int imageIndex = 0;
        private string dirPath = "";
        private int currentX1 = 0;
        private int currentX2 = 0;

        public Foto()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Foto_Shown);
            this.KeyDown += new KeyEventHandler(Foto_KeyDown);
            this.FormClosed += new FormClosedEventHandler(Foto_FormClosed);
            pictureBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
            pictureBox.MouseUp += new MouseEventHandler(PictureBox_MouseUp);
            openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(OpenFileDialog1_FileOk);
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentX2 = e.X;
                if (currentX1 - currentX2 > 40)
                {
                    LeftImageShow();
                }
                if (currentX1 - currentX2 < 40)
                {
                    RightImageShow();
                }
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            currentX1 = e.X;
        }

        private void Foto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (file == null && di.Exists) di.Delete();
            file = null;
            pictureBox.Image = null;
        }

        private void OpenFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
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
            FotoShow(0);
        }

        void Foto_Shown(object sender, EventArgs e)
        {
            FotoShow(0);
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


        private void FotoShow(int imNumber)
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
                        imageIndex = -1;
                        RightImageShow();
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Нет фотографий";
                    }
                }
                else
                {
                    LeftImageShow();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Нет фотографий";
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
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
                    RightImageShow();
                    break;
                case Keys.PageUp:
                    LeftImageShow();
                    break;
                case Keys.Delete:
                    DeleteImage();
                    break;
            }
        }

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteImage();
        }

        private void NextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RightImageShow();
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            RightImageShow();
        }

        private void PreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LeftImageShow();
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            LeftImageShow();
        }

        private void LeftImageShow()
        {
            if (file != null)
            {
                if (imageIndex > 0)
                {
                    imageIndex--;
                }
                else
                {
                    imageIndex = file.Length - 1;
                }
                pictureBox.Image = Image.FromFile(file[imageIndex].FullName);
                toolStripStatusLabel1.Text = file[imageIndex].FullName;
            }
        }

        private void RightImageShow()
        {
            if (file != null)
            {
                if (imageIndex < file.Length - 1)
                {
                    imageIndex++;
                }
                else
                {
                    imageIndex = 0;
                }
                pictureBox.Image = Image.FromFile(file[imageIndex].FullName);
                toolStripStatusLabel1.Text = file[imageIndex].FullName;
            }
        }

        private void DeleteImage()
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
            FotoShow(imageIndex);
        }
    }
}
