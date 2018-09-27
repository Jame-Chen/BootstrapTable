using BLL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Web.Caching;
using System.Web.UI;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.IO;

namespace PSDDProject.Controllers
{
    public class BCGLController : Controller
    {


        //
        // GET: /BCGL/
        BcglBLL bcglBll = new BcglBLL();


        public ActionResult Index(string mark)
        {



            return View();
        }

        public JsonResult GetBCGLList(string S_QX, string S_PZ, string S_PT_ID, string QXZT)
        {
            var pageNum = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            var pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            var total = 0;
            try
            {
                var rows = bcglBll.GetBCGLList(S_QX, S_PZ, S_PT_ID, QXZT, pageNum, pageSize, out total);

                var data = new { total = total, rows = rows };
                return Json(data);
            }
            catch (Exception e)
            {
                //  LogHelper.Error("Control执行Select_ChuZhiInfo_List方法:" + e.Message);
                throw;
            }
        }



        public ActionResult ADD()
        {


            return View();
        }

        [HttpPost]
        public ActionResult ADD(T_PUMPTRUCK t_pumptruck)
        {
            try
            {
                t_pumptruck.S_PT_ID = Guid.NewGuid().ToString("N");
                bcglBll.Add(t_pumptruck);
                return Json(new { code = "1", msg = "新增成功!" });
            }
            catch (Exception e)
            {

                return Json(new { code = "0", msg = e.Message });
            }

        }




        public ActionResult Edt()
        {
            string S_PT_ID = Request.Params["S_PT_ID"] == null ? "" : Request.Params["S_PT_ID"];

            T_PUMPTRUCK t_pumptruck = bcglBll.GetLIstT_PUMPTRUCK(S_PT_ID);

            return View(t_pumptruck);
        }


        [HttpPost]
        public async Task<ActionResult> Edt(T_PUMPTRUCK t_pumptruck)
        {
            try
            {
                bcglBll.Edt(t_pumptruck);

                return Json(new { code = "1", msg = "修改成功!" });
            }
            catch (DbUpdateConcurrencyException e)
            {
                var entry = e.Entries.Single();
                 T_PUMPTRUCK tp=   (T_PUMPTRUCK)entry.Entity;
                 var databaseEntry = entry.GetDatabaseValues();
                 if (databaseEntry == null)
                 {
                     return Json(new { code = "0", msg = "该记录已被其他人删除!" });
                 }
                 else {
                     string ret = "";
                     var databaseValues = (T_PUMPTRUCK)databaseEntry.ToObject();
                     if (databaseValues.S_SPBH!=tp.S_SPBH)
                     {
                         ret+="S_SPBH,当前值：" + databaseValues.S_SPBH+"。";
                     }
                     if (databaseValues.S_PT_NAME != tp.S_PT_NAME)
                     {
                         ret += "S_PT_NAME,当前值：" + databaseValues.S_PT_NAME + "。";
                     }
                     if (databaseValues.S_PZ != tp.S_PZ)
                     {
                         ret += "S_PZ,当前值：" + databaseValues.S_PZ + "。";
                     }
                     if (databaseValues.S_PT_FLOW != tp.S_PT_FLOW)
                     {
                         ret += "S_PT_FLOW,当前值：" + databaseValues.S_PT_FLOW + "。";
                     }
                     if (databaseValues.S_PT_LIFT != tp.S_PT_LIFT)
                     {
                         ret += "S_PT_LIFT,当前值：" + databaseValues.S_PT_LIFT + "。";
                    }
                     if (databaseValues.N_PT_CLASS != tp.N_PT_CLASS)
                     {
                         ret += "N_PT_CLASS,当前值：" + databaseValues.N_PT_CLASS + "。";
                     }
                     if (databaseValues.S_ADD != tp.S_ADD)
                     {
                         ret += "S_ADD,当前值：" + databaseValues.S_ADD + "。";
                    }
                     if (databaseValues.S_PT_AREA != tp.S_PT_AREA)
                     {
                         ret += "S_PT_AREA,当前值：" + databaseValues.S_PT_AREA + "。";
                     }
                     if (databaseValues.S_SYSCOMMANDOID != tp.S_SYSCOMMANDOID)
                     {
                         ret += "S_SYSCOMMANDOID,当前值：" + databaseValues.S_SYSCOMMANDOID + "。";
                    }
                     if (databaseValues.S_PT_CONTACTS != tp.S_PT_CONTACTS)
                     {
                         ret += "S_PT_CONTACTS,当前值：" + databaseValues.S_PT_CONTACTS + "。";
                     }
                     if (databaseValues.S_PT_CONTACTPHONE != tp.S_PT_CONTACTPHONE)
                     {
                         ret += "S_PT_CONTACTPHONE,当前值：" + databaseValues.S_PT_CONTACTPHONE + "。";
                    }
                     if (databaseValues.S_BZ != tp.S_BZ)
                     {
                         ret += "S_BZ,当前值：" + databaseValues.S_BZ + "。";
                     }
                     if (databaseValues.S_QX != tp.S_QX)
                     {
                         ret += "S_QX,当前值：" + databaseValues.S_QX + "。";
                     }
                     tp.RowVersion = databaseValues.RowVersion;
                     return Json(new { code = "0", msg = ret });
                 }
               
            }

            
        }

        public ActionResult Del(string iditems)
        {
            try
            {
                    iditems = iditems.Replace("[", "").Replace("]", "").Replace("\"", "");
                bcglBll.Del(iditems);
                return Json(new { code = "1", msg = "删除成功!" });

            }
            catch (Exception e)
            {

                return Json(new { code = "0", msg = e.Message });
            }

           
        }




