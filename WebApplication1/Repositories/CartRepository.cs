using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public static class CartRepository
    {
        public static ShoppingCart Cart { get; set; } = new();
    }
}