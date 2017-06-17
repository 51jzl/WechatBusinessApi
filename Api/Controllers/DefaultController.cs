using System.Web.Http;
using WB.Api.Models;
using WB.Api.Secure;

namespace WB.Api.Controllers
{
    [ApiAuthorize]
    public class DefaultController : ApiController
    {
        public string Get()
        {
            //获取回用户信息(在ApiAuthorize中通过解析token的payload并保存在RouteData中)
            AuthInfo authInfo = this.RequestContext.RouteData.Values["token"] as AuthInfo;
            if (authInfo == null)
                return "无效的验收信息";
            else
                return string.Format("你好:{0},成功取得数据", authInfo.UserName);
        }
    }
}