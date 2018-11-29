using Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IArticleService : IGenericService<ArticleModel>
    {
        List<ArticleModel> ListByStore(int storeid, int take = 0, int skip = 0, string name = "");
    }
}
