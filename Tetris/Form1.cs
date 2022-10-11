using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public enum Block
    {
        Yellow,
        Red, 
        Orange, 
        Blue, 
        Purple, 
        Green, 
        Turquoise
    }

    public partial class Form1 : Form
    {
        
        bool gameOver = false; 

        int blockSize = 30;
        int blockSize_Margin = 1;
        
        Point spawnPosition;

        int panelWidth = 10 + 2, panelHeight = 20 + 2; // Count for walls

        Panel currentBlock = new Panel();
        Block currentType;

        int[,] block_placeHolders;

        bool collision = false;

        int[,] positions_Yellow =
        {
            { 1, 1 },
            { 1, 1 }
        };
        int[,] positions_Red =
        {
            { 1, 1, 0 },
            { 0, 1, 1 }
        };
        int[,] positions_Orange =
        {
            { 0, 0, 0, 1 },
            { 1, 1, 1, 1 }
        };
        int[,] positions_Blue =
        {
            { 1, 0, 0, 0 },
            { 1, 1, 1, 1 }
        };
        int[,] positions_Purple =
        {
            { 0, 1, 0 },
            { 1, 1, 1 }
        };
        int[,] positions_Green =
        {
            { 0, 1, 1 },
            { 1, 1, 0 }
        };
        int[,] positions_Turquoise =
        {
            { 1, 1, 1, 1 }
        };

        PictureBox[,] blockPositions = new PictureBox[20, 10];

        int movingInterval = 1500; //milliseconds
        int timerInterval = 100;
        int timerCount = 0;

        public Form1()
        {
            InitializeComponent();

        }

        //Init
        private void Form1_Load(object sender, EventArgs e)
        {

            spawnPosition = new Point(blockSize*6, blockSize*3);
            playArea.Width = panelWidth*blockSize;
            playArea.Height = panelHeight*blockSize;

            currentBlock.Parent = playArea;
            currentBlock.BackColor = Color.Black;
            currentBlock.Visible = true;
            currentBlock.BringToFront();

            block_placeHolders = new int[panelHeight - 2, panelWidth - 2];
            for(int y = 0; y < block_placeHolders.GetLength(0); y++) 
            {
                for(int x = 0; x < block_placeHolders.GetLength(1); x++)
                {
                    block_placeHolders[y, x] = 0; 
                }
            }

            for(int y = 0; y < blockPositions.GetLength(0); y++) 
            {
                for(int x = 0; x < blockPositions.GetLength(1); x++)
                {
                    blockPositions[y, x] = new PictureBox();
                    blockPositions[y, x].Parent = playArea;

                    //(+1 to count for walls on left side and on the top)
                    blockPositions[y, x].SetBounds(blockSize*(x+1), blockSize*(y+1), blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                    blockPositions[y, x].Visible = false;
                }
            }
            
            CreatePlayArea();
            SpawnBlock(Block.Red);

            //CheckPlayArea();
        }

        //Update function
        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }


        //Update function for moving the current block etc.  
        private void movingTimer_Tick(object sender, EventArgs e)
        {

            timerCount++;  
            // Go into if-statement every time timerCount goes up to its limit
            if(timerCount >= (movingInterval / timerInterval))
            {
                timerCount = 0;

                if (!gameOver)
                {
                    CheckCollision();

                    if (!collision) //Move block down if it hasn't collided with anything yet
                    {
                        int x = currentBlock.Location.X;
                        int y = currentBlock.Location.Y;

                        currentBlock.Location = new Point(x, y + blockSize);
                    }

                    collision = false;
                }
            }

        }

        void SpawnBlock(Block type)
        {

            switch(type)
            {
                case Block.Yellow:
                    currentType = type;
                    currentBlock.Controls.Clear(); //Remove the current children of the object
                    currentBlock.SetBounds(spawnPosition.X - blockSize, spawnPosition.Y, blockSize * 2, blockSize * 2);

                    for (int y = 0; y < positions_Yellow.GetLength(0); y++)
                    {
                        for (int x = 0; x < positions_Yellow.GetLength(1); x++)
                        {
                            if (positions_Yellow[y, x] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * x, blockSize * y, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Yellow;
                            }
                        }
                    }

                    break;
                case Block.Red:
                    currentType = type;
                    currentBlock.Controls.Clear();
                    currentBlock.SetBounds(spawnPosition.X - blockSize, spawnPosition.Y, blockSize * 3, blockSize * 2);

                    for(int i = 0; i < positions_Red.GetLength(0); i++)
                    {
                        for(int j = 0; j < positions_Red.GetLength(1); j++)
                        {
                            if(positions_Red[i, j] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * j, blockSize * i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Red;
                            }
                        }
                    }
                    break;
                case Block.Orange:
                    currentType = type;
                    currentBlock.Controls.Clear();
                    currentBlock.SetBounds(spawnPosition.X - blockSize * 2, spawnPosition.Y, blockSize * 4, blockSize * 2);

                    for (int i = 0; i < positions_Orange.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Orange.GetLength(1); j++)
                        {
                            if (positions_Orange[i, j] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * j, blockSize * i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Orange;
                            }
                        }
                    }
                    break;
                case Block.Blue:
                    currentType = type;
                    currentBlock.Controls.Clear();
                    currentBlock.SetBounds(spawnPosition.X - blockSize * 2, spawnPosition.Y, blockSize * 4, blockSize * 2);

                    for (int i = 0; i < positions_Blue.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Blue.GetLength(1); j++)
                        {
                            if (positions_Blue[i, j] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * j, blockSize * i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Blue;
                            }
                        }
                    }
                    break;
                case Block.Purple:
                    currentType = type;
                    currentBlock.Controls.Clear();
                    currentBlock.SetBounds(spawnPosition.X - blockSize, spawnPosition.Y, blockSize * 3, blockSize * 2);

                    for (int i = 0; i < positions_Purple.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Purple.GetLength(1); j++)
                        {
                            if (positions_Purple[i, j] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * j, blockSize * i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Purple;
                            }
                        }
                    }
                    break;
                case Block.Green:
                    currentType = type;
                    currentBlock.Controls.Clear();
                    currentBlock.SetBounds(spawnPosition.X - blockSize, spawnPosition.Y, blockSize * 4, blockSize * 2);

                    for (int i = 0; i < positions_Green.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Green.GetLength(1); j++)
                        {
                            if (positions_Green[i, j] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * j, blockSize * i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Green;
                            }
                        }
                    }
                    break;
                case Block.Turquoise:
                    currentType = type;
                    currentBlock.Controls.Clear();
                    currentBlock.SetBounds(spawnPosition.X - blockSize * 2, spawnPosition.Y, blockSize * 4, blockSize);

                    for (int i = 0; i < positions_Turquoise.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Turquoise.GetLength(1); j++)
                        {
                            if (positions_Turquoise[i, j] == 1)
                            {
                                PictureBox pb = new PictureBox();
                                pb.SetBounds(blockSize * j, blockSize * i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);
                                pb.Parent = currentBlock;
                                pb.Visible = true;
                                pb.BackColor = Color.Turquoise;
                            }
                        }
                    }
                    break; 
                default:
                    Console.WriteLine("Error in code in SpawnBlock()");
                    break;

            }
        }

        void CheckCollision()
        {

            collision = false;

            //Checking if it is colliding with another block
            switch(currentType)
            {
                case Block.Yellow:
                    for (int i = 0; i < positions_Yellow.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Yellow.GetLength(1); j++)
                        {
                            if (positions_Yellow[i, j] == 1)
                            {
                                int x_index = (int)(currentBlock.Location.X + blockSize * i)/blockSize; 
                                int y_index = (int)(currentBlock.Location.Y + blockSize * j)/blockSize;

                                if(y_index == 20)
                                {
                                    collision = true;
                                    break;
                                }
                                else if (block_placeHolders[y_index, x_index-1] == 1)
                                {
                                    collision = true;
                                    break; 
                                }
                            }
                        }
                    }
                    break;
                case Block.Red:
                    for (int i = 0; i < positions_Red.GetLength(0); i++)
                    {
                        for (int j = 0; j < positions_Red.GetLength(1); j++)
                        {
                            if (positions_Red[i, j] == 1)
                            {
                                /*
                                  1 1 0
                                  0 1 1 
                                 */

                                int x_index = (int)(currentBlock.Location.X + blockSize * i) / blockSize;
                                int y_index = (int)(currentBlock.Location.Y + blockSize * j) / blockSize;

                                if (y_index == 20)
                                {
                                    collision = true;
                                    break;
                                }
                                else if (block_placeHolders[y_index, x_index - 1] == 1)
                                {
                                    collision = true;
                                    break;
                                }
                            }
                        }
                    }
                    break; 
                default:
                    Console.WriteLine("Fel i kod i switch-sats i CheckCollision()");
                    break; 
            }

            if(collision)
            {
                //kod för att dokumentera placerat block i placeholders
                switch(currentType)
                {
                    case Block.Yellow:
                        for (int i = 0; i < positions_Yellow.GetLength(0); i++)
                        {
                            for (int j = 0; j < positions_Yellow.GetLength(1); j++)
                            {
                                if (positions_Yellow[i, j] == 1)
                                {
                                    int x_index = (int)(currentBlock.Location.X + blockSize * i) / blockSize;
                                    int y_index = (int)(currentBlock.Location.Y + blockSize * j) / blockSize;

                                    block_placeHolders[y_index - 1, x_index - 1] = 1;
                                    blockPositions[y_index - 1, x_index - 1].Visible = true;
                                    blockPositions[y_index - 1, x_index - 1].BackColor = Color.Yellow;
                                }
                            }
                        }
                        break;
                    case Block.Red:
                        for (int i = 0; i < positions_Red.GetLength(0); i++)
                        {
                            for (int j = 0; j < positions_Red.GetLength(1); j++)
                            {
                                if (positions_Red[i, j] == 1)
                                {
                                    int x_index = (int)(currentBlock.Location.X + blockSize * j) / blockSize;
                                    int y_index = (int)(currentBlock.Location.Y + blockSize * i) / blockSize;

                                    block_placeHolders[y_index, x_index - 1] = 1;
                                    blockPositions[y_index, x_index - 1].Visible = true;
                                    blockPositions[y_index, x_index - 1].BackColor = Color.Red;
                                }
                            }
                        }
                        break; 
                    default:
                        break; 
                }

                int amountOfBlocks = Enum.GetNames(typeof(Block)).Length;
                Random rnd = new Random();
                int blockIndex = rnd.Next(0, amountOfBlocks - 1);

                switch (blockIndex)
                {
                    case 0:
                        SpawnBlock(Block.Yellow);
                        break;
                    case 1:
                        SpawnBlock(Block.Red); 
                        break;
                    default:
                        Console.WriteLine("Fel värde på blockIndex i CheckCollision()");
                        break;
                }

            }

            CheckPlayArea();
        }

        void CheckPlayArea()
        {
            for (int i = 0; i < block_placeHolders.GetLength(0); i++)
            {
                for (int j = 0; j < block_placeHolders.GetLength(1); j++)
                {
                    if(j == panelWidth - 3)
                    {
                        Console.WriteLine(block_placeHolders[i, j]);
                    }
                    else
                    {
                        Console.Write(block_placeHolders[i, j]);    
                    }
                }
            }
        }

        private void CreatePlayArea()
        {

            for(int i = 0; i < panelWidth; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(blockSize*i, 0, blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor =  Color.Gray;
                pb.Visible = true;

                pb.BringToFront();
            }
            for (int i = 0; i < panelWidth; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(blockSize * i, blockSize*(panelHeight-1), blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor = Color.Gray;
                pb.Visible = true;

                pb.BringToFront();
            }

            for (int i = 0; i < panelHeight; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(0, blockSize*i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor = Color.Gray;
                pb.Visible = true;

                pb.BringToFront();
            }
            for (int i = 0; i < panelHeight; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(blockSize*(panelWidth-1), blockSize*i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor = Color.Gray;
                pb.Visible = true;

                pb.BringToFront();
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                int x = currentBlock.Location.X;
                int y = currentBlock.Location.Y;

                if (true) //statement (make later) to check if block touches another block from below
                {
                    currentBlock.Location = new Point(x, y + blockSize);
                }
            }
            else if(e.KeyCode == Keys.Left)
            {
                int x = currentBlock.Location.X;
                int y = currentBlock.Location.Y;

                if(x > blockSize)
                {
                    currentBlock.Location = new Point(x - blockSize, y);
                }
            }
            else if(e.KeyCode == Keys.Right)
            {
                int x = currentBlock.Location.X;
                int y = currentBlock.Location.Y;

                int rightCorner = currentBlock.Location.X + currentBlock.Width;

                if(rightCorner < blockSize*(panelWidth-1))
                {
                    currentBlock.Location = new Point(x + blockSize, y);
                }
            }
            else if(e.KeyCode == Keys.Up)
            {
                //switch block (extra for later)
            }  
            else if(e.KeyCode == Keys.Space)
            {
                //make block go down the furthest instantly
            }
        }

    }
}
