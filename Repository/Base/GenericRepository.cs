using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        protected readonly DbSet<T> entity;
        protected readonly UniversityMapContext context;
        public GenericRepository(UniversityMapContext context)
        {
            entity = context.Set<T>();
            this.context = context;
        }

        public virtual void Add(T obj)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Rooms ON");

                entity.Add(obj);
                context.SaveChanges();

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Rooms OFF");
                transaction.Commit();
            }           
        }

        public void AddRange(List<T> objs)
        {
            entity.AddRange(objs);
            context.SaveChanges();
        }

        public T Get(int id)
        {
            return entity.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T obj)
        {
            entity.Update(obj);
            context.SaveChanges();
            return obj;
        }
        public bool Delete(int id)
        {
            var obj = entity.Single(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }

            entity.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return entity.AsEnumerable();
        }
    }
}
