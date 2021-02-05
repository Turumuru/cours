using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Course
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
        InitializeComponent();
        }

        static string ClientS(string message)
        {
            // адрес и порт сервера, к которому будем подключаться
            int port = 7000; // порт сервера
            string address = "127.0.0.1"; // адрес сервера

            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                string answer_data = "";

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                answer_data = builder.ToString();
                //Console.WriteLine("ответ сервера: " + builder.ToString());

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                //Console.Read();

                return answer_data;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                // Console.Read();
                return ex.Message;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if ((login_textBox.Text != "") && (password_textBox.Text != ""))
            {
                string get = "get_authorization" + "/" + login_textBox.Text + "/" + password_textBox.Text;
                string[] Tex = ClientS(get).Split(new char[] { '"' });

                if (Tex[1] == "Неверный логин или пароль")
                {
                    string message = "Неверный логин или пароль";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    string caption = "Ошибка!";
                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Hide();
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                    }
                }
                if (Tex[1] == "Кладовщик")
                {
                    this.Hide();
                    StorageForm storageForm = new StorageForm();
                    storageForm.Show();
                }
                if (Tex[1] == "Менеджер")
                {
                    this.Hide();
                    ManagerForm managerForm = new ManagerForm();
                    managerForm.Show();
                }
                if (Tex[1] == "Дирекция")
                {
                    this.Hide();
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                }
                if (Tex[1] == "Заказчик")
                {
                    this.Hide();
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.Show();
                }
            }
            else
            {
                string message = "Вы не ввели логин или пароль";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                string caption = "Ошибка!";
                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.Hide();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                }
            }
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
