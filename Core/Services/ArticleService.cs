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

        public ResponseModel<ArticleModel> Delete(int id)
        {
            return this.SecureExcecution<ArticleModel>(() =>
            {
                if (id < 1) throw new BadRequestException();
                var obj = _repository.GetById<Article>(id);
                if (obj == null) throw new NotFoundException();
                _repository.Delete(obj);
                _repository.SaveChages<Article>();
            });
        }

        public ResponseModel<ArticleModel> FindById(int id)
        {
            return this.SecureExcecution<ArticleModel,ArticleModel>(() =>
            {
                if (id < 1) throw new BadRequestException();
                var obj = _repository.GetById<Article>(id);
                if (obj == null) throw new NotFoundException();
                return obj.ToModel();
            });
        }

        public ResponseModel<ArticleModel> Insert(ArticleModel model)
        {
            return this.SecureExcecution<ArticleModel>(() => 
            {
                if (model == null) throw new BadRequestException();
                if (model.Id > 0) throw new BadRequestException();
                var obj = model.ToEntity();
                if (!obj.IsValid()) throw new BadRequestException();
                _repository.Add(obj);
                _repository.SaveChages<Article>();
            });
        }

        public ResponseModel<ArticleModel> List(int take = 0, int skip = 0, string name = "")
        {
            return this.SecureExcecution<ArticleModel,List<ArticleModel>>(() =>
            {
                return _repository.List(new GenericSpec<Article>(f => f.Name.Contains(name)), take, skip)
                        .Select(f => f.ToModel()).ToList();
            });
        }

        public ResponseModel<ArticleModel> ListByStore(int storeid, int take = 0, int skip = 0, string name = "")
        {
            return this.SecureExcecution<ArticleModel,List<ArticleModel>>(() =>
            {
                if (storeid < 1) throw new BadRequestException();
                var store = _repository.GetById<Store>(storeid);
                if (store == null) throw new NotFoundException();
                var list = _repository.List(new GenericSpec<Article>(f => (f.StoreId == storeid) && f.Name.Contains(name)), take, skip);
                return list.Select(f => f.ToModel()).ToList();
            });
        }

        public ResponseModel<ArticleModel> Update(ArticleModel model)
        {
            return this.SecureExcecution<ArticleModel>(() =>
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
            });
        }
    }
}
