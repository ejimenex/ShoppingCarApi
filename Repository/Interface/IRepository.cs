using ShoppingCar.Common.Filter.Base;
using ShoppingCar.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interface
{
   public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        bool Exist(Expression<Func<T, bool>> expression);
        void AddRange(List<T> data);
        T GetOne(Guid Id);
        Guid Create(T entity);
        T Update(T entity);
        void Delete(Guid Id);
        PagedData<T> GetPaged(BaseFilter filter, IQueryable<T> List);
    }
}
