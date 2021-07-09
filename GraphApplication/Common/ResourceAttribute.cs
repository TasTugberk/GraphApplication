using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Common
{
    public class ResourceAttribute : Attribute
    {
        /// <summary>
        /// Resource Key
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ResourceKey">Key</param>
        public ResourceAttribute(string ResourceKey)
        {
            this.ResourceKey = ResourceKey;
        }
    }
}
