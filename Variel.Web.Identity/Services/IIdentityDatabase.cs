using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variel.Web.Identity.Services
{
    public interface IIdentityDatabase
    {
        DbSet<Account> Accounts { get; }
        DbSet<Login> Logins { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
