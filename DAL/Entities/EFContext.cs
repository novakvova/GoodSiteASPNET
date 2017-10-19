using DAL.Abstract;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EFContext : IdentityDbContext<AppUser>, IEFContext
    {
        public EFContext() : base("TourConnection")
        {
            Database.SetInitializer<EFContext>(null);
        }
        public EFContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        #region User Security
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        #endregion

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
