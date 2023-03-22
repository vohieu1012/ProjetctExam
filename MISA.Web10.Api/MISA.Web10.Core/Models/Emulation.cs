using MISA.Web10.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Models
{
    public class Emulation :BaseModels
    {
        #region properties

        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid EmulationID { get; set; }

        /// <summary>
        /// Mã danh hiệu thi đua
        /// </summary>
        public string EmulationCode { get; set; }
        /// <summary>
        /// Tên danh hiệu thi đua
        /// </summary>
        public string EmulationName { get; set; }

        /// <summary>
        /// Đối tượng khen thưởng
        /// </summary>
        public ApplyObjec ApplyObject { get; set; }

        /// <summary>
        /// Khóa ngoại bảng CommendationLevel
        /// </summary>
        public Guid CommendationLevelID { get; set; }

        /// <summary>
        /// Tên cấp khen thưởng
        /// </summary>
        public string? CommendationLevelName { get; set; }

        /// <summary>
        /// Loại phong trào áp dụng
        /// </summary>
        public MovementType MovementType { get; set; }

        /// <summary>
        /// Trạng thái (1:sử dụng,2:Ngưng sử dụng)
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }


        /// <summary>
        /// dữ liệu hệ thống
        /// </summary>
        public IsSystemEnum IsSystem { get; set; }
        #endregion
    }
}
