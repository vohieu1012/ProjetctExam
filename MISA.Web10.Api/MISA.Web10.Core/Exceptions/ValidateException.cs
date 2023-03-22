using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Exceptions
{
    public class ValidateException : Exception
    {
        /// <summary>
        /// Nội dung lỗi
        /// </summary>
        public string? ValidateErrorMsg { get; set; }
        public IDictionary Errors { get; set; }
        public ValidateException(string Msg)
        {
            ValidateErrorMsg = Msg;
        }
        /// <summary>
        /// Mảng lưu các lỗi handler được
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="errorMsg"></param>
        public ValidateException(List<string> errorMsg)
        {
            Errors = new Dictionary<string, object>();
            Errors.Add("erros", errorMsg);
        }
        /// <summary>
        /// Trả về message thông báo là nội dung
        /// đã hanler
        /// </summary>
        public override string Message => this.ValidateErrorMsg;
        public override IDictionary Data => Errors;
    }
}
