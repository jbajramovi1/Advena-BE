using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public virtual Article Article { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
