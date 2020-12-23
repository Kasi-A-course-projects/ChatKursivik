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
using System.Threading;

namespace MainServer
{
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
            StartServer();
        }
        TcpListener _server;
        const int MAXNUMCLIENTS = 1000;
        TcpClient[] clients = new TcpClient[MAXNUMCLIENTS];
        int _countClient = 0;
        bool _stopNetwork;

        void StartServer()
        {
            if (_server == null)
            {
                try
                {
                    _stopNetwork = false;
                    _countClient = 0;

                    int port = 15000;
                    _server = new TcpListener(IPAddress.Any, port);
                    _server.Start();

                    Thread acceptThread = new Thread(AcceptClients);
                    acceptThread.Start();
                }
                catch
                {
                    
                }
            }
        }

        void AcceptClients()
        {
            while (true)
            {
                try
                {
                    clients[_countClient] = _server.AcceptTcpClient();
                    Thread readThread = new Thread(ReceiveRun);
                    readThread.Start(_countClient);
                    _countClient++;
                }
                catch
                {

                }
                if (_stopNetwork == true)
                {
                    break;
                }

            }
        }
        protected delegate void UpdateReceiveDisplayDelegate(int clientcount, string message);

        void ReceiveRun(object num)
        {
            while (true)
            {
                try
                {
                    
                    string s = null;
                    NetworkStream ns = clients[(int)num].GetStream();

                    
                    while (ns.DataAvailable == true)
                    {
   
                        byte[] buffer = new byte[clients[(int)num].Available];

                        ns.Read(buffer, 0, buffer.Length);
                        s += Encoding.Default.GetString(buffer);
                    }

                    if (s != null)
                    {
                        Invoke(new UpdateReceiveDisplayDelegate(UpdateReceiveDisplay), new object[] { (int)num, s });

                        string ss = s;
                        string[] words = ss.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        s = words[0] + ": " + s.Remove(0 ,words[0].ToString().Length );
                        SendToClients(s, (int)num);
                        s = String.Empty;
                    }
                    Thread.Sleep(100);
                }
                catch
                {
                }

                if (_stopNetwork == true) break;

            }
        }

        public void UpdateReceiveDisplay(int clientnum, string message)
        {
            string s = message;
            string[] words = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            listBox1.Items.Add(words[0]+ ": " + message.Remove(0,words[0].Length));
        }

        void SendToClients(string text, int skipindex)
        {
            for (int i = 0; i < MAXNUMCLIENTS; i++)
            {
                if (clients[i] != null)
                {
                    NetworkStream ns = clients[i].GetStream();
                    byte[] myReadBuffer = Encoding.Default.GetBytes(text);
                    ns.BeginWrite(myReadBuffer, 0, myReadBuffer.Length, new AsyncCallback(AsyncSendCompleted), ns);
                }
            }
        }

        public void AsyncSendCompleted(IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            ns.EndWrite(ar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
