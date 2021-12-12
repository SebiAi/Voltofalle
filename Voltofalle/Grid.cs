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

        Grid(List<List<TextBox>> IOBoxes)
        {
            // Initialize Rows
            this.rows = new List<Axis>();

            // Add all 7 rows with input values
            for (int row = 0; row < 7; row++)
            {
                rows.Add(new Axis());
            }
        }
    }
}
