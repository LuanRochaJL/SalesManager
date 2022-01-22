using Microsoft.EntityFrameworkCore;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Context;


namespace SalesManager.Repository.Repositories
{
    public class OrderRepository  : BaseRepository<Order>
    {
        public OrderRepository(SalesManagerDbContext context) : base(context)
        {

        }

        public async Task<Order[]> ListAsync(OrderFilter model)
        {
            IQueryable<Order> qry = _context.Orders;

            if(!string.IsNullOrWhiteSpace(model.UserName))
            {
                qry = qry.Where(a => a.User.UserName.Equals(model.UserName));
            }

            if(!string.IsNullOrWhiteSpace(model.FromCreationDate.ToString()))
            {
                qry = qry.Where(a => a.CreationDate >= model.FromCreationDate);
            }
            
            if(!string.IsNullOrWhiteSpace(model.ToCreationDate.ToString()))
            {
                qry = qry.Where(a => a.CreationDate <= model.ToCreationDate);
            }

            return await qry
                .Include(a => a.User)
                .Include(a => a.OrderProducts)
                .ThenInclude(b => b.Product)
                .ToArrayAsync();
        }
    }
}