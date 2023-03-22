using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Models
{
    public class CommendationLevel: BaseModels
    {
        #region properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid CommendationLevelID { get; set; }

        /// <summary>
        /// Mã cấp khen thưởng
        /// </summary>
        public string CommendationLevelCode { get; set; }

        /// <summary>
        /// Tên cấp khen thưởng
        /// </summary>
        public string CommendationLevelName { get; set; }
        #endregion
    }
}
