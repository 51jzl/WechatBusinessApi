using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEON.BusinessClasses
{
    /// <summary>
    /// 封装后台管理用户时用于查询用户的条件
    /// </summary>
    public class UserQuery
    {
        /// <summary>
        /// 名称（姓名、昵称、用户名）
        /// </summary>
        public string Keyword = string.Empty;

        /// <summary>
        /// 注册时间下限（晚于或等于本时间注册的）
        /// </summary>
        public DateTime? RegisterTimeLowerLimit = null;

        /// <summary>
        /// 注册时间上限（早于或等于本时间注册的）
        /// </summary>
        public DateTime? RegisterTimeUpperLimit = null;

        /// <summary>
        /// 排序方式
        /// </summary>
        public UserSortBy? UserSortBy = null;
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum UserSortBy
    {
        /// <summary>
        /// 根据id排序
        /// </summary>
        UserId = 1,

        /// <summary>
        /// 根据id倒序排列
        /// </summary>
        UserId_Desc = 2,

        /// <summary>
        /// 根据上次活动时间排序
        /// </summary>
        LastActivityTime = 3,

        /// <summary>
        /// 根据上次活动时间倒序
        /// </summary>
        LastActivityTime_Desc = 4
    }
}
