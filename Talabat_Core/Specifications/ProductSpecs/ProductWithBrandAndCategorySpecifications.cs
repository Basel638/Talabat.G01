using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace Talabat_Core.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecificatoins<Product>
	{
		// This Constructor will be Used for Creating an Object, That will be Used to Get all Products

		public ProductWithBrandAndCategorySpecifications()
		   : base()
		{
			AddIncludes();

		}

		


		// This Constructor will be Used for Creating an Object, That Will Be Used To Get A Specific ProductId
		public ProductWithBrandAndCategorySpecifications(int id)
        :base(P => P.Id == id)
		{
			AddIncludes();
		}


		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}
	}
}
