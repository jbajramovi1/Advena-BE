using Advena.Api.Models;
using Advena.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Advena.Api.Controllers
{
    public class BaseController: ApiController
    {
        private UnitOfWork _unitOfWork;
        private Factory _factory;

        protected UnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork ?? (_unitOfWork = new UnitOfWork());
            }
        }

        protected Factory Factory
        {
            get
            {
                return _factory ?? (_factory = new Factory(UnitOfWork));
            }
        }



    
}
}
