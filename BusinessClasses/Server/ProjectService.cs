using System.Collections.Generic;
using WB.Entity;

namespace WB.BusinessClasses
{
    /// <summary>
    /// 项目业务
    /// </summary>
    public class ProjectService
    {
        ProjectRepository repository;

        public ProjectService()
        {
            repository = new ProjectRepository();
        }

        /// <summary>
        /// 根据条件返回
        /// </summary>
        /// <returns></returns>
        public IEnumerable<bsProject> Query(long userid, string name = "")
        {
            return repository.Query(userid, name);
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="entity"></param>
        public bool Create(bsProject entity)
        {
            try
            {
                repository.Insert(entity);
                if (entity.ID > 0)
                    return true;
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(bsProject entity)
        {
            try
            {
                repository.Update(entity);
                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
