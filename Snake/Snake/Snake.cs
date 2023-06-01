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
        private int[] coordinatesX; // x coordinates of body parts
        private int[] coordinatesY; // y coordinates of body parts
        private int bodyParts; // counter of all body parts (including head and "remover")
        private bool collision;

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public Snake(Table table, Apple apple, char head, char body)
        {
            this.head = head;
            this.body = body;
            this.apple = apple;
            this.table = table;
            coordinatesX = new int[(table.Length - 2) * (table.Length - 2)];
            coordinatesY = new int[(table.Length - 2) * (table.Length - 2)];
            n = (table.Length + 1) / 2 - 1;
            m = n;
            coordinatesX[0] = n;
            coordinatesY[0] = m;
            coordinatesX[1] = n; // x coor. of "remover"
            coordinatesY[1] = m + 1; // y coor. of "remover"
            bodyParts = 2; // number of body segments: head + "remover"
            collision = true;

            apple.CreateApple(coordinatesX, coordinatesY, bodyParts, m, n); // creates first apple
        }

        void MoveSnake()
        {
            for (int i = bodyParts - 1; i > 0; i--)
            {
                coordinatesX[i] = coordinatesX[i - 1];
                coordinatesY[i] = coordinatesY[i - 1];  
            }
        }

        void MoveSnakeAfterEatingApple()
        {
            for (int i = bodyParts - 1; i > -1; i--)
            {
                coordinatesX[i + 1] = coordinatesX[i];
                coordinatesY[i + 1] = coordinatesY[i];
            }
        }

        void ChangeCoordinateAfterCrossingTheEdge(Direction direction)
        {
            if (direction == Direction.Up)
                coordinatesY[0] = table.Length - 2;
            else if (direction == Direction.Down)
                coordinatesY[0] = 1;
            else if (direction == Direction.Left)
                coordinatesX[0] = table.Length - 2;
            else if (direction == Direction.Right)
                coordinatesX[0] = 1;
        }

        void ChangeCoordinatesAfterNormalMovement(Direction direction)
        {
            if (direction == Direction.Up)
                coordinatesY[0] = coordinatesY[0] - 1;
            else if (direction == Direction.Down)
                coordinatesY[0] = coordinatesY[0] + 1;
            else if (direction == Direction.Left)
                coordinatesX[0] = coordinatesX[0] - 1;
            else if (direction == Direction.Right)
                coordinatesX[0] = coordinatesX[0] + 1;
        }

        void OverTheEdgeMovement(Direction direction)
        {
            if (apple.CheckStateOfApple(m, n) == false) // false -> snakes moves normally 
            {
                MoveSnake();
                ChangeCoordinateAfterCrossingTheEdge(direction);

            }
            else // snake eats apple and its body grows
            {
                MoveSnakeAfterEatingApple();
                ChangeCoordinateAfterCrossingTheEdge(direction);
                bodyParts++; // +1 body segment
                apple.CreateApple(coordinatesX, coordinatesY, bodyParts, m, n);
            }
        }

        void NormalMovement(Direction direction)
        {
            if (apple.CheckStateOfApple(m, n) == false) // false -> snakes moves normally 
            {
                MoveSnake();
                ChangeCoordinatesAfterNormalMovement(direction);
            }
            else // snake eats apple and its body grows 
            {
                MoveSnakeAfterEatingApple();
                ChangeCoordinatesAfterNormalMovement(direction);
                bodyParts++; // +1 body segment 
                apple.CreateApple(coordinatesX, coordinatesY, bodyParts, m, n);
            }
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
                OverTheEdgeMovement(Direction.Up);
            }
            else
                NormalMovement(Direction.Up);
            SnakeOutput();
        }

        /// <summary>
        /// Downwards movement of snake
        /// </summary>
        public void GoDown()
        {
            m++;
            if (m == table.Length - 1) // over the edge
            {
                m = 1;
                OverTheEdgeMovement(Direction.Down);
            }
            else
                NormalMovement(Direction.Down);
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
                OverTheEdgeMovement(Direction.Left);
            }
            else
                NormalMovement(Direction.Left);
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
                OverTheEdgeMovement(Direction.Right);
            }
            else
                NormalMovement(Direction.Right);
            SnakeOutput();
        }
        /// <summary>
        /// Outputing snake on the screen
        /// </summary>
        public void SnakeOutput() // outputs whole snake on new positions
        {
            Console.SetCursorPosition(2 * coordinatesX[0], coordinatesY[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(head);

            for (int i = 1; i < bodyParts - 1; i++)
            {
                Console.SetCursorPosition(2 * coordinatesX[i], coordinatesY[i]);
                Console.WriteLine(body);
            }

            Console.SetCursorPosition(2 * coordinatesX[bodyParts - 1], coordinatesY[bodyParts - 1]);
            Console.WriteLine(table.Character); // deletes last character
            Console.ResetColor();
        }
        /// <summary>
        /// Checks if snake collided with his body
        /// </summary>
        /// <returns> did/did not </returns>
        public bool CheckCollision() // checks if collision happened
        {
            for (int i = 1; i < bodyParts - 1; i++) // i = 1, because head is not counted to body parts without "remover"
            {
                if (n == coordinatesX[i] && m == coordinatesY[i])
                {
                    collision = false;
                }
            }
            return collision;
        }
    }
}
