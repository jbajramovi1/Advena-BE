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
    [RoutePrefix("api/articles")]
    public class ArticleController : BaseController
    {
        //GET
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Articles.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{title}")]
        public IHttpActionResult Get(string title)
        {
            return Ok(UnitOfWork.Articles.Get().Where(x => x.Title.Contains(title)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
               Article article = UnitOfWork.Articles.Get(id);
                if (article == null) return NotFound();
                return Ok(Factory.Create(article));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //POST
        [Route("")]
        public IHttpActionResult Post([FromBody]ArticleModel model)
        {
            try
            {
                Article article = Factory.Create(model);
                UnitOfWork.Articles.Insert(article);
                return Ok(Factory.Create(article));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //PUT
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]ArticleModel model)
        {
            try
            {
                Article article = Factory.Create(model);
                UnitOfWork.Articles.Update(article, id);
                return Ok(Factory.Create(article));
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
                Article article = UnitOfWork.Articles.Get(id);
                if (article == null) return NotFound();
                UnitOfWork.Articles.Delete(id);
                UnitOfWork.Articles.Commit();
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
