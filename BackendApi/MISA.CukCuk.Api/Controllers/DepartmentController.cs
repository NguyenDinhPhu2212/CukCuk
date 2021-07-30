using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class DepartmentController : BaseEntityController<Department>
    {
        #region Constructor
        IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentServices departmentServices) : base(departmentServices)
        {
            _departmentServices = departmentServices;
        }
        #endregion
    }
}
