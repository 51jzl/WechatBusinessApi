using System.Collections.Generic;
using System.Linq;

namespace WB.BusinessClasses
{
    /// <summary>
    /// 代理等级业务
    /// </summary>
    public class ProxyLevelService
    {
        /// <summary>
        /// 根据项目id获取所有代理
        /// </summary>
        /// <returns></returns>
        public IEnumerable<bs_ProxyLevel> Get(long projectid)
        {
            return bs_ProxyLevel.Query("WHERE ProjectId=@0", projectid).OrderBy(n => n.DisplayOrder);
        }

        /// <summary>
        /// 创建代理级别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Create(bs_ProxyLevel entity)
        {
            try
            {
                object id = entity.Insert();
                long intid = 0;
                if (id != null)
                {
                    if (long.TryParse(id.ToString(), out intid))
                        return true;
                }
                return false;
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// 修改代理级别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(bs_ProxyLevel entity)
        {
            try
            {
                object id = entity.Update(new string[] { "ParentId", "Name", "DisplayOrder", "PurchaseAmount", "ReplenishmentAmount" });
                long intid = 0;
                if (id != null)
                {
                    if (long.TryParse(id.ToString(), out intid) && intid > 0)
                        return true;
                }
                return false;
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// 删除代理级别
        /// </summary>
        /// <param name="plid"></param>
        /// <returns></returns>
        public bool Delete(long plid)
        {
            try
            {
                object id = bs_ProxyLevel.Delete(plid);
                long intid = 0;
                if (id != null)
                {
                    if (long.TryParse(id.ToString(), out intid) && intid > 0)
                        return true;
                }
                return false;
            }
            catch
            {
            }
            return false;
        }
    }
}
