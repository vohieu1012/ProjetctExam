using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Interfaces.Services;
using MISA.Web10.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Services
{
    public class EmulationService : BaseService<Emulation>, IEmulationService
    {
        #region field
        private readonly IEmulationRepository _repository;
        private string lagCode = Common.LanguageCode;
        #endregion
        #region contructor
        public EmulationService(IEmulationRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion


        #region methods
        /// <summary>
        /// validate object khi insert
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        protected override bool ValidateInsertObject(Emulation emulation)
        {
            //kiểm tra mã nhân viên có bị trùng
            if (_repository.CheckEmployeeCodeInsertExits(emulation.EmulationCode) == true)
            {
                IsValid = false;

                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_DuplicateEmulationCode"));
            }

            IsValid = ValidateCommon(emulation);
            return IsValid;
        }



        private bool ValidateCommon(Emulation emulation)
        {
            //kiểm tra mã danh hiệu có trống
            if (string.IsNullOrEmpty(emulation.EmulationCode.Trim()))
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_EmulationCode"));
            }

            //kiểm tra tên danh hiệu có trống
            if (string.IsNullOrEmpty(emulation.EmulationName.Trim()))
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_EmulationName"));
            }


            //valide đối tượng khen thưởng
            if (emulation.ApplyObject != Enum.ApplyObjec.Individual && emulation.ApplyObject != Enum.ApplyObjec.Team && emulation.ApplyObject != Enum.ApplyObjec.Both)
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_ApplyObject"));
            }

            //validate loại phong trào áp dụng
            if (emulation.MovementType != Enum.MovementType.Frequent && emulation.MovementType != Enum.MovementType.Both && emulation.MovementType != Enum.MovementType.Batched)
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_MovementType"));
            }


            // Nếu hợp lệ thì gọi yêu cầu thực hiện thêm mới dữ liệu vào CSDL
            return IsValid;
        }



        /// <summary>
        /// Hàm validate khi update thông tin
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="emulation"></param>
        /// <returns></returns>
        protected override bool ValidateUpdateObject(Emulation emulation)
        {

            //kiểm tra mã nhân viên có bị trùng
            if (_repository.CheckEmployeeCodeUpdateExits(emulation.EmulationID,emulation.EmulationCode) == true)
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_DuplicateEmulationCode"));
            }

            IsValid = ValidateCommon(emulation);
            return IsValid;
        }
        #endregion
    }
}
