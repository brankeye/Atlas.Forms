using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages
{
    public class TargetPageInfo
    {
        public TargetPageInfo(string key, CacheState cacheState)
        {
            Key = key;
            CacheState = cacheState;
        }

        public string Key { get; set; }

        public CacheState CacheState { get; set; }

        public string LifetimeInstanceKey { get; set; }
    }
}
