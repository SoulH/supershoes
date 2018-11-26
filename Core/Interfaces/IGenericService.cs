using Models;

namespace Core.Interfaces
{
    public interface IGenericService<T> : IService
    {
        ResponseModel<T> FindById(int id);
        ResponseModel<T> List(int take = 0, int skip = 0, string name = "");
        ResponseModel<T> Insert(T model);
        ResponseModel<T> Update(T model);
        ResponseModel<T> Delete(int id);
    }
}
