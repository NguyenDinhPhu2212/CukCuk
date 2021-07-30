using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.DataAccess
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Kiểm tra trùng lặp mã nhân viên
        public bool CheckDuplicateEmployeeCode(string employeeCode)
        {
            //Khai báo lệnh truy vấn dữ liệu
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@EmployeeCode", employeeCode);
            //Truy vấn
            var response = _dbConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: parameters);
            if (response != null)
            {
                return true;
            }
            else return false;
        }
        #endregion
    }
}
