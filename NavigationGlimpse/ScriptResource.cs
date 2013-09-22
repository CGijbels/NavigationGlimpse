using Glimpse.Core.Extensibility;

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
