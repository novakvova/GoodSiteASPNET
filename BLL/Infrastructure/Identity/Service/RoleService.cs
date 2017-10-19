using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Identity.Service
{
    public class RoleService : RoleManager<AppRole>
    {
        public RoleService(RoleStore<AppRole> store) : base(store) { }

        public static RoleService Create(IdentityFactoryOptions<RoleService> options, IOwinContext context)
        {
            return new RoleService(new RoleStore<AppRole>(context.Get<EFContext>()));
        }
    }
}
