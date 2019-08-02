using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGToJson
{
    public class DataStructure
    {
        public List<NationColorCode> NationColorCodes { get; set; }
    }

    public class NationColorCode
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
    }
}
