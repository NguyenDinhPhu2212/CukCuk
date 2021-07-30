using MISA.CukCuk.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Business.Interfaces
{
    public interface IEmployeeServices : IBaseServices<Employee>
    {
        /// <summary>
        /// Kiểm tra các trường bắt buộc đã điền đủ hay không
        /// </summary>
        /// <param name="employee">Bản ghi cần kiểm tra</param>
        /// <returns>true-nếu đủ, false-nếu không đủ</returns>
        public bool CheckRequiredFields(Employee employee);
        /// <summary>
        /// Kiểm tra tính hợp lệ số CMND/Căn cước
        /// </summary>
        /// <param name="identityNumber">số CMND/Căn cước</param>
        /// <returns>true-nếu hợp lệ, false-nếu không hợp lệ</returns>
        public bool CheckIdentitynumber(string identityNumber);
        /// <summary>
        /// Kiểm tra tính hợp lệ của số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>true-nếu hợp lệ, false-nếu không hợp lệ</returns>
        public bool CheckPhoneNumber(string phoneNumber);
        /// <summary>
        /// Kiểm tra tính hợp lệ của email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>true-nếu hợp lệ, false-nếu không hợp lệ</returns>
        public bool CheckEmailAddress(string email);
        /// <summary>
        /// Kiểm tra chuối đầu vào có là 1 số hay không
        /// </summary>
        /// <param name="str">Chuối cần kiểm tra</param>
        /// <returns>true-nếu hợp lệ, false-nếu không hợp lệ</returns>
        public bool CheckNumber(string str);
    }
}
