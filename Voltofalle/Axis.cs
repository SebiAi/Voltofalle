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

        public Axis()
        {
            // Initiate variables
            this.fields = new List<Field>();
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
                fields.Add(field);
                i++;
            }
            return 0;
        }

        public void outputAxis(List<TextBox> IOBoxesAxis)
        {
            int i = 0;
            foreach (Field field in fields)
            {
                field.outputField(IOBoxesAxis[i]);
                i++;
            }
        }
    }
}
