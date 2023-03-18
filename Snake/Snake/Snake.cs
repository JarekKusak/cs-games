using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Snake
    {
        private Table table;
        private Apple apple;
        private char head;
        private char body;
        public int n;
        public int m;
        private int[] cx; // pole ukládající souřadnice x 
        private int[] cy; // pole ukládající souřadnice y 
        private int bodySegments; // counter všech článků těla (včetně hlavy) + zmizík na konci 
        private bool collision;

        public Snake(Table tabulka, Apple jablko, char hlava, char telo)
        {
            this.head = hlava;
            this.body = telo;
            this.apple = jablko;
            this.table = tabulka;
            cx = new int[(table.Length - 2) * (table.Length - 2)];
            cy = new int[(table.Length - 2) * (table.Length - 2)];
            n = (table.Length + 1) / 2 - 1;
            m = n;
            cx[0] = n;
            cy[0] = m;
            cx[1] = n; // x souř. zmizíku
            cy[1] = m + 1; // y souř. zmizíku
            bodySegments = 2; // počet článků těla hada - ze začátku hlava + "zmizík"
            collision = true;

            apple.CreateApple(cx, cy, bodySegments, m, n); // creates first apple
        }

        public void GoUp()
        {
            m--; // coordinates of head for control 
            if (m == 0) // collision with edge 
            {
                m = table.Length - 2; // head of snake goes over the edge (upwards)
                if (apple.CheckStateOfApple(m, n) == false) // false -> snakes moves normally 
                {
                    for (int i = bodySegments - 1; i > 0; i--)  
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1]; // swapping y coordinates 
                    }
                    cy[0] = table.Length - 2; // displacement of head (upwards)
                }
                else // snake eats apple and its body grows
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = table.Length - 2; // displacement of head (upwards)
                    bodySegments++; // +1 body segment
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            else // out of range
            {
                if (apple.CheckStateOfApple(m, n) == false) // false -> snakes moves normally 
                {
                    for (int i = bodySegments - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cy[0] = cy[0] - 1; // displacement of head (upwards)
                }
                else // snake eats apple and its body grows 
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = cy[0] - 1; // displacement of head (upwards)
                    bodySegments++; // +1 body segment 
                    
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            SnakeOutput();
        }
        public void GoDown()
        {
            m++;
            if (m == table.Length - 1) 
            {
                m = 1;
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodySegments - 1; i > 0; i--)
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1]; 
                    }
                    cy[0] = 1;
                }
                else 
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = 1;
                    bodySegments++;
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            else // out of range
            {
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodySegments - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cy[0] = cy[0] + 1;
                }
                else
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = cy[0] + 1;
                    bodySegments++;
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            SnakeOutput();
        }
        public void GoLeft()
        {
            n--;
            if (n == 0)
            {
                n = table.Length - 2;
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodySegments - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cx[0] = table.Length - 2;
                }
                else
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = table.Length - 2;
                    bodySegments++;
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            else
            {
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodySegments - 1; i > 0; i--)  
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cx[0] = cx[0] - 1;
                }
                else 
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = cx[0] - 1;
                    bodySegments++;
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            SnakeOutput();
        }
        public void GoRight()
        {
            n++;
            if (n == table.Length - 1)
            {
                n = 1;
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodySegments - 1; i > 0; i--)  
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1]; 
                    }
                    cx[0] = 1;

                }
                else 
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = 1; 
                    bodySegments++;
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            else // out of range
            {
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodySegments - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cx[0] = cx[0] + 1; // displacement of head coordinates 
                }
                else // snake eats apple and its body grows
                {
                    for (int i = bodySegments - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = cx[0] + 1; // displacement of head coordinates  
                    bodySegments++; // +1 segment
                    apple.CreateApple(cx, cy, bodySegments, m, n);
                }
            }
            SnakeOutput();
        }

        public void SnakeOutput() // outputs whole snake on new positions
        {
            Console.SetCursorPosition(2 * cx[0], cy[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(head);

            for (int i = 1; i < bodySegments - 1; i++)
            {
                Console.SetCursorPosition(2 * cx[i], cy[i]);
                Console.WriteLine(body);
            }

            Console.SetCursorPosition(2 * cx[bodySegments - 1], cy[bodySegments - 1]);
            Console.WriteLine(table.Character); // deletes last character
            Console.ResetColor();
        }

        public bool CheckCollision() // checks if collision happened
        {
            for (int i = 1; i < bodySegments - 1; i++) // i = 1, because head is not counted
            {
                if (n == cx[i] && m == cy[i])
                {
                    collision = false;
                }
            }
            return collision;
        }
    }
}
