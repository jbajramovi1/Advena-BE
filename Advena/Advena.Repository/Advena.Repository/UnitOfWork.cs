using Advena.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly AdvenaContext _context = new AdvenaContext();

        private UsersRepository _users;
        private ArticlesRepository _articles;
        private CommentsRepository _comments;

        public AdvenaContext Context { get { return _context; } }

        public UsersRepository Users { get { return _users ?? (_users = new UsersRepository(_context)); } }
        public ArticlesRepository Articles { get { return _articles ?? (_articles = new ArticlesRepository(_context)); } }
        public CommentsRepository Comments { get { return _comments ?? (_comments = new CommentsRepository(_context)); } }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
