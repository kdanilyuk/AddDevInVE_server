using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonServer
{
    public partial class Form1 : Form
    {
        private const string defaultGateway = "0.0.0.0";
        private const int PORT = 8005;
        private const int lengthQueue = 10;
        private const int SIZE = 1024;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.AppendText("Сервер запущен...");
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(defaultGateway), PORT);
                Socket s1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                s1.Bind(ipEndPoint); // Связываем объёкт Socket с локальной конечной точкой 
                s1.Listen(lengthQueue); // Устанавливаем объект Socket в состояние прослушивания, 
                                        // задаем количество клиентов, ожидающих в очередфи
                richTextBox1.AppendText("\tSERVER INFORMATION");
                richTextBox1.AppendText("Descriptor\t: " + s1.Handle);
                richTextBox1.AppendText("IPv4\t\t: " + ipEndPoint.Address.ToString());
                richTextBox1.AppendText("Port\t\t: " + PORT + Environment.NewLine);
                Thread.Sleep(1000);
                while (true)
                {
                    richTextBox1.AppendText("Сервер ждёт подключения клиентов: ");
                    //ПРограмма приостанавливается, ожидая входящее соединение
                    Socket s2 = s1.Accept(); // Извлекает из очередиоиждающих запросов первый запрос на соединение и создает для его обработки новый соект

                    richTextBox1.AppendText("Получен запрос от клиента на установление соединения:\n");

                    //декодируем все символы в последовательность байтов

                    Random rnd = new Random();
                    int dataSend = rnd.Next(5, 16);

                    byte[] byteSend = Encoding.ASCII.GetBytes(dataSend.ToString());
                    //Передаем данные клиенту
                    int lenBytesSend = s2.Send(byteSend);

                    richTextBox1.AppendText("Передано клиенту успешно " + lenBytesSend + " байт; ");

                    s2.Shutdown(SocketShutdown.Both); // Блокирует передачу и получение данных
                    s2.Close(); // закрывает подключение Socket и освбождает все связанные ресурсы
                    richTextBox1.AppendText("Сервер завершил соединение");
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("Ошибка: " + ex.ToString());
            }
            finally
            {
                richTextBox1.AppendText("");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
