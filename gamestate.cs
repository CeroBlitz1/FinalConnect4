using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Four_in_a_Row
{
    class gamestate
    {
     
        public Square[,] squares;
        gamestate parent;
        int columnused;
        string id;
        
        public gamestate(gamestate ParentState, int column, int size)
        {
           
            parent = ParentState;

           
            id = parent.ID;
            id += "-";
            id += column.ToString();

            
            squares = new Square[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    squares[x, y] = new Square(60, ParentState.squares[x, y].Owner);
                }
            }

           
            columnused = column;

        }  

        public gamestate(Square[,] CurrentSquares, int column, int size)
        {
           
            squares = new Square[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    squares[x, y] = new Square(60, CurrentSquares[x, y].Owner);
                  
                }
            }

            
            columnused = column;
        } 
        
      

        public gamestate Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public int Column
        {
            get { return columnused; }
            set { columnused = value; }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }



    }
}
