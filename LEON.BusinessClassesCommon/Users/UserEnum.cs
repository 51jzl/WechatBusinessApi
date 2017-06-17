using System.ComponentModel.DataAnnotations;

namespace LEON.BusinessClasses
{
    /// <summary>
    /// 用户密码存储格式
    /// </summary>
    public enum UserPasswordFormat
    {
        /// <summary>
        /// 密码未加密
        /// </summary>
        [Display(Name = "不加密")]
        Clear = 0,

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        [Display(Name = "MD5加密")]
        MD5 = 1,
    }


    /// <summary>    
    /// 用于创建用户账号时的返回值
    /// </summary>
    public enum UserCreateStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownFailure = 0,

        /// <summary>
        /// 创建成功
        /// </summary>
        Created = 1,

        /// <summary>
        /// 用户名重复
        /// </summary>
        DuplicateUsername = 2,

        /// <summary>
        /// Email重复
        /// </summary>
        DuplicateEmailAddress = 3,

        /// <summary>
        /// 手机号重复
        /// </summary>
        DuplicateMobile = 4,


        /// <summary>
        /// 不允许的用户名
        /// </summary>
        DisallowedUsername = 5,

        ///// <summary>
        ///// 更新成功
        ///// </summary>
        //Updated = 6,

        /// <summary>
        /// 不合法的密码提示问题/答案
        /// </summary>
        InvalidQuestionAnswer = 7,

        /// <summary>
        /// 不合法的密码
        /// </summary>
        InvalidPassword = 8
    }


    /// <summary>
    /// 用户登录状态
    /// </summary>
    public enum UserLoginStatus
    {
        /// <summary>
        /// 通过身份验证，登录成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 用户名、密码不匹配
        /// </summary>
        InvalidCredentials = 1,

        /// <summary>
        /// 帐户未激活
        /// </summary>
        NotActivated = 2,

        /// <summary>
        /// 帐户被封禁
        /// </summary>
        Banned = 3,

        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 100
    }
}
