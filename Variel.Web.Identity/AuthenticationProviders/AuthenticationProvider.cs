using System.Threading.Tasks;
using Variel.Web.Identity.Services;

namespace Variel.Web.Identity.AuthenticationProviders
{
    public abstract class AuthenticationProvider
    {
        protected IIdentityDatabase Database { get; private set; }

        public AuthenticationProvider(IIdentityDatabase database)
        {
            Database = database;
        }

        public abstract AuthenticationProviderKind Kind { get; }

        public abstract Task<Account> Authenticate(string identity, string password);
        public abstract Task<Account> CreateAccount(string identity, string password, string name, string nickname = null);

        protected async Task<Login> FindLogin(string identity)
        {
            return await Database.Logins.FindAsync(Kind, identity);
        }
    }
}
