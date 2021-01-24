using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 7000; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера

        static string ClientS (string message)
        {
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
                answer_data += builder.ToString();
                // Console.WriteLine("ответ сервера: " + builder.ToString());

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                // Console.Read();

                return answer_data;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                // Console.Read();
                return ex.Message;
            }
            
        }
        static void Main(string[] args)
        {
            string message = "";
            ClientS(message);
        }
    }
}