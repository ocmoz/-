using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;

namespace FM2E.DALFactory
{
    abstract class InstanceCache
    {
        // Hashtable to store cached parameters
        private static Hashtable instanceCache = Hashtable.Synchronized(new Hashtable());

        public static void CacheInstance(string cacheKey, object instance)
        {
            instanceCache[cacheKey] = instance;
        }

        public static object GetCachedInstance(string cacheKey)
        {
            object cachedInstance = instanceCache[cacheKey];

            if (cachedInstance != null)
                return cachedInstance;
            else return null;
        }

        public static object CreateInstance(string path, string className)
        {
            object dal = InstanceCache.GetCachedInstance(path + className);
            if (dal == null)
            {
                dal = Assembly.Load(path).CreateInstance(path + className);
                InstanceCache.CacheInstance(path + className, dal);
            }
            return dal;
        }
    }
}
