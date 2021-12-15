using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Activities.Expressions;

namespace Voltofalle
{
    public partial class MainForm : Form
    {
        private List<List<TextBox>> IOBoxes = new List<List<TextBox>>();

        public MainForm()
        {
            InitializeComponent();

            // Add all TextBoxes
            List<TextBox> row1 = new List<TextBox>(); row1.Add(grid11); row1.Add(grid12); row1.Add(grid13); row1.Add(grid14); row1.Add(grid15); row1.Add(row1p); row1.Add(row1v);
            IOBoxes.Add(row1);
            List<TextBox> row2 = new List<TextBox>(); row2.Add(grid21); row2.Add(grid22); row2.Add(grid23); row2.Add(grid24); row2.Add(grid25); row2.Add(row2p); row2.Add(row2v);
            IOBoxes.Add(row2);
            List<TextBox> row3 = new List<TextBox>(); row3.Add(grid31); row3.Add(grid32); row3.Add(grid33); row3.Add(grid34); row3.Add(grid35); row3.Add(row3p); row3.Add(row3v);
            IOBoxes.Add(row3);
            List<TextBox> row4 = new List<TextBox>(); row4.Add(grid41); row4.Add(grid42); row4.Add(grid43); row4.Add(grid44); row4.Add(grid45); row4.Add(row4p); row4.Add(row4v);
            IOBoxes.Add(row4);
            List<TextBox> row5 = new List<TextBox>(); row5.Add(grid51); row5.Add(grid52); row5.Add(grid53); row5.Add(grid54); row5.Add(grid55); row5.Add(row5p); row5.Add(row5v);
            IOBoxes.Add(row5);
            List<TextBox> row6 = new List<TextBox>(); row6.Add(column1p); row6.Add(column2p); row6.Add(column3p); row6.Add(column4p); row6.Add(column5p);
            IOBoxes.Add(row6);
            List<TextBox> row7 = new List<TextBox>(); row7.Add(column1v); row7.Add(column2v); row7.Add(column3v); row7.Add(column4v); row7.Add(column5v);
            IOBoxes.Add(row7);

#if DEBUG
            DebugButton.Visible = true;
#endif
        }        

        private void OnValidate(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string regex = "[XB#.1-3]";

            // If row or column input only match 0-10
            if (textBox.Name.Contains("row") || textBox.Name.Contains("column"))
            {
                try
                {
                    if (Convert.ToUInt16(textBox.Text) <= 15 && textBox.Name.Contains("p") || Convert.ToUInt16(textBox.Text) <= 5 && textBox.Name.Contains("v"))
                        regex = "[0-9]+";
                    else
                        throw new FormatException();
                }
                catch
                {
                    // If not convertable to number
                    regex = "100";
                }                
            }
            MatchCollection matches = Regex.Matches(textBox.Text, regex);
            if (matches.Count == 0)
            {
                if (textBox.Text == "")
                {
                    // OK
                    return;
                }
                textBox.Text = "";
                e.Cancel = true;
            }
        }        

        //private enum LookupTableValue
        //{
        //    NOTHING = 0,
        //    ONE = 1,
        //    TWO = 2,
        //    THREE = 3,
        //    ONE_TWO = 4,
        //    ONE_THREE = 5,
        //    TWO_THREE = 6,
        //    ONE_TWO_THREE = 7
        //}

        ///**
        // * @brief Lookup Table
        // * 
        // * @param number 2-11
        // * @param countFields 2-4
        // * 
        // * @return Combinationm numbers
        // */
        //private static readonly LookupTableValue[,] numberLookupTable = new LookupTableValue[11, 3]
        //{
        //    //          2                        3                       4
        //    { LookupTableValue.ONE, LookupTableValue.NOTHING, LookupTableValue.NOTHING },                  // 2
        //    { LookupTableValue.ONE_TWO, LookupTableValue.ONE, LookupTableValue.NOTHING },                  // 3
        //    { LookupTableValue.ONE_TWO_THREE, LookupTableValue.ONE_TWO, LookupTableValue.ONE },            // 4
        //    { LookupTableValue.TWO_THREE, LookupTableValue.ONE_TWO_THREE, LookupTableValue.ONE_TWO },      // 5
        //    { LookupTableValue.THREE, LookupTableValue.ONE_TWO_THREE, LookupTableValue.ONE_TWO_THREE },    // 6
        //    { LookupTableValue.NOTHING, LookupTableValue.ONE_TWO_THREE, LookupTableValue.ONE_TWO_THREE },  // 7
        //    { LookupTableValue.NOTHING, LookupTableValue.TWO_THREE, LookupTableValue.ONE_TWO_THREE },      // 8
        //    { LookupTableValue.NOTHING, LookupTableValue.THREE, LookupTableValue.ONE_TWO_THREE },          // 9
        //    { LookupTableValue.NOTHING, LookupTableValue.NOTHING, LookupTableValue.ONE_TWO_THREE },        // 10
        //    { LookupTableValue.NOTHING, LookupTableValue.NOTHING, LookupTableValue.TWO_THREE },            // 11
        //    { LookupTableValue.NOTHING, LookupTableValue.NOTHING, LookupTableValue.THREE }                 // 12
        //};

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            // Init grid and read TextBoxes
            Grid Voltofalle = new Grid(IOBoxes);
            if (Voltofalle.readValues() != 0)
                return;

