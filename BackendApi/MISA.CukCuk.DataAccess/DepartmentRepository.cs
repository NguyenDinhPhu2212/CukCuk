using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.CukCuk.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace MISA.CukCuk.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion
        
    }
}
