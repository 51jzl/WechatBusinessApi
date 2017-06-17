using LEON.Entity;
using System;
using System.Collections.Generic;

namespace LEON.BusinessClasses
{
    /// <summary>
    /// 用户业务逻辑
    /// </summary>
    public class UserService
    {
        private UserRepository userRepository = DIContainer.Resolve<UserRepository>();

        #region Create/Update/Delete

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns>创建成功返回IUser，创建失败返回null</returns>
        public User CreateUser(User user, out UserCreateStatus userCreateStatus)
        {
            user.DateCreated = DateTime.Now;
            user.LastActivityTime = DateTime.MinValue;
            return userRepository.CreateUser(user, out userCreateStatus);
        }

        #endregion

        #region Get & Gets

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userIsOnline">该用户是否在线，更新用户在线状态</param>
        public User GetUser(long userId)
        {
            if (userId <= 0)
                return null;
            return userRepository.GetUser(userId);
        }

        /// <summary>
        /// 依据UserId集合组装IUser集合
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public IEnumerable<User> GetUsers(IEnumerable<long> userIds)
        {
            return userRepository.PopulateEntitiesByEntityIds<long>(userIds);
        }

        #endregion
    }
}
