using Glimpse.Core.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public sealed class ScriptResource : IDynamicClientScript
    {
        public string GetResourceName()
        {
            return ClientScript.InternalName;
        }

        public ScriptOrder Order
        {
            get
            {
                return ScriptOrder.IncludeAfterClientInterfaceScript;
            }
        }
    }
}
