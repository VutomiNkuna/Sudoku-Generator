namespace SudokuGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GenerateSudoku sudoku = new GenerateSudoku();
            int gridSize = 9;
            int[,] grid = sudoku.MakeSudoku(gridSize);

            if (grid != null)
            {
                sudoku.displayGrid(gridSize);
            }
        }
    }
}
