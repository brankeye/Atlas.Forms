using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Enums;

namespace Atlas.Forms.Caching
{
    public static class CacheEvents
    {
        public static string OnPageAppeared => "OnPageAppeared";

        public static string OnPageDisappeared => "OnPageDisappeared";

        public static string OnPageCreated => "OnPageCreated";
    }
}
