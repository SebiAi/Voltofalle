using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voltofalle
{
    class Field
    {
        public int currentValue;
        public List<char> possibleValues;

        public Field()
        {
            this.possibleValues = new List<char>();
            this.currentValue = 0;
        }
    }
}
