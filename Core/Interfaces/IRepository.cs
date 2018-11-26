using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(int id) where T : Base;
        T GetSingleBySpec<T>(ISpecification<T> spec) where T : Base;
        List<T> ListAll<T>(int take = 0, int skip = 0) where T : Base;
        List<T> List<T>(ISpecification<T> spec, int take = 0, int skip = 0) where T : Base;
        T Add<T>(T entity) where T : Base;
        void Update<T>(T entity) where T : Base;
        void Delete<T>(T entity) where T : Base;
        void SaveChages<T>() where T : Base;
        void ReloadEntity<T>(T entity) where T : Base;
        /* Async section */
        Task<T> GetByIdAsync<T>(int id) where T : Base;
        Task<T> GetSingleBySpecAsync<T>(ISpecification<T> spec) where T : Base;
        Task<List<T>> ListAllAsync<T>(int take = 0, int skip = 0) where T : Base;
        Task<List<T>> ListAsync<T>(ISpecification<T> spec, int take = 0, int skip = 0) where T : Base;
        Task<T> AddAsync<T>(T entity) where T : Base;
        Task UpdateAsync<T>(T entity) where T : Base;
        Task DeleteAsync<T>(T entity) where T : Base;
        Task SaveChagesAsync<T>() where T : Base;
        Task ReloadEntityAsync<T>(T entity) where T : Base;
    }
}
