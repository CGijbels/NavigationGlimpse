using Glimpse.Core.Extensibility.Fakes;
using Glimpse.Core.Extensions.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Fakes;

namespace NavigationGlimpse.Test
{
    [TestClass]
    public class NavigationTabShould
    {
        private static IEnumerable<StateElement> StateElements
        {
            get;
            set;
        }

        private static IEnumerable<TransitionElement> TransitionElements
        {
            get;
            set;
        }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            using (ShimsContext.Create())
            {
                StateController.Navigate("D1");
                var tabContext = new StubITabContext();
                var request = new StubHttpRequestBase();
                request.ItemGetString = k => string.Empty;
                tabContext.GetRequestContextOf1<HttpContextBase>(() => new StubHttpContextBase { RequestGet = () => request });
                ShimTabContextExtensions.GetMessagesOf1ITabContext<StateRouteHandler.GetDisplayInfoForPage.Message>(t =>
                    new List<StateRouteHandler.GetDisplayInfoForPage.Message>());
                ShimTabContextExtensions.GetMessagesOf1ITabContext<StateRouteHandler.GetPageForDisplayInfo.Message>(t =>
                    new List<StateRouteHandler.GetPageForDisplayInfo.Message>());
                ShimTabContextExtensions.GetMessagesOf1ITabContext<StateRouteHandler.GetDisplayInfoForTheme.Message>(t =>
                    new List<StateRouteHandler.GetDisplayInfoForTheme.Message>());
                ShimTabContextExtensions.GetMessagesOf1ITabContext<StateRouteHandler.GetThemeForDisplayInfo.Message>(t =>
                    new List<StateRouteHandler.GetThemeForDisplayInfo.Message>());
                ShimTabContextExtensions.GetMessagesOf1ITabContext<StateRouteHandler.GetDisplayInfoForMaster.Message>(t =>
                    new List<StateRouteHandler.GetDisplayInfoForMaster.Message>());
                ShimTabContextExtensions.GetMessagesOf1ITabContext<StateRouteHandler.GetMasterForDisplayInfo.Message>(t =>
                    new List<StateRouteHandler.GetMasterForDisplayInfo.Message>());
                var elements = (CanvasData)new NavigationTab().GetData(tabContext);
                StateElements = elements.States;
                TransitionElements = elements.Transitions;
            }
        }

        private static StateElement GetState(string statePath)
        {
            var keys = statePath.Split('.');
            return StateElements.Where(s => s.State.Key == keys[1]
                && s.State.Parent.Key == keys[0]).First();
        }

        private static TransitionElement GetTransition(string transitionPath)
        {
            var keys = transitionPath.Split('.');
            return TransitionElements.Where(t => t.Transition.Key == keys[2]
                && t.Transition.Parent.Key == keys[1]
                && t.Transition.Parent.Parent.Key == keys[0]).First();
        }

        [TestMethod]
        public void ReturnAllStates()
        {
            var q = from d in StateInfoConfig.Dialogs
                    from s in d.States
                    select s;
            Assert.AreEqual(q.Count(), StateElements.Count());
        }

        [TestMethod]
        public void ReturnAllTransitions()
        {
            var q = from d in StateInfoConfig.Dialogs
                    from s in d.States
                    from t in s.Transitions
                    select t;
            Assert.AreEqual(q.Count(), TransitionElements.Count());
        }

        [TestMethod]
        public void SetXTo10ForState1()
        {
            Assert.AreEqual(10, GetState("D1.S1").X);
        }

        [TestMethod]
        public void SetXTo200ForState2()
        {
            Assert.AreEqual(200, GetState("D1.S2").X);
        }

        [TestMethod]
        public void SetXTo390ForState2()
        {
            Assert.AreEqual(390, GetState("D1.S3").X);
        }

        [TestMethod]
        public void SetYTo25ForAllDialog1States()
        {
            var states = StateElements.Where(s => s.State.Parent.Key == "D1" && s.Y != 25);
            Assert.AreEqual(0, states.Count());
        }

        [TestMethod]
        public void SetWTo150ForAllDialog1States()
        {
            var states = StateElements.Where(s => s.State.Parent.Key == "D1" && s.W != 150);
            Assert.AreEqual(0, states.Count());
        }

        [TestMethod]
        public void SetHTo50ForAllDialog1States()
        {
            var states = StateElements.Where(s => s.State.Parent.Key == "D1" && s.H != 50);
            Assert.AreEqual(0, states.Count());
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

        [TestMethod]
        public void SetX1To455ForSingleSelfTransitionFromState3()
        {
            Assert.AreEqual(455, GetTransition("D1.S3.T1").X1);
        }

        [TestMethod]
        public void SetX2To475ForSingleSelfTransitionToState3()
        {
            Assert.AreEqual(475, GetTransition("D1.S3.T1").X2);
        }

        [TestMethod]
        public void SetX1To845ForSingleBackTransitionFromState5()
        {
            Assert.AreEqual(845, GetTransition("D1.S5.T1").X1);
        }

        [TestMethod]
        public void SetX2To655ForSingleBackTransitionToState4()
        {
            Assert.AreEqual(655, GetTransition("D1.S5.T1").X2);
        }

        [TestMethod]
        public void SetHTo20ForAllDialog1Transitions()
        {
            var trans = TransitionElements.Where(t => t.Transition.Parent.Parent.Key == "D1" && t.H != 20);
            Assert.AreEqual(0, trans.Count());
        }

        [TestMethod]
        public void SetYTo75ForAllDialog1Transitions()
        {
            var trans = TransitionElements.Where(t => t.Transition.Parent.Parent.Key == "D1" && t.Y != 75);
            Assert.AreEqual(0, trans.Count());
        }
    }
}
