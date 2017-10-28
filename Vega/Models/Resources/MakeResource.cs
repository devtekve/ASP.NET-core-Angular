using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.Resources
{
    public class MakeResource
    {
        public MakeResource()
        {
            Models = new HashSet<ModelResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ModelResource> Models { get; set; }
    }
}
