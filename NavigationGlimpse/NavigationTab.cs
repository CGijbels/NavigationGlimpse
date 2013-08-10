using Glimpse.Core.Extensibility;
using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class NavigationTab : TabBase
    {
        public override object GetData(ITabContext context)
        {
            return StateInfoConfig.Dialogs;
        }

        public override string Name
        {
            get 
            { 
                return "Navigation";
            }
        }
    }
}
