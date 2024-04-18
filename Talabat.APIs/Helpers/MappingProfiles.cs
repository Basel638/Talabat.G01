using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat_Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d=>d.Category,o=>o.MapFrom(s=>s.Category.Name));


        }
    }
}
