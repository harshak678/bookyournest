using System.ComponentModel.DataAnnotations;

namespace Signup.Models
{
    public class admin
    {
        [Display(Name = "ID")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Room number is required.")]
        [Display(Name = "Room number")]
        public string room_number { get; set; }


        [Required(ErrorMessage = "Room catagory is required.")]
        [Display(Name = "Room catagory")]
        public string room_catagory { get; set; }


        [Required(ErrorMessage = "price is required.")]
        [Display(Name = "Price")]
        public string price { get; set; }

        [Required(ErrorMessage = "fill the availability")]
        [Display(Name = "Availability")]
        public string availability { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [Display(Name = "Image")]
        public string image { get; set; }
    }
}