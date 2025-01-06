using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGenerator
{
    internal class GenerateSudoku
    {
        //Class properties 
        private int[,] grid;
        private Random random = new Random();

        //Generate the sudoku grid
        public int[,] MakeSudoku(int gridSize)
        {
            //Check the gird size selection 
            if (gridSize != 9)
            {
                Console.WriteLine("Invaild grid size, only grid 9");
                return null;
            } 
            //Initialize grid 
            grid = new int[gridSize,gridSize];
            populateAcross(gridSize);
            completeGrid(0, 0, gridSize);

            int removeNum = gridSize == 4 ? 4 : 40;//set numberr of cells to remove
            MakePuzzle(removeNum, gridSize);

            return grid;

        }
//populates the diagonal subgrids
        private void populateSubgrid(int row, int col, int gridSize)
        {
            int subgrid = (int)Math.Sqrt(gridSize);
            //row for sub grid
            for(int i = 0; i < subgrid; i++)
            {
                //col for subgrid
                for(int j = 0; j < subgrid; j++)
                {
                    //generate random number for subgrid
                    int num;
                    do
                    {
                      num = random.Next(1, gridSize + 1);
                    } while (!isUnique(row + 1, col + j,gridSize,num));

                    grid[row+i,col+j] = num;
                }
            }
        }
        //Checking if number placement does not invalidate sudokou rules
        private bool isUnique(int row, int col, int gridSize, int num)
        {
            //checks row and column rule
            for (int i = 0; i < gridSize; i++) 
            {
                if (grid[row,i]==num || grid[i, col] == num)
                {
                    return false;
                }
            }

            //check the subgrid rule 
            int subgrid = (int)Math.Sqrt(gridSize);
            //locate the the starting cell in current subgrid
            int subgridRow = row - row % subgrid;
            int subgridCol = col - col % subgrid;

            for (int j=0;j<subgrid; j++)
            {
                for(int k = 0; k < subgrid; k++)
                {
                    //checks if any number is being repeated
                    if (grid[subgridRow+j,subgridCol+k] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Locate the starting point of the diagonal subgrids 
        private void populateAcross(int gridSize)
        {
            int subgrid = (int)Math.Sqrt(gridSize);
            for (int i = 0; i < gridSize; i += subgrid)
            {
                populateSubgrid(i,i,gridSize);
            }
        }
        //populate the rest of the grid using recursive regression 

        private bool completeGrid(int r, int c, int gridSize)
        {
            if (c >= gridSize)
            {
                r++;
                c = 0;
            }
            if (r >= gridSize)
            {
                return true;
            }

            if (grid[r, c] != 0)
            {
                return completeGrid(r, c + 1, gridSize);
            }

            for (int i = 1; i <= gridSize; i++)
            {
                if (isUnique(r, c, gridSize, i))
                {
                    grid[r, c] = i;
                    if (completeGrid(r, c + 1, gridSize))
                    {
                        return true;
                    }
                    grid[r, c] = 0;
                }
            }
            return false;
        }
       
        //Display the grid 
        public void displayGrid(int gridSize)
        {
            for(int i=0; i<gridSize; i++)
            {
                for(int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j]+"  ");
                }
                Console.WriteLine();
            }
        }
        //turns completeted grid to puzzles but removing some cells
        private void MakePuzzle(int num, int gridSize)
        {
            while (num != 0)
            {
                //finds random cell id
                int cell = random.Next(0,gridSize*gridSize);
                int r = cell / gridSize;
                int c = cell % gridSize;

                if (grid[r, c] != 0)
                {
                    grid[r, c] = 0;
                    num--;
                }

            }
        }

    }
}
