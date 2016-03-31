using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Variel.Web.Identity.Services;

namespace Variel.Web.Identity.AuthenticationProviders
{
    public class FacebookAuthenticationProvider : AuthenticationProvider
    {
        public FacebookAuthenticationProvider(IIdentityDatabase database) : base(database)
        {
        }

        public override AuthenticationProviderKind Kind => AuthenticationProviderKind.Facebook;

        public override async Task<Account> Authenticate(string identity, string password)
        {
            var login = await FindLogin(identity);

            if (login == null)
                return null;

            var http = new HttpClient();
            var apiResult = await http.GetAsync($"https://graph.facebook.com/v2.5/me" +
                                                $"?fields=id&access_token={password}");
            var resultJson = Json.Decode(await apiResult.Content.ReadAsStringAsync());

            return identity == resultJson.id ? login.Account : null;
        }

        public override async Task<Account> CreateAccount(string identity, string password, string name, string nickname = null)
        {
            var account = new Account { Name = name, Nickname = nickname };
            var login = new Login(Kind, identity, password);

            account.Logins.Add(login);
            Database.Accounts.Add(account);

            var http = new HttpClient();
            var apiResult = await http.GetAsync($"https://graph.facebook.com/v2.5/me" +
                                                $"?fields=id,name,email,picture.width(200){{url}}&access_token={password}");
            var resultJson = Json.Decode(await apiResult.Content.ReadAsStringAsync());

            if (identity != resultJson.id)
                throw new ArgumentException("Provided identity is not identical with Graph API's identity");

            account.Picture = resultJson.picture.data.url;
            account.ContactEmail = resultJson.email;

            await Database.SaveChangesAsync();

            return account;
        }
    }
}
