using Signup.Models;
using Signup.Repository;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace Signup.Controllers
{
    public class signupController : Controller
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ToString();
            con = new SqlConnection(constr);
        }
        userRepo usrrepo = new userRepo();
        // GET: signup/Details/5
        public ActionResult getuserdetails()
        {
            userRepo usrrepo = new userRepo();
            ModelState.Clear();
            return View(usrrepo.GetAllDetails());
        }

        // GET: signup/Create
        public ActionResult adduserdetails()
        {
            return View();
        }

        // POST: signup/Create
        [HttpPost]
        public ActionResult adduserdetails(signup user)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    userRepo usrrepo = new userRepo();
                    if (usrrepo.signupuser(user))
                    {
                        TempData["bookmessage"] = "New user added successfully!.";

                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: signup/Create
        public ActionResult login(string un)
        {
            var row = usrrepo.GetAllDetails().Where(model => model.username == un).FirstOrDefault();
            return View();
        }


        // POST: signup/Create
        [HttpPost]
        public ActionResult login(signup user)
        {
            try
            {
                if (user.username == "admin123" && user.password == "@Admin123")
                {
                    return RedirectToAction("roomdetails", "admin");
                }
                var data = usrrepo.GetAllDetails().Where(model => model.username == user.username && model.password == user.password).FirstOrDefault();
                if (data != null)
                {
                    Session["uid"] = user.username;
                    return RedirectToAction("getroomdetailsforUser", "admin");
                }

                else
                {

                    TempData["loginerror"] = "Invalid login details!.";
                }
                return View();
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Editsignupdetails()
        {
            string loggedInUser = Session["uid"] as string;
            var userDetails = usrrepo.GetAllDetails().FirstOrDefault(u => u.username == loggedInUser);
            return View(userDetails);
        }


        // POST: admin/Edit/5
        [HttpPost]
        public ActionResult editsignupdetails(signup obj)
        {
            connection();
            string query = "UPDATE tbl_signup SET firstName=@firstName, lastName=@lastName, gender=@gender, phoneNumber=@phoneNumber, " +
                           "emailAddress=@emailAddress, state=@state, city=@city, username=@username, password=@password WHERE Id=@id";

            SqlCommand com = new SqlCommand(query, con);
            con.Open();

            com.Parameters.AddWithValue("@id", obj.Id);
            com.Parameters.AddWithValue("@firstName", obj.firstName);
            com.Parameters.AddWithValue("@lastName", obj.lastName);
            com.Parameters.AddWithValue("@gender", obj.gender);
            com.Parameters.AddWithValue("@phoneNumber", obj.phoneNumber);
            com.Parameters.AddWithValue("@emailAddress", obj.emailAddress);
            com.Parameters.AddWithValue("@state", obj.state);
            com.Parameters.AddWithValue("@city", obj.city);
            com.Parameters.AddWithValue("@username", obj.username);
            com.Parameters.AddWithValue("@password", obj.password);

            com.ExecuteNonQuery();
            con.Close();

            TempData["bookmessage"] = "Your details edited successfully!.";
            return RedirectToAction("editsignupdetails");

        }

        public ActionResult Deletesignupdetails()
        {
            string loggedInUser = Session["uid"] as string;
            var userDetails = usrrepo.GetAllDetails().FirstOrDefault(u => u.username == loggedInUser);
            return View(userDetails);
        }


        // POST: admin/Delete/5
        [HttpPost]
        public ActionResult deletesignupdetails(signup obj)
        {
            connection();
            string query = "delete from tbl_signup where Id=@id";
            SqlCommand com = new SqlCommand(query, con);
            con.Open();

            com.Parameters.AddWithValue("@id", obj.Id);


            com.ExecuteNonQuery();
            con.Close();
            TempData["bookmessage"] = "Your account deleted successfully!.";
            return RedirectToAction("login", "signup");

        }
    }

}

