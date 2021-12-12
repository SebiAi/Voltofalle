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
            // Read all 7 rows
            foreach (List<TextBox> IOBoxesRow in IOBoxes)
            {
                Axis axis = new Axis();
                if (axis.readAxis(IOBoxesRow) != 0)
                {
                    return 1;
                }
                rows.Add(axis);
            }
            return 0;
        }
    }
}
