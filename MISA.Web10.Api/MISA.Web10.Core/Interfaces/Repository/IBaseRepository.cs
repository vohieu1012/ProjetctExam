using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        #region methods
        //CreateBy : Đinh Phú Quý(13/11/2022)
        //lấy danh sách department
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Lấy bản ghi theo id
        /// createby: DTEntityuy (13/11/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(Guid id);

        /// <summary>
        /// Thêm mới bản ghi
        /// //createby : DTEntityuy (13/11/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);



        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// createby: DTEntityuy (13/11/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity, Guid id);


        /// <summary>
        /// Xóa bản ghi theo id
        /// createby; DTEntityuy (13/11/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(Guid id);
        #endregion
    }
}
