using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
            Size = new Size(500, 400);
            StartPosition = FormStartPosition.CenterScreen;

            CreateButtons();
        }

        private void CreateButtons()
        {
            imageButton = new Button();
            imageButton.Text = "Piltide vaatamine";
            imageButton.Size = new Size(250, 50);
            imageButton.Location = new Point(120, 60);
            imageButton.Click += ImageButton_Click;

            mathButton = new Button();
            mathButton.Text = "Matemaatikamäng";
            mathButton.Size = new Size(250, 50);
            mathButton.Location = new Point(120, 140);
            mathButton.Click += MathButton_Click;

            memoryButton = new Button();
            memoryButton.Text = "Pildimäng";
            memoryButton.Size = new Size(250, 50);
            memoryButton.Location = new Point(120, 220);
            memoryButton.Click += MemoryButton_Click;

            shopsButton = new Button();
            shopsButton.Text = "Veebipoed";
            shopsButton.Size = new Size(250, 50);
            shopsButton.Location = new Point(120, 300);
            shopsButton.Click += ShopsButton_Click;

            Controls.Add(shopsButton);
            Controls.Add(imageButton);
            Controls.Add(mathButton);
            Controls.Add(memoryButton);
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