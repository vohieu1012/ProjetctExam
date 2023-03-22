﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Models
{
    public class BaseModels
    {
        #region properties
        /// <summary>
        /// Ngày tạo
        /// </summary>
        
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }


        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
