using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IEFContext _context;
        public UserRepository(IEFContext context)
        {
            _context = context;
        }
        public User Add(User user)
        {
            _context.Set<User>().Add(user);
            return user;
        }

        public void Dispose()
        {
            if(this._context!=null)
                this._context.Dispose();
        }

        public IQueryable<User> GetAllUsers()
        {
            return this._context.Set<User>();
        }

        public User GetUserById(int id)
        {
            return this.GetAllUsers()
                .SingleOrDefault(c=>c.Id==id);
        }

        public User GetUserByEmail(string email)
        {
            return this.GetAllUsers()
                .SingleOrDefault(c=>c.Email==email);
        }

        public void SaveChange()
        {
            this._context.SaveChanges();
        }
    }
}
