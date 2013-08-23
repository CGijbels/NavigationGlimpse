using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationGlimpse
{
    public class Canvas
    {
        private const int StartX = 50;
        private const int StartY = 50;
        private const int StateElementWidth = 150;
        private const int WidthBetweenPoints = 10;

        public static List<TransitionElement> Arrange()
        {
            var transitionElements = new List<TransitionElement>();
            foreach (Dialog dialog in StateInfoConfig.Dialogs)
            {
                var spacesFilled = new Dictionary<int, HashSet<int>>();
                var trans = TransByDialog(dialog);
                foreach (var transitionElement in trans)
                {
                    transitionElements.Add(transitionElement);
                    CalculateDepth(spacesFilled, transitionElement);
                }
                foreach (State state in dialog.States)
                {
                    trans = TransByState(state, transitionElements);
                    var start = (StateElementWidth - (trans.Count() - 1) * WidthBetweenPoints) / 2;
                    foreach (var p in trans)
                    {
                        p.SetCoords(state, start);
                        start += WidthBetweenPoints;
                    }
                }
            }
            return transitionElements;
        }

        private static IEnumerable<TransitionElement> TransByDialog(Dialog dialog)
        {
            return from s in dialog.States
                   from t in s.Transitions
                   orderby Math.Abs(t.To.Index - t.Parent.Index)
                   select new TransitionElement(t);
        }

        private static void CalculateDepth(Dictionary<int, HashSet<int>> spacesFilled, TransitionElement transEl)
        {
            var depthFound = false;
            var depth = 0;
            while (!depthFound)
            {
                depthFound = !spacesFilled.ContainsKey(depth) ||
                    !spacesFilled[depth].Any(d => transEl.A <= d && d < transEl.B);
                if (!depthFound)
                    depth++;
            }
            transEl.Depth = depth;
            if (!spacesFilled.ContainsKey(transEl.Depth))
                spacesFilled[transEl.Depth] = new HashSet<int>();
            spacesFilled[transEl.Depth].UnionWith(Enumerable.Range(transEl.A, transEl.B - transEl.A));
        }

        private static IEnumerable<TransitionElement> TransByState(State state, List<TransitionElement> transEls)
        {
            var q = from t in transEls
                    let leftA = t.To == state && t.From.Index < t.To.Index
                    let leftB = t.From == state && t.From.Index > t.To.Index
                    let left = leftA || leftB
                    let middle = t.From == state && t.To == state
                    let right = !left && !middle
                    where t.From == state || t.To == state
                    select new { trans = t, left, right, middle };
            foreach (var t in q.Where(t => t.left).OrderBy(t => t.trans.Depth))
                yield return t.trans;
            foreach (var t in q.Where(t => t.middle).OrderByDescending(t => t.trans.Depth))
                yield return t.trans;
            foreach (var t in q.Where(t => t.middle).OrderBy(t => t.trans.Depth))
                yield return t.trans;
            foreach (var t in q.Where(t => t.right).OrderByDescending(t => t.trans.Depth))
                yield return t.trans;
        }
    }
}
