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
    public partial class TablePrintForm : Form
    {
        private Form pForm;
        public TablePrintForm(Form parentForm, DataSet ds, string tableName)
        {
            InitializeComponent();
            pForm = parentForm;
            dataGridView.DataSource = ds.Tables[tableName];
            DGVresize.Set(dataGridView);
        }

        private void TablePrintForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cansel();
        }
        private void Cansel()
        {
            pForm.Show();
        }

        private void TablePrintForm_Shown(object sender, EventArgs e)
        {
            pForm.Hide();
        }

        private void Print_Click(object sender, EventArgs e)
        {

        }

        private void Accept_Click(object sender, EventArgs e)
        {
            this.Close();
            Cansel();
        }
    }
}
