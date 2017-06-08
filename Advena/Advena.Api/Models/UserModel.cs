using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Api.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Comments = new List<CommentModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