            // Do calculation stuff
            if (Voltofalle.calculate() != 0)
                return;

            // Write textBoxes
            Voltofalle.outputValues();

            MessageBox.Show(this, "Done", Global.messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private int DoCalculation()
        //{
        //    // Check if row/column is 1 or Bomb
        //    ProcessDeadRows();

        //    // Try to calculate values
        //    bool foundValue_s = false;

        //    for (int row = 0, column = 0; row < 5; row++, column++)
        //    {
        //        // Check for row
        //        int deltaRow = allValues[row, 5] + allValues[row, 6] - SumOfRow(row);
        //        if (deltaRow > 0)
        //        {
        //            int sumDots = SumOfDotsRow(row);

        //            // Some error
        //            if (sumDots == 0)
        //            {
        //                MessageBox.Show(this, $"Input error!\r\n\r\nNo space to insert calculated number (not dots).\r\n{{Row: {row + 1}}}",
        //                    messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return 1;
        //            }
        //            if (sumDots == 1)
        //            {
        //                // Simple version: Replace dot with calculated value
        //                for (int tmpColumn = 0; tmpColumn < 5; tmpColumn++)
        //                {
        //                    // Find dot
        //                    if (allValues[row, tmpColumn] == valueDot)
        //                    {
        //                        // Replace
        //                        allValues[row, tmpColumn] = deltaRow + 1;
        //                        break;
        //                    }
        //                }
        //                foundValue_s = true;
        //            }
        //            // More complicated
        //            else if (sumDots >= 1)
        //            {
        //                // TODO: Implement more than two Dots in Row

        //                // Count of fields to fill
        //                int countFields = 5 - allValues[row, 6];
        //                int number = allValues[row, 5];

        //                LookupTableValue combinationNumber = numberLookupTable[number - 2, countFields - 2];
        //            }
        //        }

        //        // Check for column
        //        int deltaColumn = allValues[5, column] + allValues[6, column] - SumOfColumn(column);
        //        if (deltaColumn > 0)
        //        {
        //            int sumDots = SumOfDotsColumn(column);

        //            // Some error
        //            if (sumDots == 0)
        //            {
        //                MessageBox.Show(this, $"Input error!\r\n\r\nNo space to insert calculated number (not dots).\r\n{{Column: {column + 1}}}",
        //                    messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return 1;
        //            }
        //            if (sumDots == 1)
        //            {
        //                // Simple version: Replace dot with calculated value
        //                for (int tmpRow = 0; tmpRow < 5; tmpRow++)
        //                {
        //                    // Find dot
        //                    if (allValues[tmpRow, column] == valueDot)
        //                    {
        //                        // Replace
        //                        allValues[tmpRow, column] = deltaColumn + 1;
        //                        break;
        //                    }
        //                }
        //                foundValue_s = true;
        //            }
        //            // More complicated
        //            else if (sumDots >= 1)
        //            {
        //                // TODO: Implement more than two Dots in Column
        //            }
        //        }
        //    }

        //    if (foundValue_s)
        //    {
        //        // "Input" changed => Recalculate
        //        return DoCalculation();
        //    }

        //    return 0;
        //}

        //private int SumOfDotsRow(int row)
        //{
        //    int sum = 0;
        //    for (int i = 0; i < 5; i++)
        //    {
        //        if (allValues[row, i] == valueDot || allValues[row, i] == valueHashtag)
        //            sum++;
        //    }
        //    return sum;
        //}

        //private int SumOfDotsColumn(int column)
        //{
        //    int sum = 0;
        //    for (int i = 0; i < 5; i++)
        //    {
        //        if (allValues[i, column] == valueDot || allValues[i, column] == valueHashtag)
        //            sum++;
        //    }
        //    return sum;
        //}

        private void ClearAll_Click(object sender, EventArgs e)
        {
            foreach (List<TextBox> textBoxesRow in IOBoxes)
            {
                foreach (TextBox textBox in textBoxesRow)
                {
                    textBox.Text = "";
                }
            }
        }

        private void ClearOutput_Click(object sender, EventArgs e)
        {
            int row = 0;            
            foreach (List<TextBox> textBoxesRow in IOBoxes)
            {
                if (row >= 5)
                    break;
                int column = 0;
                foreach (TextBox textBox in textBoxesRow)
                {
                    if (column >= 5)
                        break;
                    textBox.Text = "";
                    column++;
                }
                row++;
            }
        }

        private void DebugButton_Click(object sender, EventArgs e)
        {
            // Inputs
            row1p.Text = "8";
            row1v.Text = "0";

            row2p.Text = "1";
            row2v.Text = "4";

            row3p.Text = "4";
            row3v.Text = "1";

            row4p.Text = "6";
            row4v.Text = "0";

            row5p.Text = "6";
            row5v.Text = "1";


            column1p.Text = "6";
            column1v.Text = "1";

            column2p.Text = "3";
            column2v.Text = "2";

            column3p.Text = "6";
            column3v.Text = "1";

            column4p.Text = "4";
            column4v.Text = "1";

            column5p.Text = "6";
            column5v.Text = "1";

            // Outputs
            grid11.Text = "1";
            grid12.Text = "1";
            grid13.Text = "2";
            grid14.Text = "1";
            grid15.Text = "3";

            grid41.Text = "2";
            grid42.Text = "1";
            grid43.Text = "1";
            grid44.Text = "1";
            grid45.Text = "1";
        }
    }
}
