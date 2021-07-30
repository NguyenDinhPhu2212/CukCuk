
using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Entity;
using MISA.CukCuk.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISA.CukCuk.Business.Services
{
    public class BaseServices<MISAEntity> : IBaseServices<MISAEntity>
    {
        #region Constructor
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseServices(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        #region Thêm bản ghi mới
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm mới</param>
        /// <returns></returns>
        public virtual ResponseMessage AddNewEntity(MISAEntity entity)
        {
            ResponseMessage resMsg = new ResponseMessage();
            if(_baseRepository.AddNewEntity(entity) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Add_Successful;
            }
            else
            {
                resMsg.Success = false;
            }
            return resMsg;
        }
        #endregion
        #region Xóa bản ghi theo khóa chính
        /// <summary>
        /// Xóa bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns></returns>
        public ResponseMessage DeleteById(Guid entityId)
        {
            ResponseMessage resMsg = new ResponseMessage();
            if (_baseRepository.DeleteById(entityId) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Delete_Successfull;
            }
            else
            {
                resMsg.Success = false;
            }
            return resMsg;
        }
        #endregion
        #region Lấy tất cả các bản ghi trong database
        /// <summary>
        /// Lấy tất cả các bản ghi trong database
        /// </summary>
        /// <returns></returns>
        public List<MISAEntity> GetAll()
        {
            return _baseRepository.GetAll();
        }
        #endregion
        #region Lấy bản ghi theo khóa chính
        /// <summary>
        /// Lấy bản ghi theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns></returns>
        public MISAEntity GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }
        #endregion
        #region Cập nhật bản ghi trong database
        /// <summary>
        /// Cập nhật bản ghi trong database
        /// </summary>
        /// <param name="entity">Bản ghi sau khi sửa đổi</param>
        /// <returns></returns>
        public virtual ResponseMessage UpdateEntity(MISAEntity entity)
        {
            ResponseMessage resMsg = new ResponseMessage();
            if (_baseRepository.UpdateEntity(entity) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Update_Successful;
            }
            else
            {
                resMsg.Success = false;
            }
            return resMsg;
        }
        #endregion
        #endregion
    }
}
