using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        public int readAxis(List<TextBox> IOBoxesAxis)
        {
            int i = 0;
            foreach (TextBox textBox in IOBoxesAxis)
            {
                Field field = new Field();
                if (field.readField(textBox, (i >= 5) ? true : false) != 0)
                {
                    return 1;
                }
                i++;
            }
            return 0;
        }
    }
}
