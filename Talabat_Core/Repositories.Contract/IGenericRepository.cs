﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;
using Talabat_Core.Specifications;

namespace Talabat_Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetAsync(int id);

		Task<IReadOnlyList<T>> GetAllAsync();


		Task<IReadOnlyList<T>>  GetAllWithSpecAsync(ISpecifications<T> spec);

		Task<T?> GetWithSpecAsync(ISpecifications<T>spec);

	}
}
