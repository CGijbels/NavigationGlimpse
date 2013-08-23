using Glimpse.Core.Extensibility;
using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class NavigationTab : TabBase, IKey
    {
        public override object GetData(ITabContext context)
        {
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
    }
}
