using Glimpse.Core.Extensibility;
using Glimpse.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class ClientScript : FileResource, IKey
    {
        internal const string InternalName = "navigationglimpse_client";

        public ClientScript()
        {
            ResourceName = "NavigationGlimpse.navigation.glimpse.js";
            ResourceType = @"application/x-javascript";
            Name = InternalName;
        }

        public string Key
        {
            get 
            { 
                return Name;
            }
        }
    }
}
