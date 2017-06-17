using LEON.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEON.BusinessClasses
{

    /// <summary>
    /// 用户账户业务逻辑
    /// </summary>
    public class MembershipService
    {
        public UserRepository userRepository { get; set; }



        /// <summary>
        /// 验证提供的用户名和密码是否匹配
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回<see cref="UserLoginStatus"/></returns>
        public UserLoginStatus ValidateUser(string username, string password)
        {
            long userId = UserIdToUserNameDictionary.GetUserId(username);

            User user = userRepository.Get(userId);
            if (user == null)
                return UserLoginStatus.InvalidCredentials;

            if (!UserPasswordHelper.CheckPassword(password, user.Password, (UserPasswordFormat)user.PasswordFormat))
                return UserLoginStatus.InvalidCredentials;

            return UserLoginStatus.Success;
        }
    }
}
