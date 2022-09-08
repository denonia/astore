using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astore.Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public UserProfile Author { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public Article Article { get; set; }
    }
}
