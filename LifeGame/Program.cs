using System;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[,] grid = new bool[5, 5];
            Console.WriteLine($"Elige casillas vivas desde 1 hasta {grid.GetLength(0)}");
            int x, y = 0;
            int counter = 0;
            while (counter < 5)
            {
                Console.Write("x:");
                x = Int32.Parse(Console.ReadLine());
                Console.Write("y:");
                y = Int32.Parse(Console.ReadLine());

                grid[x - 1, y - 1] = true;
                counter++;
            }
            counter = 0;
            while (counter < 100)
            {
                Console.WriteLine("Generación "+counter);
                StartGame(ref grid); 
                counter++;
            }
            Console.ReadKey();
        }


        static void StartGame(ref bool[,] grid)
        {
          
            DrawGrid(grid);
            int cellsAlive;
            bool[,] nextGen = new bool[5, 5];
            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j =0; j<grid.GetLength(1); j++)
                {
                    cellsAlive = CalculateNeigbors(grid, i, j);
                    if(grid[i, j] == true)
                    {
                        if(cellsAlive == 2 || cellsAlive == 3)
                        {
                            nextGen[i, j] = true;
                        }
                        else
                        {
                            nextGen[i, j] = false;
                        }
                    }
                    else if(grid[i, j] == false)
                    {
                        if(cellsAlive == 3)
                        {
                            nextGen[i, j] = true;
                        }
                    }
                }
                Console.WriteLine();
            }
            grid = (bool[,])nextGen.Clone();
        }

        //rules 
        /// <summary>
        ///  una celula muerta con exactamente 3 celulas vivas nace 
        ///  una celula viva con 2 o 3 celulas sigue viva, en otro caso dif muere
        /// </summary>
        static int CalculateNeigbors(bool[,] grid, int row, int cell)
        {
            Vecino[,] vecinos =
            {
                {new Vecino(-1, -1), new Vecino(-1, 0), new Vecino(-1, 1)},
                {new Vecino(0, -1), new Vecino(0, 0), new Vecino(0, 1)},
                {new Vecino(1, -1), new Vecino(1, 0), new Vecino(1, 1)}
            };

            int x = 0, y = 0;
            int cellAlive = 0;
            for(int i = 0; i < vecinos.GetLength(0); i++)
            {
                for(int j = 0; j < vecinos.GetLength(0); j++)
                {
                    //sacando los vecinos de la casilla actual
                    x = row + vecinos[i, j].X;
                    y = cell + vecinos[i, j].Y;
                    if (IsValid(x, y))
                    {
                        //si esta viva la casilla contamos y tambien tiene que ser dif a la casilla actual
                        if(grid[x,y] == true && (x != row || y != cell))
                        {
                            cellAlive++;
                        }
                    }
                }
            }
            return cellAlive;
        }


        static void DrawGrid(bool[,] grid)
        {
            Console.WriteLine("");
            for (int i = 0; i < grid.GetLength(0); i++) { 
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" * ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" * ");
                    }
                }
                Console.WriteLine("\n"); 
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static bool IsValid(int x, int y) => ((x < 0 || y < 0) || (x > 4 || y > 4)) ? false : true;

    }

    public struct Vecino
    {
        private int x;
        private int y; 
        public Vecino(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }

}
