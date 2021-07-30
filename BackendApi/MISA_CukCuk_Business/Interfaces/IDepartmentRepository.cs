using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Business.Interfaces
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {
        /// <summary>
        /// Kiêm tra sự trùng lặp trong mã phòng ban
        /// </summary>
        /// <param name="departmentCode">Mã phòng ban</param>
        /// <returns>true-nếu trùng, false-nếu không trùng</returns>
        public bool CheckDuplicateDepartmentCode(string departmentCode);
    }
}
