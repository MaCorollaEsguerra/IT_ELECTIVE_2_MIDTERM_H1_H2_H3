using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View(CartRepository.Cart);
        }

        [HttpPost]
        public IActionResult Add(AddToCartDTO dto)
        {
            var product = ProductRepository.Products
                .FirstOrDefault(p => p.Id == dto.ProductId);

            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            if (dto.Quantity > product.StockQuantity)
            {
                TempData["Error"] =
                    "Requested quantity exceeds available stock.";

                return RedirectToAction("Index", "Products");
            }

            var existingItem = CartRepository.Cart.Items
                .FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                if (existingItem.Quantity + dto.Quantity >
                    product.StockQuantity)
                {
                    TempData["Error"] =
                        "Requested quantity exceeds available stock.";

                    return RedirectToAction("Index", "Products");
                }

                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                CartRepository.Cart.Items.Add(
                    new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = dto.Quantity,
                        UnitPrice = product.Price
                    });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(UpdateCartDTO dto)
        {
            var cartItem = CartRepository.Cart.Items
                .FirstOrDefault(i => i.ProductId == dto.ProductId);

            var product = ProductRepository.Products
                .FirstOrDefault(p => p.Id == dto.ProductId);

            if (cartItem == null || product == null)
            {
                return RedirectToAction("Index");
            }

            if (dto.Quantity <= 0)
            {
                TempData["Error"] = "Quantity must be at least 1.";
                return RedirectToAction("Index");
            }

            if (dto.Quantity > product.StockQuantity)
            {
                TempData["Error"] = "Quantity exceeds available stock.";
                return RedirectToAction("Index");
            }

            cartItem.Quantity = dto.Quantity;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var item = CartRepository.Cart.Items
                .FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                CartRepository.Cart.Items.Remove(item);
            }

            return RedirectToAction("Index");
        }
    }
}