        public ActionResult Detail(string S_PT_ID)
        {
            T_PUMPTRUCK t_pumptruck = bcglBll.GetLIstT_PUMPTRUCK(S_PT_ID);
            return View(t_pumptruck);
        }

        /// <summary>
        /// 批量导入到oracle
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult UploadMB(HttpPostedFileBase file)
        {
            string err = "";
            OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(qpssyh);
            if (file != null)
            {
                try
                {
                    DataTable dt = NPOIHelper.ImportExceltoDt(file.InputStream, "Sheet1", 0);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string sql = "select count(1) tabcnt  from user_tables where table_name = upper('xcrw_batch') ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataReader dr = cmd.ExecuteReader();
                    int tabcnt = 0;
                    if (dr.Read())
                    {
                        tabcnt = Convert.ToInt32(dr["tabcnt"]);
                    }
                    dr.Close();
                    if (tabcnt > 0)
                    {
                        sql = "drop table xcrw_batch";
                        cmd = null;
                        cmd = new OracleCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    sql = @" create table xcrw_batch(
                                       任务名称 NVARCHAR2(100),
                                      来源   NVARCHAR2(100),
                                      大类   NVARCHAR2(100),
                                      小类   NVARCHAR2(100),
                                      紧急程度 NVARCHAR2(100)
                                      )";
                    cmd = null;
                    cmd = new OracleCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    OracleBulkCopy bulkCopy = new Oracle.DataAccess.Client.OracleBulkCopy(conn, Oracle.DataAccess.Client.OracleBulkCopyOptions.Default);
                    bulkCopy.BatchSize = 100000;
                    bulkCopy.BulkCopyTimeout = 260;
                    //targetTable目标表名
                    bulkCopy.DestinationTableName = "XCRW_BATCH";
                    if (dt.Columns.Count > 0)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i > 4)
                            {
                                continue;
                            }
                            if (dt.Columns[0].ColumnName != "任务名称")
                            {
                                err += "第一列列名应为任务名称";
                            }
                            if (dt.Columns[1].ColumnName != "来源")
                            {
                                err += "第二列列名应为来源";
                            }
                            if (dt.Columns[2].ColumnName != "大类")
                            {
                                err += "第三列列名应为大类";
                            }
                            if (dt.Columns[3].ColumnName != "小类")
                            {
                                err += "第四列列名应为小类";
                            }
                            if (dt.Columns[4].ColumnName != "紧急程度")
                            {
                                err += "第五列列名应为紧急程度";
                            }
                            bulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);//源列名->目标列名
                        }
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        bulkCopy.WriteToServer(dt);
                        bulkCopy.Dispose();
                    }
                    sql = "insert into T_PATROL_TASK(S_NAME,S_SOURCE,S_CATEGORY,S_TYPE,S_EMERGENCY) select 任务名称,来源,大类,小类,紧急程度 from  xcrw_batch";
                    cmd = null;
                    cmd = new OracleCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    sql = "drop table xcrw_batch";
                    cmd = null;
                    cmd = new OracleCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    return Json(new Result { code = "200", msg = "导入成功!" });
                }
                catch (Exception e)
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    return Json(new Result { code = "500", msg = e.Message + "," + err });
                }
            }
            else
            {
                return Json(new Result { code = "404", msg = "请选择文件!" });
            }

        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="townname"></param>
        /// <returns></returns>
        public ActionResult UploadReport(HttpPostedFileBase file, string townname)
        {
            if (file == null)
            {
                return Json(new Result() { code = "404", msg = "请选择文件!" });
            }
            try
            {
                var uuidN = Guid.NewGuid().ToString("N");
                string aFile = file.FileName;
                string aFirstName = aFile.Substring(aFile.LastIndexOf("\\") + 1, (aFile.LastIndexOf(".") - aFile.LastIndexOf("\\") - 1)); //文件名
                string aLastName = aFile.Substring(aFile.LastIndexOf(".") + 1, (aFile.Length - aFile.LastIndexOf(".") - 1)); //扩展名
                if (file.ContentLength > 4 * 1024 * 1024)
                {
                    return Json(new Result() { code = "500", msg = "文件应小于4M!" });
                }
                string filesize = file.ContentLength.ToString();
                if (aLastName.ToLower() != "doc" && aLastName.ToLower() != "docx")
                {
                    return Json(new Result() { code = "500", msg = "请上传word文件!" });
                }
                string day = DateTime.Now.ToString("yyyy-MM-dd");
                if (!Directory.Exists(Server.MapPath("~/JCfile/" + day + "/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/JCfile/" + day + "/"));
                }

                //保存文件到根目录 App_Data + 获取文件名称和格式
                var filePath = Server.MapPath("~/JCfile/" + day + "/") + uuidN + "." + aLastName;// Path.GetFileName(file.FileName);
                //创建一个追加（FileMode.Append）方式的文件流
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        //读取文件流
                        BinaryReader br = new BinaryReader(file.InputStream);
                        //将文件留转成字节数组
                        byte[] bytes = br.ReadBytes((int)file.InputStream.Length);
                        //将字节数组追加到文件
                        bw.Write(bytes);
                    }
                }
                var obj = new { ID = uuidN, FILENAME = aFile, FILESIZE = filesize, SYSNAME = uuidN + "." + aLastName, TOWNNAME = townname, UPLOADTIME = DateTime.Now };
                string str = JsonConvert.SerializeObject(obj);
                CommController comm = new CommController();
                comm.Method("http://172.18.0.21:9763/odata/PSSSYH/default/JCCJFILE", str, "post");
            }
            catch (Exception e)
            {
                return Json(new Result() { code = "500", msg = e.Message });
            }
            return Json(new Result() { code = "200", msg = "上传成功!" });
        }
    }
}
