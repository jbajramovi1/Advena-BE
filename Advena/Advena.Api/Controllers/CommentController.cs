using Advena.Api.Models;
using Advena.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Advena.Api.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentController : BaseController
    {
        //GET
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Comments.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }


        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Comment comment = UnitOfWork.Comments.Get(id);
                if (comment == null) return NotFound();
                return Ok(Factory.Create(comment));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("article/{article}")]
        public IHttpActionResult GetArticle(int article)
        {
            var comments = UnitOfWork.Comments.Get().Where(x => x.Article.Id == article).ToList().Select(x => Factory.Create(x)).ToList();
            if (comments.Count() != 0) return Ok(comments);
            return NotFound();
        }

        [Route("user/{user}")]
        public IHttpActionResult GetUser(int user)
        {
            var comments = UnitOfWork.Comments.Get().Where(x => x.User.Id == user).ToList().Select(x => Factory.Create(x)).ToList();
            if (comments.Count() != 0) return Ok(comments);
            return NotFound();
        }

        //POST
        [Route("")]
        public IHttpActionResult Post([FromBody]CommentModel model)
        {
            try
            {
                Comment comment = Factory.Create(model);
                UnitOfWork.Comments.Insert(comment);
                return Ok(Factory.Create(comment));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //PUT
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]CommentModel model)
        {
            try
            {
                Comment comment = Factory.Create(model);
                UnitOfWork.Comments.Update(comment, id);
                return Ok(Factory.Create(comment));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //DELETE
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Comment comment = UnitOfWork.Comments.Get(id);
                if (comment == null) return NotFound();
                UnitOfWork.Comments.Delete(id);
                UnitOfWork.Comments.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }


    }
}
