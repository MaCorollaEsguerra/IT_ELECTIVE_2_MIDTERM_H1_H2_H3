using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public static class TransactionRepository
    {
        public static List<Transaction> Transactions { get; set; } = new();
    }
}