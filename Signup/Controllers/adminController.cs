using Signup.Models;
using Signup.Repository;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Signup.Controllers
{
    public class adminController : Controller
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ToString();
            con = new SqlConnection(constr);
        }

        public ActionResult getroomdetails()
        {
            adminroomdetails ad = new adminroomdetails();
            ModelState.Clear();
            return View(ad.GetAllDetails());
        }
        public ActionResult getroomdetailsforUser()
        {
            adminroomdetails ad = new adminroomdetails();
            ModelState.Clear();
            return View(ad.GetAllDetails());
        }
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/Create
        public ActionResult roomdetails()
        {
            return View();
        }

        // POST: admin/Create
        [HttpPost]

        public ActionResult roomdetails(admin ad, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                connection();
                string query = "insert into tbl_room(room_number, room_catagory, price, image, availability) values (@room_number, @room_catagory, @price, @image, @availability)";
                SqlCommand com = new SqlCommand(query, con);
                con.Open();
                com.Parameters.AddWithValue("@room_number", ad.room_number);
                com.Parameters.AddWithValue("@room_catagory", ad.room_catagory);
                com.Parameters.AddWithValue("@price", ad.price);
                com.Parameters.AddWithValue("@availability", ad.availability);

                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string imgpath = Path.Combine(Server.MapPath("/roomimages/"), filename);
                    file.SaveAs(imgpath);
                    com.Parameters.AddWithValue("@image", "/roomimages/" + filename);
                }

                com.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("getroomdetails");
            }

            // If ModelState is not valid, return the form with errors
            return View(ad);
        }
        // GET: admi/Edit/5
        public ActionResult Editdetails(int? id)
        {
            adminroomdetails ad = new adminroomdetails();
            return View(ad.GetAllDetails().Find(admin => admin.Id == id));
        }


        // POST: admin/Edit/5
        [HttpPost]
        public ActionResult Editdetails(int id, admin ad, HttpPostedFileBase file)
        {
            connection();
            string query = "update tbl_room set room_number=@room_number, room_catagory=@room_catagory, price=@price, availability=@availability";


            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/roomimages/"), filename);
                file.SaveAs(imgpath);
                query += ", image=@image";
            }

            query += " where Id=@id";

            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            com.Parameters.AddWithValue("@id", id);
            com.Parameters.AddWithValue("@room_number", ad.room_number);
            com.Parameters.AddWithValue("@room_catagory", ad.room_catagory);
            com.Parameters.AddWithValue("@price", ad.price);
            com.Parameters.AddWithValue("@availability", ad.availability);


            if (file != null && file.ContentLength > 0)
            {
                com.Parameters.AddWithValue("@image", "/roomimages/" + Path.GetFileName(file.FileName));
            }

            com.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("getroomdetails");
        }
        public ActionResult deletedetails(int? id)
        {
            adminroomdetails ad = new adminroomdetails();
            return View(ad.GetAllDetails().Find(admin => admin.Id == id));
        }

        // POST: admin/Edit/5
        [HttpPost]
        public ActionResult deletedetails(int id)
        {
            connection();
            string query = "delete from tbl_room where Id=@id";
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            com.Parameters.AddWithValue("@id", id);

            com.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("getroomdetails");

        }


        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("login", "signup");
        }

        public ActionResult SendConfirmationEmail(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Open the connection

                    // Retrieve booking details
                    string selectQuery = "SELECT name,emailid FROM tbl_booking WHERE Id = @id";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", id);

                    // Execute the query and retrieve the result
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Extract values directly from the reader
                        string userEmail = reader["emailid"].ToString(); // Adjust field name based on your schema
                        string userName = reader["name"].ToString(); // Adjust field name based on your schema

                        // Send confirmation email to user
                        SendConfirmationEmailToUser(userEmail, userName, id);

                        ViewBag.SuccessMessage = $"Confirmation email sent to {userEmail}.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Booking not found.";
                    }

                    reader.Close(); // Close the reader
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error sending confirmation email: " + ex.Message;
                }
            } // Connection will be automatically closed here due to using statement

            return RedirectToAction("BookingDetails");
        }

        private void SendConfirmationEmailToUser(string recipientEmail, string recipientName, int id)
        {
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                string smtpUsername = ConfigurationManager.AppSettings["SMTPUsername"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpUsername);
                mailMessage.To.Add(new MailAddress(recipientEmail));
                mailMessage.Subject = "Booking Confirmation";
                mailMessage.Body = $"Dear {recipientName},\n\nYour booking with ID {id} has been confirmed.\n\nThank you.";
                mailMessage.IsBodyHtml = false;

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending email: " + ex.Message);
            }
        }


    }

}