using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;
using Talabat_Core.Specifications;

namespace Talabat_Infrastructure
{
	internal static class SpecifcationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
		{
			var query = inputQuery; //_dbcontext.set<TEntity>()

			if (spec.Criteria is not null) //E => E.Id == 1
				query = query.Where(spec.Criteria);


			if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);

			else if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);

			// query = _dbcontext.set<TEntity>().where(E => E.Id == 1)
			// include expressions
			// 1. P => P.Brand
			// 2. P => P.Category



			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


			// query = _dbcontext.set<TEntity>().where(E => E.Id == 1).Include(P => P.Brand)
			// query = _dbcontext.set<TEntity>().where(E => E.Id == 1).Include(P => P.Brand).Include(P => P.Category)


			return query;
		}


	}

}
