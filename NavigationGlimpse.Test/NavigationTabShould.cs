using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NavigationGlimpse.Test
{
    [TestClass]
    public class NavigationTabShould
    {
        private static IEnumerable<TransitionElement> TransitionElements
        {
            get;
            set;
        }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var elements = (Tuple<List<StateElement>,List<TransitionElement>>) new NavigationTab().GetData(null);
            TransitionElements = elements.Item2;
        }

        private static TransitionElement GetTransition(string transitionPath)
        {
            var keys = transitionPath.Split('.');
            return TransitionElements.Where(t => t.Transition.Key == keys[2]
                && t.Transition.Parent.Key == keys[1]
                && t.Transition.Parent.Parent.Key == keys[0]).First();
        }

        [TestMethod]
        public void SetDepthTo0ForSingleTransition()
        {
            Assert.AreEqual(0, GetTransition("D1.S1.T1").Depth);
        }

        [TestMethod]
        public void SetX1To85ForSingleTransitionFromState1()
        {
            Assert.AreEqual(85, GetTransition("D1.S1.T1").X1);
        }

        [TestMethod]
        public void SetX2To275ForSingleTransitionToState2()
        {
            Assert.AreEqual(275, GetTransition("D1.S1.T1").X2);
        }
    }
}
