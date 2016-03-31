using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Variel.Web.Identity
{
    public class Account
    {
        private static readonly char[] Charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_".ToCharArray();
        private const int KeyLength = 15;

        [Key, MaxLength(KeyLength)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Nickname { get; set; }
        public string ContactEmail { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<Login> Logins { get; set; } = new HashSet<Login>();

        public DateTime JoinedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogonAtUtc { get; set; }

        public Account()
        {
            var builder = new StringBuilder(KeyLength);
            var random = new Random((int)DateTime.Now.Ticks);

            for(int i = 0; i < KeyLength; i++)
            {
                builder.Append(Charset[random.Next(0, Charset.Length)]);
            }

            this.Id = builder.ToString();
        }
    }
}
