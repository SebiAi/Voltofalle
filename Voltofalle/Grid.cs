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

        public Grid()
        {
            // Initialize Rows
            this.rows = new List<Axis>();
        }

        public int readValues(List<List<TextBox>> IOBoxes)
        {
            // Read all 7 rows
            foreach (List<TextBox> IOBoxesRow in IOBoxes)
            {
                Axis axis = new Axis();
                if (axis.readAxis(IOBoxesRow) != 0)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
