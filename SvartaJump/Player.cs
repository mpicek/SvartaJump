using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvartaJump
{
    internal class Player : MoveableObject
    {
        private int speed = 0;
        private int time_from_last_jump = 0;
        private int jump_time = 33;
        private Direction direction_of_move = Direction.Up; // 1 = up, 0 = down
        private int last_x;
        private int last_y;
        private int MAX_HEIGHT_OF_PLAYER = 300;
        public int score = 0;
        public bool dead = false;

        public Player(string img_name, int _window_width, int _window_height, int _scale_img, int _x, int _y)
        {
            img = new Bitmap(img_name);
            scale_img = _scale_img;
            WINDOW_WIDTH = _window_width;
            WINDOW_HEIGHT = _window_height;
            WIDTH = img.Width/scale_img;
            HEIGHT = img.Height/scale_img;
            X_CHANGE_PER_TICK = 30;
            x = _x;
            y = _y;
            last_x = _x;
            last_y = _y;
        }

        private enum Direction
        {
            Down = 1,
            Up = -1
        }

        public override void draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, x, y, img.Width / scale_img, img.Height / scale_img);
        }

        public int X { 
            get
            {
                return x; 
            }
            set {
                if(value + WIDTH < WINDOW_WIDTH && value >= 0)
                {
                    x = value;
                }
            }
        }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed;
            set
            {
                speed = value;
            }
        }

        public override void move(bool left_pressed, bool right_pressed, float gravity, List<MoveableObject> all_objects)
        {
            //direction of move :
                //  1 ... down
                // -1 ... up
            bool collided = false;
            last_x = x;
            last_y = y;
            if (left_pressed) this.X -= X_CHANGE_PER_TICK;
            if (right_pressed) this.X += X_CHANGE_PER_TICK;

            //TODO: death 
            if(y + HEIGHT > WINDOW_HEIGHT)
            {
                dead = true;
                return;
            }

            this.Speed = (int)(gravity * time_from_last_jump * time_from_last_jump);

            //speed == 0 means we ar at the top of the jump trajectory
            if (this.Speed <= 0)
            {
                direction_of_move = Direction.Down;
            }
            if (this.y >= MAX_HEIGHT_OF_PLAYER || direction_of_move == Direction.Down)
            {
                if(direction_of_move == Direction.Up)
                    this.Y -= this.Speed;
                else
                    this.Y += this.Speed;

            }

            if (direction_of_move == Direction.Up)
            {
                score += this.Speed;
            }

            //if falling:
            if (direction_of_move == Direction.Down)
            {
                collided = collision_with_block(all_objects);
            }
            else
            {
                collided = false;
            }

            // moving all objects down
            if(this.y < MAX_HEIGHT_OF_PLAYER && direction_of_move == Direction.Up)
            {
                foreach(MoveableObject m in all_objects)
                {
                    if(m.GetType() != typeof(Player))
                    {
                        //all move down (always - look at the first condition)
                        m.y += this.Speed;

                    }
                        
                }

            }
            
            
            if (collided)
            {
                direction_of_move = Direction.Up;
                time_from_last_jump = jump_time;
            }
            if(direction_of_move == Direction.Down)
                time_from_last_jump++;
            if (direction_of_move == Direction.Up)
                time_from_last_jump--;


        }

        private bool collision_with_block(List<MoveableObject> all_objects)
        {
            foreach (MoveableObject m in all_objects)
            {
                if (m.GetType() == typeof(Block))
                {
                    //last time it didn't collide, now it does AND player was above the platform = COLLISION
                    if(!player_overlaps_with_moveable_object(last_x, last_y, m) && 
                        player_overlaps_with_moveable_object(this.x, this.y, m) &&
                        last_y + this.HEIGHT < m.y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool player_overlaps_with_moveable_object(int player_x, int player_y, MoveableObject m)
        {
            //one rectangle is on the right one on the left - they don't overlap
            if (player_x + this.WIDTH < m.x || m.x + m.WIDTH < player_x)
            {
                return false;
            }

            //one rectangle is above the other one
            if (player_y + this.HEIGHT < m.y || m.y + m.HEIGHT < player_y)
            {
                return false;
            }
                

            return true;
        }
    }
}
