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
  public partial class LoginForm : Form
  {
    public LoginForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Hide();
      StorageForm storageForm = new StorageForm();
      //ManagerForm managerForm = new ManagerForm();
      storageForm.Show();
    }

    private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void loginLabel_Click(object sender, EventArgs e)
    {
      this.Hide();
      RegisterForm registerForm = new RegisterForm();
      registerForm.Show();
    }
  }
}
