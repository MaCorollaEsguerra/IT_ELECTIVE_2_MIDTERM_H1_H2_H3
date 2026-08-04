using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class CheckoutFormDTO
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [EmailAddress]
        public string? CustomerEmail { get; set; }
    }
}