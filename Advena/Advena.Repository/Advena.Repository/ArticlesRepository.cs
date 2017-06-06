using Advena.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Repository
{
    public class ArticlesRepository : AdvenaRepository<Article>
    {
        public ArticlesRepository(AdvenaContext context) : base(context) { }

        public override void Insert(Article entity)
        {
            context.Articles.Add(entity);
            context.SaveChanges();
        }

        public override void Update(Article entity, int id)
        {
            Article oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                context.SaveChanges();
            }
        }
    }
}
