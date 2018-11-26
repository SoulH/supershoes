using Core.Entities;
using Core.Interfaces;
using Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [Authorize]
    public class StoresController : ApiController
    {
        private IStoreService _service;

        public StoresController(IStoreService service)
        {
            _service = service;
        }

        // GET: api/Store
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri] int take = 0, [FromUri] int skip = 0, [FromUri]string name = "")
        {
            return Json(_service.List(take, skip, name));
        }

        // GET: api/Store/5
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            return Json(_service.FindById(id));
        }

        // POST: api/Store
        public IHttpActionResult Post([FromBody]StoreModel model)
        {
            return Json(_service.Insert(model));
        }

        // PUT: api/Store/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Put(int id, [FromBody]StoreModel model)
        {
            if (id != model.Id) return Json(ResponseModel<Store>.BadRequest);
            return Json(_service.Update(model));
        }

        // DELETE: api/Store/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
