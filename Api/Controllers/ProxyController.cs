using LEON;
using System.Web.Http;
using WB.Api.Models;
using WB.BusinessClasses;

namespace WB.Api.Controllers
{
    /// <summary>
    /// 代理控制器
    /// </summary>
    public class ProxyLevelController : ApiController
    {
        public ProxyLevelService proxylevelservice = DIContainer.Resolve<ProxyLevelService>();

        #region 代理级别

        /// <summary>
        /// 根据项目id获取所有代理
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        [HttpGet]
        public MessageJSON GetAll(long projectid)
        {
            MessageJSON mj = new MessageJSON(MessageState.success, "");
            mj.Data = proxylevelservice.Get(projectid);
            return mj;
        }

        /// <summary>
        /// 创建代理级别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageJSON Create([FromBody]bs_ProxyLevel entity)
        {
            MessageJSON mj = new MessageJSON(MessageState.fail, "创建代理级别失败", MessageIcon.no);
            if (proxylevelservice.Create(entity))
            {
                mj = new MessageJSON(MessageState.success, "创建代理级别成功", MessageIcon.yes);
            }
            return mj;
        }

        /// <summary>
        /// 更新代理级别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageJSON Update([FromBody]bs_ProxyLevel entity)
        {
            MessageJSON mj = new MessageJSON(MessageState.fail, "修改代理级别失败", MessageIcon.no);
            if (proxylevelservice.Update(entity))
            {
                mj = new MessageJSON(MessageState.success, "修改代理级别成功", MessageIcon.yes);
            }
            return mj;
        }

        /// <summary>
        /// 删除代理级别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public MessageJSON Delete(long id)
        {
            MessageJSON mj = new MessageJSON(MessageState.fail, "删除代理级别失败", MessageIcon.no);
            if (proxylevelservice.Delete(id))
            {
                mj = new MessageJSON(MessageState.success, "删除代理级别成功", MessageIcon.yes);
            }
            return mj;
        }

        #endregion

    }
}
