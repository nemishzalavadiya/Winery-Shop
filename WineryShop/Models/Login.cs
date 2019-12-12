using System.ComponentModel.DataAnnotations;

namespace WineryShop.Core.Models
{
    public class Login
    {
        [Key]
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Designation { get; set; }

        public string Email { get; set; }

    }
}
