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
    public class BalenciagaForm : Form1
    {
        private Button openButton;

        public BalenciagaForm()
        {
            Text = "Balenciaga";
            Size = new Size(500, 300);
            StartPosition = FormStartPosition.CenterScreen;

            openButton = new Button();

            openButton.Text = "Ava Balenciaga";
            openButton.Size = new Size(200, 50);
            openButton.Location = new Point(140, 100);

            openButton.Click += OpenButton_Click;

            Controls.Add(openButton);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://www.balenciaga.com/",
                    UseShellExecute = true
                });
        }
    }
}