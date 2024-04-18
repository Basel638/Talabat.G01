using Talabat_Core.Entities;

namespace Talabat.APIs.Dtos
{
	public class ProductToReturnDto
	{
        public int Id { get; set; }
        public string Name { get; set; }

		public string Description { get; set; }

		public string PictureUrl { get; set; }

		public decimal Price { get; set; }

		public int BrandId { get; set; }  // Foreign Key Column => ProductBrand

		public string Brand { get; set; } // Navigational Property [ONE]


		public int CategoryId { get; set; }// Foreign Key Column => ProductCategory

		public string Category { get; set; } // Navigational Property [ONE]

	}
}
