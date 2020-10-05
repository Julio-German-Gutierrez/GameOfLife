using System;
using System.IO;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static bool[,] world = new bool[40, 150];
        static bool[,] newWorld = new bool[40, 150];

        static int count = 0;

        static void Main(string[] args)
        {
            for (int y = 0; y < world.GetLength(0); y++)
            {
                for (int x = 0; x < world.GetLength(1); x++)
                {
                    world[y, x] = false;
                    newWorld[y, x] = false;
                }
            }

            //Pattern(68, 13);
            LoadPattern(world, "/home/lalegion/DotNET/Lektioner/GameOfLife/Pattern 2.txt");

            Print(world, true);
            Console.Write("Press any key to START...");
            Console.ReadKey(true);

            Timer timer = new Timer(NextStep, new AutoResetEvent(false), 1000, 250);

            Console.ReadKey();

        }

        static void NextStep(Object stateInfo)
        {
            for (int y = 0; y < newWorld.GetLength(0); y++)
            {
                for (int x = 0; x < newWorld.GetLength(1); x++)
                {
                    newWorld[y, x] = IsAlive(x, y);
                }
            }

            Print(newWorld);

            world = newWorld;

            newWorld = new bool[40, 150];
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
                            if (world[i, j]) aliveAround++;
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
                        if (state) break;
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

        static void Print(bool[,] worldToPrint, bool intro = false)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            for (int y = 0; y < worldToPrint.GetLength(0); y++)
            {
                for (int x = 0; x < worldToPrint.GetLength(1); x++)
                {
                    if (worldToPrint[y, x]) Console.Write("\u2588");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
            if (!intro) System.Console.Write("Press any key to EXIT...");
        }

        static void LoadPattern(bool[,] w, string openFile)
        {
            FileStream f = File.OpenRead(openFile);
            StreamReader sr = new StreamReader(f);
            string[] infoPattern = sr.ReadLine().Split(":");
            int width = int.Parse(infoPattern[1]);
            int height = int.Parse(infoPattern[2]);

            bool[,] pattern = new bool[height, width];

            //string line;
            char[] c;

            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                c = sr.ReadLine().ToCharArray();

                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    pattern[y, x] = (c[x] == '\u2588');
                }
            }

            //Copy pattern into world.
            int newY = w.GetLength(0) / 2 - height / 2;
            int newX = w.GetLength(1) / 2 - width / 2;

            for (int y = newY; y < pattern.GetLength(0) + newY; y++)
            {
                for (int x = newX; x < pattern.GetLength(1) + newX; x++)
                {
                    w[y, x] = pattern[y - newY, x - newX];
                }
            }

            f.Close();
            sr.Close();
            f.Dispose();
            sr.Dispose();
        }

        static void SavePattern(bool[,] w, string fileName)
        {
            FileStream f = File.Create(fileName);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine("Start Pattern:");
            for (int y = 0; y < w.GetLength(0); y++)
            {
                for (int x = 0; x < w.GetLength(1); x++)
                {
                    sw.Write((w[y, x]) ? "\u2588" : "\u2022"); // Full_black_square:\u2588 - Bullet:\u2022 - Black_circle:\u25CF
                }
                sw.WriteLine();
            }
            sw.WriteLine("End Pattern.");

            sw.Close();
            f.Close();
        }

        /* static void Pattern(int x, int y)
        {
            world[y, x] = false;
            world[y, x + 1] = false;
            world[y, x + 2] = true;
            world[y, x + 3] = true;
            world[y, x + 4] = true;
            world[y, x + 5] = false;

            world[y + 1, x] = false;
            world[y + 1, x + 1] = false;
            world[y + 1, x + 2] = false;
            world[y + 1, x + 3] = false;
            world[y + 1, x + 4] = false;
            world[y + 1, x + 5] = false;

            world[y + 2, x] = true;
            world[y + 2, x + 1] = false;
            world[y + 2, x + 2] = false;
            world[y + 2, x + 3] = false;
            world[y + 2, x + 4] = false;
            world[y + 2, x + 5] = true;

            world[y + 3, x] = true;
            world[y + 3, x + 1] = false;
            world[y + 3, x + 2] = false;
            world[y + 3, x + 3] = false;
            world[y + 3, x + 4] = false;
            world[y + 3, x + 5] = true;

            world[y + 4, x] = true;
            world[y + 4, x + 1] = false;
            world[y + 4, x + 2] = false;
            world[y + 4, x + 3] = false;
            world[y + 4, x + 4] = false;
            world[y + 4, x + 5] = true;

            world[y + 5, x] = false;
            world[y + 5, x + 1] = false;
            world[y + 5, x + 2] = true;
            world[y + 5, x + 3] = true;
            world[y + 5, x + 4] = true;
            world[y + 5, x + 5] = false;

            // 1
            /////////////////////////////////////
            // 2

            world[y, x + 7] = false;
            world[y, x + 1 + 7] = true;
            world[y, x + 2 + 7] = true;
            world[y, x + 3 + 7] = true;
            world[y, x + 4 + 7] = false;
            world[y, x + 5 + 7] = false;

            world[y + 1, x + 7] = false;
            world[y + 1, x + 1 + 7] = false;
            world[y + 1, x + 2 + 7] = false;
            world[y + 1, x + 3 + 7] = false;
            world[y + 1, x + 4 + 7] = false;
            world[y + 1, x + 5 + 7] = false;

            world[y + 2, x + 7] = true;
            world[y + 2, x + 1 + 7] = false;
            world[y + 2, x + 2 + 7] = false;
            world[y + 2, x + 3 + 7] = false;
            world[y + 2, x + 4 + 7] = false;
            world[y + 2, x + 5 + 7] = true;

            world[y + 3, x + 7] = true;
            world[y + 3, x + 1 + 7] = false;
            world[y + 3, x + 2 + 7] = false;
            world[y + 3, x + 3 + 7] = false;
            world[y + 3, x + 4 + 7] = false;
            world[y + 3, x + 5 + 7] = true;

            world[y + 4, x + 7] = true;
            world[y + 4, x + 1 + 7] = false;
            world[y + 4, x + 2 + 7] = false;
            world[y + 4, x + 3 + 7] = false;
            world[y + 4, x + 4 + 7] = false;
            world[y + 4, x + 5 + 7] = true;

            world[y + 5, x + 7] = false;
            world[y + 5, x + 1 + 7] = true;
            world[y + 5, x + 2 + 7] = true;
            world[y + 5, x + 3 + 7] = true;
            world[y + 5, x + 4 + 7] = false;
            world[y + 5, x + 5 + 7] = false;

            // 2
            ////////////////////////////
            // 3

            world[y + 7, x] = false;
            world[y + 7, x + 1] = false;
            world[y + 7, x + 2] = true;
            world[y + 7, x + 3] = true;
            world[y + 7, x + 4] = true;
            world[y + 7, x + 5] = false;

            world[y + 1 + 7, x] = true;
            world[y + 1 + 7, x + 1] = false;
            world[y + 1 + 7, x + 2] = false;
            world[y + 1 + 7, x + 3] = false;
            world[y + 1 + 7, x + 4] = false;
            world[y + 1 + 7, x + 5] = true;

            world[y + 2 + 7, x] = true;
            world[y + 2 + 7, x + 1] = false;
            world[y + 2 + 7, x + 2] = false;
            world[y + 2 + 7, x + 3] = false;
            world[y + 2 + 7, x + 4] = false;
            world[y + 2 + 7, x + 5] = true;

            world[y + 3 + 7, x] = true;
            world[y + 3 + 7, x + 1] = false;
            world[y + 3 + 7, x + 2] = false;
            world[y + 3 + 7, x + 3] = false;
            world[y + 3 + 7, x + 4] = false;
            world[y + 3 + 7, x + 5] = true;

            world[y + 4 + 7, x] = false;
            world[y + 4 + 7, x + 1] = false;
            world[y + 4 + 7, x + 2] = false;
            world[y + 4 + 7, x + 3] = false;
            world[y + 4 + 7, x + 4] = false;
            world[y + 4 + 7, x + 5] = false;

            world[y + 5 + 7, x] = false;
            world[y + 5 + 7, x + 1] = false;
            world[y + 5 + 7, x + 2] = true;
            world[y + 5 + 7, x + 3] = true;
            world[y + 5 + 7, x + 4] = true;
            world[y + 5 + 7, x + 5] = false;

            // 3
            ////////////////////////////////
            // 4

            world[y + 7, x + 7] = false;
            world[y + 7, x + 1 + 7] = true;
            world[y + 7, x + 2 + 7] = true;
            world[y + 7, x + 3 + 7] = true;
            world[y + 7, x + 4 + 7] = false;
            world[y + 7, x + 5 + 7] = false;

            world[y + 1 + 7, x + 7] = true;
            world[y + 1 + 7, x + 1 + 7] = false;
            world[y + 1 + 7, x + 2 + 7] = false;
            world[y + 1 + 7, x + 3 + 7] = false;
            world[y + 1 + 7, x + 4 + 7] = false;
            world[y + 1 + 7, x + 5 + 7] = true;

            world[y + 2 + 7, x + 7] = true;
            world[y + 2 + 7, x + 1 + 7] = false;
            world[y + 2 + 7, x + 2 + 7] = false;
            world[y + 2 + 7, x + 3 + 7] = false;
            world[y + 2 + 7, x + 4 + 7] = false;
            world[y + 2 + 7, x + 5 + 7] = true;

            world[y + 3 + 7, x + 7] = true;
            world[y + 3 + 7, x + 1 + 7] = false;
            world[y + 3 + 7, x + 2 + 7] = false;
            world[y + 3 + 7, x + 3 + 7] = false;
            world[y + 3 + 7, x + 4 + 7] = false;
            world[y + 3 + 7, x + 5 + 7] = true;

            world[y + 4 + 7, x + 7] = false;
            world[y + 4 + 7, x + 1 + 7] = false;
            world[y + 4 + 7, x + 2 + 7] = false;
            world[y + 4 + 7, x + 3 + 7] = false;
            world[y + 4 + 7, x + 4 + 7] = false;
            world[y + 4 + 7, x + 5 + 7] = false;

            world[y + 5 + 7, x + 7] = false;
            world[y + 5 + 7, x + 1 + 7] = true;
            world[y + 5 + 7, x + 2 + 7] = true;
            world[y + 5 + 7, x + 3 + 7] = true;
            world[y + 5 + 7, x + 4 + 7] = false;
            world[y + 5 + 7, x + 5 + 7] = false;
        } */
    }
}
