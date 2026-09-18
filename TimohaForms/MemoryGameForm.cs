using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace TimohaForms
{
    public class MemoryGameForm : Form1
    {
        private ComboBox levelBox;
        private Button startButton;

        private Label scoreLabel;
        private Label timeLabel;

        private Timer timer;

        private List<Button> cards = new List<Button>();
        private List<int> cardNumbers = new List<int>();

        private Random random = new Random();

        private Button firstCard;
        private Button secondCard;

        private int firstNumber;
        private int secondNumber;

        private int score;
        private int time;

        public MemoryGameForm()
        {
            Text = "Pildimäng";
            Size = new Size(800, 700);
            StartPosition = FormStartPosition.CenterScreen;

            CreateControls();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void CreateControls()
        {
            levelBox = new ComboBox();
            levelBox.Location = new Point(20, 20);
            levelBox.Size = new Size(100, 30);

            levelBox.Items.Add("4x4");
            levelBox.Items.Add("6x6");

            levelBox.SelectedIndex = 0;

            startButton = new Button();
            startButton.Text = "Start";
            startButton.Location = new Point(140, 20);
            startButton.Size = new Size(100, 30);

            startButton.Click += StartButton_Click;

            scoreLabel = new Label();
            scoreLabel.Text = "Punktid: 0";
            scoreLabel.Location = new Point(300, 25);
            scoreLabel.AutoSize = true;

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 60";
            timeLabel.Location = new Point(400, 25);
            timeLabel.AutoSize = true;

            Controls.Add(levelBox);
            Controls.Add(startButton);
            Controls.Add(scoreLabel);
            Controls.Add(timeLabel);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            foreach (Button card in cards)
            {
                Controls.Remove(card);
            }

            cards.Clear();
            cardNumbers.Clear();

            firstCard = null;
            secondCard = null;

            score = 0;
            time = 60;

            scoreLabel.Text = "Punktid: 0";
            timeLabel.Text = "Aeg: 60";

            int size = 4;

            if (levelBox.SelectedIndex == 1)
            {
                size = 6;
            }

            int pairs = size * size / 2;

            // Создаём пары номеров
            for (int i = 1; i <= pairs; i++)
            {
                cardNumbers.Add(i);
                cardNumbers.Add(i);
            }

            Shuffle();

            int cardSize = 90;

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Button card = new Button();

                    card.Size = new Size(cardSize, cardSize);

                    card.Location = new Point(
                        30 + column * cardSize,
                        80 + row * cardSize
                    );

                    // Номер картинки
                    card.Tag = cardNumbers[cards.Count];

                    card.BackgroundImageLayout = ImageLayout.Zoom;

                    // Показываем вопросительный знак,
                    // пока карточка закрыта
                    card.Text = "?";

                    card.Font = new Font("Arial", 20);

                    card.Click += Card_Click;

                    cards.Add(card);
                    Controls.Add(card);
                }
            }

            timer.Start();
        }

        private void Shuffle()
        {
            for (int i = 0; i < cardNumbers.Count; i++)
            {
                int randomIndex = random.Next(cardNumbers.Count);

                int temp = cardNumbers[i];

                cardNumbers[i] = cardNumbers[randomIndex];
                cardNumbers[randomIndex] = temp;
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            Button card = (Button)sender;

            if (card == firstCard)
            {
                return;
            }

            if (card.Enabled == false)
            {
                return;
            }

            int number = (int)card.Tag;

            // Берём картинку из папки Images
            string fileName = Path.Combine(
                Application.StartupPath,
                "Images",
                number + ".jpg"
            );

            if (File.Exists(fileName))
            {
                card.BackgroundImage = Image.FromFile(fileName);
                card.Text = "";
            }
            else
            {
                MessageBox.Show(
                    "Kujutist ei leitud: " + fileName
                );

                return;
            }

            if (firstCard == null)
            {
                firstCard = card;
                firstNumber = number;

                return;
            }

            secondCard = card;
            secondNumber = number;

            if (firstNumber == secondNumber)
            {
                firstCard.Enabled = false;
                secondCard.Enabled = false;

                score++;

                scoreLabel.Text = "Punktid: " + score;

                firstCard = null;
                secondCard = null;

                CheckWin();
            }
            else
            {
                Timer hideTimer = new Timer();

                hideTimer.Interval = 700;

                hideTimer.Tick += (s, args) =>
                {
                    firstCard.BackgroundImage = null;
                    secondCard.BackgroundImage = null;

                    firstCard.Text = "?";
                    secondCard.Text = "?";

                    firstCard = null;
                    secondCard = null;

                    hideTimer.Stop();
                    hideTimer.Dispose();
                };

                hideTimer.Start();
            }
        }

        private void CheckWin()
        {
            bool win = true;

            foreach (Button card in cards)
            {
                if (card.Enabled)
                {
                    win = false;
                    break;
                }
            }

            if (win)
            {
                timer.Stop();

                MessageBox.Show(
                    "Sa võitsid!\nPunktid: " + score
                );
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
                    "Aeg on läbi!\nSkoor: " + score
                );
            }
        }
    }
}