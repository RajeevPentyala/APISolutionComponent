using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolutionComponent
{
    public class ResponseComponent
    {
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string EntityLogicalName { get; set; }
        public string EntityId { get; set; }
        public string ComponentKeyAttributeName { get; set; }
        public string ComponentNameAttributeName { get; set; }
        public string ComponentDisplayNameAttributeName { get; set; }
        public string ComponentDescriptionAttributeName { get; set; }
        public string ComponentStateAttributeName { get; set; }
        public string ParentComponentId { get; set; }
    }
}
