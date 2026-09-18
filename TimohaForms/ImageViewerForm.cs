using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TimohaForms
{
    public class ImageViewerForm : Form1
    {
        private PictureBox picture;
        private Button openButton;
        private Button colorButton;
        private Button addButton;
        private Button slideButton;

        private Timer timer;

        private string[] images;
        private int currentImage = 0;

        public ImageViewerForm()
        {
            Text = "Piltide vaatamine";
            Size = new Size(900, 650);
            StartPosition = FormStartPosition.CenterScreen;

            CreateControls();

            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += Timer_Tick;
        }

        private void CreateControls()
        {
            picture = new PictureBox();
            picture.Location = new Point(20, 20);
            picture.Size = new Size(840, 480);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.BackColor = Color.LightGray;

            openButton = new Button();
            openButton.Text = "Ava";
            openButton.Location = new Point(20, 530);
            openButton.Size = new Size(120, 40);
            openButton.Click += OpenButton_Click;

            colorButton = new Button();
            colorButton.Text = "Taustavärv";
            colorButton.Location = new Point(160, 530);
            colorButton.Size = new Size(120, 40);
            colorButton.Click += ColorButton_Click;

            addButton = new Button();
            addButton.Text = "Lisa";
            addButton.Location = new Point(300, 530);
            addButton.Size = new Size(120, 40);
            addButton.Click += AddButton_Click;

            slideButton = new Button();
            slideButton.Text = "Slaidiesitlus";
            slideButton.Location = new Point(440, 530);
            slideButton.Size = new Size(120, 40);
            slideButton.Click += SlideButton_Click;

            Controls.Add(picture);
            Controls.Add(openButton);
            Controls.Add(colorButton);
            Controls.Add(addButton);
            Controls.Add(slideButton);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picture.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picture.BackColor = dialog.Color;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (picture.Image == null)
            {
                MessageBox.Show("Kõigepealt ava pilt..");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Png;

                if (dialog.FilterIndex == 2)
                    format = ImageFormat.Jpeg;

                if (dialog.FilterIndex == 3)
                    format = ImageFormat.Bmp;

                picture.Image.Save(dialog.FileName, format);

                MessageBox.Show("Pilt salvestatud.");
            }
        }

        private void SlideButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                images = dialog.FileNames;

                currentImage = 0;

                picture.Image = Image.FromFile(images[currentImage]);

                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (images == null || images.Length == 0)
                return;

            currentImage++;

            if (currentImage >= images.Length)
                currentImage = 0;

            picture.Image = Image.FromFile(images[currentImage]);
        }
    }
}