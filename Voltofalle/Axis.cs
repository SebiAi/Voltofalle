using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voltofalle
{
    class Axis
    {
        public List<Field> fields;
        public int points;
        public int bombs;

        public Axis()
        {
            // Initiate variables
            this.fields = new List<Field>();
            this.points = 0;
            this.bombs = 0;

            // Add all 7 Fields
            for (int i = 0; i < 7; i++)
            {
                fields.Add(new Field());
            }
        }
    }
}
