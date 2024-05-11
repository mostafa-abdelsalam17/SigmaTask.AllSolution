using DataAccess.Layer.Context;
using DataAccess.Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Layer.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByEmailAsync(string email);
        Task<Candidate> AddAsync(Candidate entity);
        Task UpdateAsync(Candidate entity);
        //Task<int> ExecuteUpdateAsync(Expression<Func<Candidate, bool>> predicate, Expression<Func<SetPropertyCalls<Candidate>, SetPropertyCalls<Candidate>>> expression);
    }
}
