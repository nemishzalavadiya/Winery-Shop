using System.ComponentModel.DataAnnotations;

namespace WineryShop.Core.Models
{
    public class ShoppingCartItem
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int Qty { get; set; }
        [Required]
        public int WineId { get; set; }
       //s [Required]
        //public int total { get; set; }
        public Wine Wine { get; set; }
        public string WineName { get; set; }
        public int price { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
