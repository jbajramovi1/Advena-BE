using Advena.Database;
using Advena.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Api.Models
{
    public class Factory
    {
        private UnitOfWork _unitOfWork;

        public Factory(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //USERS
        public UserModel Create(User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                City = user.City,
                Comments = (user.Comments.Select(x => Create(x)).ToList()) ?? user.Comments.Select(x => Create(x)).ToList()
            };
        }

        public User Create(UserModel model)
        {
            User user = new User()
            {
                Id = model.Id,
                Name = model.Name,
                Username = model.Username,
                City = model.City
            };
            
            return user;
        }

        //ARTICLES

        public ArticleModel Create(Article article)
        {
            return new ArticleModel()
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Map = article.Map,
                Comments = (article.Comments.Select(x => Create(x)).ToList()) ?? article.Comments.Select(x => Create(x)).ToList()
            };
        }

        public Article Create (ArticleModel model)
        {
            return new Article()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                Map = model.Map
            };
        }

        //COMMENTS

        public CommentModel Create(Comment comment)
        {
            return new CommentModel()
            {
                Id = comment.Id,
                Content = comment.Content,
                Article = comment.Article.Title,
                ArticleId=comment.Article.Id,
                User = comment.User.Username,
                UserId=comment.User.Id
            };
        }

        public Comment Create (CommentModel model)
        {
            return new Comment()
            {
                Id = model.Id,
                Content = model.Content,
                User = _unitOfWork.Users.Get(model.UserId),
                Article = _unitOfWork.Articles.Get(model.ArticleId)
            };
        }


       
    }
}
