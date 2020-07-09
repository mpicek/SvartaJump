using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SvartaJump
{
    class Game
    {
        private float GRAVITY = 0.05F;
        private int WINDOW_HEIGHT = 900;
        private int WINDOW_WIDTH = 600;
        private int SCALE_PLAYER = 2;
        private int SCALE_BLOCK = 7;
        public int score = 0;
        private List<MoveableObject> moveable_objects = new List<MoveableObject>();
        private Player svarta;

        public enum GameSate
        {
            NotStarted,
            Started,
            Paused,
            End
        }

        public GameSate state = GameSate.NotStarted;

        public Game()
        {
            state = GameSate.NotStarted;
            svarta = new Player("svarta.png", 600, 900, SCALE_PLAYER, 100, 400);
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 100, 100));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 100, 350));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 450, 600));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 300, 600));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 150, 600));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 0, 600));
        }

        public void draw(PaintEventArgs e)
        {
            Bitmap background = new Bitmap("rsz_background5.png");
            e.Graphics.DrawImage(background, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            svarta.draw(e);
            foreach (MoveableObject m in moveable_objects)
            {
                m.draw(e);
            }
        }

        public void move(bool right_pressed, bool left_pressed)
        {
            bool all_blocks_displayed = true;

            svarta.move(left_pressed, right_pressed, GRAVITY, moveable_objects);
            foreach (MoveableObject m in moveable_objects)
            {
                m.move(left_pressed, right_pressed, GRAVITY, moveable_objects);
            }

            List<int> to_be_deleted = new List<int>();
            for (int i = 0; i < moveable_objects.Count; i++)
            {
                if (moveable_objects[i].y < 0)
                {
                    all_blocks_displayed = false;
                }

                if (moveable_objects[i].y > WINDOW_HEIGHT)
                {
                    to_be_deleted.Add(i);
                }

            }
            for (int i = to_be_deleted.Count - 1; i >= 0; i--)
            {
                moveable_objects.RemoveAt(to_be_deleted[i]);
            }

            if (all_blocks_displayed)
            {
                moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, score));
            }

            score = svarta.score;
            
            if(svarta.dead == true)
            {
                state = GameSate.End;
            }
        }

    }
}
