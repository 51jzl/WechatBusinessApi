using PetaPoco;
using System;

namespace LEON.Entity
{
    [TableName("dbo.tn_Users")]
    [PrimaryKey("UserId", AutoIncrement = false)]
    [ExplicitColumns]
    public partial class User : IEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Column] public long UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column] public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column] public string Password { get; set; }
        /// <summary>
        /// 加密方式
        /// </summary>
        [Column] public int PasswordFormat { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column] public DateTime DateCreated { get; set; }

        /// <summary>
        ///上次活动时间
        /// </summary>
        public DateTime LastActivityTime { get; set; }

        #region IEntity member

        object IEntity.EntityId { get { return this.UserId; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion
    }
}
