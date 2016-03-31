using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Variel.Web.Identity.Services;

namespace Variel.Web.Identity.AuthenticationProviders
{
    public class SelfAuthenticationProvider : AuthenticationProvider
    {
        public SelfAuthenticationProvider(IIdentityDatabase database) : base(database)
        {
        }

        public override AuthenticationProviderKind Kind => AuthenticationProviderKind.Self;

        public override async Task<Account> Authenticate(string identity, string password)
        {
            var login = await FindLogin(identity);

            if (login != null
                && Crypto.VerifyHashedPassword(login.Password, password))
                return login.Account;

            return null;
        }

        public override async Task<Account> CreateAccount(string identity, string password, string name, string nickname = null)
        {
            var account = new Account {Name = name, Nickname = nickname};
            var login = new Login(Kind, identity, Crypto.HashPassword(password));

            account.Logins.Add(login);
            Database.Accounts.Add(account);

            await Database.SaveChangesAsync();

            return account;
        }
    }
}
