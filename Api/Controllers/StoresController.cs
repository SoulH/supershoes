using Api.Security;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [BasicHttpAuthentication]
    public class StoresController : ApiController
    {
        private IStoreService _service;

        public StoresController(IStoreService service)
        {
            _service = service;
        }

        // GET: api/Store
        public IHttpActionResult Get([FromUri] int take = 0, [FromUri] int skip = 0, [FromUri]string name = "")
        {
            try
            {
                var res = _service.List(take, skip, name);
                return Json(new StoresResponseModel()
                {
                    Success = true,
                    Stores = res
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

        // GET: api/Store/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var res = _service.FindById(id);
                return Json(new StoreResponseModel()
                {
                    Success = true,
                    Store = res
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

        // POST: api/Store
        public IHttpActionResult Post([FromBody]StoreModel model)
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

        // PUT: api/Store/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Put(int id, [FromBody]StoreModel model)
        {
            try
            {
                if (id != model.Id) return Json(ResponseModel<Store>.BadRequest);
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

        // DELETE: api/Store/5
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
