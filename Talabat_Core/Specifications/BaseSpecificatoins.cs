using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace Talabat_Core.Specifications
{
    public class BaseSpecificatoins<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
		public int Skip {get;set;  }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; } 

		public BaseSpecificatoins()
        {
            // Criteria =null;
            //Includes = new List<Expression<Func<T, object>>>();

        }


        public BaseSpecificatoins(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            //Includes = new List<Expression<Func<T, object>>>();

        }



        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
        OrderBy=orderByExpression;
        
        }
        
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
        OrderBy=orderByDescExpression;
        
        }

        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled=true;
            Skip= skip;
            Take= take;
        }

    }
}
