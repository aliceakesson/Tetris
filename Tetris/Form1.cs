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
    public partial class Form1 : Form
    {

        bool gameOver = false; 

        int amountOfBlocks = 10; //??

        public Form1()
        {
            InitializeComponent();

            
        }

        //Update
        private void gameTimer_Tick(object sender, EventArgs e)
        {



        }

        void SpawnBlock(int type)
        {
            switch(type)
            {
                case 1:
                    break;
                case 2:
                    break; 
                default:
                    Console.WriteLine("Error in code in SpawnBlock()");
                    break; 
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
