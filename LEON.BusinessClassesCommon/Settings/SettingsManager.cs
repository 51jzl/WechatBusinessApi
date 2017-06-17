//------------------------------------------------------------------------------
// <copyright company="LEON">
//     Copyright (c) LEON Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using LEON;
using LEON.BusinessClasses;

namespace LEON.BusinessClasses
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    /// <typeparam name="TSettingsEntity"></typeparam>
    public class SettingManager<TSettingsEntity> : ISettingsManager<TSettingsEntity> where TSettingsEntity : class, IEntity, new()
    {
        public ISettingsRepository<TSettingsEntity> repository { get; set; }

        public TSettingsEntity Get()
        {
            return repository.Get();
        }

        public void Save(TSettingsEntity settings)
        {
            repository.Save(settings);
        }
    }
}