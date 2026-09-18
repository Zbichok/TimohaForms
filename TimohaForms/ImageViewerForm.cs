using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TimohaForms
{
    public class ImageViewerForm : Form
    {
        private PictureBox pictureBox;

        private Button previousButton;
        private Button nextButton;
        private Button addButton;
        private Button colorButton;
        private Button saveButton;

        private List<string> images = new List<string>();

        private int currentImage = 0;


        public ImageViewerForm()
        {
            Text = "Image Viewer";
            Size = new Size(900, 650);
            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(25, 25, 30);

            pictureBox = new PictureBox();

            pictureBox.Size = new Size(700, 450);
            pictureBox.Location = new Point(100, 40);

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.FromArgb(35, 35, 40);

            Controls.Add(pictureBox);


            previousButton = new Button();

            previousButton.Text = "< Eelmine";
            previousButton.Size = new Size(140, 40);
            previousButton.Location = new Point(150, 510);

            previousButton.Click += PreviousButton_Click;

            Controls.Add(previousButton);


            nextButton = new Button();

            nextButton.Text = "Järgmine >";
            nextButton.Size = new Size(140, 40);
            nextButton.Location = new Point(610, 510);

            nextButton.Click += NextButton_Click;

            Controls.Add(nextButton);

            addButton = new Button();

            addButton.Text = "Lisa";
            addButton.Size = new Size(120, 40);
            addButton.Location = new Point(310, 510);

            addButton.Click += AddButton_Click;

            Controls.Add(addButton);


            colorButton = new Button();

            colorButton.Text = "Taustavärv";
            colorButton.Size = new Size(120, 40);
            colorButton.Location = new Point(440, 510);

            colorButton.Click += ColorButton_Click;

            Controls.Add(colorButton);


            saveButton = new Button();

            saveButton.Text = "Salvesta";
            saveButton.Size = new Size(120, 40);
            saveButton.Location = new Point(390, 560);

            saveButton.Click += SaveButton_Click;

            Controls.Add(saveButton);

            LoadImages();
        }

        private void LoadImages()
        {
            string folder = Path.Combine(
                Application.StartupPath,
                "Images"
            );

            if (!Directory.Exists(folder))
                return;


            string[] files = Directory.GetFiles(folder);

            foreach (string file in files)
            {
                string extension =
                    Path.GetExtension(file).ToLower();

                if (extension == ".jpg" ||
                    extension == ".jpeg" ||
                    extension == ".png" ||
                    extension == ".bmp")
                {
                    images.Add(file);
                }
            }


            if (images.Count > 0)
            {
                currentImage = 0;
                ShowImage();
            }
        }

        private void ShowImage()
        {
            if (images.Count == 0)
                return;


            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }


            pictureBox.Image =
                Image.FromFile(images[currentImage]);
        }

        private void NextButton_Click(
            object sender,
            EventArgs e)
        {
            if (images.Count == 0)
                return;


            currentImage++;

            if (currentImage >= images.Count)
                currentImage = 0;

            ShowImage();
        }


        private void PreviousButton_Click(
            object sender,
            EventArgs e)
        {
            if (images.Count == 0)
                return;


            currentImage--;

            if (currentImage < 0)
                currentImage = images.Count - 1;

            ShowImage();
        }


        private void AddButton_Click(
            object sender,
            EventArgs e)
        {
            OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "Images|*.jpg;*.jpeg;*.png;*.bmp";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                images.Add(dialog.FileName);

                currentImage = images.Count - 1;

                ShowImage();
            }
        }


        private void ColorButton_Click(
            object sender,
            EventArgs e)
        {
            ColorDialog dialog =
                new ColorDialog();


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = dialog.Color;
            }
        }

        private void SaveButton_Click(
            object sender,
            EventArgs e)
        {
            if (images.Count == 0)
                return;


            SaveFileDialog dialog =
                new SaveFileDialog();

            dialog.Filter =
                "PNG Image|*.png|" +
                "JPEG Image|*.jpg|" +
                "Bitmap Image|*.bmp";


            if (dialog.ShowDialog() != DialogResult.OK)
                return;


            ImageFormat format =
                ImageFormat.Png;


            string extension =
                Path.GetExtension(
                    dialog.FileName).ToLower();


            if (extension == ".jpg")
                format = ImageFormat.Jpeg;

            else if (extension == ".bmp")
                format = ImageFormat.Bmp;


            pictureBox.Image.Save(
                dialog.FileName,
                format
            );
        }
    }
}