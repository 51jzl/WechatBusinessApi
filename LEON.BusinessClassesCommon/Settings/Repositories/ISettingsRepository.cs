﻿namespace LEON.BusinessClasses
{
    /// <summary>
    /// 设置Repository接口
    /// </summary>
    /// <typeparam name="TSettingsEntity">设置的实体类</typeparam>
    public interface ISettingsRepository<TSettingsEntity> where TSettingsEntity : class
    {
        /// <summary>
        /// 获取设置
        /// </summary>
        /// <returns>settings</returns>
        TSettingsEntity Get();

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="settings">settings</param>
        void Save(TSettingsEntity settings);

    }
}
