﻿using Microsoft.EntityFrameworkCore;
using Qna.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;
        protected DbSet<T> entities;

        public Repository(AppDbContext appdbcontext)
        {
            context = appdbcontext;
            entities = context.Set<T>();

        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await entities.AddAsync(entity);
            int result = await context.SaveChangesAsync();
            return result > 0 ? entity : null;
        }
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await entities.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(params object[] id)
        {
            T result = await entities.FindAsync(id);
            return result;
        }

        public virtual async Task<bool> RemoveByIdAsync(params object[] id)
        {
            T result = await entities.FindAsync(id);
            if (result == null) return false;
            entities.Remove(result);
            int success = await context.SaveChangesAsync();
            return success > 0;
        }
    }
}
