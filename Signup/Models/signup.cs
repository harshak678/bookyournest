using System.ComponentModel.DataAnnotations;


namespace Signup.Models
{
    public class signup
    {

        [Display(Name = "Student ID")]
        public int Id { get; set; }



        [Display(Name = "First name")]
        public string firstName { get; set; }


        [Display(Name = "Last name")]
        public string lastName { get; set; }



        [Display(Name = "Gender")]
        public string gender { get; set; }


        [Display(Name = "Phone number")]
        public string phoneNumber { get; set; }


        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid email address")]
        [Display(Name = "Email address")]
        public string emailAddress { get; set; }


        [Display(Name = "State")]
        public string state { get; set; }


        [Display(Name = "City")]
        public string city { get; set; }


        [Display(Name = "Username")]
        public string username { get; set; }


        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "Enter a valid password. Eg:minimum length should be 8, At least one uppercase letter, At least one lowercase letter, At least one digits, and at least one special character")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Confirm password")]
        public string confirmPassword { get; set; }

    }
}