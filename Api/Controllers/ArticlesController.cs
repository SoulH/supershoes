using Api.Security;
using Core.Exceptions;
using Core.Interfaces;
using Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [BasicHttpAuthentication]
    public class ArticlesController : ApiController
    {
        private IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        // GET: api/Articles
        public IHttpActionResult Get([FromUri]int take = 0, [FromUri]int skip = 0, [FromUri]string name = "")
        {
            try
            {
                var res = _service.List(take, skip, name);
                return Json(new ArticlesResponseModel()
                {
                    Success = true,
                    Articles = res
                });
            }
            catch (BadRequestException)
            {
                return Json(ErrorResponseModel.BadRequest);
            }
            catch (NotFoundException)
            {
                return Json(ErrorResponseModel.RecordNotFound);
            }
            catch (Exception)
            {
                return Json(ErrorResponseModel.ServerError);
            }
        }

        // GET: api/Articles/5
        public IHttpActionResult Get(int id)
        {
            var res = _service.FindById(id);
            try
            {
                return Json(new ArticleResponseModel()
                {
                    Success = true,
                    Article = res
                });
            }
            catch (BadRequestException)
            {
                return Json(ErrorResponseModel.BadRequest);
            }
            catch (NotFoundException)
            {
                return Json(ErrorResponseModel.RecordNotFound);
            }
            catch (Exception)
            {
                return Json(ErrorResponseModel.ServerError);
            }
        }

        // GET: api/Articles/5
        [Route("services/articles/store/{id}")]
        public IHttpActionResult GetByStore(int id, [FromUri]int take = 0, [FromUri]int skip = 0, [FromUri]string name = "")
        {
            try
            {
                var res = _service.ListByStore(id, take, skip, name);
                return Json(new ArticlesResponseModel()
                {
                    Success = true,
                    Articles = res
                });
            }
            catch (BadRequestException)
            {
                return Json(ErrorResponseModel.BadRequest);
            }
            catch (NotFoundException)
            {
                return Json(ErrorResponseModel.RecordNotFound);
            }
            catch (Exception)
            {
                return Json(ErrorResponseModel.ServerError);
            }
        }

        // POST: api/Articles
        public IHttpActionResult Post([FromBody]ArticleModel model)
        {
            try
            {
                _service.Insert(model);
                return Json(new ResponseModelBase());
            }
            catch (BadRequestException)
            {
                return Json(ErrorResponseModel.BadRequest);
            }
            catch (NotFoundException)
            {
                return Json(ErrorResponseModel.RecordNotFound);
            }
            catch (Exception)
            {
                return Json(ErrorResponseModel.ServerError);
            }
        }

        // PUT: api/Articles/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Put(int id, [FromBody]ArticleModel model)
        {
            
            try
            {
                if (id != model.Id) return Json(ResponseModel<ArticleModel>.BadRequest);
                _service.Update(model);
                return Json(new ResponseModelBase());
            }
            catch (BadRequestException)
            {
                return Json(ErrorResponseModel.BadRequest);
            }
            catch (NotFoundException)
            {
                return Json(ErrorResponseModel.RecordNotFound);
            }
            catch (Exception)
            {
                return Json(ErrorResponseModel.ServerError);
            }
        }

        // DELETE: api/Articles/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Json(new ResponseModelBase());
            }
            catch (BadRequestException)
            {
                return Json(ErrorResponseModel.BadRequest);
            }
            catch (NotFoundException)
            {
                return Json(ErrorResponseModel.RecordNotFound);
            }
            catch (Exception)
            {
                return Json(ErrorResponseModel.ServerError);
            }
        }
    }
}
