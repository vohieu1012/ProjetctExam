using MISA.Web10.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Interfaces.Repository
{
    public interface IEmulationRepository:IBaseRepository<Emulation>
    {

        #region methods
        /// <summary>
        ///check mã danh hiệu trùng khi insert
        ///true-đã tồn tại
        ///false-chưa có
        ///Created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="emulationCode"></param>
        /// <returns></returns>

        bool CheckEmployeeCodeInsertExits(string emulationCode);

        /// <summary>
        /// check mã trùng khi update
        /// create by: DPQuy (13/11/2022_
        /// </summary>
        /// <param name="emulationCode"></param>
        /// <returns></returns>
        bool CheckEmployeeCodeUpdateExits(Guid id, string emulationCode);

        /// <summary>
        /// Lấy danh sách theo trang và số lượng
        /// bản ghi
        /// DPQuy (13/11/2022)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Object GetPaging(int pageIndex, int pageSize, string? filter = "",string? filterSuite = "");


        /// <summary>
        /// Lấy mã danh hiệu mới
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <returns></returns>
        Emulation GetNewEmulationCode();

        /// <summary>
        /// Xóa nhiều bản ghi
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        int DeleteMultiple(string listID);


        /// <summary>
        /// cập nhật danh hiệu với trạng thái đang sử dụng
        /// created by:DPQuy (13/11/20220
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        int UpdateActiveStatus(string listID);

        /// <summary>
        /// cập nhật danh hiệu với trạng thái ngừng sử dụng
        /// created by:DPQuy (13/11/20220
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        int UpdateInActiveStatus(string listID);
        #endregion

    }
}
