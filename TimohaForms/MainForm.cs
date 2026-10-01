using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimohaForms
{
    public class MainForm : Form1
    {
        private Button imageButton;
        private Button mathButton;
        private Button memoryButton;
        private Button shopsButton;

        public MainForm()
        {
            Text = "Minu programmid";
            Size = new Size(500, 450);
            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10);

            CreateButtons();
        }

        private void CreateButtons()
        {
            imageButton = new Button();
            imageButton.Text = "Piltide vaatamine";
            imageButton.Size = new Size(280, 55);
            imageButton.Location = new Point(110, 50);
            imageButton.Click += ImageButton_Click;
            StyleButton(imageButton);

            mathButton = new Button();
            mathButton.Text = "Matemaatikamäng";
            mathButton.Size = new Size(280, 55);
            mathButton.Location = new Point(110, 125);
            mathButton.Click += MathButton_Click;
            StyleButton(mathButton);

            memoryButton = new Button();
            memoryButton.Text = "Pildimäng";
            memoryButton.Size = new Size(280, 55);
            memoryButton.Location = new Point(110, 200);
            memoryButton.Click += MemoryButton_Click;
            StyleButton(memoryButton);

            shopsButton = new Button();
            shopsButton.Text = "Veebipoed";
            shopsButton.Size = new Size(280, 55);
            shopsButton.Location = new Point(110, 275);
            shopsButton.Click += ShopsButton_Click;
            StyleButton(shopsButton);

            Controls.Add(imageButton);
            Controls.Add(mathButton);
            Controls.Add(memoryButton);
            Controls.Add(shopsButton);
        }

        private void StyleButton(Button button)
        {
            button.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            button.ForeColor = Color.FromArgb(55, 60, 70);
            button.BackColor = Color.FromArgb(225, 229, 235);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            ImageViewerForm form = new ImageViewerForm();
            form.Show();
        }

        private void MathButton_Click(object sender, EventArgs e)
        {
            MathGameForm form = new MathGameForm();
            form.Show();
        }

        private void MemoryButton_Click(object sender, EventArgs e)
        {
            MemoryGameForm form = new MemoryGameForm();
            form.Show();
        }

        private void ShopsButton_Click(object sender, EventArgs e)
        {
            ShopsForm form = new ShopsForm();
            form.Show();
        }
    }
}