using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public partial class FormPacman : Form
    {
        private PacManClass pacMan;
        private PictureBox ghostPictureBox1, ghostPictureBox2, ghostPictureBox3;
        private GhostClass ghost1, ghost2, ghost3;//instances of ghost class!
       

        // private List<PictureBox> FoodList = new List<PictureBox>(); replaced with custom list!
        private CustomList<PictureBox> FoodList = new CustomList<PictureBox>();

        private string[] directionsArray = { "Right", "Left", "Up", "Down" };
        private Random rnd = new Random();
        private int lives = 3;
 
        private List<Label> walls = new List<Label>();

        public FormPacman()
        {
            InitializeComponent();
            pacMan = new PacManClass(pbPacman, imageList1, walls, FoodList);
            CreateFood(); 
            CreateWall();
            CreateGhost();
            
        }
        private void FormPacman_Load(object sender, EventArgs e)
        {
            timerPacmanDirection.Start();
            timerGhostDirection.Start();
        }

        private void timerPacmanDirection_Tick(object sender, EventArgs e)
        {
            pacMan.Move();
            CheckFoodCollision();
            lblScore.Text = pacMan.Score.ToString();
            CheckWinCondition();
        }
        private void timerGhostDirection_Tick(object sender, EventArgs e)
        {
            ghost1.Move();
            ghost2.Move();
            ghost3.Move();

            CheckGhostCollision();
        }

        private void CheckFoodCollision()
        {
            List<PictureBox> itemsToRemove = new List<PictureBox>();

            foreach (var Food in FoodList)
            {
                if (pacMan.pacManPictureBox.Bounds.IntersectsWith(Food.Bounds))
                {
                    bool shouldRemove = pacMan.EatFood(Food);
                    if (shouldRemove)
                    {
                        itemsToRemove.Add(Food);
                    }
                }
            }

            // remove the collected items after iteration
            foreach (var item in itemsToRemove)
            {
                FoodList.Remove(item);
                this.Controls.Remove(item); 
            }
        }



        private void FormPacman_KeyDown(object sender, KeyEventArgs e)
        {
            pacMan.ChangeDirection(e);
        }

       
        private void Win()
        {
            timerPacmanDirection.Stop();
            timerGhostDirection.Stop();
            DialogResult result = MessageBox.Show("Congratulations!" + "\n Want to play again?", "Pacman", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                ResetEverything();
                timerGhostDirection.Start();
            }
            else if (result == DialogResult.Cancel)
            {
                ApplicationExit();
            }
        }
        private void CheckWinCondition()
        {
            if (pacMan.Score == 198)
            {
                Win();
            }
        }

        private void GoToReturn()
        {
            lives--;
            UpdateLivesDisplay();
            if (lives <= 0)
            {
                GameOver();
            }
            else
            {
                pacMan.Reset();
            }
        }

        private void GameOver()
        {
            timerPacmanDirection.Stop();
            timerGhostDirection.Stop();
            DialogResult result = MessageBox.Show("Your score=> " + pacMan.Score + "\n Want to Play Again?", "Pacman", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                ResetEverything();

            }
            else if (result == DialogResult.Cancel)
            {
                ApplicationExit();
            }
        }

        public void UpdateScore(int score)
        {

            lblScore.Text = "Score: " + score.ToString();
        }




        private void UpdateLivesDisplay()
        {
            pblives1.Visible = lives > 0;
            pblives2.Visible = lives > 1;
            pblives3.Visible = lives > 2;
        }

       

        private void ApplicationExit()
        {
            Application.Exit();
        }

      
     
        public void UpdateLives(int lives)
        {

            pblives1.Visible = lives > 0;
            pblives2.Visible = lives > 1;
            pblives3.Visible = lives > 2;
        }

        private void ResetEverything()
        {
            timerPacmanDirection.Start();
            timerGhostDirection.Start();
            lives = 3;
            UpdateLivesDisplay();
            FoodList.Clear();
            Controls.Clear();
            InitializeComponent();
            pacMan.Reset();
            CreateFood();
            CreateWall();
            CreateGhost();

            this.KeyPreview = true;
            this.Focus();

        }

        private void CheckGhostCollision()
        {
            if (ghost1.CheckCollisionWithPacMan(pacMan.pacManPictureBox) ||
                ghost2.CheckCollisionWithPacMan(pacMan.pacManPictureBox) ||
                ghost3.CheckCollisionWithPacMan(pacMan.pacManPictureBox))
            {
                GoToReturn();
            }
        }

        private Label CreateWallLabel(int x, int y)
        {
            Label wall = new Label();
            wall.Name = "Wall" + x + y;
            wall.Size = new Size(15, 15);
            wall.Location = new Point(x, y);
            wall.BackColor = Color.Blue;
            wall.BringToFront();
            return wall;
        }

        public void CreateFood() //creates a grid of dots on the form
        {
            for (int i = 50; i <= 1050; i += 50)
            {
                for (int j = 50; j <= 500; j += 50)
                {
                    PictureBox Food = new PictureBox();

                    Food.Name = "Food" + i + j;
                    Food.Size = new Size(20, 20);
                    Food.Location = new Point(i, j);
                    Food.BackgroundImageLayout = ImageLayout.Stretch;
                    Food.BackColor = Color.Transparent;
                    Food.BackgroundImage = Properties.Resources.Food;
                    Food.BackgroundImageLayout = ImageLayout.Stretch;
                   
                    FoodList.Add(Food);
                    this.Controls.Add(Food);
                }
            }
            FoodList[0].Visible = false;
            this.Controls.Remove(FoodList[0]);
            this.Controls.Remove(FoodList[65]);
            this.Controls.Remove(FoodList[64]);
            this.Controls.Remove(FoodList[63]);
            this.Controls.Remove(FoodList[75]);
            this.Controls.Remove(FoodList[85]);
            this.Controls.Remove(FoodList[95]);
            this.Controls.Remove(FoodList[105]);
            this.Controls.Remove(FoodList[115]);
            this.Controls.Remove(FoodList[125]);
            this.Controls.Remove(FoodList[124]);
            this.Controls.Remove(FoodList[123]);
        }

        public void CreateWall()
        {
            for (int i = 0; i <= 1100; i += 20)
            {
                for (int j = 0; j <= 540; j += 20)
                {
                    if (i == 0 || j == 0 || i == 1100 || j == 540)
                    {
                        Label wall = CreateWallLabel(i, j);
                        walls.Add(wall);
                        this.Controls.Add(wall);
                    }

                    if (((i >= 340 && i <= 360) && (j >= 200 && j <= 300)) || ((j >= 300 && j <= 320) && (i >= 340 && i <= 660)) || ((i >= 640 && i <= 660) && (j <= 300 && j >= 200)))
                    {
                        Label wall2 = CreateWallLabel(i, j);
                        walls.Add(wall2);
                        this.Controls.Add(wall2);
                    }
                }
            }
        }


       



        public void CreateGhost()
        {
            // Initialise and set properties for ghostPictureBox1
            ghostPictureBox1 = new PictureBox();
            ghostPictureBox1.Location = new Point(390, 240); // starting position
            ghostPictureBox1.BackgroundImage = imageListGhost.Images[0]; 
            ghostPictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            ghostPictureBox1.Size = new Size(45, 45);
            ghostPictureBox1.BackColor = Color.Transparent;
            this.Controls.Add(ghostPictureBox1);
            ghostPictureBox1.BringToFront();

            // Initialise and set properties for ghostPictureBox2
            ghostPictureBox2 = new PictureBox();
            ghostPictureBox2.Location = new Point(590, 240); // starting position
            ghostPictureBox2.BackgroundImage = imageListGhost.Images[1];
            ghostPictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            ghostPictureBox2.Size = new Size(45, 45);
            ghostPictureBox2.BackColor = Color.Transparent;
            this.Controls.Add(ghostPictureBox2);
            ghostPictureBox2.BringToFront();
            // Initialise and set properties for ghostPictureBox3
            ghostPictureBox3 = new PictureBox();
            ghostPictureBox3.Location = new Point(490, 240); // starting position
            ghostPictureBox3.BackgroundImage = imageListGhost.Images[2];
            ghostPictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            ghostPictureBox3.Size = new Size(45, 45);
            ghostPictureBox3.BackColor = Color.Transparent;
            this.Controls.Add(ghostPictureBox3);
            ghostPictureBox3.BringToFront();
            ghost1 = new GhostClass(ghostPictureBox1, walls);
            ghost2 = new GhostClass(ghostPictureBox2, walls);
            ghost3 = new GhostClass(ghostPictureBox3, walls);
        }

       

        //Ghost Move
        public void GhostMove(string directionGhost, PictureBox ghost) // movement of ghosts+ checks for collisions with pacman 
        {

            if (directionGhost == "Up")
            {
                if (50 <= ghost.Top && !(ghost.Top == 340 && (ghost.Left == 340 || ghost.Left == 390 || ghost.Left == 440 || ghost.Left == 490 || ghost.Left == 540 || ghost.Left == 590 || ghost.Left == 640)))
                {
                    ghost.Top -= 50;

                    if (pbPacman.Top < ghost.Bottom && pbPacman.Bottom > ghost.Top && pbPacman.Left <= ghost.Right && pbPacman.Right >= ghost.Left)
                    {
                        GoToReturn();
                    }
                }
            }
            else if (directionGhost == "Down")
            {

                if (this.ClientSize.Height - 30 >= ghost.Bottom && !(ghost.Top == 140 && (ghost.Left == 640 && ghost.Right == 685)) && !(ghost.Top == 140 && (ghost.Left == 340 && ghost.Right == 385)) && !(ghost.Top == 240 && (ghost.Left == 390 || ghost.Left == 440 || ghost.Left == 490 || ghost.Left == 540 || ghost.Left == 590)))
                {
                    ghost.Top += 50;

                    if (pbPacman.Top < ghost.Bottom && pbPacman.Bottom > ghost.Top && pbPacman.Left <= ghost.Right && pbPacman.Right >= ghost.Left)
                    {
                        GoToReturn();
                    }
                }

            }
            else if (directionGhost == "Right")
            {
                if (1050 >= ghost.Right && !(ghost.Right == 335 && (ghost.Top == 190 || ghost.Top == 240 || ghost.Top == 290)) && !(ghost.Right == 635 && (ghost.Top == 190 || ghost.Top == 240)))
                {
                    ghost.Left += 50;

                    if (pbPacman.Top <= ghost.Bottom && pbPacman.Bottom >= ghost.Top && pbPacman.Left < ghost.Right && pbPacman.Right > ghost.Left)
                    {
                        GoToReturn();
                    }
                }

            }
            else if (directionGhost == "Left")
            {
                if (50 <= ghost.Left && !(ghost.Right == 735 && (ghost.Top == 190 || ghost.Top == 240 || ghost.Top == 290)) && !(ghost.Right == 435 && (ghost.Top == 190 || ghost.Top == 240)))
                {
                    ghost.Left -= 50;

                    if (pbPacman.Top <= ghost.Bottom && pbPacman.Bottom >= ghost.Top && pbPacman.Left < ghost.Right && pbPacman.Right > ghost.Left)
                    {
                        GoToReturn();
                    }

                }

            }

        }
    }
}