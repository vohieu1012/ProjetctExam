using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MISA.Web10.Core;
using MISA.Web10.Core.Enum;
using MISA.Web10.Core.Exceptions;
using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Interfaces.Services;
using System.Resources;

namespace MISA.Web10.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase
    {
        #region contructor
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IBaseService<TEntity> _baseService;
        public BaseController(IBaseRepository<TEntity> baseRepository, IBaseService<TEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region methods
        /// <summary>
        /// lấy toàn bộ bản ghi
        /// created by : DPQuy (13/11/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = _baseRepository.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Lấy ra bản ghi theo id
        /// created by : DPQuy (13/11/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var res = _baseRepository.GetById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thêm nhân viên
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            try
            {
                var res = _baseService.InsertService(entity);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(TEntity entity, Guid id)
        {
            try
            {
                var res = _baseService.UpdateService(entity, id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// Xóa danh hiệu theo id
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var res = _baseRepository.Delete(id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý Exception
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult HandleException(Exception ex)
        {


            if (ex is ValidateException)
            {
                //ghi log vào hệ thống
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    errorCode = ErrorEnum.BadRequest,
                    userMsg = ex.Message,
                };
                return StatusCode(400, res);

            }
            else
            {
                //ghi log vào hệ thống
                var res = new
                {
                    devMsg = ex.Message,
                    errorCode = ErrorEnum.ServerError,
                    userMsg = MISA.Web10.Core.Resources.ResourceVN.ResourceManager.GetString($"ErrorException_{Common.LanguageCode}"),

                };
            return StatusCode(500, res);
            }
        }
        #endregion
    }
}
