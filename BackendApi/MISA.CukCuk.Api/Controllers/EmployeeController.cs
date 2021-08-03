using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class EmployeeController : BaseEntityController<Employee>
    {
        #region Constructor
        IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeServices employeeServices) : base(employeeServices)
        {
            _employeeServices = employeeServices;
        }
        #endregion
    }
}
