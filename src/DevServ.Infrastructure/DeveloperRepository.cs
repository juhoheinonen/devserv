﻿using DevServ.Core.Entities;
using DevServ.SharedKernel;
using DevServ.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.Infrastructure
{
    public class DeveloperRepository: IRepository<Developer>
    {
        private readonly DevServDbContext _dbContext;

        public DeveloperRepository(DevServDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Developer> GetByIdAsync(int id)
        {
            return await _dbContext.Developers.Include(d => d.Skills).SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<Developer>> ListAsync()
        {
            return _dbContext.Developers.Include(d => d.Skills).ToListAsync();
        }


        //public T GetById<T>(int id) where T : BaseEntity, IAggregateRoot
        //{
        //    return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        //}

        //public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, IAggregateRoot
        //{
        //    return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        //}

        //public Task<List<T>> ListAsync<T>() where T : BaseEntity, IAggregateRoot
        //{
        //    return _dbContext.Set<T>().ToListAsync();
        //}

        //public Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot
        //{
        //    var specificationResult = ApplySpecification(spec);
        //    return specificationResult.ToListAsync();
        //}

        //public async Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        //{
        //    await _dbContext.Set<T>().AddAsync(entity);
        //    await _dbContext.SaveChangesAsync();

        //    return entity;
        //}

        //public Task UpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        //{
        //    _dbContext.Entry(entity).State = EntityState.Modified;
        //    return _dbContext.SaveChangesAsync();
        //}

        //public Task DeleteAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        //{
        //    _dbContext.Set<T>().Remove(entity);
        //    return _dbContext.SaveChangesAsync();
        //}

        //private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : BaseEntity
        //{
        //    var evaluator = new SpecificationEvaluator<T>();
        //    return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        //}
    }
}
