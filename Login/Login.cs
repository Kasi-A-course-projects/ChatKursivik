using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registrarion;
using Client;
using Chat.DAL.Models;
using RecoveryPass;

namespace Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        static string connectionString = @"data source=DESKTOP-8NCHV6N;initial catalog=ChatUsers;integrated security=True;";
        string rName;
        int res;
        int role;
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registration regform = new Registration();
            regform.ShowDialog();
        }
        
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string sqlExpression = "LoginTrue";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter namePar = new SqlParameter
                {
                    ParameterName = "@n",
                    Value = tbLogin.Text
                };
                command.Parameters.Add(namePar);

                SqlParameter passPar = new SqlParameter
                {
                    ParameterName = "@p",
                    Value = tbPassword.Text
                };
                command.Parameters.Add(passPar);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    res = reader.GetInt32(0);
                    rName = reader.GetString(1);
                    role = reader.GetInt32(2);
                    if(res == 1)
                    {
                        Connect();
                        SendMessage(rName);
                        tmpCls.Nm = rName;
                        tmpCls.Rl = role;
                        FormClient cl = new FormClient();
                        this.Hide();
                        cl.ShowDialog();
                    }
                    else 
                    {
                        MessageBox.Show("Вы ввели неправильный логин/пароли или ваш аккаунт не зарегестрирован");
                    }

                }
                reader.Close();
                
            }
        }

        TcpClient _tcpСlient = new TcpClient();
        NetworkStream ns;


        

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
                }
                catch
                {
                }


            }
        }
        delegate void UpdateReceiveDisplayDelegate(string message);

        
        void SendMessage(string name)
        {
            if (ns != null)
            {
                byte[] buffer = Encoding.Default.GetBytes($"{name} вошёл в общий чат");
                ns.Write(buffer, 0, buffer.Length);
            }
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            RecoveryForm rc = new RecoveryForm();
            rc.ShowDialog();
        }
    }
}
