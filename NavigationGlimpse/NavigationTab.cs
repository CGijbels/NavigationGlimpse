using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Navigation;
using System;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace NavigationGlimpse
{
    public class NavigationTab : TabBase, ITabSetup, IKey
    {
        public override object GetData(ITabContext context)
        {
            var request = context.GetRequestContext<HttpContextBase>();
            var mobile = request.Request["n0"] == null ? request.GetOverriddenBrowser().IsMobileDevice : 
                request.Request["n0"].StartsWith("Mobile", StringComparison.Ordinal);
            return Canvas.Arrange(new StateDisplayInfo
            {
                Page = GetCurrentPage(context, mobile),
                Route = GetCurrentRoute(mobile)
            });
        }

        private string GetCurrentPage(ITabContext context, bool mobile)
        {
            string page = null;
            var getDisplayInfoForPageMessage = context.GetMessages<StateRouteHandler.GetDisplayInfoForPage.Message>().FirstOrDefault();
            var getPageForDisplayInfoMessage = context.GetMessages<StateRouteHandler.GetPageForDisplayInfo.Message>().FirstOrDefault();
            if (getDisplayInfoForPageMessage != null)
                page = getDisplayInfoForPageMessage.Page;
            if (getPageForDisplayInfoMessage != null)
                page = getPageForDisplayInfoMessage.Page;
            if (page == null)
                page = !mobile || StateContext.State.MobilePage.Length == 0 ? StateContext.State.Page : StateContext.State.MobilePage;
            return page;
        }

        private string GetCurrentRoute(bool mobile)
        {
            return !mobile || (StateContext.State.MobilePage.Length == 0 && StateContext.State.MobileRoute.Length == 0) ? 
                StateContext.State.Route : StateContext.State.MobileRoute;
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
