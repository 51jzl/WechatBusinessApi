﻿using LEON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WB.Api.Models;
using WB.Api.Secure;
using WB.BusinessClasses;
using WB.Entity;

namespace WB.Api.Controllers
{
    /// <summary>
    /// 微商项目控制器
    /// </summary>
    [ApiAuthorize]
    public class ProjectController : ApiController
    {
        public ProjectService projectService = DIContainer.Resolve<ProjectService>();

        /// <summary>
        /// 获取用户所有项目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageJSON GetAll()
        {
            MessageJSON mj = new MessageJSON(MessageState.success, "");
            AuthInfo authInfo = this.RequestContext.RouteData.Values["token"] as AuthInfo;
            mj.Data = projectService.Query(authInfo.UserId);
            return mj;
        }

        /// <summary>
        /// 获取用户 搜索的 项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public MessageJSON Query(string name)
        {
            MessageJSON mj = new MessageJSON(MessageState.success, "");
            AuthInfo authInfo = this.RequestContext.RouteData.Values["token"] as AuthInfo;
            mj.Data = projectService.Query(authInfo.UserId, name);
            return mj;
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageJSON Create([FromBody]bsProject entity)
        {
            MessageJSON mj = new MessageJSON(MessageState.fail, "创建项目失败，是否存在相同的项目名", MessageIcon.no);
            AuthInfo authInfo = this.RequestContext.RouteData.Values["token"] as AuthInfo;
            entity.UserID = authInfo.UserId;
            if (projectService.Create(entity))
            {
                mj = new MessageJSON(MessageState.success, "创建项目成功", MessageIcon.yes);
            }
            return mj;
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageJSON Update([FromBody]bsProject entity)
        {
            MessageJSON mj = new MessageJSON(MessageState.fail, "修改项目失败，是否存在相同的项目名", MessageIcon.no);
            AuthInfo authInfo = this.RequestContext.RouteData.Values["token"] as AuthInfo;
            entity.UserID = authInfo.UserId;
            if (projectService.Update(entity))
            {
                mj = new MessageJSON(MessageState.success, "修改项目成功", MessageIcon.yes);
            }
            return mj;
        }
    }
}
