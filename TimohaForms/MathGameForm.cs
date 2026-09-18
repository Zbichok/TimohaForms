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
    public class MathGameForm : Form1
    {
        private Label questionLabel;
        private Label scoreLabel;
        private Label timeLabel;

        private TextBox answerBox;

        private Button checkButton;
        private Button startButton;

        private ComboBox difficultyBox;

        private Timer timer;

        private Random random = new Random();

        private int correctAnswer;
        private int score;
        private int time;

        public MathGameForm()
        {
            Text = "Matemaatikamäng";
            Size = new Size(600, 450);
            StartPosition = FormStartPosition.CenterScreen;

            CreateControls();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void CreateControls()
        {
            questionLabel = new Label();
            questionLabel.Text = "Klõpsake nuppu Start";
            questionLabel.Font = new Font("Arial", 24);
            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(170, 60);

            scoreLabel = new Label();
            scoreLabel.Text = "Punktid: 0";
            scoreLabel.Font = new Font("Arial", 14);
            scoreLabel.Location = new Point(30, 20);
            scoreLabel.AutoSize = true;

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 30";
            timeLabel.Font = new Font("Arial", 14);
            timeLabel.Location = new Point(450, 20);
            timeLabel.AutoSize = true;

            answerBox = new TextBox();
            answerBox.Location = new Point(190, 130);
            answerBox.Size = new Size(200, 30);

            checkButton = new Button();
            checkButton.Text = "Kontrolli";
            checkButton.Location = new Point(210, 180);
            checkButton.Size = new Size(150, 40);
            checkButton.Click += CheckButton_Click;

            startButton = new Button();
            startButton.Text = "Start";
            startButton.Location = new Point(210, 240);
            startButton.Size = new Size(150, 40);
            startButton.Click += StartButton_Click;

            difficultyBox = new ComboBox();
            difficultyBox.Location = new Point(210, 310);
            difficultyBox.Size = new Size(150, 30);

            difficultyBox.Items.Add("Lihtne");
            difficultyBox.Items.Add("Keskmine");
            difficultyBox.Items.Add("Raske");

            difficultyBox.SelectedIndex = 0;

            Controls.Add(questionLabel);
            Controls.Add(scoreLabel);
            Controls.Add(timeLabel);
            Controls.Add(answerBox);
            Controls.Add(checkButton);
            Controls.Add(startButton);
            Controls.Add(difficultyBox);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            score = 0;
            time = 30;

            scoreLabel.Text = "Punktid: 0";
            timeLabel.Text = "Aeg: 30";

            timer.Start();

            CreateQuestion();
        }

        private void CreateQuestion()
        {
            int maxNumber = 10;

            if (difficultyBox.SelectedIndex == 1)
                maxNumber = 50;

            if (difficultyBox.SelectedIndex == 2)
                maxNumber = 100;

            int number1 = random.Next(1, maxNumber);
            int number2 = random.Next(1, maxNumber);

            int operation = random.Next(0, 4);

            if (operation == 0)
            {
                correctAnswer = number1 + number2;

                questionLabel.Text =
                    number1 + " + " + number2 + " = ?";
            }
            else if (operation == 1)
            {
                correctAnswer = number1 - number2;

                questionLabel.Text =
                    number1 + " - " + number2 + " = ?";
            }
            else if (operation == 2)
            {
                correctAnswer = number1 * number2;

                questionLabel.Text =
                    number1 + " × " + number2 + " = ?";
            }
            else
            {
                correctAnswer = number1;

                int result = number1 * number2;

                questionLabel.Text =
                    result + " ÷ " + number2 + " = ?";

                correctAnswer = number1;
            }

            answerBox.Clear();
            answerBox.Focus();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            int answer;

            if (!int.TryParse(answerBox.Text, out answer))
            {
                MessageBox.Show("Sisesta arv.");
                return;
            }

            if (answer == correctAnswer)
            {
                score++;
                scoreLabel.Text = "Punktid: " + score;

                CreateQuestion();
            }
            else
            {
                MessageBox.Show("Vale!");
                CreateQuestion();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time--;

            timeLabel.Text = "Aeg: " + time;

            if (time <= 0)
            {
                timer.Stop();

                MessageBox.Show(
                    "Aeg on läbi!\nTeie tulemus: " + score
                );
            }
        }
    }
}
