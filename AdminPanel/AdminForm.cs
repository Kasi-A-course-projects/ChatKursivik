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
using Chat.DAL.Models;

namespace AdminPanel
{
    public partial class AdminForm : Form
    {
        static string connectionString = @"data source=DESKTOP-8NCHV6N;initial catalog=ChatUsers;integrated security=True;";
        int rid = tmpCls.Rl;
        public AdminForm()
        {
            
            InitializeComponent();
            if(rid>1)
            {
                btnRole.Enabled = false;
            }
            string sqlExpression = "GetAllNames";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        lbUsers.Items.Add(name);
                    }
                }
                reader.Close();
            }

            string sqlExpression2 = "GetAllRoles";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression2, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        lbRoles.Items.Add(name);
                    }
                }
                reader.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlExpression = "EditUserRole";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter newRole = new SqlParameter
                {
                    ParameterName = "@nR",
                    Value= lbRoles.SelectedIndex+2
                };
                command.Parameters.Add(newRole);

                SqlParameter UserName = new SqlParameter
                {
                    ParameterName = "@n",
                    Value = lbUsers.SelectedItem
                };
                command.Parameters.Add(UserName);
                var result = command.ExecuteScalar();
            }
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            MessageBox.Show((lbRoles.SelectedIndex+1).ToString());
        }
    }
}
