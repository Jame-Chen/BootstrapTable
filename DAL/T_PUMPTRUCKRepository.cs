using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Common;

namespace DAL
{
    public partial class T_PUMPTRUCKRepository : BaseRepository<T_PUMPTRUCK>, IDisposable
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns>集合</returns>
        public IQueryable<T_PUMPTRUCK> GetAll()
        {
            using (Entities db = new Entities())
            {
                return GetAll(db);
            }
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns>集合</returns>
        public IQueryable<T_PUMPTRUCK> GetAll(Entities db)
        {
            return db.Set<T_PUMPTRUCK>().AsQueryable();
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entity">将要创建的对象</param>
        public void Create(Entities db, T_PUMPTRUCK entity)
        {
            if (entity != null)
            {
                db.Set<T_PUMPTRUCK>().Add(entity);
            }
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity">一个对象</param>
        /// <returns></returns>
        public int Create(T_PUMPTRUCK entity)
        {
            using (Entities db = new Entities())
            {
                Create(db, entity);
                return this.Save(db);
            }
        }
        /// <summary>
        /// 创建对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entitys">对象集合</param>
        public void Create(Entities db, IQueryable<T_PUMPTRUCK> entitys)
        {
            foreach (var entity in entitys)
            {
                this.Create(db, entity);
            }
        }
        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entity">将要编辑的一个对象</param>
        /// <param name="isAttach">是否附加到数据库上下文</param>
        public T_PUMPTRUCK Edit(Entities db, T_PUMPTRUCK entity, bool isAttach = true)
        {
            if (isAttach)
                db.Set<T_PUMPTRUCK>().Attach(entity);

            db.Entry(entity).State = System.Data.EntityState.Modified;
            return entity;
        }
        /// <summary>
        /// 编辑对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entitys">对象集合</param>
        public void Edit(Entities db, IQueryable<T_PUMPTRUCK> entitys)
        {
            foreach (T_PUMPTRUCK entity in entitys)
            {
                this.Edit(db, entity);
            }
        }
        /// <summary>
        /// 提交保存，持久化到数据库
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <returns>受影响行数</returns>
        public int Save(Entities db)
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
