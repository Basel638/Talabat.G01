﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Core.Entities;
using Talabat_Core.Repositories.Contract;

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
			var products = await _productRepo.GetAllAsync();
			 
			return Ok(products);
		}

    }
}
