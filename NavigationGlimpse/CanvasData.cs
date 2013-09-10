using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class CanvasData
    {
        public List<StateElement> States { get; set; }

        public List<TransitionElement> Transitions { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
