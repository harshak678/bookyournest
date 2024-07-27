using Signup.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Signup.Repository
{
    public class adminroomdetails
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ToString();
            con = new SqlConnection(constr);
        }
        public List<admin> GetAllDetails()
        {
            {
                connection();
                List<admin> roomlist = new List<admin>();
                string query = "SELECT TOP 1000 [id],[room_number],[room_catagory],[price],[image],[availability] FROM tbl_room";
                SqlCommand com = new SqlCommand(query, con);
                con.Open();
                com.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)

                    roomlist.Add(
                        new admin
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            room_number = Convert.ToString(dr["room_number"]),
                            room_catagory = Convert.ToString(dr["room_catagory"]),
                            price = Convert.ToString(dr["price"]),
                            availability = Convert.ToString(dr["availability"]),
                            image = Convert.ToString(dr["image"]),

                        });

                return roomlist;
            }


        }
        public void SendConfirmationEmail(string recipientEmail, string subject, string body)
        {
            // Get SMTP settings from configuration
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            string smtpUsername = ConfigurationManager.AppSettings["SMTPUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];

            // Create SMTP client and credentials
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.EnableSsl = true; // Enable SSL if required
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            // Create the email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername); // Sender email address
            mailMessage.To.Add(new MailAddress(recipientEmail)); // Recipient email address
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true; // Set to true if using HTML in the body

            // Send the email
            smtpClient.Send(mailMessage);
        }

    }
}