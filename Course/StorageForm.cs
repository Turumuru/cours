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
  public partial class StorageForm : Form
  {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlCommandBuilder sqlBuilder1 = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlDataAdapter sqlDataAdapter1 = null;
        private DataSet dataSet = null;
        private DataSet dataSet1 = null;

        public StorageForm()
    {
      
      InitializeComponent();
            
    }
        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT id_cloth, name, price, length FROM Cloths", sqlConnection);
                sqlDataAdapter1 = new SqlDataAdapter("SELECT id_fittings, name, price, weight FROM Fittings", sqlConnection);

                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlBuilder1 = new SqlCommandBuilder(sqlDataAdapter1);

                //sqlBuilder.GetInsertCommand();
                //sqlBuilder.GetUpdateCommand();

                //sqlBuilder1.GetInsertCommand();
                //sqlBuilder1.GetUpdateCommand();

                dataSet = new DataSet();
                dataSet1 = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Cloths");
                sqlDataAdapter1.Fill(dataSet1, "Fittings");

                dataGridView2.DataSource = dataSet.Tables["Cloths"];
                dataGridView1.DataSource = dataSet1.Tables["Fittings"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ощибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StorageForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nastya\Desktop\Course-main\Course\Course\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();

            LoadData();
        }

        private void ReloadDataCloths()
        {
            try
            {
                dataSet.Tables["Cloths"].Clear();

                sqlDataAdapter.Fill(dataSet, "Cloths");

                dataGridView2.DataSource = dataSet.Tables["Cloths"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ощибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ReloadDataFittings()
        {
            try
            {
                dataSet1.Tables["Fittings"].Clear();

                sqlDataAdapter1.Fill(dataSet1, "Fittings");

                dataGridView1.DataSource = dataSet1.Tables["Fittings"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ощибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void StorageForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void receipt_MenuItem_Click(object sender, EventArgs e)
    {
      this.Hide();
      ReceiptForm receiptForm = new ReceiptForm();
      receiptForm.Show();
    }

    private void writeOff_MenuItem_Click(object sender, EventArgs e)
    {
      this.Hide();
      WriteOffForm writeOffForm = new WriteOffForm();
      writeOffForm.Show();
    }

    private void inventory_MenuItem_Click(object sender, EventArgs e)
    {

    }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    dataSet.Tables["Cloths"].Rows[i]["length"] = dataGridView2[3, i].Value;
                }

                sqlDataAdapter.Update(dataSet, "Cloths");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ощибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ReloadDataCloths();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
             try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataSet1.Tables["Fittings"].Rows[i]["weight"] = dataGridView1[3, i].Value;
                }

                sqlDataAdapter1.Update(dataSet1, "Fittings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ощибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ReloadDataFittings();
        }
    }
}
