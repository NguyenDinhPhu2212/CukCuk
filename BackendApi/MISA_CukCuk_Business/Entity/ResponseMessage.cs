using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entity
{
    public class ResponseMessage
    {
        #region Khai báo thuộc tính cho thông điệp phản hồi
        public Boolean Success { get; set; }
        /// <summary>
        /// Thông điệp của người phát triển
        /// </summary>
        public String DevMsg { get; set; }
        /// <summary>
        /// Thông điệp cho người dùng
        /// </summary>
        public String UserMsg { get; set; }
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public String ErrorCode { get; set; }
        #endregion
    }
}
