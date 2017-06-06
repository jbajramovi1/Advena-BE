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
                City = user.City
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
                Map = article.Map
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
                Article = comment.Article.ToString(),
                ArticleId=comment.Article.Id,
                User = comment.User.ToString(),
                UserId=comment.User.Id
            };
        }


       
    }
}
