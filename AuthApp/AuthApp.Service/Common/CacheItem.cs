using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Common {
    public class CacheItem<T> {


        public T Item { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
