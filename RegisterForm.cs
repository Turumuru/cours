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
  public partial class RegisterForm : Form
  {
    public RegisterForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string message = "Аккаунт успешно зарегестрирован.";
        string caption = "Успех!";
        MessageBoxButtons buttons = MessageBoxButtons.OK;
        DialogResult result;
        result = MessageBox.Show(message, caption, buttons);
        if (result == System.Windows.Forms.DialogResult.OK)
        {
          this.Hide();
          LoginForm loginForm = new LoginForm();
          loginForm.Show();
        }
      
    }

    private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void password_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
