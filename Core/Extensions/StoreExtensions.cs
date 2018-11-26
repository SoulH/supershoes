using AutoMapper;
using Core.Entities;
using Models;

namespace Core.Extensions
{
    public static class StoreExtensions
    {
        public static StoreModel ToModel(this Store obj)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Store, StoreModel>();

            }).CreateMapper().Map<Store, StoreModel>(obj);
        }

        public static Store ToEntity(this StoreModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreModel, Store>();

            }).CreateMapper().Map<StoreModel, Store>(model);
        }
    }
}
