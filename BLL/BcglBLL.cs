using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
//using Moresoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
//using BLL.ResultType;
//using Common.SerializerClass;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Data.Entity.Validation;

namespace BLL
{
    public class BcglBLL
    {
        protected Entities db = new Entities();

        public object GetBCGLList(string S_QX, string S_PZ, string S_PT_ID, string QXZT, int pageNum, int pageSize, out int count)
        {
            count = 0;
          //  decimal deQXZT = Convert.ToDecimal(QXZT);
            try
            {
                //左连接，没有绑定突击队的显示空。
                var query = from a in db.T_PUMPTRUCK
                           
                            select a;

                if (!(string.IsNullOrWhiteSpace(S_QX) || S_QX == "--请选择--"))
                {
                    query = query.Where(p => p.S_QX == S_QX);
                }
                if (!string.IsNullOrWhiteSpace(S_PZ))//模糊查询
                {
                    query = query.Where(p => p.S_PZ.IndexOf(S_PZ) >= 0);
                }
                if (!string.IsNullOrWhiteSpace(S_PT_ID))//模糊查询
                {
                    query = query.Where(p => p.S_PT_ID.IndexOf(S_PT_ID) >= 0);
                }
                if (!(string.IsNullOrWhiteSpace(QXZT) || QXZT == "0"))
                {
                    query = query.Where(p => p.QXZT == QXZT);
                }
                count = query.Count();

                query = query.OrderByDescending(o => o.S_PT_ID).Skip(pageSize * (pageNum - 1)).Take(pageSize);
                return query;

            }
            catch (Exception e)
            {
                //LogHelper.Error("BLL执行GetBCGLList方法:" + e.Message);
                return null;
            }

        }


        //public object GetT_CAMERA() {
        //    try
        //    {
        //        var query=from a in db.T_
        //    }
        //    catch (Exception e)
        //    {

        //        LogHelper.Error("BLL执行GetT_CAMERA方法:" + e.Message);
        //        return null;
        //    }
        //}

        //public List<T_MANAGEUNIT> GetT_MANAGEUNIT()
        //{
        //    List<T_MANAGEUNIT> list = new List<T_MANAGEUNIT>();
        //    T_MANAGEUNIT item = new T_MANAGEUNIT();
        //    item.S_ID = "--请选择--";
        //    item.S_PT_DEPARTMENT = "--请选择--";
        //    list.Add(item);
        //    try
        //    {

        //        var query = from a in db.T_MANAGEUNIT
        //                    select a;
        //        list.AddRange(query.ToList());
        //        return list;
        //    }
        //    catch (Exception e)
        //    {

        //       // LogHelper.Error("BLL执行GetT_MANAGEUNIT方法:" + e.Message);
        //        return list;
        //    }
        //}

        public List<T_PUMPTRUCK> GetT_PUMPTRUCK()
        {
            List<T_PUMPTRUCK> list = new List<T_PUMPTRUCK>();
            T_PUMPTRUCK item = new T_PUMPTRUCK();
            item.S_PT_ID = "";
            item.S_PT_NAME = "--请选择--";
            list.Add(item);
            try
            {

                var query = from a in db.T_PUMPTRUCK
                            select a;
                list.AddRange(query.ToList());
                return list;
            }
            catch (Exception e)
            {

              //  LogHelper.Error("BLL执行GetT_PUMPTRUCK方法:" + e.Message);
                return list;
            }
        }


        public bool Add(T_PUMPTRUCK t_pumptruck)
        {
            try
            {
                db.T_PUMPTRUCK.Add(t_pumptruck);
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
             //   LogHelper.Error("BLL执行Add方法:" + e.Message);
            }
            return false;
        }


        public T_PUMPTRUCK GetLIstT_PUMPTRUCK(string S_PT_ID)
        {

            try
            {
                var query = from a in db.T_PUMPTRUCK
                            where a.S_PT_ID == S_PT_ID
                            select a;

                T_PUMPTRUCK t_pumptruck = query.ToList().FirstOrDefault();
                return t_pumptruck;
            }
            catch (System.Exception e)
            {

                //   LogHelper.Error("BLL执行GetLIstT_PUMPTRUCK方法:" + e.Message);
                return null;
            }
        }


        //public SYSCOMMANDO GetSYSCOMMANDO(string id)
        //{

        //    SYSCOMMANDO SYSCOMMANDO = new SYSCOMMANDO();

