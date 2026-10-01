
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimohaForms
{
    public class MathGameForm : Form1
    {
        private Label questionLabel;
        private Label scoreLabel;
        private Label timeLabel;
        private Label difficultyLabel;

        private TextBox answerBox;

        private Button checkButton;
        private Button startButton;
        private Button backButton;

        private ComboBox difficultyBox;

        private Timer timer;

        private Random random = new Random();

        private int correctAnswer;
        private int score;
        private int time;


        public MathGameForm()
        {
            Text = "Matemaatikamäng";

            Size = new Size(600, 500);

            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(245, 247, 250);

            Font = new Font("Segoe UI", 10);

            CreateControls();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }


        private void CreateControls()
        {
            // SCORE

            scoreLabel = new Label();

            scoreLabel.Text = "Punktid: 0";

            scoreLabel.Font = new Font(
                "Segoe UI",
                11,
                FontStyle.Regular
            );

            scoreLabel.ForeColor =
                Color.FromArgb(70, 75, 85);

            scoreLabel.AutoSize = true;

            scoreLabel.Location =
                new Point(40, 30);


            // TIME

            timeLabel = new Label();

            timeLabel.Text = "Aeg: 30";

            timeLabel.Font = new Font(
                "Segoe UI",
                11,
                FontStyle.Regular
            );

            timeLabel.ForeColor =
                Color.FromArgb(70, 75, 85);

            timeLabel.AutoSize = true;

            timeLabel.Location =
                new Point(480, 30);


            // QUESTION

            questionLabel = new Label();

            questionLabel.Text =
                "Klõpsake nuppu Start";

            questionLabel.Font = new Font(
                "Segoe UI",
                26,
                FontStyle.Regular
            );

            questionLabel.ForeColor =
                Color.FromArgb(45, 48, 55);

            questionLabel.AutoSize = false;

            questionLabel.TextAlign =
                ContentAlignment.MiddleCenter;

            questionLabel.Location =
                new Point(50, 80);

            questionLabel.Size =
                new Size(500, 60);


            // ANSWER BOX

            answerBox = new TextBox();

            answerBox.Font =
                new Font("Segoe UI", 14);

            answerBox.TextAlign =
                HorizontalAlignment.Center;

            answerBox.Location =
                new Point(175, 155);

            answerBox.Size =
                new Size(250, 35);

            answerBox.BorderStyle =
                BorderStyle.FixedSingle;

            answerBox.Visible = false;

            // CHECK BUTTON

            checkButton = new Button();

            checkButton.Text = "Kontrolli";

            checkButton.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular
            );

            checkButton.ForeColor =
                Color.White;

            checkButton.BackColor =
                Color.FromArgb(90, 120, 150);

            checkButton.FlatStyle =
                FlatStyle.Flat;

            checkButton.FlatAppearance.BorderSize = 0;

            checkButton.Location =
                new Point(175, 210);

            checkButton.Size =
                new Size(250, 42);

            checkButton.Cursor =
                Cursors.Hand;

            checkButton.Click +=
                CheckButton_Click;

            checkButton.Visible = false;


            // START / RESTART BUTTON


            startButton = new Button();

            startButton.Text = "Start";

            startButton.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular
            );

            startButton.ForeColor =
                Color.FromArgb(55, 60, 70);

            startButton.BackColor =
                Color.FromArgb(225, 229, 235);

            startButton.FlatStyle =
                FlatStyle.Flat;

            startButton.FlatAppearance.BorderSize = 0;

            startButton.Location =
                new Point(175, 265);

            startButton.Size =
                new Size(120, 42);

            startButton.Cursor =
                Cursors.Hand;

            startButton.Click +=
                StartButton_Click;


            // BACK BUTTON

            backButton = new Button();

            backButton.Text = "Tagasi";

            backButton.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular
            );

            backButton.ForeColor =
                Color.FromArgb(55, 60, 70);

            backButton.BackColor =
                Color.FromArgb(225, 229, 235);

            backButton.FlatStyle =
                FlatStyle.Flat;

            backButton.FlatAppearance.BorderSize = 0;

            backButton.Location =
                new Point(305, 265);

            backButton.Size =
                new Size(120, 42);

            backButton.Cursor =
                Cursors.Hand;

            backButton.Click +=
                BackButton_Click;

            backButton.Visible = false;


            // DIFFICULTY LABEL

            difficultyLabel = new Label();

            difficultyLabel.Text =
                "Raskusaste";

            difficultyLabel.Font = new Font(
                "Segoe UI",
                10
            );

            difficultyLabel.ForeColor =
                Color.FromArgb(90, 95, 105);

            difficultyLabel.AutoSize = true;

            difficultyLabel.Location =
                new Point(175, 330);


            // DIFFICULTY BOX

            difficultyBox = new ComboBox();

            difficultyBox.Font =
                new Font("Segoe UI", 10);

            difficultyBox.DropDownStyle =
                ComboBoxStyle.DropDownList;

            difficultyBox.Location =
                new Point(175, 355);

            difficultyBox.Size =
                new Size(250, 32);

            difficultyBox.Items.Add("Lihtne");
            difficultyBox.Items.Add("Keskmine");
            difficultyBox.Items.Add("Raske");

            difficultyBox.SelectedIndex = 0;


            // ADD CONTROLS

            Controls.Add(scoreLabel);
            Controls.Add(timeLabel);

            Controls.Add(questionLabel);

            Controls.Add(answerBox);

            Controls.Add(checkButton);

            Controls.Add(startButton);
            Controls.Add(backButton);

            Controls.Add(difficultyLabel);
            Controls.Add(difficultyBox);
        }


        // START / RESTART GAME

        private void StartButton_Click(
            object sender,
            EventArgs e)
        {
            score = 0;

            time = 30;

            scoreLabel.Text =
                "Punktid: 0";

            timeLabel.Text =
                "Aeg: 30";

            answerBox.Visible = true;

            checkButton.Visible = true;

            difficultyLabel.Visible = false;

            difficultyBox.Visible = false;
            startButton.Text = "Uuesti";

            backButton.Visible = true;


            timer.Start();

            CreateQuestion();
        }


        // CREATE QUESTION

        private void CreateQuestion()
        {
            int maxNumber = 10;

            if (difficultyBox.SelectedIndex == 0)
            {
                maxNumber = 10;
            }

            if (difficultyBox.SelectedIndex == 1)
            {
                maxNumber = 50;
            }

            if (difficultyBox.SelectedIndex == 2)
            {
                maxNumber = 100;
            }


            int number1 =
                random.Next(1, maxNumber);

            int number2 =
                random.Next(1, maxNumber);

            int operation =
                random.Next(0, 4);


            // ADDITION

            if (operation == 0)
            {
                correctAnswer =
                    number1 + number2;

                questionLabel.Text =
                    number1 +
                    " + " +
                    number2 +
                    " = ?";
            }


            // SUBTRACTION

            else if (operation == 1)
            {
                correctAnswer =
                    number1 - number2;

                questionLabel.Text =
                    number1 +
                    " - " +
                    number2 +
                    " = ?";
            }


            // MULTIPLICATION

            else if (operation == 2)
            {
                correctAnswer =
                    number1 * number2;

                questionLabel.Text =
                    number1 +
                    " × " +
                    number2 +
                    " = ?";
            }

            // DIVISION

            else
            {
                correctAnswer =
                    number1;

                int result =
                    number1 * number2;

                questionLabel.Text =
                    result +
                    " ÷ " +
                    number2 +
                    " = ?";
            }


            answerBox.Clear();

            answerBox.Focus();
        }


        // CHECK ANSWER

        private void CheckButton_Click(
            object sender,
            EventArgs e)
        {
            int answer;

            if (!int.TryParse(
                answerBox.Text,
                out answer))
            {
                MessageBox.Show(
                    "Sisesta arv.",
                    "Viga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }


            if (answer == correctAnswer)
            {
                score++;

                scoreLabel.Text =
                    "Punktid: " + score;

                CreateQuestion();
            }

            else
            {
                MessageBox.Show(
                    "Vale vastus.",
                    "Proovi uuesti",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CreateQuestion();
            }
        }


        // BACK TO MAIN MENU

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            timer.Stop();

            this.Close();
        }


        // TIMER

        private void Timer_Tick(
            object sender,
            EventArgs e)
        {
            time--;

            timeLabel.Text =
                "Aeg: " + time;


            if (time <= 0)
            {
                timer.Stop();

                MessageBox.Show(
                    "Aeg on läbi!\n\n" +
                    "Teie tulemus: " +
                    score,
                    "Mäng läbi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}

