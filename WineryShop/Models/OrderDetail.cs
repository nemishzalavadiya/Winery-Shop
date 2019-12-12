using System.ComponentModel.DataAnnotations;

namespace WineryShop.Core.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string WineName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }

        public string UserId { get; set; }

        [Required]
        public System.DateTime OrderPlacedTime { get; set; }
    }
}
