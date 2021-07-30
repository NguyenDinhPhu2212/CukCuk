using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Dapper;
using MySqlConnector;
using System.Linq;
using System.Reflection;
using MISA.CukCuk.Business.Interfaces;

namespace MISA.CukCuk.DataAccess
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
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
        public int AddNewEntity(MISAEntity entity)
        {
            //Build lệnh truy vấn dữ liệu
            var insertPath = $"INSERT INTO {entityName} (";
            var valuePath = "VALUE (";
            DynamicParameters parameters = new DynamicParameters();
            //Lấy ra các thuộc tính của entity
            PropertyInfo[] propInfos = entity.GetType().GetProperties();
            //Thêm vào câu lệnh truy vấn
            for (int i = 0; i < propInfos.Length; i++)
            {
                var propName = propInfos[i].Name;
                var propValue = propInfos[i].GetValue(entity, null);
                insertPath += $"{propName}";
                valuePath += $"@{propName}";
                if (i != propInfos.Length - 1)
                {
                    insertPath += ",";
                    valuePath += ",";
                }
                parameters.Add($"@{propName}", propValue);
            }
            insertPath += ") ";
            valuePath += ") ";
            var sqlCommand = insertPath + valuePath;
            //Truy vấn
            var rowAffected = _dbConnection.Execute(sqlCommand, parameters);
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
            //Tạo câu truy vấn
            var sqlCommand = $"SELECT * FROM {entityName}";
            //Thực hiện truy vấn
            var entities = _dbConnection.Query<MISAEntity>(sqlCommand).ToList();
            return entities;
        }
        #endregion
        #region Lấy bản ghi theo khóa chính
        public MISAEntity GetById(Guid entityId)
        {
            //Tạo câu truy vấn
            var sqlCommand = $"SELECT * FROM {entityName} WHERE {entityName}Id = @{entityName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{entityName}Id", entityId);
            //Thực hiện truy vấn
            var entity = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand,parameters);
            return entity;
        }
        #endregion
        #region Cập nhật bản ghi
        public int UpdateEntity(MISAEntity entity)
        {
            //Build lệnh truy vấn dữ liệu
            var updatePath = $"UPDATE {entityName} ";
            var valuePath = "SET ";
            DynamicParameters parameters = new DynamicParameters();
            //Lấy ra các thuộc tính của entity
            PropertyInfo[] propInfos = entity.GetType().GetProperties();
            //Thêm vào câu lệnh truy vấn
            for (int i = 0; i < propInfos.Length; i++)
            {
                var propName = propInfos[i].Name;
                var propValue = propInfos[i].GetValue(entity, null);
                if (propName != $"{entityName}Id")
                {
                    valuePath += $"{propName} = @{propName}";
                    if (i != propInfos.Length - 1)
                    {
                        valuePath += ",";
                    }
                    parameters.Add($"@{propName}", propValue);
                }
            }
            valuePath += $" WHERE {entityName}Id = @{entityName}Id";
            var sqlCommand = updatePath + valuePath;
            //Truy vấn
            var rowAffected = _dbConnection.Execute(sqlCommand, parameters);
            return rowAffected;
        }
        #endregion
    }
}
