using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Enum
{
    public enum MovementType
    {
        //Enum phong trào áp dụng

        /// <summary>
        /// Theo đợt
        /// </summary>
        Batched = 1,

        /// <summary>
        /// Thường xuyên
        /// </summary>
        Frequent = 2,

        /// <summary>
        /// Thường xuyên,theo đợt
        /// </summary>
        Both = 3,
    }
}
