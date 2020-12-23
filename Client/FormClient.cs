using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Chat.DAL.Models;
using AdminPanel;


namespace Client
{
    public partial class FormClient : Form
    {
        string MyName = tmpCls.Nm;
        int MyRole = tmpCls.Rl;
        public FormClient()
        {
            InitializeComponent();
            Connect();
            if (MyRole > 2)
            {
                btnAdm.Enabled = false;
            }
        }
       
        TcpClient _tcpСlient = new TcpClient();

        NetworkStream ns;
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
       
        void Connect()
        {
            try
            {
                _tcpСlient.Connect("127.0.0.1", 15000);

                ns = _tcpСlient.GetStream();
                Thread th = new Thread(ReceiveRun);
                th.Start();
            }
            catch
            {

            }
        }

        void ReceiveRun()
        {
            while (true)
            {
                try
                {
                    string s = null;
                    while (ns.DataAvailable == true)
                    {
                        
                        byte[] buffer = new byte[_tcpСlient.Available];

                        ns.Read(buffer, 0, buffer.Length);
                        s += Encoding.Default.GetString(buffer);
                    }

                    if (s != null)
                    {
                        ShowReceiveMessage(s);
                        s = String.Empty;
                    }

                    Thread.Sleep(100);
                }
                catch
                {
                }

            }
        }
        delegate void UpdateReceiveDisplayDelegate(string message);

        void ShowReceiveMessage(string message)
        {
            if (listBox1.InvokeRequired == true)
            {
                UpdateReceiveDisplayDelegate rdd = new UpdateReceiveDisplayDelegate(ShowReceiveMessage);
                Invoke(rdd, new object[] { message });
            }
            else
            {
                listBox1.Items.Add(message);
            }
        }
        void SendMessage()
        {
            if (ns != null)
            {
                byte[] buffer = Encoding.Default.GetBytes(MyName+' '+ textBox1.Text);
                ns.Write(buffer, 0, buffer.Length);
            }
        }

        private void btnAdm_Click(object sender, EventArgs e)
        {
            AdminForm apal = new AdminForm();
            apal.ShowDialog();
        }
    }
}
