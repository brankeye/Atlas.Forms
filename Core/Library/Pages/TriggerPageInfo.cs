using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages
{
    public class TriggerPageInfo
    {
        public TriggerPageInfo(string key, TriggerOption triggerOption)
        {
            Key = key;
            TriggerOption = triggerOption;
        }

        public string Key { get; set; }

        public TriggerOption TriggerOption { get; set; }
    }
}
