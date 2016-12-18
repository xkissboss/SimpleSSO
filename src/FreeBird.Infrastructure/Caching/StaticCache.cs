﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Runtime.Caching;
namespace FreeBird.Infrastructure.Caching
{
    
    public partial class StaticCache : ICache
    {
		private ObjectCache _cache;

        protected ObjectCache Cache
        {
            get
            {
				if (_cache == null)
				{
					_cache = new MemoryCache("FreeBird");
				}
				return _cache;
            }
        }

        public IEnumerable<KeyValuePair<string, object>> Entries
        {
            get
            {
				return Cache;
            }
        }

		public object Get(string key)
        {
			return Cache.Get(key);
        }

		public void Set(string key, object value, int? cacheTime)
		{
			var cacheItem = new CacheItem(key, value);
			CacheItemPolicy policy = null;
			if (cacheTime.GetValueOrDefault() > 0)
			{
				policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime.Value) };
			}

			Cache.Add(cacheItem, policy);
		}

        public bool Contains(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

		public bool IsSingleton
		{
			get { return true; }
		}
	}

}
