using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variel.Web.Identity.AuthenticationProviders;

namespace Variel.Web.Identity
{
    public class Login
    {
        [Key, Column(Order = 0)]
        public AuthenticationProviderKind Provider { get; set; }
        [Key, Column(Order = 1), MaxLength(128)]
        public string Identity { get; set; }

        public string Password { get; set; }
        
        public string AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Login() { }

        public Login(AuthenticationProviderKind provider, string identity, string password)
        {
            Provider = provider;
            Identity = identity;
            Password = password;
        }
    }
}
