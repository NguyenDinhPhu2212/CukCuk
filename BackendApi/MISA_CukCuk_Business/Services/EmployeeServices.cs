using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Properties;
using MISA.CukCuk.Core.Const;

namespace MISA.CukCuk.Core.Services
{
    public class EmployeeServices : BaseServices<Employee>, IEmployeeServices
    {
        #region Constructor
        IEmployeeRepository _employeeRepository;
        public EmployeeServices(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

    }

}
