using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private bool game_started = false;
        private bool left_pressed = false;
        private bool right_pressed = false;

        Game game;

        public Form1()
        {

            game = new Game();

            InitializeComponent();
            this.label1.Text = game.score.ToString();
            label1.BackColor = Color.Transparent;

            KeyPreview = true;
            this.Text = TEXT_IN_UPPER_BAR;
            this.DoubleBuffered = true; // the objects don't blick
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.state = Game.GameSate.Started;
            timer1.Enabled = true;
            button1.Visible = false;
            this.KeyPreview = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.move(right_pressed, left_pressed);
            Invalidate();
            label1.Text = game.score.ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (game.state == Game.GameSate.Started)
            {
                game.draw(e);
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
    }
}
