using Core.Entities;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces;
using Core.Specifications;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class ArticleService : IArticleService
    {
        private IRepository _repository;

        public ArticleService(IRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            if (id < 1) throw new BadRequestException();
            var obj = _repository.GetById<Article>(id);
            if (obj == null) throw new NotFoundException();
            _repository.Delete(obj);
            _repository.SaveChages<Article>();
        }

        public ArticleModel FindById(int id)
        {
            if (id < 1) throw new BadRequestException();
            var obj = _repository.GetById<Article>(id);
            if (obj == null) throw new NotFoundException();
            return obj.ToModel();
        }

        public void Insert(ArticleModel model)
        {
            if (model == null) throw new BadRequestException();
            if (model.Id > 0) throw new BadRequestException();
            var obj = model.ToEntity();
            if (!obj.IsValid()) throw new BadRequestException();
            _repository.Add(obj);
            _repository.SaveChages<Article>();
        }

        public List<ArticleModel> List(int take = 0, int skip = 0, string name = "")
        {
            return _repository.List(new GenericSpec<Article>(f => f.Name.Contains(name)), take, skip)
                        .Select(f => f.ToModel()).ToList();
        }

        public List<ArticleModel> ListByStore(int storeid, int take = 0, int skip = 0, string name = "")
        {
            if (storeid < 1) throw new BadRequestException();
            var store = _repository.GetById<Store>(storeid);
            if (store == null) throw new NotFoundException();
            var list = _repository.List(new GenericSpec<Article>(f => (f.StoreId == storeid) && f.Name.Contains(name)), take, skip);
            return list.Select(f => f.ToModel()).ToList();
        }

        public void Update(ArticleModel model)
        {
            if (model == null) throw new BadRequestException();
            if (model.Id < 1) throw new BadRequestException();
            var obj = _repository.GetById<Article>(model.Id);
            if (obj == null) throw new NotFoundException();
            obj.Name = model.Name;
            obj.Description = model.Description;
            obj.Price = model.Price;
            obj.TotalInShelf = model.TotalInShelf;
            obj.TotalInVault = model.TotalInVault;
            obj.StoreId = model.StoreId;
            if (!obj.IsValid()) throw new BadRequestException();
            _repository.Update(obj);
            _repository.SaveChages<Article>();
        }
    }
}
