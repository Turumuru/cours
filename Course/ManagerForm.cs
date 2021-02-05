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
  public partial class ManagerForm : Form
  {
    public ManagerForm()
    {
      InitializeComponent();
    }

    private void ManagerForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }
  }
}
