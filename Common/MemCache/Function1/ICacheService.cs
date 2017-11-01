namespace Common.MemCache.EnyimCache
{
    /// <summary>
    /// memcached公共缓存调用方法接口(读)
    /// </summary>
    public interface ICacheReaderService
    {

        /// <summary>
        /// 返回指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 返回指定key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool isExists(string key);
    }

    /// <summary>
    /// memcached公共缓存调用方法接口(写)
    /// </summary>
    public interface ICacheWriterService
    {
        /// <summary>
        /// 缓存有效间隔时间 (以分钟为单位)
        /// </summary>
        int TimeOut { set; get; }

        /// <summary>
        /// 添加指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        bool Add(string key, object obj);

        /// <summary>
        /// 添加指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="time">缓存有效间隔时间 (以分钟为单位)</param>
        /// <returns></returns>
        bool Add(string key, object obj,int time);

        /// <summary>
        /// 添加指定key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        bool Add<T>(string key, T obj);

        /// <summary>
        /// 添加指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="time">缓存有效间隔时间 (以分钟为单位)</param>
        /// <returns></returns>
        bool Add<T>(string key, T obj, int time);

        /// <summary>
        /// 移除指定key的对象
        /// </summary>
        /// <param name="key"></param>
        bool Remove(string key);

        /// <summary>
        /// 修改指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Modify(string key, object destObj);

        /// <summary>
        /// 修改指定key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Modify(string key, object destObj, int time);

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        bool Release();
    }
}
