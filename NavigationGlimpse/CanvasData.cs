using System.Collections.Generic;

namespace NavigationGlimpse
{
    public class CanvasData
    {
        public List<StateElement> States { get; set; }

        public List<TransitionElement> Transitions { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int W { get; set; }

        public int H { get; set; }
    }
}
