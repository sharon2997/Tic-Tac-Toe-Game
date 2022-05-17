using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;
using System.Text;

namespace B21_Ex02
{
    public class Grid
    {
        private readonly int r_Size;
        private char[,] m_Matrix;
        private bool[,] m_Occupied;
        private static StringBuilder s_MatrixSb;

        public Grid(int i_Size)
        {
            this.r_Size = i_Size;
            this.m_Matrix = new char[i_Size, i_Size];
            for(int i = 0; i < i_Size; i++)
            {
                for (int j = 0; j < i_Size; j++)
                {
                    this.m_Matrix[i, j] = ' ';
                }
            }

            this.m_Occupied = new bool[i_Size, i_Size];
        }

        public int Size
        {
            get
            {

                return r_Size;
            }
        }

        public char[,] Matrix
        {
            get
            {

                return m_Matrix;
            }
        }

        public void PrintGrid()
        {
            Screen.Clear();
            int size = this.r_Size;

            s_MatrixSb = new StringBuilder("");
            for(int i = 1; i < size + 1; i++)
            {
                s_MatrixSb.Append(string.Format("   {0}", i));
            }

            seperateRow(size);
            for(int i = 0; i < size; i++)
            {
                s_MatrixSb.Append(string.Format(@"{0}{1}", Environment.NewLine, i + 1));
                for (int j = 0; j < size; j++)
                {
                    s_MatrixSb.Append(string.Format(@" | {0}", this.m_Matrix[i,j]));
                }

                s_MatrixSb.Append(" | ");
                seperateRow(size);
            }

            s_MatrixSb.Append(Environment.NewLine);
            Console.WriteLine(s_MatrixSb.ToString());
        }

        private static void seperateRow(int i_Size)
        {
            s_MatrixSb.Append(String.Format(@"{0}  ", Environment.NewLine));
            for(int i = 0; i < i_Size; i++)
            {
                s_MatrixSb.Append("====");
            }

            s_MatrixSb.Append("=");
        }

        public bool AddSign(int i_Row, int i_Col, char i_Sign)
        {
            bool changed = true;
            int row = i_Row - 1;
            int col = i_Col - 1;

            if(!this.m_Occupied[row, col])
            {
                this.m_Matrix[row, col] = i_Sign;
                this.m_Occupied[row, col] = true;
            }
            else
            {
                changed = false;
            }

            return changed;
        }

        public List<int> FreeCells()
        {
            List<int> freeCells = new List<int>();

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if(!this.m_Occupied[i, j])
                    {
                        freeCells.Add(i * 10 + j);
                    }
                }
            }

            return freeCells; 
        }

        public bool IsFullRow(char i_Sign)
        {
            bool isFullRow = false;
            bool flag = true;

            for(int row = 0; (row < r_Size) && flag; row++)
            {
                int counter = 0;

                for (int i = 0; i < r_Size; i++)
                {
                    if(m_Matrix[row, i] == i_Sign)
                    {
                        counter++;
                    }

                    if(counter == r_Size)
                    {
                        isFullRow = true;
                        flag = false;
                        break;
                    }
                }
            }

            return isFullRow; 
        }

        public bool IsFullCol(char i_Sign)
        {
            bool isFullCol = false;
            bool flag = true;

            for (int col = 0; (col < r_Size) && flag; col++)
            {
                int counter = 0;

                for (int i = 0; i < r_Size; i++)
                {
                    if (m_Matrix[i, col] == i_Sign)
                    {
                        counter++;
                    }

                    if (counter == r_Size)
                    {
                        isFullCol = true;
                        flag = false;
                        break;
                    }
                }
            }

            return isFullCol;   
        }

        public bool IsFullMainDiagonal(char i_Sign)
        {   
            bool isFullDiagonal = true;

            for(int i = 0; i < r_Size; i++)
            {
                if(m_Matrix[i,i] != i_Sign)
                {
                    isFullDiagonal = false;
                    break;
                }
            }

            return isFullDiagonal;
        }

        public bool IsFullSecodaryDiagonal(char i_Sign)
        {
            bool isFullDiagonal = true;

            for (int i = 0; i < r_Size; i++)
            {
                if (m_Matrix[i, r_Size - i - 1] != i_Sign)
                {
                    isFullDiagonal = false;
                    break;
                }
            }

            return isFullDiagonal;
        }

        public bool IsFullBoard()
        {
            bool isFullBoard = true;

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if(this.m_Occupied[i, j] == false)
                    {
                        isFullBoard = false;
                        break;
                    }
                }
            }

            return isFullBoard;
        }
    }
}
