using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Database
{
    public class Basic
    {
        public Basic()
        {
            Deleted = false;
            CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
    }
}
