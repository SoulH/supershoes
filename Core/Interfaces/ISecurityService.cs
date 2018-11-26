using Models;

namespace Core.Interfaces
{
    public interface ISecurityService : IService
    {
        bool Login(string username, string password);
        ResponseModel<UserModel> ListAllUsers();
    }
}
