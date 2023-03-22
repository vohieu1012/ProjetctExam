using Dapper;
using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Infrastructor.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region field
        protected string ConnectionString;
        protected MySqlConnection MySqlConnection;
        protected MySqlCommand MySqlCommand;
        protected string sql;
        protected string TableName;
        #endregion

        #region contructor
        public BaseRepository()
        {
            //thông tin kết nối CSDL
            ConnectionString = "Host=localhost;" +
             "Port=3306;" +
             "Database=misa.web10.dpquy;" +
             "User Id =root;" +
             "Password=12345678";

            //lấy ra kiểu dữ liệu
            TableName = typeof(TEntity).Name;
        }
        #endregion

        #region methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {

            //khởi tạo kết nối
            //createby: DPQuy (6/7/2022)
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                //câu lệnh truy vấn
                var sql = $"Select * from {TableName}";

                //thực hiện truy vấn
                var data = MySqlConnection.Query<TEntity>(sql);

                //trả về dữ liệu
                return data;
            }
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// createby: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Select * from {TableName} Where {TableName}Id= @{TableName}Id";
                var parametes = new DynamicParameters();
                parametes.Add($"@{TableName}Id", id);
                var res = MySqlConnection.QueryFirstOrDefault<TEntity>(sql, param: parametes);
                return res;
            }
        }


        /// <summary>
        ///Xóa bản ghi theo id
        ///createby: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Delete from {TableName} where {TableName}Id = @{TableName}Id";
                var parametes = new DynamicParameters();
                parametes.Add($"@{TableName}Id", id);
                var res = MySqlConnection.Execute(sql, param: parametes, commandType: System.Data.CommandType.Text);
                return res;
            }
        }



        /// <summary>
        /// Hàm update bản ghi
        /// created by: DPQuy(13/11/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity, Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                //lấy ra các properties của object
                var properties = entity.GetType().GetProperties();
                var parametes = new DynamicParameters();

                // Khai báo chuỗi SQL động
                var sqlColumn = "";


                foreach (var property in properties)
                {
                    //lấy ra tên các property
                    var propertyName = property.Name;
                    //lấy ra giá trị các property được truyền lên
                    var propertyValue = property.GetValue(entity);
                    

                    if(propertyName != $"{TableName}ID" && propertyName != "CreatedDate")
                    {
                        // Bổ sung vào chuỗi Column động
                        sqlColumn += $"{propertyName} = @{propertyName},";
                        parametes.Add($"@{propertyName}", propertyValue);
                    }
                }
                // Cắt phần tử dấu phẩy cuối cùng trong chuối:
                sqlColumn = sqlColumn.Substring(0, sqlColumn.Length - 1);

                parametes.Add($"{TableName}ID", id);
                var sql = $"UPDATE {TableName} SET {sqlColumn} WHERE {TableName}ID = @{TableName}ID";

                var res = MySqlConnection.Execute(sql, param: entity, commandType: System.Data.CommandType.Text);
                return res;
            }
        }

        /// <summary>
        /// Hàm insert bản ghi mới
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
               
                var parametes = new DynamicParameters();

                // Khai báo chuỗi SQL động
                var sqlColumn = "";
                var sqlParam =  "";

                //lấy ra các properties của object
                var properties = entity.GetType().GetProperties();
                // Duyệt từng propert
                foreach (var property in properties)
                {
                    //lấy ra tên các property
                    var propertyName = property.Name;
                    //lấy ra giá trị các property được truyền lên
                    var propertyValue = property.GetValue(entity);


                    //lấy ra khóa chính
                    var keyAttribute = (KeyAttribute)property.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                    if(keyAttribute != null)
                    {
                        //gắn value cho khóa chính
                        propertyValue = Guid.NewGuid();
                    }
                    if(propertyName == "CreatedDate")
                    {
                        propertyValue = DateTime.Now;
                    }

                    // Bổ sung vào chuỗi Column động
                    sqlColumn += $"{propertyName},";
                    sqlParam += $"@{propertyName},";

                    parametes.Add($"@{propertyName}", propertyValue);
                    
                }
                // Cắt phần tử dấu phẩy cuối cùng trong chuối:
                sqlColumn = sqlColumn.Substring(0, sqlColumn.Length - 1);
                sqlParam = sqlParam.Substring(0, sqlParam.Length - 1);
                //câu lệnh truy vấn
                var sql = $"INSERT INTO {TableName} ({sqlColumn}) VALUES({sqlParam})";
                var res = MySqlConnection.Execute(sql, param: parametes, commandType: System.Data.CommandType.Text);
                return res;
            }
        }
        #endregion

    }
}
