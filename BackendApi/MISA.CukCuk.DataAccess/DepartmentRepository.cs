using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.CukCuk.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace MISA.CukCuk.DataAccess
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion
        /// <summary>
        /// Kiểm tra sự trùng lặp mã phòng ban trong database
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns>true-nếu trùng, false-không</returns>
        public bool CheckDuplicateDepartmentCode(string departmentCode)
        {
            //Khai báo lệnh truy vấn dữ liệu
            var sqlCommand = $"SELECT * FROM Department WHERE DepartmentCode = @DepartmentCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@DepartmentCode", departmentCode);
            //Truy vấn
            var response = _dbConnection.QueryFirstOrDefault<Department>(sql: sqlCommand, param: parameters);
            if (response != null)
            {
                return true;
            }
            else return false;
        }
    }
}
