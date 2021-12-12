using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public int readField(TextBox textBox, bool isInput)
        {
            try
            {
                // Try convert to number
                currentValue = Convert.ToUInt16(textBox.Text);
            }
            catch
            {
                // On error convert symbol to number
                if (textBox.Text != "")
                {
                    switch (textBox.Text[0])
                    {
                        case 'X':
                            currentValue = Global.valueX;
                            break;
                        case 'B':
                            currentValue = Global.valueB;
                            break;
                        case '#':
                            currentValue = Global.valueHashtag;
                            break;
                        default:
                            currentValue = Global.valueDot;
                            break;
                    }
                }
                else
                {
                    // Is input?
                    if (isInput)
                    {
                        MessageBox.Show("Inputs can't be empty!", Global.messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return 1;
                    }
                    currentValue = Global.valueDot;
                }
            }
            return 0;
        }
    }
}
