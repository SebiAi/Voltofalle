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
        private const int COUNT_POINTS_INDEX = 5;
        private const int COUNT_BOMBS_INDEX = 6;
        public bool isInput;

        public Axis(bool isInput)
        {
            // Initiate variables
            this.fields = new List<Field>();
            this.isInput = isInput;
        }

        public int readAxis(List<TextBox> IOBoxesAxis)
        {
            int i = 0;
            foreach (TextBox textBox in IOBoxesAxis)
            {
                Field field = new Field();
                if (field.readField(textBox, (i >= 5 || isInput) ? true : false) != 0)
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

        public int GetPoints()
        {
            return fields[COUNT_POINTS_INDEX].currentValue;
        }

        public int GetRemainingPoints()
        {
            int remainingPoints = GetPoints();
            foreach (Field field in fields)
            {
                if (field.isInput || field.IsFixUnknown())
                    continue;
                remainingPoints -= (field.currentValue == Global.valueB) ? 0 : field.currentValue;
            }
            return remainingPoints;
        }

        public int GetBombs()
        {
            return fields[COUNT_BOMBS_INDEX].currentValue;
        }

        public int GetRemainingBombs()
        {
            int remainingBombs = GetBombs();
            foreach (Field field in fields)
            {
                if (field.isInput || field.currentValue != Global.valueB)
                    continue;
                remainingBombs -= 1;
            }
            return remainingBombs;
        }

        public int CalculateSum()
        {
            int sum = 0;
            foreach (Field field in fields)
            {
                if (field.isInput)
                    continue;

                // If 0 bombs
                if (GetBombs() == 0)
                    return GetPoints();

                switch (field.currentValue)
                {
                    case 2:
                        sum += 2;
                        break;
                    case 3:
                        sum += 3;
                        break;
                    default:
                        sum++;
                        break;
                }
            }            
            return sum;
        }

        public void SetCurrentValueAxis()
        {
            foreach (Field field in fields)
            {
                if (field.isInput)
                    continue;
                // Set to valueX if bombs and points are not 0
                if (GetBombs() != 0 && GetPoints() != 0 && field.IsUnknownV1())
                    field.currentValue = Global.valueX;
                // Set to valueHashtag if bombs is 0
                if (GetBombs() == 0 && field.IsUnknownV1())
                    field.currentValue = Global.valueHashtag;
                // Set to valueB if points is 0
                if (GetPoints() == 0 && field.IsUnknownV1())
                    field.currentValue = Global.valueB;
            }
        }

        public int CountUnknownFields()
        {
            int sum = 0;
            foreach (Field field in fields)
            {
                if (field.isInput)
                    continue;

                if (field.IsUnknownV2())
                    sum++;
            }
            return sum;
        }

        public Field GetFirstUnknownField()
        {
            Field returnField = null;

            foreach (Field field in fields)
            {
                if (field.isInput)
                    continue;

                if (field.IsUnknownV2())
                    returnField = field;

                // Break if we found the first unknown field
                if (returnField != null)
                    break;
            }

            return returnField;
        }
    }
}
