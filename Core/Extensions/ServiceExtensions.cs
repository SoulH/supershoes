using Core.Exceptions;
using Core.Interfaces;
using Models;
using System;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceExtensions
    {
        public static ResponseModel<T1> SecureExcecution<T1,T2>(this IService service, Func<T2> func)
        {
            ResponseModel<T1> response = null;
            try
            {
                var task = Task.Factory.StartNew(func);
                task.Wait();
                response = new ResponseModel<T1>(task.Result);
            }
            catch (AggregateException e)
            {
                e.Handle(err =>
                {
                    if (err is BadRequestException)
                        response = ResponseModel<T1>.BadRequest;
                    else if (err is NotAuthorizedException)
                        response = ResponseModel<T1>.NotAuthorized;
                    else if (err is NotFoundException)
                        response = ResponseModel<T1>.RecordNotFound;
                    else
                        response = ResponseModel<T1>.ServerError;
                    return true;
                });
            }
            return response;
        }

        public static ResponseModel<T> SecureExcecution<T>(this IService service, Action func)
        {
            ResponseModel<T> response = null;

            try
            {
                var task = Task.Factory.StartNew(func);
                task.Wait();
                response = new ResponseModel<T>();
            }
            catch (AggregateException e)
            {
                e.Handle(err =>
                {
                    if (err is BadRequestException)
                        response = ResponseModel<T>.BadRequest;
                    else if (err is NotAuthorizedException)
                        response = ResponseModel<T>.NotAuthorized;
                    else if (err is NotFoundException)
                        response = ResponseModel<T>.RecordNotFound;
                    else
                        response = ResponseModel<T>.ServerError;
                    return true;
                });
            }
            return response;
        }
    }
}
