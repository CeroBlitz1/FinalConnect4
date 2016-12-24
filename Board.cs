using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Four_in_a_Row
{
    class Board
    {
        //Local variables
        int pixelsize;
        int size; //number of rows and columns
        int spacer;
        int activeplayer;
        Color backcolor;
        public Square[,] squares;
        Boolean hasfallingsquare;
        FallingSquare faller;

        public Board(int importsize, int pixels)
        {
            hasfallingsquare = false;
            spacer = 50;
            size = importsize;
            pixelsize = pixels * importsize;
            backcolor = Color.BlanchedAlmond;

            activePlayer = 1;
            squares = new Square[importsize, importsize];

            for (int x = 0; x < squares.GetLength(0); x++)
            {
                for (int y = 0; y < squares.GetLength(1); y++)
                {
                    squares[x, y] = new Square(pixels, 0);
                }

            }
        }
           

        public int Spacer
        {
            get { return spacer; }
            set { spacer = value; }
        }

        public int activePlayer
        {
            get { return activeplayer; }
            set { activeplayer = value; }
        }

        public int widthPixels
        {
            get { return pixelsize; }
            set { pixelsize = value; }
        }

        public int Width
        {
            get { return size; }
            set { size = value; }
        }

        public int Height
        {
            get { return size; }
            set { size = value; }
        }

        public int heightPixels
        {
            get { return pixelsize; }
            set { pixelsize = value; }
        }

        public SolidBrush backBrush
        {
            get
            {
                SolidBrush backbrush = new SolidBrush(backcolor);
                return backbrush;
            }

        }

        public Boolean hasFallingSquare
        {
            get { return hasfallingsquare; }
            set { hasfallingsquare = value; }
        }

        public FallingSquare Faller
        {
            get { return faller; }
            set { faller = value; }
        }

        //=====================//
        //      Methods        //
        //=====================//

        public Boolean CheckCorrectClick(int x)
        {
            // Check for playing fields bounds
            if ((x > -1) && (x < size))
            {}
            else
            {
                MessageBox.Show("Click inside the playingfield");
                return false;
            }

            // Check for free row
            if (squares[x, 0].Owner != 0)
            {
                MessageBox.Show("This column is full");
                return false;
            }

            return true;
        } 

        public void placeSquare(int x, int y, int player)
        {
                
                for (int i = size-1; i > -1; i--)
                {
                    if (squares[x, i].Owner == 0)
                    {
                        squares[x, i].Owner = player;
                        y = i;
                        break;
                    }

                }

                
                if (checkWin(x,y,player,squares))
                {
                    gamewon();
                }

                // Change active player
                if (player == 1)
                {
                    activeplayer = 2;
                }
                else if (player == 2)
                {
                    activeplayer = 1;
                }
        }

        public bool checkFull()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (squares[x, y].Owner == 0)
                    {
                        return false;
                    }
                }
            }
           
            return true;
        }

        public bool checkWin(int column, int row, int player, Square[,] Field)
        {
            int target = 3;
            int count = 0; 
            int check = 0; 

            //--------------------//
            // 1. Check vertical  //
            //--------------------//

            do
            {
                count++;
            }
            while (row + count < size && Field[column, row+count].Owner == player);

            if (count > target)
            {
                return true;
            }

            count = 0;
            check = 0;

            //---------------------//
            // 2. Check horizontal //
            //---------------------//

            //First left
            do
            {
                count++;
            }
            while (column - count >= 0 && Field[column - count, row].Owner == player);

            if (count > target)
            {
                return true;
            }

            count -= 1; //Make sure you dont double count the center piece. Deduct it here.

            //Then right
            do
            {
                count++;
                check++;
            }
            while (column + check < size && Field[column + check, row].Owner == player);

            if (count > target)
            {
                return true;
            }

            count = 0;
            check = 0;

            //------------------------//
            //  3. Check diagonal     //
            //  Lefttop - rightbottom //
            //------------------------//

            //To the top
            do
            {
                count++;
            }
            while (column - count >= 0 && row - count >= 0 && Field[column - count, row - count].Owner == player);

            if (count > target)
            {
                return true;
            }

            count -= 1; //Make sure you dont double count the center piece. Deduct it here.

            //To the bottom
            do
            {
                count++;
                check++;
            }
            while (column + check < size && row + check < size && Field[column + check, row + check].Owner == player);

            if (count > target)
            {
                return true;
            }

            count = 0;
            check = 0;

            //------------------------//
            //  3. Check diagonal     //
            //  Righttop - leftbottom //
            //------------------------//

            //To the top
            do
            {
                count++;
            }
            while (column + count < size && row - count >=0 && Field[column + count, row - count].Owner == player);

            if (count > target)
            {
                return true;
            }

            count -= 1; //Make sure you dont double count the center piece. Deduct it here.

            //To the bottom
            do
            {
                count++;
                check++;
            }
            while (column - check >=0 && row + check < size && Field[column - check, row + check].Owner == player);

            if (count > target)
            {
                return true;
            }

            return false;
        }
        
        public void gamewon()
        {
            MessageBox.Show("Player "+activeplayer.ToString()+" has won!");
        }

        


        //==============================//
        //          Abandoned           //
        //      (Pseudo-)Code snippets  //
        //==============================//

        public bool checkWinOLDv2(int column, int row, int player, Square[,] Field)
        {
            //Determine bounds
            int minRow = Math.Max(0, row - 3);
            int maxRow = Math.Min(row + 3, size - 1);
            int minColumn = Math.Max(0, column - 3);
            int maxColumn = Math.Min(column + 3, size - 1);

            Point DiagonalLeftTop = new Point(column - Math.Min(row, column), row - Math.Min(row, column)); // Checkt de hele diagonaal
            Point DiagonalRightBottom = new Point(size - 1 - DiagonalLeftTop.Y, size - 1 - DiagonalLeftTop.X); // Checkt de hele diagonaal

            Point DiagonalRightTop = new Point(Math.Min(row + column, size - 1), Math.Max(0, row + column - (size - 1))); //Checkt hele diagonaal
            Point DiagonalLeftBottom = new Point(DiagonalRightTop.Y, DiagonalRightTop.X);

            int count = 0;

            //Write checks to listbox
            Trace.WriteLine("Player " + player.ToString() + " - DLT = (" + DiagonalLeftTop.X + "," + DiagonalLeftTop.Y + ")");
            Trace.WriteLine("DRB = (" + DiagonalRightBottom.X + "," + DiagonalRightBottom.Y + ")");

            ////Check RightTop / LeftBottom
            for (int i = 0; i <= DiagonalRightTop.X - DiagonalLeftBottom.X; i++)
            {
                if (Field[DiagonalRightTop.X - i, DiagonalRightTop.Y + i].Owner == player)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                {
                    count = 0;
                }

            }

            count = 0;

            ////Check LeftTop / RightBottom
            for (int i = 0; i <= DiagonalRightBottom.X - DiagonalLeftTop.X; i++)
            {
                if (Field[DiagonalLeftTop.X + i, DiagonalLeftTop.Y + i].Owner == player)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                {
                    count = 0;
                }

            }

            count = 0;

            //Check horizontal
            for (int i = minColumn; i <= maxColumn; i++)
            {
                if (Field[i, row].Owner == player)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                    count = 0;
            }

            count = 0;

            ////Check vertical
            for (int i = minRow; i <= maxRow; i++)
            {
                if (Field[column, i].Owner == player)
                {
                    count++;
                    if (count == 4)
                        return true;
                }
                else
                    count = 0;
            }

            count = 0;

            return false;

        }

        public bool checkWinOLDv1(int x, int y, int player, Square[,] TestSquares)
        {
            //MessageBox.Show(x.ToString() + ":" + y.ToString());

            // Check horizontal winning streak
            int count = 0;

            for (int i = 0; i < size; i++)
            {
                if (TestSquares[i, y].Owner == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            count = 0;

            //Check vertical winning streak
            for (int i = 0; i < size; i++)
            {
                if (TestSquares[x, i].Owner == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            count = 0;

            //Check diagonal streak lefttop - rightbottom
            int startX = x - Math.Min(x, y);
            int startY = y - Math.Min(x, y);

            for (int i = 0; i < size - Math.Max(startX, startY); i++)
            {
                if (TestSquares[startX + i, startY + i].Owner == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            count = 0;

            //Check diagonal streak leftbottom - righttop
            startX = x;
            startY = y;

            while (startX > 0 && startY < (size - 1))
            {
                startX--;
                startY++;
            }

            int j = 0;

            while (startX + j < (size - 1) && startY - j >= 0)
            {
                if (TestSquares[startX + j, startY - j].Owner == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
                j++;
            }

            count = 0;

            return false;


        }
    }
}




            