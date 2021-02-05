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

namespace Course
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
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
                // string message = "get_inventory";
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
            if ((password_textBox.Text == repeatPasswod_textBox.Text) && (login_textBox.Text != "") && (name_textBox.Text != ""))
            {
                string get = "get_registration" + "/" + login_textBox.Text + "/" + password_textBox.Text + "/" + name_textBox.Text;
                string[] Tex = ClientS(get).Split(new char[] { '"' });

                string message = Tex[1];

                if (message == "Этот логин уже кто-то использует")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    string caption = "Ошибка!";
                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Hide();
                        RegisterForm registerForm = new RegisterForm();
                        registerForm.Show();
                    }
                }
                if (message == "Пользователь успешно добавлен")
                {
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
            else
            {
                string message = "Пароли не совпадают либо вы не ввели логин и имя";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                string caption = "Ошибка!";
                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.Hide();
                    RegisterForm registerForm = new RegisterForm();
                    registerForm.Show();
                }
            }
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
