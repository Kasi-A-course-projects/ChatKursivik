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

namespace RecoveryPass
{
    public partial class RecoveryForm : Form
    {
        

        public RecoveryForm()
        {
            InitializeComponent();
        }
        Smtp smtp = new Smtp();
        MailMessage mail;
        SmtpClient smtpClient;
        string pas;
        static string connectionString = @"data source=DESKTOP-8NCHV6N;initial catalog=ChatUsers;integrated security=True;";

        private async void btnRec_Click(object sender, EventArgs e)
        {
            smtpClient = smtp.Start("**");
            

            string sqlExpression = "GetAllPass";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter namePar = new SqlParameter
                {
                    ParameterName = "@e",
                    Value = textBox1.Text
                };
                command.Parameters.Add(namePar);

               
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pas = reader.GetString(0);

                }
                reader.Close();

            }
            
            mail = smtp.InitMailMessage(textBox1.Text, "Ваш пароль", pas);
            try
            {
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            MessageBox.Show("Вам выслали ваш пароль на почту");
            
        }
    }
}
