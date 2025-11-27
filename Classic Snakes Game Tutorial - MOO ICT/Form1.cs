using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

using System.Drawing.Imaging; // add this for the JPG compressor
using System.IO; // add this for the JPG compressor

namespace OOPETRIS
{
    public partial class Form1 : Form
    {

        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        int maxWidth;
        int maxHeight;

        int score;
        int highScore;

        Random rand = new Random();

        bool goLeft, goRight, goDown, goUp;


        public Form1()
        {
            InitializeComponent();
            new Settings();
            this.KeyPreview = true;
            this.Shown += Form1_Shown;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && Settings.directions != "right")
                goLeft = true;

            if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && Settings.directions != "left")
                goRight = true;

            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && Settings.directions != "down")
                goUp = true;

            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && Settings.directions != "up")
                goDown = true;



        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
        goLeft = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                goRight = false;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                goUp = false;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                goDown = false;
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
            startButton.Visible = false;
            gameTimer.Start();

            // Remove focus from the button so the form receives key presses
            this.ActiveControl = null;
            this.Focus();

        }

        private void TakeSnapShot(object sender, EventArgs e)
        {
            Label caption = new Label();
            caption.Text = "I scored: " + score + " and my Highscore is " + highScore + " on the Snake Game from MOO ICT";
            caption.Font = new Font("Ariel", 12, FontStyle.Bold);
            caption.ForeColor = Color.Purple;
            caption.AutoSize = false;
            caption.Width = picCanvas.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleCenter;
            picCanvas.Controls.Add(caption);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Snake Game SnapShot MOO ICT";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG Image File | *.jpg";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(picCanvas.Width);
                int height = Convert.ToInt32(picCanvas.Height);
                Bitmap bmp = new Bitmap(width, height);
                picCanvas.DrawToBitmap(bmp, new Rectangle(0,0, width, height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                picCanvas.Controls.Remove(caption);
            }





        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            // setting the directions

            if (goLeft)
            {
                Settings.directions = "left";
            }
            if (goRight)
            {
                Settings.directions = "right";
            }
            if (goDown)
            {
                Settings.directions = "down";
            }
            if (goUp)
            {
                Settings.directions = "up";
            }
            // end of directions

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }

                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }


                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {

                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }

                    }


                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }


            picCanvas.Invalidate();

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.Gray;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Settings.Width,
                    Snake[i].Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));
            }


            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
            (
            food.X * Settings.Width,
            food.Y * Settings.Height,
            Settings.Width, Settings.Height
            ));
            // Draw score manually on top-left corner
            Font font = new Font("Arial", 12, FontStyle.Bold);
            Brush brush = Brushes.Black;
            canvas.DrawString("SCORE: " + score, font, brush, new PointF(5, 15));
            canvas.DrawString("HIGH SCORE: " + highScore, font, brush, new PointF(5, 35));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            retryButton.Visible = false;  // hide retry button again
            RestartGame();                // reset everything and start again
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2 menu = new Form2();
            menu.Show();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Hide();              // hide game
            gameTimer.Stop();         // stop movement so it won’t freeze
            Form2 menu = new Form2();
            menu.Show();
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();

            startButton.Enabled = false;
            snapButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head); // adding the head part of the snake to the list

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight)};

            gameTimer.Start();
            gameTimer.Interval = initialInterval;  // reset speed

        }

        private void EatFood()
        {
            score += 1;
txtScore.Text = "Score: " + score;

Circle body = new Circle
{
    X = Snake[Snake.Count - 1].X,
    Y = Snake[Snake.Count - 1].Y
};

Snake.Add(body);

food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

if (gameTimer.Interval > 30) // minimum speed
{
    gameTimer.Interval -= speedIncrement;
}

// Play eat sound effect using relative path
try
{
    string soundPath = Path.Combine(
        Application.StartupPath,
        "Sounds",
        "food_G1U6tlb.wav"
    );

    SoundPlayer eatSound = new SoundPlayer(soundPath);
    eatSound.Play();
}
catch (Exception ex)
{
    MessageBox.Show("Eat sound not found: " + ex.Message);
}
        }

        private void GameOver()
        {
            gameTimer.Stop();            // stop the game
            retryButton.Visible = true;  // show retry button

            startButton.Enabled = true;
            snapButton.Enabled = true;

            if (score > highScore)
            {
                highScore = score;
                txtHighScore.Text = "High Score: " + Environment.NewLine + highScore;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        public void StopGame()
        {
            gameTimer.Stop();     // stop snake movement timer
            gameTimer.Dispose();  // optional, fully dispose
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Focus(); // ensures the form receives key presses
        }

        private void picCanvas_Click(object sender, EventArgs e)
        {

        }

        private void txtHighScore_Click(object sender, EventArgs e)
        {

        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                KeyIsDown(this, new KeyEventArgs(keyData));
                return true; // mark as handled
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private int initialInterval = 60;  // starting speed
        private int speedIncrement = 5;     // decrease per food

    }
}


