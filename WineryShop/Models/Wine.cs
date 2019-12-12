using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineryShop.Core.Models
{
    public class Wine
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(255)]
        public string LongDescription { get; set; }

        [Required]
        public int Price { get; set; }

        
        [StringLength(255)]
        [System.ComponentModel.DisplayName("Upload a file")]
        public string ImageUrl { get; set; }



        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
       public HttpPostedFileBase ImageFile { get; set; }
    }
}
