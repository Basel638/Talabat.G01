﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Core.Entities;
using Talabat_Core.Repositories.Contract;
using Talabat_Core.Specifications;
using Talabat_Core.Specifications.ProductSpecs;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;

		public ProductController(IGenericRepository<Product> productRepo)
		{
			_productRepo = productRepo;
		}


		// /api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{

			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);

			return Ok(products);
		}

		// /api/Products/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{

			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await _productRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new { Message = "Not Found", StatusCode = 404 }); // 404

			return Ok(product); // 200
		}

	}
}
