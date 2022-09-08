using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astore.Domain
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public UserProfile UserProfile { get; set; }
        public Article Article { get; set; }
        public int Quantity { get; set; }
    }
}
