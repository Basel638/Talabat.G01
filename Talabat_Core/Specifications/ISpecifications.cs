using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace Talabat_Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T,bool>>? Criteria { get; set; }  // P => P.Id == 1


        public List<Expression<Func<T,object>>> Includes { get; set; }




    }
}
