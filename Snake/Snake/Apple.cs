using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Apple
    {
        private Table table;
        private char apple;
        public Apple(Table table, char apple) 
        {
            this.table = table;
            this.apple = apple;
        }
    }
}
