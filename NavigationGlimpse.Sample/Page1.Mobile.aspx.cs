using Navigation;
using System;

namespace NavigationGlimpse.Sample
{
    public partial class Page1_Mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StateContext.Bag.Custom2Data = null;

        }
    }
}