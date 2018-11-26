using Core.Entities;
using Core.Interfaces;
using Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [Authorize]
    public class ArticlesController : ApiController
    {
        private IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        // GET: api/Articles
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri]int take = 0, [FromUri]int skip = 0, [FromUri]string name = "")
        {
            return Json(_service.List(take, skip, name));
        }

        // GET: api/Articles/5
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            return Json(_service.FindById(id));
        }

        // GET: api/Articles/5
        [AllowAnonymous]
        [Route("services/articles/store/{id}")]
        public IHttpActionResult GetByStore(int id, [FromUri]int take = 0, [FromUri]int skip = 0, [FromUri]string name = "")
        {
            return Json(_service.ListByStore(id, take, skip, name));
        }

        // POST: api/Articles
        public IHttpActionResult Post([FromBody]ArticleModel model)
        {
            return Json(_service.Insert(model));
        }

        // PUT: api/Articles/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Put(int id, [FromBody]ArticleModel model)
        {
            if (id != model.Id) return Json(ResponseModel<ArticleModel>.BadRequest);
            return Json(_service.Update(model));
        }

        // DELETE: api/Articles/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
