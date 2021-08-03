using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Dapper;
using MySqlConnector;
using System.Linq;
using System.Reflection;
using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Entity;
using System.Data;

namespace MISA.CukCuk.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity:BaseEntity
    {
        #region Declare
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected DbConnection _dbConnection = null;
        string entityName;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            entityName = typeof(MISAEntity).Name;
        }
        #endregion
        #region Thực hiên lệnh truy vấn thêm mới bản ghi
        /// <summary>
        /// Thực hiện lệnh truy vấn thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm mới</param>
        /// <returns></returns>
        public int Add(MISAEntity entity)
        {
            var parameters = MapingType(entity);
            //Truy vấn
            var rowAffected = _dbConnection.Execute($"Proc_Insert{entityName}", parameters,
                commandType: CommandType.StoredProcedure);
            return rowAffected;
        }
        #endregion
        #region Xóa bản ghi theo khóa chính
        /// <summary>
        /// Xóa bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns></returns>
        public int DeleteById(Guid entityId)
        {
            //Tạo câu truy vấn
            var sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}Id = @{entityName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{entityName}Id", entityId);
            //Thực hiện truy vấn
            var rowAffected = _dbConnection.Execute(sql: sqlCommand, param: parameters); ;
            return rowAffected;
        }
        #endregion
        #region Lấy tất cả các bản ghi trong database
        public List<MISAEntity> GetAll()
        {
            //Thực hiện truy vấn
            var entities = _dbConnection.Query<MISAEntity>($"Proc_Get{entityName}s", 
                commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }
        #endregion
        #region Lấy bản ghi theo khóa chính
        public MISAEntity GetById(Guid entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{entityName}Id", entityId.ToString());
            //Thực hiện truy vấn
            var entity = _dbConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{entityName}ById", parameters, 
                         commandType: CommandType.StoredProcedure);
            return entity;
        }
        #endregion
        #region Cập nhật bản ghi
        public int Update(MISAEntity entity)
        {
            var parameters = MapingType(entity);
            //Truy vấn
            var rowAffected = _dbConnection.Execute($"Proc_Update{entityName}", parameters, 
                commandType: CommandType.StoredProcedure);
            return rowAffected;
        }
        #endregion
        #region Kiểm tra trùng lặp mã code
        public bool CheckDuplicate(PropertyInfo prop, MISAEntity entity)
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(entity).ToString();
            if (entity.EntityState == Core.Enum.EntityState.Add)
            {
                //Khai báo lệnh truy vấn dữ liệu
                var sqlCommand = $"SELECT * FROM {entityName} WHERE {propName} = '{propValue}'";
                //Truy vấn
                var response = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand);
                if (response != null)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        #endregion
        #region Maping Type
        public DynamicParameters MapingType(MISAEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var paramerters = new DynamicParameters();
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                var propType = prop.PropertyType;
                if(propType == typeof(Guid) || propType == typeof(Guid?))
                {
                    paramerters.Add($"@{propName}", propValue, DbType.String);
                }else
                {
                    paramerters.Add($"@{propName}", propValue);
                }
            }
            return paramerters;
        }
        #endregion
    }
}
