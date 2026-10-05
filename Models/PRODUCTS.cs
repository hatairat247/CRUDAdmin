using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HW3Admin.Models
{
	public class PRODUCTS
	{
		[Key]
		public int Id { get; set; }

        [Required(ErrorMessage = "Enter Product Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Enter Descrition")]
        public string? Descrition { get; set; }

        [Required(ErrorMessage = "Enter Product Price")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Upload File")]
        public string? ImagesPD { get; set; }


        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
    }
}

