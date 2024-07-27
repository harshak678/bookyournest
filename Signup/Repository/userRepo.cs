using Signup.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Signup.Repository
{
    public class userRepo
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ToString();
            con = new SqlConnection(constr);
        }
        public bool signupuser(signup obj)
        {
            connection();
            SqlCommand com = new SqlCommand("SP_userregistrations", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@firstName", obj.firstName);
            com.Parameters.AddWithValue("@lastName", obj.lastName);
            com.Parameters.AddWithValue("@gender", obj.gender);
            com.Parameters.AddWithValue("@phoneNumber", obj.phoneNumber);
            com.Parameters.AddWithValue("@emailAddress", obj.emailAddress);
            com.Parameters.AddWithValue("@state", obj.state);
            com.Parameters.AddWithValue("@city", obj.city);
            com.Parameters.AddWithValue("@username", obj.username);
            com.Parameters.AddWithValue("@password", obj.password);
            com.Parameters.AddWithValue("@confirmPassword", obj.confirmPassword);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<signup> GetAllDetails()
        {
            connection();
            List<signup> signuplist = new List<signup>();
            SqlCommand com = new SqlCommand("SP_getuser", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            DateTime date = DateTime.Now;

            foreach (DataRow dr in dt.Rows)

                signuplist.Add(
                    new signup
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        firstName = Convert.ToString(dr["firstName"]),
                        lastName = Convert.ToString(dr["lastName"]),
                        gender = Convert.ToString(dr["gender"]),
                        phoneNumber = Convert.ToString(dr["phoneNumber"]),
                        emailAddress = Convert.ToString(dr["emailAddress"]),
                        state = Convert.ToString(dr["state"]),
                        city = Convert.ToString(dr["city"]),
                        username = Convert.ToString(dr["username"]),
                        password = Convert.ToString(dr["password"]),
                        confirmPassword = Convert.ToString(dr["confirmPassword"]),
                    });
            return signuplist;
        }



    }
}