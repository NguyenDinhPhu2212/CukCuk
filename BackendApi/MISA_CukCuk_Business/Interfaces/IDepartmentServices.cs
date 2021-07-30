using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Business.Interfaces
{
    public interface IDepartmentServices:IBaseServices<Department>
    {
        public bool CheckDepartment(Department department);
    }
}
