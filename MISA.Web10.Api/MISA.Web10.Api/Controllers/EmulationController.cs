using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Interfaces.Services;
using MISA.Web10.Core.Models;

namespace MISA.Web10.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationController : BaseController<Emulation>
    {
        #region field
        private readonly IEmulationRepository _repository;
        private readonly IEmulationService _service;
        #endregion

        #region contructor
        public EmulationController(IEmulationRepository repository, IEmulationService service) : base(repository, service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion



        #region methods
        /// <summary>
        /// Lấy danh sách nhân viên theo từ khóa
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string? filter,string? filterSuite)
        {
            try
            {
                var res = _repository.GetPaging(pageIndex, pageSize, filter , filterSuite);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Lấy mã danh hiệu mới
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewEmulationCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmulationCode = _repository.GetNewEmulationCode();
                return Ok(newEmulationCode);
            }
            catch (Exception ex)
            {
                throw ex;
               // return HandleException(ex.Message);
            }
        }

        /// <summary>
        /// Xóa nhiều danh hiệu
        /// author:DPQuy(13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        [HttpDelete("deleteMultiple")]
        public IActionResult DeleteMultiple(string listID)
        {
            try
            {
                var res = _repository.DeleteMultiple(listID);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }




        /// <summary>
        /// Cập nhật trạng thái đang sử dụng cho danh hiệu/nhiều danh hiệu
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        [HttpPost("UpdateActiveStatus")]

        public IActionResult UpdateActiveStatus(string listID)
        {
            try
            {
                var res = _repository.UpdateActiveStatus(listID);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>
        /// Cập nhật trạng thái ngưng sử dụng cho danh hiệu/nhiều danh hiệu
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        [HttpPost("UpdateInActiveStatus")]
        public IActionResult UpdateInActiveStatus(string listID)
        {
            try
            {
                var res = _repository.UpdateInActiveStatus(listID);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        #endregion
    }
}
