using LEON.Repositories;
using PetaPoco;
using System.Collections.Generic;
using WB.Entity;

namespace WB.BusinessClasses
{
    public class ProjectRepository : Repository<bsProject>
    {

        /// <summary>
        /// 根据条件返回
        /// </summary>
        /// <returns></returns>
        public IEnumerable<bsProject> Query(long userid, string name)
        {
            Sql sql = Sql.Builder;
            sql.Where("UserID=@0", userid);
            if (!string.IsNullOrEmpty(name))
                sql.Where("ID like @0 OR Name like @0 OR Company like @0", string.Format("%{0}%", name));
            return CreateDAO().Query<bsProject>(sql);
        }
    }
}
