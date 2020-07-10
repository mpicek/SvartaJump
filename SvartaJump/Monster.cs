using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvartaJump
{
    class Monster : MoveableObject
    {
        public Monster(string img_name, int _window_width, int _window_height, int _scale_img, int score)
        {
            img = new Bitmap(img_name);
            scale_img = _scale_img;
            WINDOW_WIDTH = _window_width;
            WINDOW_HEIGHT = _window_height;
            WIDTH = img.Width / scale_img;
            HEIGHT = img.Height / scale_img;

            Random r = new Random();
            y = r.Next(-300, -200); // negative so that it is invisible (above screen)
            x = r.Next(0, WINDOW_WIDTH - WIDTH);

            X_CHANGE_PER_TICK = 0;

            collision_energy = -1;
        }

        public override void draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, x, y, WIDTH, HEIGHT);
        }

        public override int energy_on_collision()
        {
            return collision_energy;
        }

        public override void move(bool left_pressed, bool right_pressed, float gravity, List<MoveableObject> all_objects)
        {
        }
    }
}
