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
            string page = null;
            var getDisplayInfoForPageMessage = context.GetMessages<StateRouteHandler.GetDisplayInfoForPage.Message>().FirstOrDefault();
            var getPageForDisplayInfoMessage = context.GetMessages<StateRouteHandler.GetPageForDisplayInfo.Message>().FirstOrDefault();
            if (getDisplayInfoForPageMessage != null)
                page = getDisplayInfoForPageMessage.Page;
            if (getPageForDisplayInfoMessage != null)
                page = getPageForDisplayInfoMessage.Page;
            return Canvas.Arrange(page);
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
            context.PersistMessages<StateRouteHandler.GetPageForDisplayInfo.Message>();
        }
    }
}
