using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class TransitionElement
    {
        public TransitionElement(Transition trans)
        {
            A = 2 * trans.To.Index;
            B = 2 * trans.Parent.Index + 1;
            if (trans.Parent.Index < trans.To.Index)
            {
                A = 2 * trans.Parent.Index + 1;
                B = 2 * trans.To.Index;
            }
            if (trans.Parent.Index > trans.To.Index)
            {
                A = 2 * trans.To.Index + 1;
                B = 2 * trans.Parent.Index;
            }
            Transition = trans;
        }

        public Transition Transition
        {
            get;
            private set;
        }

        public State From
        {
            get
            {
                return Transition.Parent;
            }
        }

        public State To
        {
            get
            {
                return Transition.To;
            }
        }

        public int A
        { 
            get;
            set;
        }

        public int B
        {
            get;
            set;
        }

        public int Depth
        {
            get;
            set;
        }

        public int? X
        {
            get;
            set;
        }

        public int? Y
        {
            get;
            set;
        }

        public void SetCoords(State state, int value)
        {
            if (From == state && !X.HasValue)
                X = value;
            else
                Y = value;
        }
    }
}
