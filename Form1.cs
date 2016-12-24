using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Four_in_a_Row
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        Bitmap surface;
        Graphics device;
        Graphics Backdevice;
        Board gameboard;
        
        private void Form1_Load(object sender, EventArgs e)
        {
      
            this.Width = 906;
            this.Height = 628;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.BackColor = Color.Black;
            this.Show();
            this.Focus();

         
         
            surface = new Bitmap(this.Width,this.Height);

          
            device = this.CreateGraphics();

     
            gameboard = new Board(8,60);

       
            gameloop();
                        
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            surface.Dispose();
            device.Dispose();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            decimal X = (e.X - gameboard.Spacer)/gameboard.squares[0,0].SizeH;
            decimal Y = (e.Y - gameboard.Spacer)/gameboard.squares[0,0].SizeV;
            X = Math.Floor(X);
            Y = Math.Floor(Y);

            int xSquare = Convert.ToInt32(X);
            int ySquare = Convert.ToInt32(Y);

      
            if (gameboard.CheckCorrectClick(xSquare))
            {
                gameboard.placeSquare(xSquare, ySquare, gameboard.activePlayer);

            
                if (gameboard.activePlayer == 2)
                {
                  
                   return;
                }
            }
        }

        //Methods
        private void gameloop()
        {
            bool runGame = true;

            while (runGame)
            {
                Application.DoEvents();
                
                // Update gamestate
                UpdateFallingPosition();
                
                // Draw Update
                drawall();

                //Test stuff
                //int test = gameboard.squares.GetLength(1);
                //MessageBox.Show(test.ToString());
            }

        }

  
        private void drawall()
        {
        
         
            device.FillRectangle(gameboard.backBrush, gameboard.Spacer, gameboard.Spacer, gameboard.widthPixels, gameboard.heightPixels );

            int NrSquaresX = gameboard.squares.GetLength(0);
            int NrSquaresY = gameboard.squares.GetLength(1);



            for (int x = 0; x < NrSquaresX; x++)
            {
                for (int y = 0; y < NrSquaresY; y++)
                {
                    SolidBrush SquareBrush = new SolidBrush(gameboard.squares[x,y].Color);
                    //device.FillRectangle(Brushes.Green, gameboard.Spacer + x*gameboard.squares[x,y].SizeH, gameboard.Spacer+y*gameboard.squares[x,y].SizeV , gameboard.squares[x,y].SizeH, gameboard.squares[x,y].SizeV);
                    device.FillEllipse(SquareBrush, gameboard.Spacer + 8 + x * gameboard.squares[x, y].SizeH, gameboard.Spacer + 8 + y * gameboard.squares[x, y].SizeV, gameboard.squares[x, y].SizeH - 16, gameboard.squares[x, y].SizeV - 16);
                    //MessageBox.Show(gameboard.squares[x,y].Color.ToString());
                    //MessageBox.Show((gameboard.Spacer + x * gameboard.squares[x, y].SizeH).ToString());
                }
                
            }

            //Here we copy the backbuffer (drawing paper) to the pen
            device = Graphics.FromImage(surface);

            //Here we draw the surface to the screen
            Backdevice = this.CreateGraphics();
            Backdevice.DrawImage(surface, 0, 0, this.Width, this.Height);

            //Here we fix the overdraw
            device.Clear(Color.Bisque);
        }

        //2. Updating Gamestate
        private void UpdateFallingPosition()
        {
            if (gameboard.hasFallingSquare)
            {
                //gameboard.Faller //Do something with the faller
            }
        }
    }
}
