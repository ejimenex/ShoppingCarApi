using ShoppingCar.Common.Filter.Base;
using ShoppingCar.Common.Pagination;
using Entities;
using Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nito.AsyncEx;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseClass,new()
    {
        protected ShoppingContext RepositoryContext { get; set; }
      
        protected readonly DbSet<T> entities;
        public RepositoryBase(ShoppingContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
            entities = RepositoryContext.Set<T>();
        }
        public virtual Guid Create(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;
            entity.Id = Guid.NewGuid();
            var result = entities.Add(entity);
            this.RepositoryContext.SaveChanges();
            return new Guid(result.Property("Id").CurrentValue.ToString());
        }
        
        public virtual void AddRange(List<T> data)

        {
           
            foreach (var item in data)
            {
                item.CreatedDate = DateTime.Now;
                item.IsActive = true;
            }
            RepositoryContext.AddRange(data);
            RepositoryContext.SaveChanges();

        }
        public virtual void Delete(Guid Id)
        {
          var entity =  this.GetOne(Id);
            entity.IsActive = false;
            this.Update(entity);
        }
       
        public virtual IQueryable<T> FindAll()
        {
           var result= this.entities.Where(c=> c.IsActive).OrderByDescending(c=> c.Id).AsNoTracking();
            return result;
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public virtual T GetOne(Guid Id)
        {
           return  this.RepositoryContext.Set<T>().AsNoTracking().FirstOrDefault(c=> c.Id==Id);
        }
        
        public virtual T Update(T entity)
        {
          this.RepositoryContext.Set<T>().Update(entity);
          this.RepositoryContext.SaveChanges();
          return entity;
        }

        public bool Exist(Expression<Func<T, bool>> expression)=>(this.RepositoryContext.Set<T>().Any(expression));

        public PagedData<T> GetPaged(BaseFilter filter,IQueryable<T> collection)
        {

            var data = PagedList <T>.Create(collection, filter.PageNumber, filter.PageSize);

            var pagination = new PagedData<T>
            {

                TotalCount = data.TotalCount,
                PageSize = data.PageSize,
                CurrentPage = data.CurrentPage,
                TotalPage = data.TotalPages,
                Data = data.OrderByDescending(c => c.Id)
            };
            return pagination;
        }
    }
}
