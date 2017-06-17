using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WB.Api.Models
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageJSON
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public MessageJSON()
        {
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public MessageJSON(MessageState state, string message, MessageIcon? icon = null, object data = null)
        {
            this.State = state;
            this.Message = message;
            this.Data = data;
            this.Icon = icon;
        }


        /// <summary>
        /// 状态
        /// </summary>
        public MessageState State { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public MessageIcon? Icon { get; set; }
    }

    /// <summary>
    /// 登录实体
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public MessageState State { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MessageState
    {
        /// <summary>
        /// 状态成功
        /// </summary>
        success = 1,
        /// <summary>
        /// 状态异常
        /// </summary>
        abnormality = 2,
        /// <summary>
        /// 失败
        /// </summary>
        fail = 3
    }

    /// <summary>
    /// 消息图标
    /// </summary>
    public enum MessageIcon
    {
        /// <summary>
        /// 对的
        /// </summary>
        yes = 1,
        /// <summary>
        /// 错的
        /// </summary>
        no = 2,
        /// <summary>
        /// 疑问
        /// </summary>
        question = 3,
        /// <summary>
        /// 锁
        /// </summary>
        locks = 4,
        /// <summary>
        /// 不开心
        /// </summary>
        unhappy = 5,
        /// <summary>
        /// 开心
        /// </summary>
        happy = 6,
        /// <summary>
        /// 警告
        /// </summary>
        warning = 7
    }
}