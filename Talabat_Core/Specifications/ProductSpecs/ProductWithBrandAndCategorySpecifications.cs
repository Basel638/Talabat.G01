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

		public ProductWithBrandAndCategorySpecifications(string sort,int? brandId, int? categoryId)
		   : base(P =>
		   
					(!brandId.HasValue|| P.BrandId==brandId.Value)&& 
					(!categoryId.HasValue || P.CategoryId==categoryId.Value)  
		   
		   
		   )
		{
			AddIncludes();

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "priceAsc":
						//OrderBy = P => P.Price;
						AddOrderBy(P => P.Price);
						break;

					case "priceDesc":
						//OrderByDesc = P => P.Price;
						AddOrderByDesc(P => P.Price);
						break;

					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}

			else
				AddOrderBy(P => P.Name);

		}




		// This Constructor will be Used for Creating an Object, That Will Be Used To Get A Specific ProductId
		public ProductWithBrandAndCategorySpecifications(int id)
		: base(P => P.Id == id)
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
