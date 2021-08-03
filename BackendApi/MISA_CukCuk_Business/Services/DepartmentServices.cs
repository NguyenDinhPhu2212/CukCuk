using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.CukCuk.Core.Properties;
using MISA.CukCuk.Core.Const;

namespace MISA.CukCuk.Core.Services
{
    public class DepartmentServices : BaseServices<Department>, IDepartmentServices
    {
        #region Constructor
        IDepartmentRepository _departmentRepository;
        public DepartmentServices(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
    }
}
