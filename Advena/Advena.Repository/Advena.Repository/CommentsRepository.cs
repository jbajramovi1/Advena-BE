using Advena.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Repository
{
    public class CommentsRepository : AdvenaRepository<Comment>
    {
        public CommentsRepository(AdvenaContext context) : base(context) { }

        public override void Insert(Comment entity)
        {
            context.Comments.Add(entity);
            context.SaveChanges();
        }

        public override void Update(Comment entity, int id)
        {
            Comment oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Article = entity.Article;
                oldEntity.User = entity.User;
                context.SaveChanges();
            }
        }
    }
}
