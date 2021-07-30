using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Business.Interfaces
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra tồn tại 2 bản ghi trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true-nếu tồn tại, false-nếu không tồn tại</returns>
        public bool CheckDuplicateEmployeeCode(string employeeCode);
    }
}
