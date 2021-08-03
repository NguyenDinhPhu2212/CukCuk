using MISA.CukCuk.Core.Enum;
using MISA.CukCuk.Core.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MISA.CukCuk.Core.Entity
{
    public class Employee:BaseEntity
    {
        #region Khai báo thuộc tính cho class Employee
        /*
         * Id của nhân viên
         */
        [Required]
        [PrimaryKey]
        public Guid EmployeeId { get; set; }
        /*
         * Tên nhân viên
         */
        [Required]
        public String FullName { get; set; }
        /*
         * Mã nhân viên
         */
        [Original]
        [Required]
        [DisplayName("Mã nhân viên")]
        public String EmployeeCode { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        [Required]
        public Gender? Gender { get; set; }
        /*
         * Email
         */
        [Required]
        [Email]
        public String Email { get; set; }
        /*
         * Số điện thoại
         */
        [Required]
        [PhoneNumber]
        public String PhoneNumber { get; set; }
        /*
         * Số CMND/Căn Cước
         */
        [Required]
        [IdentityNumber]
        public String IdentityNumber { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Ten
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Ngày cấp phát CMND/Căn cước
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp CMND/Căn cước
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int MartialStatus { get; set; }
        /// <summary>
        /// Học vấn
        /// </summary>
        public int EducationalBackground { get; set; }
        public Guid QualificationId { get; set; }
        /// <summary>
        /// Định danh phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Định danh chức vụ
        /// </summary>
        public Guid PositionId { get; set; }
        /// <summary>
        /// Tình trạng công việc
        /// </summary>
        public int WorkStatus { get; set; }
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// Lương
        /// </summary>
        public double Salary { get; set; }
        #endregion
    }
}
