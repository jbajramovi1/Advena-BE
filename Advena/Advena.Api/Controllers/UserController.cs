using Advena.Api.Models;
using Advena.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Advena.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController: BaseController
    {
        //GET
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Users.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Users.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                User user = UnitOfWork.Users.Get(id);
                if (user == null) return NotFound();
                return Ok(Factory.Create(user));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //POST
        [Route("")]
        public IHttpActionResult Post([FromBody]UserModel model)
        {
            try
            {
                User user = Factory.Create(model);
                UnitOfWork.Users.Insert(user);
                return Ok(Factory.Create(user));
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //PUT
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]UserModel model)
        {
            try
            {
                User user = Factory.Create(model);
                UnitOfWork.Users.Update(user, id);
                return Ok(Factory.Create(user));
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
                User user = UnitOfWork.Users.Get(id);
                if (user == null) return NotFound();
                UnitOfWork.Users.Delete(id);
                UnitOfWork.Users.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                //Helpers.Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("profiles")]
        [HttpGet]
        public IHttpActionResult CreateProfiles()
        {
            string[] users = new string[1] { "user" };
            string[] admins = new string[2] { "admin", "user" };
            //if(!WebSecurity.Initialized)
            WebSecurity.InitializeDatabaseConnection("Advena", "Users", "Id", "Username", autoCreateTables: true);
            Roles.CreateRole("admin");
            Roles.CreateRole("user");
            foreach (var user in UnitOfWork.Users.Get())
            {
                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    string[] names = user.Name.Split(' ');
                    string username = names[0].ToLower();
                    user.Username = username;
                    UnitOfWork.Users.Update(user, user.Id);
                }
                WebSecurity.CreateAccount(user.Username, "advena", false);
                if (user.Username == "jasmina") Roles.AddUserToRole(user.Username, "admin"); else; Roles.AddUserToRole(user.Username, "user");
            }
            if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("Billing", "Agents", "Id", "Username", autoCreateTables: true);
            return Ok("user profiles created");
        }
    }
}
