using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;


namespace MISA.CukCuk.Business.Interfaces
{
    public interface IBaseServices<MISAEntity>
    {
        /// <summary>
        /// Lấy các bản ghi trong database
        /// </summary>
        /// <returns>Tất cả bản ghi trong database</returns>
        public List<MISAEntity> GetAll();
        /// <summary>
        /// Lấy bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns></returns>
        public MISAEntity GetById(Guid entityId);
        /// <summary>
        /// Xóa bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public ResponseMessage DeleteById(Guid entityId);
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm mới</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public ResponseMessage AddNewEntity(MISAEntity entity);
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cập nhật</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public ResponseMessage UpdateEntity(MISAEntity entity);
    }
}
