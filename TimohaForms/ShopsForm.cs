using System;
using System.Drawing;
using System.Windows.Forms;

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
        private Button backButton;

        public ShopsForm()
        {
            Text = "Veebipoed";
            Size = new Size(400, 520);
            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10);

            CreateButtons();
        }

        private void CreateButtons()
        {
            erdButton = CreateButton("ERD", 40);
            balenciagaButton = CreateButton("Balenciaga", 100);
            rickOwensButton = CreateButton("Rick Owens", 160);
            vetementsButton = CreateButton("Vetements", 220);
            ssenseButton = CreateButton("SSENSE", 280);

            privateButton = CreateButton("Privaatne sait", 350);
            StylePrimaryButton(privateButton);

            backButton = CreateButton("Tagasi", 410);

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

            backButton.Click += BackButton_Click;

            Controls.Add(erdButton);
            Controls.Add(balenciagaButton);
            Controls.Add(rickOwensButton);
            Controls.Add(vetementsButton);
            Controls.Add(ssenseButton);
            Controls.Add(privateButton);
            Controls.Add(backButton);
        }

        private Button CreateButton(string text, int y)
        {
            Button button = new Button();

            button.Text = text;
            button.Size = new Size(280, 45);
            button.Location = new Point(55, y);

            StyleSecondaryButton(button);

            return button;
        }

        private void StyleSecondaryButton(Button button)
        {
            button.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            button.ForeColor = Color.FromArgb(55, 60, 70);
            button.BackColor = Color.FromArgb(225, 229, 235);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;
        }

        private void StylePrimaryButton(Button button)
        {
            button.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            button.ForeColor = Color.White;
            button.BackColor = Color.FromArgb(90, 120, 150);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;
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
            passwordForm.Size = new Size(330, 210);
            passwordForm.StartPosition = FormStartPosition.CenterScreen;
            passwordForm.BackColor = Color.FromArgb(245, 247, 250);
            passwordForm.Font = new Font("Segoe UI", 10);

            Label label = new Label();
            label.Text = "Sisesta parool:";
            label.Location = new Point(35, 25);
            label.AutoSize = true;
            label.ForeColor = Color.FromArgb(55, 60, 70);

            TextBox passwordBox = new TextBox();
            passwordBox.Location = new Point(35, 55);
            passwordBox.Width = 245;
            passwordBox.PasswordChar = '*';

            Button button = new Button();
            button.Text = "Sisesta";
            button.Location = new Point(35, 100);
            button.Size = new Size(110, 40);

            StylePrimaryButton(button);

            button.Click += (s, args) =>
            {
                if (passwordBox.Text == "bandera")
                {
                    passwordForm.Close();

                    OpenSite("https://azov.org.ua/en/");
                }
                else
                {
                    MessageBox.Show(
                        "Vale parool!",
                        "Viga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            };

            passwordForm.Controls.Add(label);
            passwordForm.Controls.Add(passwordBox);
            passwordForm.Controls.Add(button);

            passwordForm.AcceptButton = button;

            passwordForm.Show();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}