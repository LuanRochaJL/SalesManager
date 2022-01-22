using Microsoft.EntityFrameworkCore;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Context;

namespace SalesManager.Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(SalesManagerDbContext context) : base(context)
        {

        }

        public async Task<Product[]> ListAsync(ProductFilter model)
        {
            IQueryable<Product> qry = _context.Products;

            if(!string.IsNullOrWhiteSpace(model.Name))
            {
                qry = qry.Where(a => a.Name.StartsWith(model.Name));
            }

            if(model.OrderbyDescName)
                qry = qry.OrderByDescending(o => o.Name);
            else
                qry = qry.OrderBy(o => o.Name);

            if(!string.IsNullOrWhiteSpace(model.FromCreationDate.ToString()))
                qry = qry.Where(a => a.CreationDate >= model.FromCreationDate);
            
            if(!string.IsNullOrWhiteSpace(model.ToCreationDate.ToString()))
                qry = qry.Where(a => a.CreationDate <= model.ToCreationDate);
            
            return await qry.ToArrayAsync();
        }
    }
}