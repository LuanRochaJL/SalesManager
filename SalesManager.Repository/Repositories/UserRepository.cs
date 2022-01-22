using Microsoft.EntityFrameworkCore;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Context;

namespace SalesManager.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SalesManagerDbContext context) : base(context)
        {

        }
        public async Task<User> AuthenticatorAsync(UserAuthenticator model)
        {
            return await _context.Users.Where(a => a.UserName.Equals(model.UserName) && a.Password.Equals(model.Password)).FirstOrDefaultAsync();
        }

        public async Task<User[]> ListAsync(UserFilter model)
        {
            IQueryable<User> qry = _context.Users;

            if(!string.IsNullOrWhiteSpace(model.UserName))
            {
                qry = qry.Where(a => a.UserName.Equals(model.UserName));
            }
                
            if(model.OrderbyDescUserName)
            {
                qry = qry.OrderByDescending(o => o.UserName);
            }

            if(!string.IsNullOrWhiteSpace(model.Name))
            {
                qry = qry.Where(a => a.Name.StartsWith(model.Name));
            }

            if(model.OrderbyDescName)
            {
                 qry = qry.OrderByDescending(o => o.Name);
            }
                
            if(!string.IsNullOrWhiteSpace(model.Email))
            {
                qry = qry.Where(a => a.Email.Equals(model.Email));
            }

            if(model.OrderbyDescEmail)
            {
                qry = qry.OrderByDescending(o => o.Email);
            }

            if(!string.IsNullOrWhiteSpace(model.FromCreationDate.ToString()))
            {
                qry = qry.Where(a => a.CreationDate >= model.FromCreationDate);
            }
            
            if(!string.IsNullOrWhiteSpace(model.ToCreationDate.ToString()))
            {
                qry = qry.Where(a => a.CreationDate <= model.ToCreationDate);
            }
            
            return await qry.ToArrayAsync();
        }
    }
}