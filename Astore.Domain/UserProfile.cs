using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astore.Domain
{
    public class UserProfile
    {
        public User User { get; set; }
        public string Address { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Article> Favorites { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
