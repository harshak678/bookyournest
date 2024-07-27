using Signup.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Signup.Repository
{
    public class bookingrepo
    {
        private string connectionString;

        public bookingrepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ConnectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public List<userbooking> GetbookingDetails()
        {
            List<userbooking> bookingdetails = new List<userbooking>();

            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT [id], [name], [emailid], [checkin], [checkout], [guestsnumber] FROM tbl_booking";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        bookingdetails.Add(new userbooking
                        {
                            Id = Convert.ToInt32(rdr["id"]),
                            name = rdr["name"].ToString(),
                            emailid = rdr["emailid"].ToString(),
                            checkin = Convert.ToDateTime(rdr["checkin"]),
                            checkout = Convert.ToDateTime(rdr["checkout"]),
                            guestsnumber = rdr["guestsnumber"].ToString()
                        });
                    }
                    con.Close();
                }
            }

            return bookingdetails;
        }

        public List<userbooking> GetBookingDetailsForUser(string emailAddress)
        {
            List<userbooking> bookings = new List<userbooking>();

            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT [id], [name], [emailid], [checkin], [checkout], [guestsnumber] " +
                               "FROM tbl_booking " +
                               "WHERE emailid = @email";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@email", emailAddress);
                    con.Open();
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        bookings.Add(new userbooking
                        {
                            Id = Convert.ToInt32(rdr["id"]),
                            name = rdr["name"].ToString(),
                            emailid = rdr["emailid"].ToString(),
                            checkin = Convert.ToDateTime(rdr["checkin"]),
                            checkout = Convert.ToDateTime(rdr["checkout"]),
                            guestsnumber = rdr["guestsnumber"].ToString()
                        });
                    }
                    con.Close();
                }
            }

            return bookings;
        }

        public void InsertBooking(userbooking ub)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "INSERT INTO tbl_booking (name, emailid, checkin, checkout, guestsnumber) " +
                               "VALUES (@name, @email, @checkin, @checkout, @guests)";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@name", ub.name);
                    com.Parameters.AddWithValue("@email", ub.emailid);
                    com.Parameters.AddWithValue("@checkin", ub.checkin);
                    com.Parameters.AddWithValue("@checkout", ub.checkout);
                    com.Parameters.AddWithValue("@guests", ub.guestsnumber);

                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
