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
    public class ShopsForm : Form
    {
        private Button erdButton;
        private Button balenciagaButton;
        private Button rickOwensButton;
        private Button vetementsButton;
        private Button ssenseButton;
        private Button privateButton;

        public ShopsForm()
        {
            Text = "Veebipoed";
            Size = new Size(350, 500);
            StartPosition = FormStartPosition.CenterScreen;

            CreateButtons();
        }

        private void CreateButtons()
        {
            erdButton = CreateButton("ERD", 30);
            balenciagaButton = CreateButton("Balenciaga", 90);
            rickOwensButton = CreateButton("Rick Owens", 150);
            vetementsButton = CreateButton("Vetements", 210);
            ssenseButton = CreateButton("SSENSE", 270);
            privateButton = CreateButton("Privaatne sait", 350);

            erdButton.Click += (s, e) =>
                OpenSite("https://www.erdclothing.com/");

            balenciagaButton.Click += (s, e) =>
                OpenSite("https://www.balenciaga.com/");

            rickOwensButton.Click += (s, e) =>
                OpenSite("https://www.rickowens.eu/");

            vetementsButton.Click += (s, e) =>
                OpenSite("https://vetementswebsite.com/");

            ssenseButton.Click += (s, e) =>
                OpenSite("https://www.ssense.com/");

            privateButton.Click += PrivateButton_Click;

            Controls.Add(erdButton);
            Controls.Add(balenciagaButton);
            Controls.Add(rickOwensButton);
            Controls.Add(vetementsButton);
            Controls.Add(ssenseButton);
            Controls.Add(privateButton);
        }

        private Button CreateButton(string text, int y)
        {
            Button button = new Button();

            button.Text = text;
            button.Size = new Size(250, 45);
            button.Location = new Point(40, y);

            return button;
        }

        private void OpenSite(string url)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
        }

        private void PrivateButton_Click(object sender, EventArgs e)
        {
            Form passwordForm = new Form();

            passwordForm.Text = "Parool";
            passwordForm.Size = new Size(300, 180);
            passwordForm.StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label();
            label.Text = "Sisesta parool:";
            label.Location = new Point(30, 20);
            label.AutoSize = true;

            TextBox passwordBox = new TextBox();
            passwordBox.Location = new Point(30, 50);
            passwordBox.Width = 220;
            passwordBox.PasswordChar = '*';

            Button button = new Button();
            button.Text = "Sisesta";
            button.Location = new Point(30, 90);
            button.Size = new Size(100, 30);

            button.Click += (s, args) =>
            {
                if (passwordBox.Text == "bandera")
                {
                    passwordForm.Close();

                    OpenSite("https://azov.org.ua/en/");
                }
                else
                {
                    MessageBox.Show("Vale parool!");
                }
            };

            passwordForm.Controls.Add(label);
            passwordForm.Controls.Add(passwordBox);
            passwordForm.Controls.Add(button);

            passwordForm.Show();
        }
    }
}