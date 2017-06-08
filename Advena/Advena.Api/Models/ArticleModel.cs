using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Api.Models
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            Comments = new List<CommentModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Map { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
