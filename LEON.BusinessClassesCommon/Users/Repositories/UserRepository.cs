using LEON.Entity;
using LEON.Repositories;
using LEON.Utilities;
using PetaPoco;

namespace LEON.BusinessClasses
{
    /// <summary>
    /// 用户数据访问
    /// </summary>
    public class UserRepository : Repository<User>
    {
        #region Create/Update/Delete

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns>创建成功返回IUser，创建失败返回null</returns>
        public User CreateUser(User user, out UserCreateStatus userCreateStatus)
        {
            userCreateStatus = UserCreateStatus.UnknownFailure;

            //判断用户名是否唯一
            if (GetUserIdByUserName(user.UserName) > 0)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return null;
            }

            if (GetUser(user.UserId) != null)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return null;
            }
            object userId_objcet = base.Insert(user);

            if (userId_objcet != null && (long)userId_objcet > 0)
                userCreateStatus = UserCreateStatus.Created;
            return user;
        }

        /// <summary>
        /// 用户名验证
        /// </summary>
        /// <param name="userName">待创建的用户名</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        public void RegisterValidate(string userName, out UserCreateStatus userCreateStatus)
        {
            userCreateStatus = UserCreateStatus.UnknownFailure;
            //判断用户名是否唯一
            if (GetUserIdByUserName(userName) > 0)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return;
            }
            if (GetUser(GetUserIdByUserName(userName)) != null)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return;
            }
            else
            {
                userCreateStatus = UserCreateStatus.Created;
                return;
            }
        }

        ///	<summary>
        ///	重设密码（无需验证当前密码，供管理员或忘记密码时使用）
        ///	</summary>
        /// <param name="user">用户</param>
        ///	<param name="newPassword">新密码</param>
        ///	<returns>更新成功返回true，否则返回false</returns>
        public bool ResetPassword(User user, string newPassword)
        {
            if (user == null)
                return false;
            var sql_update = PetaPoco.Sql.Builder;
            sql_update.Append("update tn_Users set Password = @0 where UserId = @1", newPassword, user.UserId);
            int affectCount = CreateDAO().Execute(sql_update);
            if (affectCount > 0)
            {
                user.Password = newPassword;
                base.OnUpdated(user);
                return true;
            }
            return false;
        }

        #endregion


        #region Get && Gets

        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户Id</returns>
        public long GetUserIdByUserName(string userName)
        {
            var sql_Select = Sql.Builder.Select("UserId").From("tn_Users").Where("UserName = @0", userName);
            return CreateDAO().FirstOrDefault<long>(sql_Select);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>        
        public User GetUser(long userId)
        {
            User user = base.Get(userId);
            return user;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userQuery">查询用户条件</param>
        /// <param name="pageSize">页面显示条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>用户分页集合</returns>
        public PagingDataSet<User> GetUsers(UserQuery userQuery, int pageSize, int pageIndex)
        {
            var sql_select = PetaPoco.Sql.Builder;
            sql_select.Select("*").From("tn_Users");

            buildSqlWhere(userQuery, ref sql_select);

            switch (userQuery.UserSortBy)
            {
                case UserSortBy.UserId:
                    sql_select.OrderBy("UserId");
                    break;
                case UserSortBy.UserId_Desc:
                    sql_select.OrderBy("UserId DESC");
                    break;
                case UserSortBy.LastActivityTime:
                    sql_select.OrderBy("LastActivityTime");
                    break;
                case UserSortBy.LastActivityTime_Desc:
                    sql_select.OrderBy("LastActivityTime DESC");
                    break;
                default:
                    sql_select.OrderBy("UserId DESC");
                    break;
            }
            return GetPagingEntities(pageSize, pageIndex, sql_select);
        }

        #endregion


        /// <summary>
        /// 从UserQuery构建PetaPoco.Sql的where条件
        /// </summary>
        /// <param name="userQuery">UserQuery查询条件</param>
        /// <param name="sql">PetaPoco.Sql对象</param>
        private void buildSqlWhere(UserQuery userQuery, ref PetaPoco.Sql sql)
        {
            if (sql == null)
            {
                sql = PetaPoco.Sql.Builder;
            }

            if (!string.IsNullOrEmpty(userQuery.Keyword))
                sql.Where("UserName like @0", "%" + StringUtility.StripSQLInjection(userQuery.Keyword) + "%");
            if (userQuery.RegisterTimeLowerLimit.HasValue)
                sql.Where("DateCreated >= @0", userQuery.RegisterTimeLowerLimit.Value.ToUniversalTime());
            if (userQuery.RegisterTimeUpperLimit.HasValue)
                sql.Where("DateCreated <= @0", userQuery.RegisterTimeUpperLimit.Value.AddDays(1).ToUniversalTime());
        }
    }
}
