using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Enum
{
    public enum ErrorEnum
    {
        /// <summary>
        /// Lỗi khi gửi request
        /// </summary>
        BadRequest = 0,
        /// <summary>
        /// Lỗi server
        /// </summary>
        ServerError = 1,
    }
}
