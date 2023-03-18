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
        public Snake(Table table, Apple apple, char head, char body) 
        {
            this.table = table;
            this.apple = apple;
            this.head = head;
            this.body = body;
        }
    }
}
