using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    public partial class ReceiptForm : Form
    {
        List<TextBox[]> dataMat;
        private void AddRow()
        {
            tableLayoutPanel.RowCount++;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 20F));
            int lastRow = tableLayoutPanel.RowCount - 1;
            TextBox tb1 = outFilledTB.GetInstance("", false, lastRow % 2), tb2 = outFilledTB.GetInstance("", false, lastRow % 2);
            dataMat.Add(new TextBox[2] { tb1, tb2 });
            tableLayoutPanel.Controls.Add(dataMat[lastRow][0], 0, lastRow);
            tableLayoutPanel.Controls.Add(dataMat[lastRow][1], 1, lastRow);
        }

        private void RemoveRow()
        {
            int lastRow = tableLayoutPanel.RowCount - 1;
            if (lastRow > 0)
            {
                tableLayoutPanel.Controls.Remove(dataMat[lastRow][0]);
                tableLayoutPanel.Controls.Remove(dataMat[lastRow][1]);
                dataMat.RemoveAt(lastRow);
                tableLayoutPanel.RowCount--;
            }
        }
        public ReceiptForm()
        {
            InitializeComponent();
            dataMat = new List<TextBox[]>();
            tableLayoutPanel.RowCount = 0;
            AddRow();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            RemoveRow();
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            this.Hide();
            StorageForm storageForm = new StorageForm();
            storageForm.Show();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            this.Hide();
            StorageForm storageForm = new StorageForm();
            storageForm.Show();
        }
    }
}
