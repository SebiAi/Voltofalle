using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voltofalle
{
    class Grid
    {
        public List<Axis> rows;
        private List<List<TextBox>> IOBoxes;

        public Grid(List<List<TextBox>> IOBoxes)
        {
            // Initialize
            this.IOBoxes = IOBoxes;
            this.rows = new List<Axis>();
        }

        public int readValues()
        {
            int i = 0;
            // Read all 7 rows
            foreach (List<TextBox> IOBoxesRow in IOBoxes)
            {
                Axis axis = new Axis((i >= 5) ? true : false);
                if (axis.readAxis(IOBoxesRow) != 0)
                {
                    return 1;
                }
                rows.Add(axis);
                i++;
            }
            return 0;
        }

        public void outputValues()
        {
            // Output all 7 rows
            int i = 0;
            foreach (Axis axis in rows)
            {
                axis.outputAxis(IOBoxes[i]);
                i++;
            }
        }

        public int calculate()
        {
            // Check if row/column is 1 or Bomb
            ProcessDeadRows();
            return 0;
        }

        private void ProcessDeadRows()
        {
            int columnCounter = 0;
            foreach (Axis row in rows)
            {
                if (row.isInput)
                    continue;
                // Check for row
                ProcessDeadRowsHelper(row);
                

                // Check for column
                Axis column = GetColumn(columnCounter);
                ProcessDeadRowsHelper(column);

                columnCounter++;
            }
        }

        private void ProcessDeadRowsHelper(Axis axis)
        {
            if (axis.GetPoints() + axis.GetBombs() == axis.CalculateSum() || axis.GetPoints() == 0 || axis.GetBombs() == 0)
            {
                axis.SetCurrentValueAxis();
            }
        }

        public Axis GetColumn(int n)
        {
            Axis column = new Axis(false);
            if (n >= 5)
                return column;
            
            foreach (Axis row in rows)
            {
                column.fields.Add(row.fields[n]);
            }

            return column;
        }
    }
}
