using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Database
{
    public class Comment: Basic
    {
        public Comment() { }

        public int Id { get; set; }
        public string Content { get; set; }
        public Article Article { get; set; }
        public User User { get; set; }
    }
}
