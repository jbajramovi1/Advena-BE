using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Api.Models
{
    public class CommentModel
    {
        public CommentModel() { }

        public int Id { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public string Article { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
    }
}
