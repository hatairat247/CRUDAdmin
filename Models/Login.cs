using System.ComponentModel.DataAnnotations;

namespace HW3Admin.Models
{
	public class Login
	{
        [Key]
        [Required(ErrorMessage = "Enter Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string? Password { get; set; }
    }
}
