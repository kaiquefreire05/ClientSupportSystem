﻿using CustomerSupportSystem.Database;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportSystem.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        // Dependence Injection

        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new Exception("Error deleting.");
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }
    }
}