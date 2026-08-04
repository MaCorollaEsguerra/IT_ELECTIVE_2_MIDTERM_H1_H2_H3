using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Process(CheckoutFormDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", dto);
            }

            if (!CartRepository.Cart.Items.Any())
            {
                TempData["Error"] = "Cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Date = DateTime.Now,
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                TotalAmount = CartRepository.Cart.GrandTotal,
                PurchasedItems = CartRepository.Cart.Items
                    .Select(i => new CartItem
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    })
                    .ToList()
            };

            foreach (var item in CartRepository.Cart.Items)
            {
                var product = ProductRepository.Products
                    .FirstOrDefault(p => p.Id == item.ProductId);

                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                }
            }

            TransactionRepository.Transactions.Add(transaction);

            CartRepository.Cart = new ShoppingCart();

            return RedirectToAction("History");
        }

        public IActionResult History()
        {
            return View(TransactionRepository.Transactions);
        }

        public IActionResult Details(Guid id)
        {
            var transaction = TransactionRepository.Transactions
                .FirstOrDefault(t => t.TransactionId == id);

            if (transaction == null)
            {
                return RedirectToAction("History");
            }

            return View(transaction);
        }
    }
}