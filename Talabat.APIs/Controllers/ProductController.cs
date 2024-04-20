using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat_Core.Entities;
using Talabat_Core.Repositories.Contract;
using Talabat_Core.Specifications;
using Talabat_Core.Specifications.ProductSpecs;

namespace Talabat.APIs.Controllers
{

	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductCategory> _categoriesRepo;

		public ProductController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand>brandsRepo, IMapper mapper,IGenericRepository<ProductCategory> categoriesRepo)
		{
			_productRepo = productRepo;
			_brandsRepo = brandsRepo;
			_mapper = mapper;
			_categoriesRepo = categoriesRepo;
		}


		// /api/Products
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
		{

			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);

			return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
		}

		// /api/Products/1

		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{

			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await _productRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new ApiResponse(404)); // 404

			return Ok(_mapper.Map<Product, ProductToReturnDto>(product)); // 200
		}


		[HttpGet("brands")]//GET: /api/products/brands
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brandsRepo.GetAllAsync();
			return Ok(brands);

		}

		[HttpGet("categories")] //GET: /api/products/categories

		public async Task<ActionResult<IReadOnlyList<ProductCategory>>>GetCategories()
		{
			var categories = await _categoriesRepo.GetAllAsync();
			return Ok(categories);
		}

	}
}
