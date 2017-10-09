using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EFContext : DbContext
    {
        public EFContext() : base("TourConnection")
        {
            Database.SetInitializer<EFContext>(null);
        }
        public EFContext(string connString)
            :base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }
    }
}
