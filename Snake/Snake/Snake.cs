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
        private int[] cx; // x coordinates of body parts
        private int[] cy; // y coordinates of body parts
        private int bodyParts; // counter of all body parts (including head and "remover")
        private bool collision;

        public Snake(Table table, Apple apple, char head, char body)
        {
            this.head = head;
            this.body = body;
            this.apple = apple;
            this.table = table;
            cx = new int[(table.Length - 2) * (table.Length - 2)];
            cy = new int[(table.Length - 2) * (table.Length - 2)];
            n = (table.Length + 1) / 2 - 1;
            m = n;
            cx[0] = n;
            cy[0] = m;
            cx[1] = n; // x coor. of "remover"
            cy[1] = m + 1; // y coor. of "remover"
            bodyParts = 2; // number of body segments: head + "remover"
            collision = true;

            apple.CreateApple(cx, cy, bodyParts, m, n); // creates first apple
        }

        /// <summary>
        /// Upwards movement of snake
        /// </summary>
        public void GoUp()
        {
            m--; // coordinates of head for control 
            if (m == 0) // collision with edge 
            {
                m = table.Length - 2; // head of snake goes over the edge (upwards)
                if (apple.CheckStateOfApple(m, n) == false) // false -> snakes moves normally 
                {
                    for (int i = bodyParts - 1; i > 0; i--)  
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1]; // swapping y coordinates 
                    }
                    cy[0] = table.Length - 2; // displacement of head (upwards)
                }
                else // snake eats apple and its body grows
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = table.Length - 2; // displacement of head (upwards)
                    bodyParts++; // +1 body segment
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            else // out of range
            {
                if (apple.CheckStateOfApple(m, n) == false) // false -> snakes moves normally 
                {
                    for (int i = bodyParts - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cy[0] = cy[0] - 1; // displacement of head (upwards)
                }
                else // snake eats apple and its body grows 
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = cy[0] - 1; // displacement of head (upwards)
                    bodyParts++; // +1 body segment 
                    
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            SnakeOutput();
        }

        /// <summary>
        /// Downwards movement of snake
        /// </summary>
        public void GoDown()
        {
            m++;
            if (m == table.Length - 1) 
            {
                m = 1;
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodyParts - 1; i > 0; i--)
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1]; 
                    }
                    cy[0] = 1;
                }
                else 
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = 1;
                    bodyParts++;
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            else // out of range
            {
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodyParts - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cy[0] = cy[0] + 1;
                }
                else
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cy[0] = cy[0] + 1;
                    bodyParts++;
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            SnakeOutput();
        }
        /// <summary>
        /// Left movement of snake
        /// </summary>
        public void GoLeft()
        {
            n--;
            if (n == 0)
            {
                n = table.Length - 2;
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodyParts - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cx[0] = table.Length - 2;
                }
                else
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = table.Length - 2;
                    bodyParts++;
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            else
            {
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodyParts - 1; i > 0; i--)  
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cx[0] = cx[0] - 1;
                }
                else 
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = cx[0] - 1;
                    bodyParts++;
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            SnakeOutput();
        }
        /// <summary>
        /// Right movement of snake
        /// </summary>
        public void GoRight()
        {
            n++;
            if (n == table.Length - 1)
            {
                n = 1;
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodyParts - 1; i > 0; i--)  
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1]; 
                    }
                    cx[0] = 1;

                }
                else 
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = 1; 
                    bodyParts++;
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            else // out of range
            {
                if (apple.CheckStateOfApple(m, n) == false)
                {
                    for (int i = bodyParts - 1; i > 0; i--) 
                    {
                        cx[i] = cx[i - 1];
                        cy[i] = cy[i - 1];
                    }
                    cx[0] = cx[0] + 1; // displacement of head coordinates 
                }
                else // snake eats apple and its body grows
                {
                    for (int i = bodyParts - 1; i > -1; i--)
                    {
                        cx[i + 1] = cx[i];
                        cy[i + 1] = cy[i];
                    }
                    cx[0] = cx[0] + 1; // displacement of head coordinates  
                    bodyParts++; // +1 segment
                    apple.CreateApple(cx, cy, bodyParts, m, n);
                }
            }
            SnakeOutput();
        }
        /// <summary>
        /// Outputing snake on the screen
        /// </summary>
        public void SnakeOutput() // outputs whole snake on new positions
        {
            Console.SetCursorPosition(2 * cx[0], cy[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(head);

            for (int i = 1; i < bodyParts - 1; i++)
            {
                Console.SetCursorPosition(2 * cx[i], cy[i]);
                Console.WriteLine(body);
            }

            Console.SetCursorPosition(2 * cx[bodyParts - 1], cy[bodyParts - 1]);
            Console.WriteLine(table.Character); // deletes last character
            Console.ResetColor();
        }
        /// <summary>
        /// Checks if snake collided with his body
        /// </summary>
        /// <returns> did/did not </returns>
        public bool CheckCollision() // checks if collision happened
        {
            for (int i = 1; i < bodyParts - 1; i++) // i = 1, because head is not counted
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
