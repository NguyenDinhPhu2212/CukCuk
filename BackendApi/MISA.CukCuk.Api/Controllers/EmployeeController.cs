using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Entity;
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
