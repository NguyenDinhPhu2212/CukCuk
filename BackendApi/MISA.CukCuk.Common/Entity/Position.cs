using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Common.Entity
{
    public class Positions
    {
        #region Khai báo các thuộc tính cho class Position
        //Định danh vị trí
        public Guid PositionId { get; set; }
        //Tên chức vụ
        public String PositionName { get; set; }
        //Mã chức vụ
        public String PositionCode { get; set; }
        //mô tả chức vụ
        public String Description { get; set; }
        #endregion
    }
}
