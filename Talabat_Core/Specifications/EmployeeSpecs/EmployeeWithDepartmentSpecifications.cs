using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace Talabat_Core.Specifications.EmployeeSpecs
{
	public class EmployeeWithDepartmentSpecifications:BaseSpecificatoins<Employee>
	{


        public EmployeeWithDepartmentSpecifications()
        :base()
        {
            Includes.Add(E => E.Department);
        }


        public EmployeeWithDepartmentSpecifications(int id):base(E => E.Id==id)
        {
            Includes.Add(E => E.Department);

		}

	}
}
