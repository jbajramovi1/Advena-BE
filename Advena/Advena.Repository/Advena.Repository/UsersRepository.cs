using Advena.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Repository
{
    public class UsersRepository : AdvenaRepository<User>
    {
        public UsersRepository(AdvenaContext context) : base(context) { }

        public override void Insert(User entity)
        {
            context.Users.Add(entity);
            context.SaveChanges();
        }

        public override void Update(User entity, int id)
        {
            User oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                context.SaveChanges();
            }
        }
    }
}