        //    try
        //    {
        //        var query = from a in db.SYSCOMMANDO
        //                    where a.ID == id
        //                    select a;
        //        SYSCOMMANDO syscommando = query.ToList().FirstOrDefault();
        //        return syscommando;
        //    }
        //    catch (System.Exception e)
        //    {

        //     //   LogHelper.Error("BLL执行GetSYSCOMMANDO方法:" + e.Message);
        //        return null;
        //    }
        //}


   

        public bool Edt(T_PUMPTRUCK model)
        {
            try
            {
                db.T_PUMPTRUCK.Attach(model);
                db.Entry<T_PUMPTRUCK>(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }

            return false;

        }

        public void Del(string iditems)
        {
            try
            {

                string[] ss = iditems.Split(',');
                foreach (string item in ss)
                {

                    T_PUMPTRUCK model = (T_PUMPTRUCK)db.T_PUMPTRUCK.Where(w => w.S_PT_ID == item).FirstOrDefault();
                    db.T_PUMPTRUCK.Attach(model);
                    db.Entry<T_PUMPTRUCK>(model).State = System.Data.EntityState.Deleted;
                }
                db.SaveChanges();
            }
            catch (System.Exception e)
            {

             //   LogHelper.Error("BLL执行Del方法:" + e.Message);
                throw;
            }
        }


        //public void Audit(string iditems, string userName)
        //{
        //    try
        //    {

        //        string[] ss = iditems.Split(',');
        //        foreach (string item in ss)
        //        {

        //            T_PUMPTRUCK model = (T_PUMPTRUCK)db.T_PUMPTRUCK.Where(w => w.S_PT_ID == item).FirstOrDefault();
        //            model.N_ISSH = 1;
        //            model.S_SHR = userName;
        //            model.T_SHSJ = DateTime.Now;
        //            db.T_PUMPTRUCK.Attach(model);
        //            db.Entry<T_PUMPTRUCK>(model).State = System.Data.EntityState.Modified;
        //        }
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //      //  LogHelper.Error("BLL执行Audit方法:" + e.Message);
        //        throw;
        //    }
        //}


        //public List<T_CAMERA> GetCAMERA()
        //{
        //    List<T_CAMERA> list = new List<T_CAMERA>();
        //    try
        //    {

        //        T_CAMERA t_camera = new T_CAMERA();
        //        t_camera.S_ID = "";
        //        t_camera.S_SBBH = "--请选择--";
        //        list.Add(t_camera);
        //        var query = from a in db.T_CAMERA
        //                    select a;
        //        list.AddRange(query.ToList());
        //        return list;

        //    }
        //    catch (System.Exception e)
        //    {
        //      //  LogHelper.Error("BLL执行GetCAMERA方法:" + e.Message);
        //        return list;
        //    }
        //}


        //public bool CheckS_PT_ID(string S_PT_ID, string S_PT_ID2)
        //{
        //    bool flag = true;
        //    try
        //    {
        //        int cnt = 0;
        //        if (!string.IsNullOrEmpty(S_PT_ID2) && S_PT_ID2 != "undefined")
        //        { cnt = db.T_PUMPTRUCK.Where(s => s.S_PT_ID == S_PT_ID).Where(s => s.S_PT_ID != S_PT_ID2).Count(); }
        //        else
        //        {
        //            cnt = db.T_PUMPTRUCK.Where(s => s.S_PT_ID == S_PT_ID).Count();
        //        }

        //        if (cnt == 0)
        //        {
        //            flag = false;
        //        }
        //        return flag;
        //    }
        //    catch (System.Exception e)
        //    {

        //      //  LogHelper.Error("BLL执行CheckS_PT_ID方法:" + e.Message);
        //        return flag;
        //    }

        //}

        //public bool CheckS_SBBH(string S_PT_ID2, string S_SPBH)
        //{
        //    bool flag = true;
        //    try
        //    {
        //        int cnt = 0;
        //        if (!string.IsNullOrEmpty(S_PT_ID2) && S_PT_ID2 != "undefined")
        //        {
        //            cnt = db.T_PUMPTRUCK.Where(s => s.S_SPBH == S_SPBH).Where(s => s.S_PT_ID != S_PT_ID2).Count();
        //        }
        //        else
        //        {
        //            cnt = db.T_PUMPTRUCK.Where(s => s.S_SPBH == S_SPBH).Count();
        //        }

        //        if (cnt == 0)
        //        {
        //            flag = false;
        //        }
        //        return flag;
        //    }
        //    catch (System.Exception e)
        //    {

        //     //   LogHelper.Error("BLL执行CheckS_SBBH方法:" + e.Message);
        //        return flag;
        //    }

        //}
    }
}
