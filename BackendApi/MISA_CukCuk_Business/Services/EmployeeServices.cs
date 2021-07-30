using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MISA.CukCuk.Common.Entity;
using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Properties;

namespace MISA.CukCuk.Business.Services
{
    public class EmployeeServices : BaseServices<Employee>, IEmployeeServices
    {
        #region Constructor
        IEmployeeRepository _employeeRepository;
        public EmployeeServices(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Override methods
        /// <summary>
        /// Kiểm tra dữ liệu đầu vào và thêm bản ghi
        /// </summary>
        /// <param name="employee">Bản ghi cần thêm mới</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public override ResponseMessage AddNewEntity(Employee employee)
        {
            ResponseMessage resMsg = new ResponseMessage();
            resMsg.Success = false;
            // 1. Kiểm tra các trường bắt buộc có khác rỗng không
            if (!CheckRequiredFields(employee))
            {
                resMsg.UserMsg = Resources.ValidateError_RequiredFields;
                return resMsg;
            }
            // 2. Kiểm tra có tồn tại bản ghi nào có mã nhân viên giống hay không
            if (_employeeRepository.CheckDuplicateEmployeeCode(employee.EmployeeCode))
            {
                resMsg.UserMsg = Resources.ValidateError_DuplicateEmployeeCode;
                return resMsg;
            }
            // 3. Kiểm tra Email hợp lệ hay không
            if (!CheckEmailAddress(employee.Email))
            {
                resMsg.UserMsg = Resources.ValidateError_InvalidEmail;
                return resMsg;
            }
            // 4. Kiểm tra tính hợp lệ của số CMND/Căn cước
            if (!CheckIdentitynumber(employee.IdentityNumber))
            {
                resMsg.UserMsg = Resources.ValidateError_InvalidIdentityNumber;
                return resMsg;
            }
            // 5. Kiểm tra tính hợp lệ của số điện thoại
            if (!CheckPhoneNumber(employee.PhoneNumber))
            {
                resMsg.UserMsg = Resources.ValidateError_InvalidPhoneNumber;
                return resMsg;
            }
            // 6. Thực hiện thêm mới
            if (_employeeRepository.AddNewEntity(employee) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Add_Successful;
            }
            return null;
        }
        /// <summary>
        /// Kiểm tra dữ liệu đầu vào và cập nhật bản ghi
        /// </summary>
        /// <param name="employee">Bản ghi sau khi sửa đổi</param>
        /// <returns>Thông điệp trả lời chuẩn resful</returns>
        public override ResponseMessage UpdateEntity(Employee employee)
        {
            ResponseMessage resMsg = new ResponseMessage();
            resMsg.Success = false;
            // 1. Kiểm tra các trường bắt buộc có khác rỗng không
            if (!CheckRequiredFields(employee))
            {
                resMsg.UserMsg = Resources.ValidateError_RequiredFields;
                return resMsg;
            }
            // 2. Kiểm tra Email hợp lệ hay không
            if (!CheckEmailAddress(employee.Email))
            {
                resMsg.UserMsg = Resources.ValidateError_InvalidEmail;
                return resMsg;
            }
            // 3. Kiểm tra tính hợp lệ của số CMND/Căn cước
            if (!CheckIdentitynumber(employee.IdentityNumber))
            {
                resMsg.UserMsg = Resources.ValidateError_InvalidIdentityNumber;
                return resMsg;
            }
            // 4. Kiểm tra tính hợp lệ của số điện thoại
            if (!CheckPhoneNumber(employee.PhoneNumber))
            {
                resMsg.UserMsg = Resources.ValidateError_InvalidPhoneNumber;
                return resMsg;
            }
            // 5. Thực hiện thêm mới
            if (_employeeRepository.UpdateEntity(employee) > 0)
            {
                resMsg.Success = true;
                resMsg.UserMsg = Resources.Update_Successful;
            }
            return null;
        }
        #endregion
        #region Kiểm tra các trường bắt buộc
        /// <summary>
        /// Kiểm tra các trường bắt buộc đã được nhập hay chưa
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần kiểm tra</param>
        /// <returns>true-nếu thõa mãn, false-không thỏa mãn</returns>
        public bool CheckRequiredFields(Employee employee)
        {
            List<string> requiredFields = new List<string>();
            requiredFields.Add("EmployeeCode");
            requiredFields.Add("FullName");
            requiredFields.Add("Gender");
            requiredFields.Add("IdentityNumber");
            requiredFields.Add("Email");
            requiredFields.Add("PhoneNumber");
            var isRequired = true;
            foreach (var prop in employee.GetType().GetProperties())
            {
                if (requiredFields.Contains(prop.Name.ToString()))
                {
                    if (string.IsNullOrEmpty(prop.GetValue(employee, null).ToString()))
                    {
                        isRequired = false;
                    }
                }
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
    }

}
