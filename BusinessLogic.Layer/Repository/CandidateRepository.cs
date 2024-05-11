using BusinessLogic.Layer.Interfaces;
using DataAccess.Layer.Context;
using DataAccess.Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Layer.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly SigmaDbContext _dbContext;
        public CandidateRepository(SigmaDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<Candidate> AddAsync(Candidate entity)
        {
            await _dbContext.Set<Candidate>().AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        //public async Task<int> ExecuteUpdateAsync(Expression<Func<Candidate, bool>> predicate, Expression<Func<SetPropertyCalls<Candidate>, SetPropertyCalls<Candidate>>> expression)
        //{
        //    var entity = await _dbContext.Set<Candidate>().Where(predicate).ExecuteUpdateAsync<Candidate>(expression); ;
        //    return entity;
        //}

        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            Candidate? record = await _dbContext.Set<Candidate>().FirstOrDefaultAsync(c => c.Email == email); ;
            return record;
        }

        public async Task UpdateAsync(Candidate entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
