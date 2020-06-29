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
        private int X_CHANGE_PER_TICK = 30;
        private float GRAVITY = 1;
        private string TEXT_IN_UPPER_BAR = "Svarta Jump";
        private int window_height = 900;
        private int window_width = 600;
        private bool game_started = false;
        private bool left = false;
        private bool right = false;
        int last_fallen = 0;
        int direction = 1;
        int time_now = 0;

        private Player player = new Player(600, 900);

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            this.Text = TEXT_IN_UPPER_BAR;
            player.X = 1;
            player.Y = 1;
            this.DoubleBuffered = true;
        }

        private void movement()
        {
            if (left) player.X -= X_CHANGE_PER_TICK;
            if (right) player.X += X_CHANGE_PER_TICK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game_started = true;
            timer1.Enabled = true;
            button1.Visible = false;
            this.KeyPreview = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            movement();

            player.Speed = (int)(GRAVITY * time_now*time_now/20);

            //this.Text = player.Speed.ToString();
            player.Y += player.Speed*direction;
            if(player.Y > window_height-160)
            {
                player.Y = window_height-160;
                direction = -1;
            }
            //player.Y += player.Speed;
            Invalidate();
            time_now += direction;
            if(player.Speed <= 0)
            {
                direction = 1;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (game_started)
            {
                Bitmap background = new Bitmap("background.png");
                e.Graphics.DrawImage(background, 0, 0, window_width, window_height);
                Bitmap block = new Bitmap("block.png");
                e.Graphics.DrawImage(block, 300, 400, block.Width/5, block.Height/5);
                
                e.Graphics.DrawRectangle(new Pen(Brushes.Red, 5), new Rectangle(300, 400, block.Width/5, block.Height/5));
                
                //Bitmap s = new Bitmap("svarta.png");
                //e.Graphics.DrawImage(s, 0, 0, s.Width / 2, s.Height / 2);
                //e.Graphics.FillRectangle(Brushes.BlueViolet, player.X, player.Y, 50, 50);
                Bitmap svarta = new Bitmap("svarta.png");
                e.Graphics.DrawImage(svarta, player.X, player.Y, svarta.Width/2, svarta.Height/2);
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right =false;
            }
        }
    }
}
