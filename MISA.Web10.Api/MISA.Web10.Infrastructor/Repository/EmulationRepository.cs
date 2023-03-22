using Dapper;
using MISA.Web10.Core.Enum;
using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Infrastructor.Repository
{
    public class EmulationRepository:BaseRepository<Emulation>,IEmulationRepository
    {
        #region methods
        /// <summary>
        /// kiểm tra có bị trùng mã 
        /// createby : DPQuy (11/5/2022)
        /// </summary>
        /// <param name="emulationCode"></param>
        /// <returns></returns>
        public bool CheckEmployeeCodeInsertExits(string emulationCode)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT EmulationCode FROM Emulation WHERE EmulationCode = @EmulationCode";
                var parametes = new DynamicParameters();
                parametes.Add("@EmulationCode", emulationCode);
                var res = MySqlConnection.QueryFirstOrDefault(sql: sql, param: parametes);
                if (res == null)
                    return false;
                return true;
            }
        }


        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex">vị tri trang</param>
        /// <param name="pageSize">Số lượng bản ghi trên trang</param>
        /// <param name="filter">tìm kiếm nhập trong input</param>
        /// <param name="filterSuite">tìm kiếm dựa vào bộ lọc</param>
        /// <returns></returns>
        public object GetPaging(int pageIndex, int pageSize, string? filter = "",string? filterSuite = "")
        {
            try
            {
                //kiểm tra xem điều kiện ở bộ lọc có giá trị ko
                if (String.IsNullOrEmpty(filterSuite))
                {
                    filterSuite = "1=1";
                }
                //tính toán lấy từ bản ghi bao nhiêu (VD: trang 1, 10 bản ghi thì lấy từ bản ghi 0)
                pageIndex = (pageIndex - 1) * pageSize;
                //câu lệnh truy vấn
                var sqlFilter = @$"SELECT  c.CommendationLevelName,e.EmulationID,e.EmulationCode,e.IsSystem,e.EmulationName,e.ApplyObject,e.CommendationLevelID,e.MovementType,e.Status,e.Note" +
                    $" FROM emulation e INNER JOIN commendationlevel c ON e.CommendationLevelID = c.CommendationLevelID" +
                    $" WHERE ((@filter IS NULL) OR (e.EmulationName LIKE CONCAT('%',@filter,'%') OR e.EmulationCode LIKE CONCAT('%',@filter,'%'))) AND ({filterSuite})" +
                    $" ORDER BY e.Status DESC,e.CreatedDate DESC " +
                    $"LIMIT @m_pageIndex,@m_pageSize";

                //Các biến dùng trong câu lệnh sql
                var parametes = new DynamicParameters();
                //vị trí trang
                parametes.Add("@m_pageIndex", pageIndex);
                //số bản ghi trên trang
                parametes.Add("@m_pageSize", pageSize);
                //tìm kiếm từ input
                parametes.Add("@filter", filter);
                ////điều kiện lọc từ bộ lọc
                //parametes.Add("@filterSuite", filterSuite);
                //câu lệnh truy vấn lấy ra tổng số bản ghi
                var sqlTotal = $"SELECT COUNT(e.EmulationID) FROM emulation e WHERE  ((@filter IS NULL) OR e.EmulationName LIKE CONCAT('%',@filter,'%') OR e.EmulationCode LIKE CONCAT('%',@filter,'%')) AND ((@filterSuite IS NULL) OR ({filterSuite}) )";
                var parametesTotal = new DynamicParameters();
                parametesTotal.Add("@filter", filter);
                parametesTotal.Add("@filterSuite", filterSuite);
                //khởi tạo kết nối
                //createby: DPQuy (6/7/2022)
                using (MySqlConnection = new MySqlConnection(ConnectionString))
                {
                    //thực hiện truy vấn lấy ra danh sách các danh hiệu
                    List<Emulation> data = (List<Emulation>)MySqlConnection.Query<Emulation>(sqlFilter, param: parametes);
                    //Lấy ra tổng số bản ghi
                    int TotalRecord = MySqlConnection.QueryFirst<int>(sqlTotal, parametesTotal);
                    //tính số trang dựa theo tổng số bản ghi và số bản ghi trên 1 trang
                    int TotalPage = 0;
                    if (TotalRecord / pageSize == 0)
                    {
                        TotalPage = TotalRecord / pageSize;
                    }
                    else
                    {
                        TotalPage = (TotalRecord / pageSize) + 1;
                    }
                    return new
                    {
                        data,
                        TotalRecord,
                        TotalPage,
                    };
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        /// <summary>
        /// lấy mã danhh hiệu mới
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <returns></returns>
        public Emulation GetNewEmulationCode()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT e.EmulationCode FROM emulation e ORDER BY e.CreatedDate DESC LIMIT 1";
                string emulationCode = MySqlConnection.QueryFirst<string>(sql: sql);
                //string emulationCode = "001";
                string res = "";
                //trường hợp khi mà database chưa có 1 danh hiệu nào
                if (emulationCode.Length == 0)
                {
                    res = "EC-0011";
                }
                else
                {
                    //kiểm tra xem chuỗi có phải chuỗi số ko
                    if (int.TryParse(emulationCode, out int isNumberic) == true)
                    {
                        isNumberic += 1;
                        res = isNumberic.ToString().PadLeft(emulationCode.Length, '0');
                    }
                    //chuỗi ko phải chuỗi số thì thực hiện tách chuỗi
                    else {
                        int index = emulationCode.Length - 1;

                        ////lấy ra vị trí các số liên tiếp từ cuối vd chuỗi là abc001 thì index = 2
                        while (index >= 0)
                        {
                            //kiểm tra xem kí tự có phải là số ko
                            //nếu ko phải số thì break lấy ra index 
                            if (char.IsDigit(emulationCode[index]) == false)
                            {
                                index++;
                                break;
                            }
                            index--;
                        }

                        //nếu chuỗi ko có dãy số liên tiếp ở cuối thì thêm số 1 vào cuối chuỗi
                        //(chữ cái ở cuối chuỗi)
                        if (index == emulationCode.Length)
                        {
                            res = emulationCode + "1";
                        }
                        else
                        {
                            //thực hiện tách chuỗi thành string và chuỗi liên tiếp, ví dụ chuỗi = abc001 => first = abc, last= 001;
                            //lấy ra chuỗi đầu
                            string first = emulationCode.Substring(0, index);
                            //tách ra các số cuối chuỗi để lấy length
                            string last = emulationCode.Substring(index);
                            //lấy ra length để format vd lấy ra số 001 thì format thành 002
                            int lenght = last.Length;
                            //tách ra các số cuối chuỗi và +1 đơn vị
                            int code = (int.Parse(emulationCode.Substring(index)) + 1);

                            //trả về mã mới
                            res = first + code.ToString().PadLeft(lenght, '0');
                        }
                    }

                }

                var result = new Emulation();
                result.EmulationCode = res;
                //trả về mã mới
                return result;

            }
        }

        /// <summary>
        /// check mã trùng khi update 
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emulationCode"></param>
        /// <returns></returns>
        public bool CheckEmployeeCodeUpdateExits(Guid id,string emulationCode)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"SELECT e.EmulationCode FROM emulation e WHERE e.EmulationCode NOT IN(SELECT e.EmulationCode FROM emulation e WHERE e.EmulationID = @EmulationID) AND e.EmulationCode=@EmulationCode;";
                var parametes = new DynamicParameters();
                parametes.Add("@EmulationID", id);
                parametes.Add("@EmulationCode", emulationCode);
                var res = MySqlConnection.QueryFirstOrDefault(sql: sql, param: parametes);
                if (res == null)
                    return false;
                return true;
            }
        }


        /// <summary>
        /// Xóa nhiều bản ghi
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public int DeleteMultiple(string listID)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"DELETE FROM emulation e WHERE FIND_IN_SET(e.EmulationID,'{listID}');";
                var res = MySqlConnection.Execute(sql);
                return res;
            }
        }

        /// <summary>
        /// Cập nhật trạng thái cho danh hiệu thành trạng thái đang sử dụng
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int UpdateActiveStatus(string listID)
        {
            return UpdateStatusCommon(listID, (int)Status.Active);
        }


        /// <summary>
        /// Hàm chung cho cập nhật trạng thái danh hiệu
        /// mode = 0, cập nhật các danh hiệu thành ngưng sử dụng
        /// mode = 1, cập nhật các danh hiệu thành sử dụng
        /// created by: DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private int UpdateStatusCommon(string listID,int mode)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                string[] arrID = listID.Split(',');

                string sql = "";
                try
                {

                    //có 2 id trong chuỗi
                    if (arrID.Length > 1)
                    {
                        //nối thành câu lệnh truy vấn (update .... ,update)
                        for (int i = 0; i < arrID.Length; i++)
                        {
                            sql += $"UPDATE emulation e SET e.Status = {mode}  WHERE EmulationID = '{arrID[i]}';";
                        }


                        //xóa dấu phẩy cuối ở chuỗi truy vấn
                        sql = sql.Substring(0, sql.Length - 1);
                    }
                    else
                    {
                        sql = $"UPDATE emulation e SET e.Status = {mode} WHERE EmulationID = '{listID}'";
                    }

                    var res = MySqlConnection.Execute(sql);
                    return res;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }



        /// <summary>
        /// Cập nhật trạng thái cho danh hiệu thành trạng thái ngừng sử dụng
        /// created by:DPQuy (13/11/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int UpdateInActiveStatus(string listID)
        {
            return UpdateStatusCommon(listID, (int)Status.Inactive);
        }
        #endregion
    }
}
