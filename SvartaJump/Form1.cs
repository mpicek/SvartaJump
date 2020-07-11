using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvartaJump
{
    public partial class Form1 : Form
    {
        private string TEXT_IN_UPPER_BAR = "Svarta Jump";

        private bool left_pressed = false;
        private bool right_pressed = false;

        private int WINDOW_HEIGHT = 900;
        private int WINDOW_WIDTH = 600;

        private Bitmap start_background;
        private Bitmap end_background;

        Game game;

        private void start_game()
        {
            /* starts the game */

            timer1.Enabled = true;
            this.KeyPreview = true;

            game = new Game(WINDOW_HEIGHT, WINDOW_WIDTH);
            game.state = Game.GameSate.Started;

            this.scoreLabel.Text = game.score.ToString();
            
            playButton.Visible = false;
            playAgainButton.Visible = false;
            exitButton.Visible = false;
            pauseButton.Visible = true;

            scoreLabel.Visible = true;
            highScoresList.Visible = false;
            scoreAfterDeath.Visible = false;
        }

        private void end_game()
        {
            /* ends the game */

            timer1.Enabled = false;

            scoreAfterDeath.Visible = true;
            scoreLabel.Visible = false;
            warningLabel.Visible = false;
            playAgainButton.Visible = true;
            exitButton.Visible = true;
            pauseButton.Visible = false;

            scoreAfterDeath.Text = "TVÉ SKÓRE JE \n" + game.score.ToString();       
        }
        public Form1()
        {
            game = new Game(WINDOW_HEIGHT, WINDOW_WIDTH);

            start_background = new Bitmap("start_background.png");
            end_background = new Bitmap("end_background.png");

            InitializeComponent();

            this.highScoresList.Text = HighscoreHandler.return_high_scores();
            
            scoreLabel.BackColor = Color.Transparent;
            scoreLabel.Visible = false;

            highScoresList.BackColor = Color.Transparent;
            highScoresList.ForeColor = Color.Red;

            scoreAfterDeath.Visible = false;
            scoreAfterDeath.BackColor = Color.Transparent;
            scoreAfterDeath.ForeColor = Color.Red;

            warningLabel.Visible = false;
            warningLabel.BackColor = Color.Transparent;
            warningLabel.ForeColor = Color.Red;

            playButton.ForeColor = Color.Red;
            playButton.BackColor = Color.Black;
            playButton.FlatStyle = FlatStyle.Flat;
            
            playAgainButton.Visible = false;
            playAgainButton.ForeColor = Color.Red;
            playAgainButton.BackColor = Color.Black;
            playAgainButton.FlatStyle = FlatStyle.Flat;

            exitButton.Visible = false;
            exitButton.ForeColor = Color.Red;
            exitButton.BackColor = Color.Black;
            exitButton.FlatStyle = FlatStyle.Flat;

            pauseButton.Visible = false;
            pauseButton.ForeColor = Color.Red;
            pauseButton.BackColor = Color.Transparent;
            pauseButton.FlatStyle = FlatStyle.Flat;

            this.Text = TEXT_IN_UPPER_BAR;

            KeyPreview = true;
            this.DoubleBuffered = true; // the objects don't blick
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.move(right_pressed, left_pressed);
            Invalidate(); // redraws the screen
            scoreLabel.Text = game.score.ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /* draws on the screen based on game_state */

            if(game.state == Game.GameSate.NotStarted)
            {
                e.Graphics.DrawImage(start_background, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            }
            else if (game.state == Game.GameSate.Started)
            {
                game.draw(e);
                if (game.warning == true)
                {
                    warningLabel.Visible = true;
                }
                else
                {
                    warningLabel.Visible = false;
                }
            }
            else if (game.state == Game.GameSate.End)
            {
                e.Graphics.DrawImage(end_background, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
                end_game();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                left_pressed = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                right_pressed = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left_pressed = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right_pressed = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /* needs to be here - if button is present, only Form1_keyDown method won't work */

            if (keyData == Keys.Left)
            {
                left_pressed = true;
            }
            if (keyData == Keys.Right)
            {
                right_pressed = true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            start_game();
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            start_game();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
            }
        }
    }
}
