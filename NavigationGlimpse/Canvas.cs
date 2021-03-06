﻿using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NavigationGlimpse
{
    internal class Canvas
    {
        private const int Top = 25;
        private const int Left = 10;
        private const int Width = 750;
        private const int Height = 275;
        private const int StateWidth = 150;
        private const int StateHeight = 50;
        private const int StateSeparation = 40;
        private const int TransitionSeparation = 20;
        private const int TransitionStepHeight = 20;
        private const int FontSize = 15;
        private const int PaddingX = 10;
        private const int PaddingY = 5;

        internal static CanvasData Arrange(StateDisplayInfo stateDisplayInfo)
        {
            var transitionElements = new List<TransitionElement>();
            var stateElements = new List<StateElement>();
            var stateX = Left;
            var stateY = Top;
            var crumbs = StateController.Crumbs.Select((c, i) => new { Crumb = c, Index = i })
                .ToDictionary(c => c.Crumb.State, c => new { Crumb = c.Crumb, c.Index });
            foreach (Dialog dialog in StateInfoConfig.Dialogs)
            {
                stateX = Left;
                var depths = CalculateDepths(dialog, transitionElements);
                foreach (State state in dialog.States)
                {
                    var stateElement = new StateElement(state);
                    stateElements.Add(stateElement);
                    stateElement.X = stateX;
                    stateElement.Y = stateY;
                    stateElement.H = StateHeight;
                    stateElement.Page = state.Page;
                    stateElement.Route = state.Route;
                    stateElement.Theme = state.Theme;
                    stateElement.Masters = state.Masters.ToList();
                    if (state == StateContext.State)
                    {
                        stateElement.Current = state == StateContext.State;
                        stateElement.Data = StateContext.Data;
                        stateElement.Page = stateDisplayInfo.Page;
                        stateElement.Route = stateDisplayInfo.Route;
                        stateElement.Theme = stateDisplayInfo.Theme;
                        stateElement.Masters = stateDisplayInfo.Masters;
                    }
                    stateElement.Previous = state == StateContext.PreviousState;
                    stateElement.Back = 0;
                    if (crumbs.ContainsKey(state))
                    {
                        stateElement.Back = crumbs.Count - crumbs[state].Index;
                        stateElement.Data = crumbs[state].Crumb.Data;
                    }
                    if (stateElement.Data == null)
                    {
                        stateElement.Data = new NavigationData();
                        foreach (string key in stateElement.State.Defaults.Keys)
                            stateElement.Data[key] = stateElement.State.Defaults[key];
                    }
                    stateElement.W = ProcessTransitions(stateElement, transitionElements);
                    stateX += stateElement.W + StateSeparation;
                }
                stateY += Top + StateHeight + depths.Count * TransitionStepHeight + FontSize;
            }
            var selected = stateElements.FirstOrDefault(s => s.Current) ?? new StateElement(new State());
            var selectedDialogTrans = transitionElements.Where(t => t.Transition.Parent.Parent == selected.State.Parent).DefaultIfEmpty();
            var lastState = stateElements.Last();
            return new CanvasData
            {
                States = stateElements,
                Transitions = transitionElements,
                X = Math.Min(0, Width - Math.Min(selected.X + selected.W + Left, selected.X + Width - Left)),
                Y = Math.Min(0, Height - Math.Min(Math.Max(selected.Y + selected.H, selectedDialogTrans.Max(t => t != null ? t.Y + t.H : 0)) + PaddingY + FontSize, selected.Y + Height - Top)),
                W = Math.Max(Width, stateElements.Max(s => s.X + s.W) + Left),
                H = Math.Max(Height, Math.Max(lastState.Y + lastState.H, transitionElements.DefaultIfEmpty().Max(t => t != null ? t.Y + t.H : 0)) + PaddingY + FontSize)
            };
        }

        private static Dictionary<int, HashSet<int>> CalculateDepths(Dialog dialog, List<TransitionElement> transEls)
        {
            var depths = new Dictionary<int, HashSet<int>>();
            var trans = from s in dialog.States
                        from t in s.Transitions
                        orderby Math.Abs(t.To.Index - t.Parent.Index)
                        select new TransitionElement(t);
            foreach (var transitionElement in trans)
            {
                transEls.Add(transitionElement);
                CalculateDepth(depths, transitionElement);
            }
            return depths;
        }

        private static void CalculateDepth(Dictionary<int, HashSet<int>> depths, TransitionElement transEl)
        {
            var depthFound = false;
            var depth = 0;
            while (!depthFound)
            {
                depthFound = !depths.ContainsKey(depth) ||
                    !depths[depth].Any(d => transEl.A <= d && d < transEl.B);
                if (!depthFound)
                    depth++;
            }
            transEl.Depth = depth;
            if (!depths.ContainsKey(transEl.Depth))
                depths[transEl.Depth] = new HashSet<int>();
            depths[transEl.Depth].UnionWith(Enumerable.Range(transEl.A, transEl.B - transEl.A));
        }

        private static int ProcessTransitions(StateElement stateElement, List<TransitionElement> transEls)
        {
            var trans = TransByState(stateElement.State, transEls);
            var transWidth = (trans.Count() - 1) * TransitionSeparation;
            var width = StateWidth + Math.Max(0, transWidth + 2 * PaddingX - StateWidth);
            var start = stateElement.X + (width - transWidth) / 2;
            foreach (var transEl in trans)
            {
                transEl.Y = stateElement.Y + StateHeight;
                transEl.H = (transEl.Depth + 1) * TransitionStepHeight;
                transEl.SetCoords(stateElement.State, start);
                start += TransitionSeparation;
            }
            return width;
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
