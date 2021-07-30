using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Business.Interfaces
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy tất cả các bản ghi trong database
        /// </summary>
        /// <returns>Tất cả các bản ghi</returns>
        public List<MISAEntity> GetAll();
        /// <summary>
        /// Lấy ra bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Bản ghi đầu tiên hoặc không có</returns>
        public MISAEntity GetById(Guid entityId);
        /// <summary>
        /// Xóa bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>số cột bị ảnh hưởng</returns>
        public int DeleteById(Guid entityId);
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>Số cột ảnh hưởng</returns>
        public int AddNewEntity(MISAEntity entity);
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần cập nhật</param>
        /// <returns></returns>
        public int UpdateEntity(MISAEntity entity);
    }
}
