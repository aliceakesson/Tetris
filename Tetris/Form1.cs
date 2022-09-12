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
        Red
    }
    public partial class Form1 : Form
    {

        bool gameOver = false; 

        int amountOfBlocks = 10; //??
        int blockSize = 30;
        int blockSize_Margin = 1;
        
        Point spawnPosition;

        int panelWidth = 12, panelHeight = 22;

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

            CreatePlayArea();
            SpawnBlock(Block.Yellow);
        }

        //Update
        private void gameTimer_Tick(object sender, EventArgs e)
        {



        }

        void SpawnBlock(Block type)
        {
            switch(type)
            {
                case Block.Yellow:
                    Panel p = new Panel();
                    p.Parent = playArea;
                    p.SetBounds(spawnPosition.X-blockSize, spawnPosition.Y, blockSize * 2, blockSize * 2);

                    for(int i = 0; i < 2; i++)
                    {
                        for(int j = 0; j < 2; j++)
                        {
                            PictureBox pb = new PictureBox();
                            pb.BackColor = Color.Yellow; 
                            pb.Parent = p;

                            pb.Visible = true; 
                            pb.SetBounds(blockSize * i, blockSize * j, blockSize - blockSize_Margin, blockSize-blockSize_Margin);
                            pb.Show();
                        }
                    }

                    p.BackColor = Color.Black; 
                    p.Visible = true; 
                    p.BringToFront();

                    break;
                case Block.Red:
                    break; 
                default:
                    Console.WriteLine("Error in code in SpawnBlock()");
                    break;

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
            }
            for (int i = 0; i < panelWidth; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(blockSize * i, blockSize*(panelHeight-1), blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor = Color.Gray;
                pb.Visible = true;
            }

            for (int i = 0; i < panelHeight; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(0, blockSize*i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor = Color.Gray;
                pb.Visible = true;
            }
            for (int i = 0; i < panelHeight; i++)
            {

                PictureBox pb = new PictureBox();
                pb.Parent = playArea;

                pb.SetBounds(blockSize*(panelWidth-1), blockSize*i, blockSize - blockSize_Margin, blockSize - blockSize_Margin);

                pb.BackColor = Color.Gray;
                pb.Visible = true;
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                //move active block down
            }
            else if(e.KeyCode == Keys.Left)
            {
                Console.WriteLine("test");
            }
            else if(e.KeyCode == Keys.Right)
            {

            }
            else if(e.KeyCode== Keys.Up)
            {
                //switch block (extra for later)
            }  
        }

    }
}
