using System;

namespace CJ.Core.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        string Get<T>(string key);
        //T Get<T>(string key, Func<T> acquire);
        /// <summary>
        /// 存储缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        void Set<T>(string key, T data);
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void Refresh(string key);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}