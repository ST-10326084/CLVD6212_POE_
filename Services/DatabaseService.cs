using System;
using testCLVD.DB_Models;
using testCLVD.DBContext;

namespace testCLVD.Services
{
    public class DatabaseService
    {
        private readonly MyContext _context;

        public DatabaseService(MyContext context)
        {
            _context = context;
        }

        public async Task RegCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
    }
}
