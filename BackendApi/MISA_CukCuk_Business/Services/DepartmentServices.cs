using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.CukCuk.Common.Properties;
namespace MISA.CukCuk.Business.Services
{
    public class DepartmentServices : BaseServices<Department>, IDepartmentServices
    {
        #region Constructor
        IDepartmentRepository _departmentRepository;
        public DepartmentServices(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
        #region Override methods
        /// <summary>
        /// Thêm mới bản ghi vào trong Department
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm mới</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public override ResponseMessage AddNewEntity(Department entity)
        {
            ResponseMessage resMsg = new ResponseMessage();
            resMsg.Success = false;
            // 1. Kiểm tra đã cung cấp đầy đủ thông tin chưa
            if (!CheckDepartment(entity))
            {
                resMsg.UserMsg = Resources.ValidateError_RequiredFields;
                return resMsg;
            }
            // 2. Kiểm tra trong database đã tồn tại mã phòng ban này chưa
            if (_departmentRepository.CheckDuplicateDepartmentCode(entity.DepartmentCode))
            {
                resMsg.UserMsg = Resources.ValidateError_DuplicateDepartmentCode;
                return resMsg;
            }
            // 3. Thực hiện thêm mới bản ghi và trả về thông điệp
            if (_departmentRepository.AddNewEntity(entity) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Add_Successful;
            }
            return resMsg;

        }
        /// <summary>
        /// Cập nhật bản ghi trong Department
        /// </summary>
        /// <param name="entity">Bản ghi sau khi sửa đổi</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public override ResponseMessage UpdateEntity(Department entity)
        {
            ResponseMessage resMsg = new ResponseMessage();
            resMsg.Success = false;
            // 1. Kiểm tra đã cung cấp đầy đủ thông tin chưa
            if (!CheckDepartment(entity))
            {
                resMsg.UserMsg = Resources.ValidateError_RequiredFields;
                return resMsg;
            }
            // 2. Thực hiện thêm mới bản ghi và trả về thông điệp
            if (_departmentRepository.UpdateEntity(entity) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Update_Successful;
            }
            return resMsg;
        }
        #endregion
        /// <summary>
        /// Kiểm tra các trương có trống hay không
        /// </summary>
        /// <param name="department">Bản ghi cần kiểm tra</param>
        /// <returns>true-nếu thỏa mãn, false-nếu không thỏa mãn</returns>
        public bool CheckDepartment(Department department)
        {
            if (string.IsNullOrEmpty(department.DepartmentCode) 
                || string.IsNullOrEmpty(department.Description) 
                || string.IsNullOrEmpty(department.DepartmentName))
            {
                return false;
            }
            return true;
        }
    }
}
