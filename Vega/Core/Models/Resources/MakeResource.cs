using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public MakeResource()
        {
            Models = new HashSet<KeyValuePairResource>();
        }
        public ICollection<KeyValuePairResource> Models { get; set; }
    }
}
