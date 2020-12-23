using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.DAL.Models;

namespace Registrarion
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        static string connectionString = @"data source=DESKTOP-8NCHV6N;initial catalog=ChatUsers;integrated security=True;";

        private static void AddUser()
        {

        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string sqlExpression = "AddUser";

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

                SqlParameter mailIdPar = new SqlParameter
                {
                    ParameterName = "@e",
                    Value = tbEmail.Text
                };
                command.Parameters.Add(mailIdPar);

                SqlParameter roleIdPar = new SqlParameter
                {
                    ParameterName = "@rId",
                    Value = 3
                };
                command.Parameters.Add(roleIdPar);
                var result = command.ExecuteScalar();
                this.DialogResult = DialogResult.OK;



            }
        }

      
    }
}
