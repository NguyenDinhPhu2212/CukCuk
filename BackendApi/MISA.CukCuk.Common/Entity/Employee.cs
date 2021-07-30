using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Common.Entity
{
    public class Employee
    {
        #region Khai báo thuộc tính cho class Employee
        /*
         * Id của nhân viên
         */
        public Guid EmployeeId { get; set; }
        /*
         * Tên nhân viên
         */
        public String FullName { get; set; }
        /*
         * Mã nhân viên
         */
        public String EmployeeCode { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int Gender { get; set; }
        /*
         * Email
         */
        public String Email { get; set; }
        /*
         * Số điện thoại
         */
        public String PhoneNumber { get; set; }
        /*
         * Số CMND/Căn Cước
         */
        public String IdentityNumber { get; set; }
        #endregion
    }
}
