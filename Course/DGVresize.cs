using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course
{
    class DGVresize
    {
        public static void Set(DataGridView dgv)
        {
            dgv.Columns[0].Width = (int)(dgv.Width * 0.2);
            dgv.Columns[1].Width = (int)(dgv.Width * 0.4);
            dgv.Columns[2].Width = (int)(dgv.Width * 0.2);
            dgv.Columns[3].Width = (int)(dgv.Width * 0.2);
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
