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
        public void SetXTo10ForDialog1State1()
        {
            Assert.AreEqual(10, GetState("D1.S1").X);
        }

        [TestMethod]
        public void SetXTo200ForDialog1State2()
        {
            Assert.AreEqual(200, GetState("D1.S2").X);
        }

        [TestMethod]
        public void SetXTo390ForDialog1State3()
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
        public void SetX1To85ForSingleTransitionFromDialog1State1()
        {
            Assert.AreEqual(85, GetTransition("D1.S1.T1").X1);
        }

        [TestMethod]
        public void SetX2To275ForSingleTransitionToDialog1State2()
        {
            Assert.AreEqual(275, GetTransition("D1.S1.T1").X2);
        }

        [TestMethod]
        public void SetX1To455ForSingleSelfTransitionFromDialog1State3()
        {
            Assert.AreEqual(455, GetTransition("D1.S3.T1").X1);
        }

        [TestMethod]
        public void SetX2To475ForSingleSelfTransitionToDialog1State3()
        {
            Assert.AreEqual(475, GetTransition("D1.S3.T1").X2);
        }

        [TestMethod]
        public void SetX1To845ForSingleBackTransitionFromDialog1State5()
        {
            Assert.AreEqual(845, GetTransition("D1.S5.T1").X1);
        }

        [TestMethod]
        public void SetX2To655ForSingleBackTransitionToDialog1State4()
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

        [TestMethod]
        public void SetXTo10ForDialog2State1()
        {
            Assert.AreEqual(10, GetState("D2.S1").X);
        }

        [TestMethod]
        public void SetXTo200ForDialog2State2()
        {
            Assert.AreEqual(200, GetState("D2.S2").X);
        }

        [TestMethod]
        public void SetYTo135ForAllDialog2States()
        {
            var states = StateElements.Where(s => s.State.Parent.Key == "D2" && s.Y != 135);
            Assert.AreEqual(0, states.Count());
        }

        [TestMethod]
        public void SetX1To95ForDoubleTransition1FromDialog2State1()
        {
            Assert.AreEqual(95, GetTransition("D2.S1.T1").X1);
        }

        [TestMethod]
        public void SetX2To265ForDoubleTransition1ToDialog2State2()
        {
            Assert.AreEqual(265, GetTransition("D2.S1.T1").X2);
        }

        [TestMethod]
        public void SetHTo20ForDoubleTransition2FromDialog2State1()
        {
            Assert.AreEqual(20, GetTransition("D2.S1.T1").H);
        }

        [TestMethod]
        public void SetX1To75ForDoubleTransition2FromDialog2State1()
        {
            Assert.AreEqual(75, GetTransition("D2.S1.T2").X1);
        }

        [TestMethod]
        public void SetX2To285ForDoubleTransition2ToDialog2State2()
        {
            Assert.AreEqual(285, GetTransition("D2.S1.T2").X2);
        }

        [TestMethod]
        public void SetHTo40ForDoubleTransition2FromDialog2State1()
        {
            Assert.AreEqual(40, GetTransition("D2.S1.T2").H);
        }

        [TestMethod]
        public void SetX1To455ForDoubleSelfTransition1FromDialog2State3()
        {
            Assert.AreEqual(455, GetTransition("D2.S3.T1").X1);
        }

        [TestMethod]
        public void SetX2To475ForDoubleSelfTransition1ToDialog2State3()
        {
            Assert.AreEqual(475, GetTransition("D2.S3.T1").X2);
        }

        [TestMethod]
        public void SetHTo20ForDoubleSelfTransition1FromDialog2State3()
        {
            Assert.AreEqual(20, GetTransition("D2.S3.T1").H);
        }

        [TestMethod]
        public void SetX1To435ForDoubleSelfTransition2FromDialog2State3()
        {
            Assert.AreEqual(435, GetTransition("D2.S3.T2").X1);
        }

        [TestMethod]
        public void SetX2To495ForDoubleSelfTransition2ToDialog2State3()
        {
            Assert.AreEqual(495, GetTransition("D2.S3.T2").X2);
        }

        [TestMethod]
        public void SetHTo40ForDoubleSelfTransition2FromDialog2State3()
        {
            Assert.AreEqual(40, GetTransition("D2.S3.T2").H);
        }

        [TestMethod]
        public void SetX1To835ForDoubleBackTransition1FromDialog2State5()
        {
            Assert.AreEqual(835, GetTransition("D2.S5.T1").X1);
        }

        [TestMethod]
        public void SetX2To665ForDoubleBackTransition1ToDialog2State4()
        {
            Assert.AreEqual(665, GetTransition("D2.S5.T1").X2);
        }

        [TestMethod]
        public void SetHTo20ForDoubleBackTransition1FromDialog2State5()
        {
            Assert.AreEqual(20, GetTransition("D2.S5.T1").H);
        }

        [TestMethod]
        public void SetX1To855ForDoubleBackTransition2FromDialog2State5()
        {
            Assert.AreEqual(855, GetTransition("D2.S5.T2").X1);
        }

        [TestMethod]
        public void SetX2To645ForDoubleBackTransition2ToDialog2State4()
        {
            Assert.AreEqual(645, GetTransition("D2.S5.T2").X2);
        }

        [TestMethod]
        public void SetHTo40ForDoubleBackTransition2FromDialog2State5()
        {
            Assert.AreEqual(40, GetTransition("D2.S5.T2").H);
        }

        [TestMethod]
        public void SetX1To1045ForDoubleFromAndToTransition1FromDialog2State6()
        {
            Assert.AreEqual(1045, GetTransition("D2.S6.T1").X1);
        }

        [TestMethod]
        public void SetX2To1215ForDoubleFromAndToTransition1ToDialog2State7()
        {
            Assert.AreEqual(1215, GetTransition("D2.S6.T1").X2);
        }

        [TestMethod]
        public void SetHTo20ForDoubleFromAndToTransition1FromDialog2State6()
        {
            Assert.AreEqual(20, GetTransition("D2.S6.T1").H);
        }

        [TestMethod]
        public void SetX1To1235ForDoubleFromAndToTransition1FromDialog2State7()
        {
            Assert.AreEqual(1235, GetTransition("D2.S7.T1").X1);
        }

        [TestMethod]
        public void SetX2To1025ForDoubleFromAndToTransition1ToDialog2State6()
        {
            Assert.AreEqual(1025, GetTransition("D2.S7.T1").X2);
        }

        [TestMethod]
        public void SetHTo40ForDoubleFromAndToTransition1FromDialog2State7()
        {
            Assert.AreEqual(40, GetTransition("D2.S7.T1").H);
        }

        [TestMethod]
        public void SetX1To1395ForDoubleSelfAndSelfTransition1FromDialog2State8()
        {
            Assert.AreEqual(1395, GetTransition("D2.S8.T1").X1);
        }

        [TestMethod]
        public void SetX2To1415ForDoubleSelfAndFromTransition1ToDialog2State8()
        {
            Assert.AreEqual(1415, GetTransition("D2.S8.T1").X2);
        }

        [TestMethod]
        public void SetHTo20ForDoubleSelfAndFromTransition1FromDialog2State8()
        {
            Assert.AreEqual(20, GetTransition("D2.S8.T1").H);
        }
        [TestMethod]
        public void SetX1To1435ForDoubleSelfAndSelfTransition2FromDialog2State8()
        {
            Assert.AreEqual(1435, GetTransition("D2.S8.T2").X1);
        }

        [TestMethod]
        public void SetX2To1605ForDoubleSelfAndFromTransition2ToDialog2State9()
        {
            Assert.AreEqual(1605, GetTransition("D2.S8.T2").X2);
        }

        [TestMethod]
        public void SetHTo20ForDoubleSelfAndFromTransition2FromDialog2State8()
        {
            Assert.AreEqual(20, GetTransition("D2.S8.T2").H);
        }

        [TestMethod]
        public void SetYTo185ForAllDialog2Transitions()
        {
            var trans = TransitionElements.Where(t => t.Transition.Parent.Parent.Key == "D2" && t.Y != 185);
            Assert.AreEqual(0, trans.Count());
        }
    }
}
