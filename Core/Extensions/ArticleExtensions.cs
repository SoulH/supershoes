using AutoMapper;
using Core.Entities;
using Models;

namespace Core.Extensions
{
    public static class ArticleExtensions
    {
        public static ArticleModel ToModel(this Article obj)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Article, ArticleModel>()
                    .ForMember(m => m.StoreName, o => o.MapFrom(s => s.Store.Name));

            }).CreateMapper().Map<Article, ArticleModel>(obj);
        }

        public static Article ToEntity(this ArticleModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleModel, Article>();

            }).CreateMapper().Map<ArticleModel, Article>(model);
        }
    }
}
