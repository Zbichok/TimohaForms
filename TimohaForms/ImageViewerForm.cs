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

        private Button restartButton;
        private Button backButton;

        private List<string> images = new List<string>();

        private int currentImage = 0;


        public ImageViewerForm()
        {
            Text = "Image Viewer";

            Size = new Size(900, 650);

            StartPosition =
                FormStartPosition.CenterScreen;

            // =========================
            // GENERAL STYLE
            // =========================

            BackColor =
                Color.FromArgb(245, 247, 250);

            Font =
                new Font("Segoe UI", 10);


            // =========================
            // PICTURE BOX
            // =========================

            pictureBox = new PictureBox();

            pictureBox.Size =
                new Size(700, 430);

            pictureBox.Location =
                new Point(100, 35);

            pictureBox.SizeMode =
                PictureBoxSizeMode.Zoom;

            pictureBox.BackColor =
                Color.White;

            Controls.Add(pictureBox);


            // =========================
            // PREVIOUS BUTTON
            // =========================

            previousButton = new Button();

            previousButton.Text =
                "< Eelmine";

            previousButton.Size =
                new Size(140, 40);

            previousButton.Location =
                new Point(100, 490);

            StyleSecondaryButton(
                previousButton
            );

            previousButton.Click +=
                PreviousButton_Click;

            Controls.Add(previousButton);


            // =========================
            // NEXT BUTTON
            // =========================

            nextButton = new Button();

            nextButton.Text =
                "Järgmine >";

            nextButton.Size =
                new Size(140, 40);

            nextButton.Location =
                new Point(660, 490);

            StyleSecondaryButton(
                nextButton
            );

            nextButton.Click +=
                NextButton_Click;

            Controls.Add(nextButton);


            // =========================
            // ADD BUTTON
            // =========================

            addButton = new Button();

            addButton.Text =
                "Lisa";

            addButton.Size =
                new Size(120, 40);

            addButton.Location =
                new Point(260, 490);

            StyleSecondaryButton(
                addButton
            );

            addButton.Click +=
                AddButton_Click;

            Controls.Add(addButton);


            // =========================
            // COLOR BUTTON
            // =========================

            colorButton = new Button();

            colorButton.Text =
                "Taustavärv";

            colorButton.Size =
                new Size(120, 40);

            colorButton.Location =
                new Point(390, 490);

            StyleSecondaryButton(
                colorButton
            );

            colorButton.Click +=
                ColorButton_Click;

            Controls.Add(colorButton);


            // =========================
            // SAVE BUTTON
            // =========================

            saveButton = new Button();

            saveButton.Text =
                "Salvesta";

            saveButton.Size =
                new Size(120, 40);

            saveButton.Location =
                new Point(520, 490);

            StylePrimaryButton(
                saveButton
            );

            saveButton.Click +=
                SaveButton_Click;

            Controls.Add(saveButton);


            // =========================
            // RESTART BUTTON
            // =========================

            restartButton = new Button();

            restartButton.Text =
                "Uuesti";

            restartButton.Size =
                new Size(140, 40);

            restartButton.Location =
                new Point(260, 545);

            StyleSecondaryButton(
                restartButton
            );

            restartButton.Click +=
                RestartButton_Click;

            Controls.Add(restartButton);


            // =========================
            // BACK BUTTON
            // =========================

            backButton = new Button();

            backButton.Text =
                "Tagasi";

            backButton.Size =
                new Size(140, 40);

            backButton.Location =
                new Point(500, 545);

            StyleSecondaryButton(
                backButton
            );

            backButton.Click +=
                BackButton_Click;

            Controls.Add(backButton);


            // =========================
            // LOAD IMAGES
            // =========================

            LoadImages();
        }


        // =====================================================
        // BUTTON STYLES
        // =====================================================

        private void StyleSecondaryButton(
            Button button)
        {
            button.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                );

            button.ForeColor =
                Color.FromArgb(55, 60, 70);

            button.BackColor =
                Color.FromArgb(225, 229, 235);

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize =
                0;

            button.Cursor =
                Cursors.Hand;
        }


        private void StylePrimaryButton(
            Button button)
        {
            button.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                );

            button.ForeColor =
                Color.White;

            button.BackColor =
                Color.FromArgb(90, 120, 150);

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize =
                0;

            button.Cursor =
                Cursors.Hand;
        }


        // =====================================================
        // LOAD IMAGES
        // =====================================================

        private void LoadImages()
        {
            string folder =
                Path.Combine(
                    Application.StartupPath,
                    "Images"
                );

            if (!Directory.Exists(folder))
                return;


            string[] files =
                Directory.GetFiles(folder);


            foreach (string file in files)
            {
                string extension =
                    Path.GetExtension(file)
                    .ToLower();


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


        // =====================================================
        // SHOW IMAGE
        // =====================================================

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
                Image.FromFile(
                    images[currentImage]
                );
        }


        // =====================================================
        // NEXT
        // =====================================================

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


        // =====================================================
        // PREVIOUS
        // =====================================================

        private void PreviousButton_Click(
            object sender,
            EventArgs e)
        {
            if (images.Count == 0)
                return;


            currentImage--;


            if (currentImage < 0)
                currentImage =
                    images.Count - 1;


            ShowImage();
        }


        // =====================================================
        // ADD IMAGE
        // =====================================================

        private void AddButton_Click(
            object sender,
            EventArgs e)
        {
            OpenFileDialog dialog =
                new OpenFileDialog();


            dialog.Filter =
                "Images|*.jpg;*.jpeg;*.png;*.bmp";


            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                images.Add(
                    dialog.FileName
                );


                currentImage =
                    images.Count - 1;


                ShowImage();
            }
        }


        // =====================================================
        // CHANGE BACKGROUND COLOR
        // =====================================================

        private void ColorButton_Click(
            object sender,
            EventArgs e)
        {
            ColorDialog dialog =
                new ColorDialog();


            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                BackColor =
                    dialog.Color;
            }
        }


        // =====================================================
        // SAVE IMAGE
        // =====================================================

        private void SaveButton_Click(
            object sender,
            EventArgs e)
        {
            if (images.Count == 0)
                return;


            if (pictureBox.Image == null)
                return;


            SaveFileDialog dialog =
                new SaveFileDialog();


            dialog.Filter =
                "PNG Image|*.png|" +
                "JPEG Image|*.jpg|" +
                "Bitmap Image|*.bmp";


            if (dialog.ShowDialog() !=
                DialogResult.OK)
                return;


            ImageFormat format =
                ImageFormat.Png;


            string extension =
                Path.GetExtension(
                    dialog.FileName
                ).ToLower();


            if (extension == ".jpg")
            {
                format =
                    ImageFormat.Jpeg;
            }
            else if (extension == ".bmp")
            {
                format =
                    ImageFormat.Bmp;
            }


            pictureBox.Image.Save(
                dialog.FileName,
                format
            );
        }


        // =====================================================
        // RESTART
        // =====================================================

        private void RestartButton_Click(
            object sender,
            EventArgs e)
        {
            images.Clear();

            currentImage = 0;

            ShowImage();

            LoadImages();
        }


        // =====================================================
        // BACK
        // =====================================================

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();

                pictureBox.Image = null;
            }


            this.Close();
        }
    }
}
