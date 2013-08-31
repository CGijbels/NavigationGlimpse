using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class StateElement
    {
        public StateElement(State state)
        {
            State = state;
        }

        public State State { get; private set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int W { get; set; }

        public int H { get; set; }

        public bool Previous { get; set; }

        public int Crumb { get; set; }
    }
}
