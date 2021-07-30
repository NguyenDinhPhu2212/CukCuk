using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Common.Entity
{
    public class Department
    {
        #region Khai báo các thuộc tính cho class Department
        //Định danh phòng ban
        public Guid DepartmentId { get; set; }
        //Tên phòng ban
        public String DepartmentName { get; set; }
        //Mã phòng ban
        public String DepartmentCode { get; set; }
        //mô tả
        public String Description { get; set; }
        #endregion
    }
}
