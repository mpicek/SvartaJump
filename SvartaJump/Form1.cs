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
            game = new Game(WINDOW_HEIGHT, WINDOW_WIDTH);
            game.state = Game.GameSate.Started;
            this.label1.Text = game.score.ToString();
            timer1.Enabled = true;
            button1.Visible = false;
            button2.Visible = false;
            this.KeyPreview = true;
            label2.Visible = false;
            label1.Visible = true;
            label3.Visible = false;
        }

        private void end_game()
        {
            timer1.Enabled = false;
            label3.Visible = true;
            label1.Visible = false;
            button2.Visible = true;

            //label3.Text = "";
            //if (is_new_high_score(game.score))
              //  label3.Text += "VYTVOŘIL JSI NOVÉ HIGHSCORE!";
            label3.Text = "TVÉ SKÓRE JE \n" + game.score.ToString();       
        }
        public Form1()
        {
            game = new Game(WINDOW_HEIGHT, WINDOW_WIDTH);

            start_background = new Bitmap("start_background.png");
            end_background = new Bitmap("end_background.png");

            InitializeComponent();
            this.label2.Text = HighscoreHandler.return_high_scores();

            
            label1.BackColor = Color.Transparent;
            label1.Visible = false;

            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.Red;

            label3.Visible = false;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.Red;

            button1.ForeColor = Color.Red;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Flat;
            
            button2.Visible = false;
            button2.ForeColor = Color.Red;
            button2.BackColor = Color.Black;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Text = "ZNOVA!";

            this.Text = TEXT_IN_UPPER_BAR;

            KeyPreview = true;
            this.DoubleBuffered = true; // the objects don't blick
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start_game();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.move(right_pressed, left_pressed);
            Invalidate();
            label1.Text = game.score.ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(game.state == Game.GameSate.NotStarted)
            {
                e.Graphics.DrawImage(start_background, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            }
            else if (game.state == Game.GameSate.Started)
            {
                game.draw(e);
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
                right_pressed =false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            start_game();
        }
    }
}
