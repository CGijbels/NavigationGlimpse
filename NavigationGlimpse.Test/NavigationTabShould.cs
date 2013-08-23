using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NavigationGlimpse.Test
{
    [TestClass]
    public class NavigationTabShould
    {
        private static TransitionElement GetTransition(string dialog, string state, string transition)
        {
            var trans = (IEnumerable<TransitionElement>)new NavigationTab().GetData(null);
            return trans.Where(t => t.Transition.Key == transition
                && t.Transition.Parent.Key == state
                && t.Transition.Parent.Parent.Key == dialog).First();
        }

        [TestMethod]
        public void SetSingleTransitionDepth0()
        {
            Assert.AreEqual(0, GetTransition("D1", "S1", "T1").Depth);
        }
    }
}
