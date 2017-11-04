using Enyim.Caching;

namespace Common.MemCache.EnyimCache
{


    public abstract class BaseService
    {
        public static MemcachedClient Client { get; set; }
       
        static BaseService()
        {
            Client = new MemcachedClient();
        }
    }
}
