using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Signup.Models
{
    public class userbooking
    {
        [Display(Name = "Student ID")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string name { get; set; }


        [Required(ErrorMessage = "Email id is required.")]
        [Display(Name = "Emailid")]
        public string emailid { get; set; }

        [Required(ErrorMessage = "Check in time is required.")]
        [Display(Name = "Check-in")]
        public DateTime? checkin { get; set; }

        [Required(ErrorMessage = "Check out time is required.")]
        [Display(Name = "Check-out")]
        public DateTime? checkout { get; set; }

        [Required(ErrorMessage = "Number of guests is required.")]
        [Display(Name = "Number of guests")]
        public string guestsnumber { get; set; }

        public List<signup> signups { get; set; }

    }
}