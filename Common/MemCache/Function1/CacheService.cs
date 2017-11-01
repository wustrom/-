using System;
using Enyim.Caching.Memcached;
using System.Collections.Generic;

namespace Common.MemCache.EnyimCache
{


    public class CacheReaderService : BaseService, ICacheReaderService
    {
        private string AppKey = "GZRYVillage";
        public int TimeOut
        {
            get;
            set;
        }

        public CacheReaderService()
        {

        }

        public object Get(string key)
        {
            key = AppKey + key;
            object obj = null;
            Client.TryGet(key, out obj);
            return obj;
        }

        public T Get<T>(string key)
        {
            key = AppKey + key;
            object obj = Get(key);
            T result = default(T);
            if (obj != null)
            {
                result = (T)obj;
            }
            return result;
        }

        public bool isExists(string key)
        {
            key = AppKey + key;
            object obj = Get(key);
            return (obj == null) ? false : true;
        }
    }

    public class CacheWriterService : BaseService, ICacheWriterService
    {
        private string AppKey = "GZRYVillage";
        public int TimeOut
        {
            get;
            set;
        }

        public CacheWriterService()
        {

        }

        public CacheWriterService(int timeOut)
        {
            this.TimeOut = timeOut;
        }

        public bool Add(string key, object obj)
        {
            key = AppKey + key;
            if (TimeOut > 0)
            {
                return Client.Store(StoreMode.Add, key, obj, DateTime.Now.AddMinutes(TimeOut));
            }
            else
            {
                return Client.Store(StoreMode.Add, key, obj);
            }
        }

        public bool Add(string key, object obj, int time)
        {
            key = AppKey + key;
            return Client.Store(StoreMode.Add, key, obj, DateTime.Now.AddMinutes(time));
        }


        public bool Add<T>(string key, T obj)
        {
            key = AppKey + key;
            if (TimeOut > 0)
            {
                return Client.Store(StoreMode.Add, key, obj, DateTime.Now.AddMinutes(TimeOut));
            }
            else
            {
                return Client.Store(StoreMode.Add, key, obj);
            }
        }

        public bool Add<T>(string key, T obj, int time)
        {
            key = AppKey + key;
            return Client.Store(StoreMode.Add, key, obj, DateTime.Now.AddMinutes(time));
        }

        public bool Remove(string key)
        {
            key = AppKey + key;
            return Client.Remove(key);
        }

        public bool Modify(string key, object destObj)
        {
            key = AppKey + key;
            return Client.Store(StoreMode.Set, key, destObj);
        }

        public bool Modify(string key, object destObj, int time)
        {
            key = AppKey + key;
            return Client.Store(StoreMode.Set, key, destObj, DateTime.Now.AddMinutes(time));
        }

        /// <summary>
        /// 清空缓存 TO DO
        /// </summary>
        /// <returns></returns>
        public bool Release()
        {
            Client.FlushAll();
            return true;
        }
    }
}
