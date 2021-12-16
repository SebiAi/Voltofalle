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

        #region IO Operations
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
        #endregion

        public int calculate()
        {
            int returnVal = 0;
            bool rerunCalc = false;

            // Check if row/column is 1 or Bomb
            ProcessDeadRows();

            // Calculate fields
            returnVal = ProcessSolving();
            if (returnVal != 0 && returnVal != 2)
                return returnVal;

            if (returnVal == 2)
                rerunCalc = true;

            // Calculate next best possible move
            // TODO: Implement calculation of next best possible move

            if (rerunCalc)
                returnVal = calculate();

            return returnVal;
        }

        #region Dead Rows
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
        #endregion

        #region Solving
        private int ProcessSolving()
        {
            bool foundValue = false;
            int returnVal = 0;

            int columnCounter = 0;
            foreach (Axis row in rows)
            {
                if (row.isInput)
                    continue;

                // Check for row
                returnVal = ProcessSolvingHelper(row);
                if (returnVal != 0 && returnVal != 2)
                    return returnVal;

                // If could solve at least one set to true
                if (returnVal == 2)
                    foundValue = true;

                // Check for column
                Axis column = GetColumn(columnCounter);
                returnVal = ProcessSolvingHelper(column);
                if (returnVal != 0 && returnVal != 2)
                    return returnVal;

                // If could solve at least one set to true
                if (returnVal == 2)
                    foundValue = true;

                columnCounter++;
            }

            // If could solve at least one return 2
            if (foundValue)
                return 2;

            return 0;
        }

        private int ProcessSolvingHelper(Axis axis)
        {
            int returnVal = 0;
            bool foundValue = false;

            int deltaAxis = axis.GetPoints() + axis.GetBombs() - axis.CalculateSum();
            // If bigger than 0 => not complete
            if (deltaAxis > 0)
            {
                int sumUnknownFields = axis.CountUnknownFields();

                if (sumUnknownFields == 0)
                {
                    MessageBox.Show("Input error!\r\n\r\nDid you input the right values?",
                        Global.messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }
                if (sumUnknownFields == 1)
                {
                    // Basic solving
                    returnVal = ProcessBasicSolving(axis, deltaAxis);
                    if (returnVal != 0 && returnVal != 2)
                        return returnVal;
                    if (returnVal == 2)
                        foundValue = true;
                }
                if (sumUnknownFields > 1)
                {
                    // Advanced solving
                    // TODO: Implement advanced solving
                }
            }

            if (foundValue)
                returnVal = 2;

            return returnVal;
        }

        private int ProcessBasicSolving(Axis axis, int value)
        {
            Field unknownField = axis.GetFirstUnknownField();
            if (unknownField == null)
            {
                MessageBox.Show("This error should never be displayed.\r\n\r\n If you see this you are a wizard!",
                    Global.messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            unknownField.currentValue = value + 1;
            return 2;
        }
        #endregion

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
