using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Core.Entity
{
    public class Position:BaseEntity
    {
        #region Khai báo các thuộc tính cho class Position
        //Định danh vị trí
        [Required]
        [PrimaryKey]
        public Guid PositionId { get; set; }
        //Tên chức vụ
        [Required]
        public String PositionName { get; set; }
        [Required]
        [Original]
        //Mã chức vụ
        public String PositionCode { get; set; }
        //mô tả chức vụ
        [Required]
        public String Description { get; set; }
        #endregion
    }
}
