﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if ( typeof(TEntity) == typeof(Product))
            {
                return trackChanges ?
              await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync() as IEnumerable <TEntity>
              :
              await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
           return trackChanges ?
                  await _context.Set<TEntity>().ToListAsync()
                  :
                  await _context.Set<TEntity>().AsNoTracking().ToListAsync();

            //    if ( trackChanges ) return await _context.Set<TEntity>().ToListAsync();
            //    return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                //return await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
                 return await _context.Products.Where(p => p.Id == id as int ?).Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
              

            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
           _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISepcifications<TEntity, TKey> sepc, bool trackChanges = false)
        {
          return await ApplySepcification(sepc).ToListAsync();
        }

        public async Task<TEntity> GetAsync(ISepcifications<TEntity, TKey> sepc)
        {
           return await ApplySepcification(sepc).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISepcifications<TEntity, TKey> spec)
        {
            return await  ApplySepcification(spec).CountAsync();
        }
        private IQueryable<TEntity> ApplySepcification(ISepcifications<TEntity, TKey> sepc)
        {
            return SepcificationEvaluator.GetQuery(_context.Set<TEntity>(), sepc);
        }

     
    }
}
