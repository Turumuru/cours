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
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Course
{
    public partial class StorageForm : Form
    {
        public StorageForm()
        {
            InitializeComponent();


            string[] get = ClientS("get_list_cloth").Split(new char[] { ',' });
            int t = 0;
            int count_cloth = 0;

            for (int i = 0; i < (get.Length)/3; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Label text = new Label();
                    if (j == 2)
                    {
                        text.Text = get[t].Substring(0, get[t].Length - 1);

                        if ((j == 2) && (i == ((get.Length) / 3) - 1))
                        {
                            text.Text = text.Text.Substring(0, text.Text.Length - 1);
                        }
                    }
                    if (j == 0)
                    {
                        text.Text = get[t].Substring(2);
                        if ((j == 0) && (i == 0))
                        {
                            text.Text = text.Text.Substring(1);
                        }
                    }
                    if ((j != 2) && (j != 0))
                    {
                        text.Text = get[t];
                    }
                    t++;
                    fabric_tableLayoutPanel.Controls.Add(text, j, i);
                }
            }

            get = ClientS("get_list_fittings").Split(new char[] { ',' });
            t = 0;

            for (int i = 0; i < (get.Length) / 3; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Label text = new Label();
                    if (j == 2)
                    {
                        text.Text = get[t].Substring(0, get[t].Length - 1);
                        if ((j == 2) && (i == ((get.Length) / 3) - 1))
                        {
                            text.Text = text.Text.Substring(0, text.Text.Length - 1);
                        }
                    }
                    if (j == 0)
                    {
                        text.Text = get[t].Substring(3);
                        if ((j == 0) && (i == 0))
                        {
                            text.Text = text.Text.Substring(1);
                        }
                    }
                    if ((j != 2) && (j != 0))
                    {
                        text.Text = get[t];
                    }
                    t++;
                    furniture_tableLayoutPanel.Controls.Add(text, j, i);
                }
            }
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
            this.Hide();
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.Show();
        }

    }
}
