using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class YorumlarModel
    {
        public string YaziId { get; set; }
        public string DateTime{ get; set; }
        public string UserName { get; set; }
        public string YorumText { get; set; }
        public bool Onayli { get; set; }
        public int YildizPuan { get; set; }
    }
}
