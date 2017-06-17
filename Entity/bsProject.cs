using LEON;
using PetaPoco;

namespace WB.Entity
{
    /// <summary>
    /// 微商项目
    /// </summary>
	[TableName("dbo.bs_Project")]
    [PrimaryKey("ID")]
    [ExplicitColumns]
    public partial class bsProject : IEntity
    {
        [Column] public long ID { get; set; }
        [Column] public long UserID { get; set; }
        [Column] public string Name { get; set; }
        [Column] public string Company { get; set; }
        [Column] public bool Enable { get; set; }

        #region IEntity member

        object IEntity.EntityId { get { return this.ID; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion
    }
}
