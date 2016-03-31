using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variel.Web.Identity.Services
{
    public abstract class IdentityDatabase : DbContext, IIdentityDatabase
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Logins)
                .WithRequired(l => l.Account)
                .HasForeignKey(l => l.AccountId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
