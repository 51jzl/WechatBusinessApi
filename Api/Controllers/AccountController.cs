using WB.Api.Models;
using LEON.BusinessClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Http;
using LEON;

namespace WB.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : ApiController
    {
        public MembershipService membershipService = DIContainer.Resolve<MembershipService>();

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/account/Login")]
        public LoginResult Login([FromBody]LoginRequest request)
        {
            LoginResult rs = new LoginResult();
            //使用用户名尝试登录
            UserLoginStatus userLoginStatus = membershipService.ValidateUser(request.UserName, request.Password);

            if (userLoginStatus == UserLoginStatus.Success)
            {
                try
                {
                    long userid = new UserRepository().GetUserIdByUserName(request.UserName);
                    AuthInfo authInfo = new AuthInfo
                    {
                        IsAdmin = false,
                        Roles = new List<string> { "test", "test" },
                        UserName = request.UserName,
                        UserId = userid
                    };
                    //生成token,SecureKey是配置的web.config中，用于加密token的key，打死也不能告诉别人
                    byte[] key = Encoding.Default.GetBytes(ConfigurationManager.AppSettings["SecureKey"]);
                    //采用HS256加密算法
                    string token = JWT.JsonWebToken.Encode(authInfo, key, JWT.JwtHashAlgorithm.HS256);
                    rs.Token = token;
                    rs.State = MessageState.success;
                    rs.Message = "登录成功";
                }
                catch (Exception ex)
                {
                    rs.State = MessageState.abnormality;
                    rs.Message = "发生异常,请联系管理员";
                }
            }
            else
            {
                rs.State = MessageState.fail;
                rs.Message = "用户名或密码不正确";
            }
            return rs;
        }
    }
}