using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvartaJump
{
    abstract class MoveableObject
    {
        protected int WINDOW_WIDTH;
        protected int WINDOW_HEIGHT;
        public int WIDTH;
        public int HEIGHT;
        public int x;
        public int y;
        protected int X_CHANGE_PER_TICK;
        protected int scale_img;
        protected int collision_energy;
        public Game.GameMode block_mode;
        public Bitmap img;
        public abstract void move(bool left_pressed, bool right_pressed, float gravity, List<MoveableObject> all_objects);
        public abstract void draw(PaintEventArgs e);
        public abstract int energy_on_collision();

    }
}
