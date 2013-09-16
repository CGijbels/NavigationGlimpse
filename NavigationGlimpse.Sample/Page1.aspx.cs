using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NavigationGlimpse.Sample
{
    public partial class Page1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StateContext.Bag.CustomData = new CustomData();
            StateContext.Bag.Custom2Data = null;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            StateContext.Bag.Number = 1;
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            StateContext.Bag.Custom2Data = new Custom2Data();
            base.OnSaveStateComplete(e);
        }
    }
}