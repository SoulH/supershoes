﻿using Core.Entities;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces;
using Core.Specifications;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class StoreService : IStoreService
    {
        private IRepository _repository;

        public StoreService(IRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            if (id < 1) throw new BadRequestException();
            var obj = _repository.GetById<Store>(id);
            if (obj == null) throw new NotFoundException();
            _repository.Delete(obj);
            _repository.SaveChages<Store>();
        }

        public StoreModel FindById(int id)
        {
            if (id < 1) throw new BadRequestException();
            var obj = _repository.GetById<Store>(id);
            if (obj == null) throw new NotFoundException();
            return obj.ToModel();
        }

        public void Insert(StoreModel model)
        {
            if (model == null) throw new BadRequestException();
            if (model.Id > 0) throw new BadRequestException();
            var obj = model.ToEntity();
            if (!obj.IsValid()) throw new BadRequestException();
            _repository.Add(obj);
            _repository.SaveChages<Store>();
        }

        public List<StoreModel> List(int take = 0, int skip = 0, string name = "")
        {
            return _repository.List(new GenericSpec<Store>(f => f.Name.Contains(name)), take, skip)
                        .Select(f => f.ToModel()).ToList();
        }

        public void Update(StoreModel model)
        {
            if (model == null) throw new BadRequestException();
            if (model.Id < 1) throw new BadRequestException();
            var obj = _repository.GetById<Store>(model.Id);
            if (obj == null) throw new NotFoundException();
            obj.Address = model.Address;
            obj.Name = model.Name;
            if (!obj.IsValid()) throw new BadRequestException();
            _repository.Update(obj);
            _repository.SaveChages<Store>();
         }
    }
}
