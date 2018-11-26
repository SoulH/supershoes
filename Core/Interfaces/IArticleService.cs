using Models;

namespace Core.Interfaces
{
    public interface IArticleService : IGenericService<ArticleModel>
    {
        ResponseModel<ArticleModel> ListByStore(int storeid, int take = 0, int skip = 0, string name = "");
    }
}
