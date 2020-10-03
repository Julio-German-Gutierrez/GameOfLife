using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static bool[,] world = new bool[40, 150];
        static bool[,] newWorld = new bool[40, 150];

        static void Main(string[] args)
        {
            for (int y = 0; y < world.GetLength(0); y++)
            {
                for (int x = 0; x < world.GetLength(1); x++)
                {
                    world[y,x] = false;
                    newWorld[y,x] = false;
                }
            }

            Pattern(68, 13);

            Print(world);
            Console.ReadKey();

            Timer timer = new Timer(NextStep, new AutoResetEvent(false), 1000, 250);

            Console.ReadKey();
            
        }

        static void NextStep(Object stateInfo)
        {
            for (int y = 0; y < world.GetLength(0); y++)
            {
                for (int x = 0; x < world.GetLength(1); x++)
                {
                    newWorld[y,x] = IsAlive(x, y);
                }
            }

            Print(newWorld);

            for (int y = 0; y < world.GetLength(0); y++)
            {
                for (int x = 0; x < world.GetLength(1); x++)
                {
                    world[y, x] = newWorld[y, x];
                    newWorld[y, x] = false;
                }
            }
        }

        static bool IsAlive(int x, int y)
        {
            bool state = world[y, x];
            bool newState = state;
            int aliveAround = 0;

            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (x == j && y == i)
                    {
                        ;
                    }
                    else
                    {
                        try
                        {
                            //aliveAround += (world[i, j])? 1: 0; 
                            if(world[i, j]) aliveAround++;
                        }
                        catch (System.Exception)
                        {
                            //throw;
                        }
                    }
                    
                }
            }

            switch (aliveAround)
            {
                case 0:
                case 1:
                {
                    newState = false;
                    break;
                }
                case 2:
                    break;
                case 3:
                {
                    if(state) break;
                    else
                    {
                        newState = true;
                        break;
                    }
                    
                }
                default:
                {
                    newState = false;
                    break;
                }
            }

            return newState;
        }// End IsAlive()

        static void Print(bool[,] worldToPrint)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            for (int y = 0; y < worldToPrint.GetLength(0); y++)
            {
                for (int x = 0; x < worldToPrint.GetLength(1); x++)
                {
                    if(worldToPrint[y,x]) Console.Write("\u2588");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void Pattern(int x, int y)
        {
            world[y    , x    ] = false;
            world[y    , x + 1] = false;
            world[y    , x + 2] = true;
            world[y    , x + 3] = true;
            world[y    , x + 4] = true;
            world[y    , x + 5] = false;

            world[y + 1, x    ] = false;
            world[y + 1, x + 1] = false;
            world[y + 1, x + 2] = false;
            world[y + 1, x + 3] = false;
            world[y + 1, x + 4] = false;
            world[y + 1, x + 5] = false;

            world[y + 2, x    ] = true;
            world[y + 2, x + 1] = false;
            world[y + 2, x + 2] = false;
            world[y + 2, x + 3] = false;
            world[y + 2, x + 4] = false;
            world[y + 2, x + 5] = true;

            world[y + 3, x    ] = true;
            world[y + 3, x + 1] = false;
            world[y + 3, x + 2] = false;
            world[y + 3, x + 3] = false;
            world[y + 3, x + 4] = false;
            world[y + 3, x + 5] = true;

            world[y + 4, x    ] = true;
            world[y + 4, x + 1] = false;
            world[y + 4, x + 2] = false;
            world[y + 4, x + 3] = false;
            world[y + 4, x + 4] = false;
            world[y + 4, x + 5] = true;

            world[y + 5, x    ] = false;
            world[y + 5, x + 1] = false;
            world[y + 5, x + 2] = true;
            world[y + 5, x + 3] = true;
            world[y + 5, x + 4] = true;
            world[y + 5, x + 5] = false;

            // 1
            /////////////////////////////////////
            // 2

            world[y    , x     + 7] = false;
            world[y    , x + 1 + 7] = true;
            world[y    , x + 2 + 7] = true;
            world[y    , x + 3 + 7] = true;
            world[y    , x + 4 + 7] = false;
            world[y    , x + 5 + 7] = false;

            world[y + 1, x     + 7] = false;
            world[y + 1, x + 1 + 7] = false;
            world[y + 1, x + 2 + 7] = false;
            world[y + 1, x + 3 + 7] = false;
            world[y + 1, x + 4 + 7] = false;
            world[y + 1, x + 5 + 7] = false;

            world[y + 2, x     + 7] = true;
            world[y + 2, x + 1 + 7] = false;
            world[y + 2, x + 2 + 7] = false;
            world[y + 2, x + 3 + 7] = false;
            world[y + 2, x + 4 + 7] = false;
            world[y + 2, x + 5 + 7] = true;

            world[y + 3, x     + 7] = true;
            world[y + 3, x + 1 + 7] = false;
            world[y + 3, x + 2 + 7] = false;
            world[y + 3, x + 3 + 7] = false;
            world[y + 3, x + 4 + 7] = false;
            world[y + 3, x + 5 + 7] = true;

            world[y + 4, x     + 7] = true;
            world[y + 4, x + 1 + 7] = false;
            world[y + 4, x + 2 + 7] = false;
            world[y + 4, x + 3 + 7] = false;
            world[y + 4, x + 4 + 7] = false;
            world[y + 4, x + 5 + 7] = true;

            world[y + 5, x     + 7] = false;
            world[y + 5, x + 1 + 7] = true;
            world[y + 5, x + 2 + 7] = true;
            world[y + 5, x + 3 + 7] = true;
            world[y + 5, x + 4 + 7] = false;
            world[y + 5, x + 5 + 7] = false;

            // 2
            ////////////////////////////
            // 3

            world[y     + 7, x    ] = false;
            world[y     + 7, x + 1] = false;
            world[y     + 7, x + 2] = true;
            world[y     + 7, x + 3] = true;
            world[y     + 7, x + 4] = true;
            world[y     + 7, x + 5] = false;

            world[y + 1 + 7, x    ] = true;
            world[y + 1 + 7, x + 1] = false;
            world[y + 1 + 7, x + 2] = false;
            world[y + 1 + 7, x + 3] = false;
            world[y + 1 + 7, x + 4] = false;
            world[y + 1 + 7, x + 5] = true;

            world[y + 2 + 7, x    ] = true;
            world[y + 2 + 7, x + 1] = false;
            world[y + 2 + 7, x + 2] = false;
            world[y + 2 + 7, x + 3] = false;
            world[y + 2 + 7, x + 4] = false;
            world[y + 2 + 7, x + 5] = true;

            world[y + 3 + 7, x    ] = true;
            world[y + 3 + 7, x + 1] = false;
            world[y + 3 + 7, x + 2] = false;
            world[y + 3 + 7, x + 3] = false;
            world[y + 3 + 7, x + 4] = false;
            world[y + 3 + 7, x + 5] = true;

            world[y + 4 + 7, x    ] = false;
            world[y + 4 + 7, x + 1] = false;
            world[y + 4 + 7, x + 2] = false;
            world[y + 4 + 7, x + 3] = false;
            world[y + 4 + 7, x + 4] = false;
            world[y + 4 + 7, x + 5] = false;

            world[y + 5 + 7, x    ] = false;
            world[y + 5 + 7, x + 1] = false;
            world[y + 5 + 7, x + 2] = true;
            world[y + 5 + 7, x + 3] = true;
            world[y + 5 + 7, x + 4] = true;
            world[y + 5 + 7, x + 5] = false;

            // 3
            ////////////////////////////////
            // 4

            world[y     + 7, x     + 7] = false;
            world[y     + 7, x + 1 + 7] = true;
            world[y     + 7, x + 2 + 7] = true;
            world[y     + 7, x + 3 + 7] = true;
            world[y     + 7, x + 4 + 7] = false;
            world[y     + 7, x + 5 + 7] = false;

            world[y + 1 + 7, x     + 7] = true;
            world[y + 1 + 7, x + 1 + 7] = false;
            world[y + 1 + 7, x + 2 + 7] = false;
            world[y + 1 + 7, x + 3 + 7] = false;
            world[y + 1 + 7, x + 4 + 7] = false;
            world[y + 1 + 7, x + 5 + 7] = true;

            world[y + 2 + 7, x     + 7] = true;
            world[y + 2 + 7, x + 1 + 7] = false;
            world[y + 2 + 7, x + 2 + 7] = false;
            world[y + 2 + 7, x + 3 + 7] = false;
            world[y + 2 + 7, x + 4 + 7] = false;
            world[y + 2 + 7, x + 5 + 7] = true;

            world[y + 3 + 7, x     + 7] = true;
            world[y + 3 + 7, x + 1 + 7] = false;
            world[y + 3 + 7, x + 2 + 7] = false;
            world[y + 3 + 7, x + 3 + 7] = false;
            world[y + 3 + 7, x + 4 + 7] = false;
            world[y + 3 + 7, x + 5 + 7] = true;

            world[y + 4 + 7, x     + 7] = false;
            world[y + 4 + 7, x + 1 + 7] = false;
            world[y + 4 + 7, x + 2 + 7] = false;
            world[y + 4 + 7, x + 3 + 7] = false;
            world[y + 4 + 7, x + 4 + 7] = false;
            world[y + 4 + 7, x + 5 + 7] = false;

            world[y + 5 + 7, x     + 7] = false;
            world[y + 5 + 7, x + 1 + 7] = true;
            world[y + 5 + 7, x + 2 + 7] = true;
            world[y + 5 + 7, x + 3 + 7] = true;
            world[y + 5 + 7, x + 4 + 7] = false;
            world[y + 5 + 7, x + 5 + 7] = false;
        }

        static void Pattern2(int x, int y)
        {
            world[y    , x    ] = false;
            world[y    , x + 1] = false;
            world[y    , x + 2] = true;
            world[y    , x + 3] = true;
            world[y    , x + 4] = true;
            world[y    , x + 5] = false;

            world[y + 1, x    ] = false;
            world[y + 1, x + 1] = false;
            world[y + 1, x + 2] = false;
            world[y + 1, x + 3] = false;
            world[y + 1, x + 4] = false;
            world[y + 1, x + 5] = false;

            world[y + 2, x    ] = true;
            world[y + 2, x + 1] = false;
            world[y + 2, x + 2] = false;
            world[y + 2, x + 3] = false;
            world[y + 2, x + 4] = false;
            world[y + 2, x + 5] = true;

            world[y + 3, x    ] = true;
            world[y + 3, x + 1] = false;
            world[y + 3, x + 2] = false;
            world[y + 3, x + 3] = false;
            world[y + 3, x + 4] = false;
            world[y + 3, x + 5] = true;

            world[y + 4, x    ] = true;
            world[y + 4, x + 1] = false;
            world[y + 4, x + 2] = false;
            world[y + 4, x + 3] = false;
            world[y + 4, x + 4] = false;
            world[y + 4, x + 5] = true;

            world[y + 5, x    ] = false;
            world[y + 5, x + 1] = false;
            world[y + 5, x + 2] = true;
            world[y + 5, x + 3] = true;
            world[y + 5, x + 4] = true;
            world[y + 5, x + 5] = false;

            // 1
            /////////////////////////////////////
            // 2

            world[y    , x     + 7] = false;
            world[y    , x + 1 + 7] = true;
            world[y    , x + 2 + 7] = true;
            world[y    , x + 3 + 7] = true;
            world[y    , x + 4 + 7] = false;
            world[y    , x + 5 + 7] = false;

            world[y + 1, x     + 7] = false;
            world[y + 1, x + 1 + 7] = false;
            world[y + 1, x + 2 + 7] = false;
            world[y + 1, x + 3 + 7] = false;
            world[y + 1, x + 4 + 7] = false;
            world[y + 1, x + 5 + 7] = false;

            world[y + 2, x     + 7] = true;
            world[y + 2, x + 1 + 7] = false;
            world[y + 2, x + 2 + 7] = false;
            world[y + 2, x + 3 + 7] = false;
            world[y + 2, x + 4 + 7] = false;
            world[y + 2, x + 5 + 7] = true;

            world[y + 3, x     + 7] = true;
            world[y + 3, x + 1 + 7] = false;
            world[y + 3, x + 2 + 7] = false;
            world[y + 3, x + 3 + 7] = false;
            world[y + 3, x + 4 + 7] = false;
            world[y + 3, x + 5 + 7] = true;

            world[y + 4, x     + 7] = true;
            world[y + 4, x + 1 + 7] = false;
            world[y + 4, x + 2 + 7] = false;
            world[y + 4, x + 3 + 7] = false;
            world[y + 4, x + 4 + 7] = false;
            world[y + 4, x + 5 + 7] = true;

            world[y + 5, x     + 7] = false;
            world[y + 5, x + 1 + 7] = true;
            world[y + 5, x + 2 + 7] = true;
            world[y + 5, x + 3 + 7] = true;
            world[y + 5, x + 4 + 7] = false;
            world[y + 5, x + 5 + 7] = false;

            // 2
            ////////////////////////////
            // 3

            world[y     + 7, x    ] = false;
            world[y     + 7, x + 1] = false;
            world[y     + 7, x + 2] = true;
            world[y     + 7, x + 3] = true;
            world[y     + 7, x + 4] = true;
            world[y     + 7, x + 5] = false;

            world[y + 1 + 7, x    ] = true;
            world[y + 1 + 7, x + 1] = false;
            world[y + 1 + 7, x + 2] = false;
            world[y + 1 + 7, x + 3] = false;
            world[y + 1 + 7, x + 4] = false;
            world[y + 1 + 7, x + 5] = true;

            world[y + 2 + 7, x    ] = true;
            world[y + 2 + 7, x + 1] = false;
            world[y + 2 + 7, x + 2] = false;
            world[y + 2 + 7, x + 3] = false;
            world[y + 2 + 7, x + 4] = false;
            world[y + 2 + 7, x + 5] = true;

            world[y + 3 + 7, x    ] = true;
            world[y + 3 + 7, x + 1] = false;
            world[y + 3 + 7, x + 2] = false;
            world[y + 3 + 7, x + 3] = false;
            world[y + 3 + 7, x + 4] = false;
            world[y + 3 + 7, x + 5] = true;

            world[y + 4 + 7, x    ] = false;
            world[y + 4 + 7, x + 1] = false;
            world[y + 4 + 7, x + 2] = false;
            world[y + 4 + 7, x + 3] = false;
            world[y + 4 + 7, x + 4] = false;
            world[y + 4 + 7, x + 5] = false;

            world[y + 5 + 7, x    ] = false;
            world[y + 5 + 7, x + 1] = false;
            world[y + 5 + 7, x + 2] = true;
            world[y + 5 + 7, x + 3] = true;
            world[y + 5 + 7, x + 4] = true;
            world[y + 5 + 7, x + 5] = false;

            // 3
            ////////////////////////////////
            // 4

            world[y     + 7, x     + 7] = false;
            world[y     + 7, x + 1 + 7] = true;
            world[y     + 7, x + 2 + 7] = true;
            world[y     + 7, x + 3 + 7] = true;
            world[y     + 7, x + 4 + 7] = false;
            world[y     + 7, x + 5 + 7] = false;

            world[y + 1 + 7, x     + 7] = true;
            world[y + 1 + 7, x + 1 + 7] = false;
            world[y + 1 + 7, x + 2 + 7] = false;
            world[y + 1 + 7, x + 3 + 7] = false;
            world[y + 1 + 7, x + 4 + 7] = false;
            world[y + 1 + 7, x + 5 + 7] = true;

            world[y + 2 + 7, x     + 7] = true;
            world[y + 2 + 7, x + 1 + 7] = false;
            world[y + 2 + 7, x + 2 + 7] = false;
            world[y + 2 + 7, x + 3 + 7] = false;
            world[y + 2 + 7, x + 4 + 7] = false;
            world[y + 2 + 7, x + 5 + 7] = true;

            world[y + 3 + 7, x     + 7] = true;
            world[y + 3 + 7, x + 1 + 7] = false;
            world[y + 3 + 7, x + 2 + 7] = false;
            world[y + 3 + 7, x + 3 + 7] = false;
            world[y + 3 + 7, x + 4 + 7] = false;
            world[y + 3 + 7, x + 5 + 7] = true;

            world[y + 4 + 7, x     + 7] = false;
            world[y + 4 + 7, x + 1 + 7] = false;
            world[y + 4 + 7, x + 2 + 7] = false;
            world[y + 4 + 7, x + 3 + 7] = false;
            world[y + 4 + 7, x + 4 + 7] = false;
            world[y + 4 + 7, x + 5 + 7] = false;

            world[y + 5 + 7, x     + 7] = false;
            world[y + 5 + 7, x + 1 + 7] = true;
            world[y + 5 + 7, x + 2 + 7] = true;
            world[y + 5 + 7, x + 3 + 7] = true;
            world[y + 5 + 7, x + 4 + 7] = false;
            world[y + 5 + 7, x + 5 + 7] = false;
        }
    }
}
