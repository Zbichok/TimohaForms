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
        private Button restartButton;
        private Button backButton;

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

        private bool isChecking = false;

        public MemoryGameForm()
        {
            Text = "Pildimäng";
            Size = new Size(800, 700);
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
            levelBox = new ComboBox();
            levelBox.Location = new Point(30, 25);
            levelBox.Size = new Size(100, 30);

            levelBox.Items.Add("4x4");
            levelBox.Items.Add("6x6");

            levelBox.SelectedIndex = 0;

            levelBox.Font = new Font("Segoe UI", 10);
            levelBox.BackColor = Color.White;
            levelBox.ForeColor = Color.FromArgb(55, 60, 70);

            startButton = new Button();
            startButton.Text = "Start";
            startButton.Location = new Point(150, 23);
            startButton.Size = new Size(110, 35);
            startButton.Click += StartButton_Click;
            StylePrimaryButton(startButton);

            restartButton = new Button();
            restartButton.Text = "Uuesti";
            restartButton.Location = new Point(275, 23);
            restartButton.Size = new Size(110, 35);
            restartButton.Click += RestartButton_Click;
            StyleSecondaryButton(restartButton);

            backButton = new Button();
            backButton.Text = "Tagasi";
            backButton.Location = new Point(400, 23);
            backButton.Size = new Size(110, 35);
            backButton.Click += BackButton_Click;
            StyleSecondaryButton(backButton);

            scoreLabel = new Label();
            scoreLabel.Text = "Punktid: 0";
            scoreLabel.Location = new Point(540, 29);
            scoreLabel.AutoSize = true;
            scoreLabel.ForeColor = Color.FromArgb(55, 60, 70);
            scoreLabel.Font = new Font("Segoe UI", 10);

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 60";
            timeLabel.Location = new Point(650, 29);
            timeLabel.AutoSize = true;
            timeLabel.ForeColor = Color.FromArgb(55, 60, 70);
            timeLabel.Font = new Font("Segoe UI", 10);

            Controls.Add(levelBox);
            Controls.Add(startButton);
            Controls.Add(restartButton);
            Controls.Add(backButton);
            Controls.Add(scoreLabel);
            Controls.Add(timeLabel);
        }

        private void StyleSecondaryButton(Button button)
        {
            button.Font = new Font("Segoe UI", 10);
            button.ForeColor = Color.FromArgb(55, 60, 70);
            button.BackColor = Color.FromArgb(225, 229, 235);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;
        }

        private void StylePrimaryButton(Button button)
        {
            button.Font = new Font("Segoe UI", 10);
            button.ForeColor = Color.White;
            button.BackColor = Color.FromArgb(90, 120, 150);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            timer.Stop();

            foreach (Button card in cards)
            {
                if (card.BackgroundImage != null)
                {
                    card.BackgroundImage.Dispose();
                    card.BackgroundImage = null;
                }

                Controls.Remove(card);
                card.Dispose();
            }

            cards.Clear();

            Close();
        }

        private void StartGame()
        {
            timer.Stop();

            foreach (Button card in cards)
            {
                if (card.BackgroundImage != null)
                {
                    card.BackgroundImage.Dispose();
                    card.BackgroundImage = null;
                }

                Controls.Remove(card);
                card.Dispose();
            }

            cards.Clear();
            cardNumbers.Clear();

            firstCard = null;
            secondCard = null;

            isChecking = false;

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

            for (int i = 1; i <= pairs; i++)
            {
                cardNumbers.Add(i);
                cardNumbers.Add(i);
            }

            Shuffle();

            int cardSize = 90;

            int fieldWidth = size * cardSize;

            int startX = (ClientSize.Width - fieldWidth) / 2;

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Button card = new Button();

                    card.Size = new Size(cardSize, cardSize);

                    card.Location = new Point(
                        startX + column * cardSize,
                        90 + row * cardSize
                    );

                    card.Tag = cardNumbers[cards.Count];

                    card.BackgroundImageLayout = ImageLayout.Zoom;

                    card.Text = "?";

                    card.Font = new Font(
                        "Segoe UI",
                        20,
                        FontStyle.Regular
                    );

                    card.ForeColor = Color.FromArgb(90, 95, 105);
                    card.BackColor = Color.White;

                    card.FlatStyle = FlatStyle.Flat;
                    card.FlatAppearance.BorderColor =
                        Color.FromArgb(215, 220, 228);

                    card.FlatAppearance.BorderSize = 1;

                    card.Cursor = Cursors.Hand;

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
            if (isChecking)
            {
                return;
            }

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

            string imagesFolder = Path.Combine(
                Application.StartupPath,
                "Images"
            );

            string fileName = Path.Combine(
                imagesFolder,
                number + ".jpg"
            );

            if (!File.Exists(fileName))
            {
                fileName = Path.Combine(
                    imagesFolder,
                    number + ".png"
                );
            }

            if (!File.Exists(fileName))
            {
                MessageBox.Show(
                    "Kujutist ei leitud: " +
                    number +
                    ".jpg või " +
                    number +
                    ".png"
                );

                return;
            }

            using (Image tempImage = Image.FromFile(fileName))
            {
                card.BackgroundImage = new Bitmap(tempImage);
            }

            card.Text = "";

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
                isChecking = true;

                Timer hideTimer = new Timer();
                hideTimer.Interval = 700;

                hideTimer.Tick += (s, args) =>
                {
                    if (firstCard != null)
                    {
                        if (firstCard.BackgroundImage != null)
                        {
                            firstCard.BackgroundImage.Dispose();
                            firstCard.BackgroundImage = null;
                        }

                        firstCard.Text = "?";
                    }

                    if (secondCard != null)
                    {
                        if (secondCard.BackgroundImage != null)
                        {
                            secondCard.BackgroundImage.Dispose();
                            secondCard.BackgroundImage = null;
                        }

                        secondCard.Text = "?";
                    }

                    firstCard = null;
                    secondCard = null;

                    isChecking = false;

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