﻿
using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MISA.CukCuk.Core.Services
{
    public class BaseServices<MISAEntity> : IBaseServices<MISAEntity> where MISAEntity:BaseEntity
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
        public virtual ResponseMessage Add(MISAEntity entity)
        {
            entity.EntityState = Enum.EntityState.Add;
            var resMsg = Validate(entity);
            if (!resMsg.Success)
            {
                return resMsg;
            }
            // Thực hiện thêm mới
            if (_baseRepository.Add(entity) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Add_Successful;
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
                resMsg.UserMsg = Resources.ExistData;
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
        public virtual ResponseMessage Update(MISAEntity entity)
        {
            entity.EntityState = Enum.EntityState.Update;
            var resMsg = Validate(entity);
            if (!resMsg.Success)
            {
                return resMsg;
            }

            // 5. Thực hiện thêm mới
            if (_baseRepository.Update(entity) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Update_Successful;
            }
            return resMsg;
        }
        #endregion
        #region Kiểm tra các trường bắt buộc
        /// <summary>
        /// Kiểm tra thuộc tính đã được nhập hay chưa
        /// </summary>
        /// <param name="entity">Bản ghi cần kiểm tra</param>
        /// <param name="prop">Thuộc tính cần kiểm tra</param>
        /// <returns>true-nếu thuộc tính đã được nhập, false-không</returns>
        public bool Required(MISAEntity entity, PropertyInfo prop)
        {
            var isRequired = true;
            if (prop.PropertyType.IsEnum)
            {
                if (string.IsNullOrEmpty(prop.GetValue(entity).ToString()))
                {
                    isRequired = false;
                }
            }
            else if (prop.GetValue(entity) == null)
            {
                isRequired = false;
            }
            else if (Guid.TryParse(prop.GetValue(entity).ToString(), out var newGuid))
            {
                if (newGuid == Guid.Empty)
                    isRequired = false;
            }
            return isRequired;
        }
        #endregion
        #region Kiểm tra tính hợp lệ của số CMND/Căn cước
        /// <summary>
        /// Kiểm tra số CMND/Căn cước hợp lệ hay không
        /// </summary>
        /// <param name="identityNumber">Số CMND/Căn cước cần kiểm tra</param>
        /// <returns>true-nếu hợp lệ, false-nếu không</returns>
        public bool CheckIdentitynumber(string identityNumber)
        {
            if (this.CheckNumber(identityNumber))
                return true;
            return false;
        }
        #endregion
        #region Kiểm tra tính hợp lệ của số điện thoại
        /// <summary>
        /// Kiểm tra số điện thoại đã hợp lệ hay không
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại cần kiểm tra</param>
        /// <returns>true-nếu hợp lệ, false-nếu không</returns>
        public bool CheckPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && this.CheckNumber(phoneNumber))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Kiểm tra tính hợp lệ của Email
        /// <summary>
        /// Kiểm tra tính hợp lệ của địa chỉ email
        /// </summary>
        /// <param name="email">Địa chỉ email cần kiểm tra</param>
        /// <returns>true-nếu hợp lệ, false- nếu không hợp lệ</returns>
        public bool CheckEmailAddress(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Kiểm tra chuỗi đầu vào là số hay không
        /// <summary>
        /// Kiểm tra chuỗi có phải là số hay không
        /// </summary>
        /// <param name="str">chuỗi cần kiểm tra</param>
        /// <returns></returns>
        public bool CheckNumber(string str)
        {
            if (!string.IsNullOrEmpty(str) && str.All(char.IsDigit))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Validate dữ liệu
        public ResponseMessage Validate(MISAEntity entity)
        {
            ResponseMessage resMsg = new ResponseMessage();
            resMsg.Success = false;
            // Đọc các property
            var properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //1. Kiểm tra có bắt buộc nhập hay không
                if (prop.IsDefined(typeof(Required), false))
                {
                    if(entity.EntityState == Enum.EntityState.Add && prop.IsDefined(typeof(PrimaryKey), false))
                    {
                    }
                    else if (!Required(entity, prop))
                    {
                        resMsg.UserMsg = Resources.ValidateError_RequiredFields;
                        return resMsg;
                    }
                }
                // 2. Kiểm tra có trùng lặp hay không
                if (prop.IsDefined(typeof(Original), false))
                {
                    var displayName = Attribute.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    if (_baseRepository.CheckDuplicate(prop, entity))
                    {
                        resMsg.UserMsg = $"{displayName} " + Resources.ValidateError_Duplicate;
                        return resMsg;
                    }
                }
                // 3. Kiểm tra Email hợp lệ hay không
                if (prop.IsDefined(typeof(Email), false))
                {
                    if (!CheckEmailAddress(prop.GetValue(entity).ToString()))
                    {
                        resMsg.UserMsg = Resources.ValidateError_InvalidEmail;
                        return resMsg;
                    }
                }
                // 4. Kiểm tra tính hợp lệ của số CMND/Căn cước
                if (prop.IsDefined(typeof(IdentityNumber), false))
                {
                    if (!CheckIdentitynumber(prop.GetValue(entity).ToString()))
                    {
                        resMsg.UserMsg = Resources.ValidateError_InvalidIdentityNumber;
                        return resMsg;
                    }
                }
                // 5. Kiểm tra tính hợp lệ của số điện thoại
                if (prop.IsDefined(typeof(PhoneNumber), false))
                {
                    if (!CheckPhoneNumber(prop.GetValue(entity).ToString()))
                    {
                        resMsg.UserMsg = Resources.ValidateError_InvalidPhoneNumber;
                        return resMsg;
                    }
                }
                // 6.Kiểm tra khóa chính
            }
            resMsg.Success = true;
            return resMsg;
        }
        #endregion
        #endregion
    }
}
