using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvartaJump
{
    class Block : MoveableObject
    {
        public Block(string img_name, int _window_width, int _window_height, int _scale_img, int _x_change_per_tick, int _x, int _y)
        {
            //constructor with static x, y set manually
            img = new Bitmap(img_name);
            scale_img = _scale_img;
            WINDOW_WIDTH = _window_width;
            WINDOW_HEIGHT = _window_height;
            WIDTH = img.Width / scale_img;
            HEIGHT = img.Height / scale_img;
            X_CHANGE_PER_TICK = _x_change_per_tick;

            x = _x;
            y = _y;
        }

        public Block(string img_name, int _window_width, int _window_height, int _scale_img, int _x_change_per_tick, int score){
            img = new Bitmap(img_name);
            scale_img = _scale_img;
            WINDOW_WIDTH = _window_width;
            WINDOW_HEIGHT = _window_height;
            WIDTH = img.Width / scale_img;
            HEIGHT = img.Height / scale_img;

            Random r = new Random();
            y = r.Next(-300, -100); // negative so that it is invisible (above screen)
            x = r.Next(0, WINDOW_WIDTH - WIDTH);

            if(score < 5000)
            {
                X_CHANGE_PER_TICK = 0;
            }
            else
            {
                X_CHANGE_PER_TICK = r.Next(-score/2000, score/2000);
            }
            

        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value + WIDTH < WINDOW_WIDTH && value >= 0)
                {
                    x = value;
                }
            }
        }

        public int Y { get => y; set => y = value; }

        public override void move(bool left_pressed, bool right_pressed, float gravity, List<MoveableObject> all_objects)
        {
            x += X_CHANGE_PER_TICK;
            if(x < 0 || x + WIDTH > WINDOW_WIDTH)
            {
                X_CHANGE_PER_TICK = -X_CHANGE_PER_TICK;
            }
        }

        public override void draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, x, y, img.Width / scale_img, img.Height / scale_img);
            e.Graphics.DrawRectangle(new Pen(Brushes.Red, 5), new Rectangle(x, y, img.Width / scale_img, img.Height / scale_img));
        }
    }
}
