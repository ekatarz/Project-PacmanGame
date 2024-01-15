using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class PacManClass
    {
        public CustomList<PictureBox> foodList; // Class field
        public PictureBox pacManPictureBox;
        private ImageList imageList;
        private string direction;
        private int score;
        private List<Label> walls;//ADT! 

        public PacManClass(PictureBox pictureBox, ImageList images, List<Label> walls, CustomList<PictureBox> foodList)
        {
            this.pacManPictureBox = pictureBox;
            this.imageList = images;
            this.walls = walls;
            this.foodList = foodList;

            direction = "Right";
            score = 0;
            pacManPictureBox.BackgroundImage = imageList.Images[0];
            pacManPictureBox.BackColor = Color.Transparent;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void Move()
        {
            switch (direction)
            {
                case "Up":
                    MoveUp();
                    break;
                case "Down":
                    MoveDown();
                    break;
                case "Left":
                    MoveLeft();
                    break;
                case "Right":
                    MoveRight();
                    break;
            }
        }

        private bool CanMove(int x, int y)
        {
            Rectangle nextPosition = new Rectangle(x, y, pacManPictureBox.Width, pacManPictureBox.Height);
            foreach (var wall in walls)
            {
                if (nextPosition.IntersectsWith(wall.Bounds))
                {
                    return false; // collision detected
                }
            }
            return true; // no collision
        }

        private void MoveUp()
        {
            if (pacManPictureBox != null && pacManPictureBox.Parent != null)
            {
                int nextTop = pacManPictureBox.Top - 50;
                if (nextTop >= 0 && CanMove(pacManPictureBox.Left, nextTop))
                {
                    pacManPictureBox.Top = nextTop;
                }
            }
        }

        private void MoveDown()
        {
            if (pacManPictureBox != null && pacManPictureBox.Parent != null)
            {
                int nextBottom = pacManPictureBox.Bottom + 50;
                if (nextBottom <= pacManPictureBox.Parent.ClientSize.Height && CanMove(pacManPictureBox.Left, pacManPictureBox.Top + 50))
                {
                    pacManPictureBox.Top += 50;
                }
            }
        }

        private void MoveLeft()
        {
            if (pacManPictureBox != null && pacManPictureBox.Parent != null)
            {
                int nextLeft = pacManPictureBox.Left - 50;
                if (nextLeft >= 0 && CanMove(nextLeft, pacManPictureBox.Top))
                {
                    pacManPictureBox.Left = nextLeft;
                }
            }
        }

        private void MoveRight()
        {
            
            if (pacManPictureBox != null && pacManPictureBox.Parent != null)
            {
                int nextRight = pacManPictureBox.Right + 50;
                if (nextRight <= pacManPictureBox.Parent.ClientSize.Width && CanMove(pacManPictureBox.Left + 50, pacManPictureBox.Top))
                {
                    pacManPictureBox.Left += 50;
                }
            }
        }


        public bool EatFood(PictureBox Food)
        {
            if (Food.Visible)
            {
                score++;
                Food.Visible = false;
                return true; //  food should be removed
            }
            return false; // no need to remove
        }



        public void ChangeDirection(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                direction = "Up";
                pacManPictureBox.BackgroundImage = imageList.Images[3];
            }
            else if (e.KeyCode == Keys.Down)
            {
                direction = "Down";
                pacManPictureBox.BackgroundImage = imageList.Images[2];
            }
            else if (e.KeyCode == Keys.Left)
            {
                direction = "Left";
                pacManPictureBox.BackgroundImage = imageList.Images[1];
            }
            else if (e.KeyCode == Keys.Right)
            {
                direction = "Right";
                pacManPictureBox.BackgroundImage = imageList.Images[0];
            }
        }

        public void Reset()
        {
            pacManPictureBox.Location = new Point(37, 37);
            pacManPictureBox.BackgroundImage = imageList.Images[0];
            pacManPictureBox.BackColor = Color.Transparent;
            direction = "Right";
            //score = 0;

        }

    }
}
