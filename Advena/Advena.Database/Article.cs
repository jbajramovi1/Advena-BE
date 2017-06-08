using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Database
{
    public class Article: Basic
    {
        public Article()
        {
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Map { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
