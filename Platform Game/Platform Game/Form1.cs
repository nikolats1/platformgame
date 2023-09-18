using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Platform_Game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed, force, score = 0, playerSpeed = 7;
        int horizontalSpeed = 5, verticalSpeed = 3;
        int enemyOneSpeed = 5, enemyTwoSpeed = 3;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            scoreLabel.Text = "Score: " + score;

            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;
                            if((string)x.Name == "horizontal" && goLeft == false && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            scoreLabel.Text = "Score: " + score + Environment.NewLine + "You were killed!";
                        }
                    }
                    if ((string)x.Name == "door")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && score == 26)
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            scoreLabel.Text = "Score: " + score + Environment.NewLine + "Your quest is complete!";
                        }
                    }
                }
            }

            horizontal.Left -= horizontalSpeed;
            if (horizontal.Left < 0 || horizontal.Left + horizontal.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            vertical.Top += verticalSpeed;
            if (vertical.Top < 180 || vertical.Top > 600)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemy1.Left -= enemyOneSpeed;
            if (enemy1.Left < pictureBox5.Left || enemy1.Left + enemy1.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemy2.Left += enemyTwoSpeed;
            if (enemy2.Left < pictureBox2.Left || enemy2.Left + enemy2.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                scoreLabel.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();  
            }
        }

        private void RestartGame()
        {
            Point Location = this.Location;
            Form1 Newform = new Form1();
            Newform.Show();
            Newform.Location = Location;
            this.Dispose(false);
        }
    }
}
