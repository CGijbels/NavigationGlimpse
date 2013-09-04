using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class NavigationTab : TabBase, ITabSetup, IKey
    {
        public override object GetData(ITabContext context)
        {
            var getDisplayInfoForPageMessage = context.GetMessages<StateRouteHandler.GetDisplayInfoForPage.Message>().FirstOrDefault();
            return Canvas.Arrange();
        }

        public override string Name
        {
            get 
            { 
                return "Navigation";
            }
        }

        public string Key
        {
            get
            {
                return "navigation_glimpse";
            }
        }

        public void Setup(ITabSetupContext context)
        {
            context.PersistMessages<StateRouteHandler.GetDisplayInfoForPage.Message>();
        }
    }
}
