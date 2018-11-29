using Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IGenericService<T> : IService
    {
        T FindById(int id);
        List<T> List(int take = 0, int skip = 0, string name = "");
        void Insert(T model);
        void Update(T model);
        void Delete(int id);
    }
}
