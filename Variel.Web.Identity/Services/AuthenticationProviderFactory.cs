using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Variel.Web.Identity.AuthenticationProviders;

namespace Variel.Web.Identity.Services
{
    public class AuthenticationProviderFactory
    {
        private IIdentityDatabase _database;
        private static readonly Dictionary<AuthenticationProviderKind, Type> _providers;

        public static void AddProvider(AuthenticationProviderKind kind, AuthenticationProvider provider)
        {
            _providers.Add(kind, provider.GetType());
        }
        
        static AuthenticationProviderFactory()
        {
            _providers = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
                where t.IsSubclassOf(typeof (AuthenticationProvider))
                let a = Activator.CreateInstance(t, null as object) as AuthenticationProvider
                select new
                {
                    kind = a.Kind,
                    type = t
                }).ToDictionary(t => t.kind, t => t.type);

        }

        public AuthenticationProviderFactory(IIdentityDatabase database)
        {
            _database = database;
        }

        public AuthenticationProvider Create(AuthenticationProviderKind provider)
        {
            return Activator.CreateInstance(_providers[provider], _database) as AuthenticationProvider;
        }
    }
}
