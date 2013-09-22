using Glimpse.Core.Extensibility;
using Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NavigationGlimpse
{
    public class StateElementConverter : SerializationConverter<StateElement>
    {
        public override object Convert(StateElement stateEl)
        {
            return new
            {
                stateEl.State.Key,
                stateEl.X,
                stateEl.Y,
                stateEl.W,
                stateEl.H,
                stateEl.Current,
                stateEl.Previous,
                stateEl.Back,
                Selected = stateEl.Current,
                DialogKey = stateEl.State.Parent.Key,
                Data = GetDictionary(stateEl.Data),
                stateEl.Page,
                stateEl.State.Title,
                stateEl.Route,
                stateEl.State.Theme,
                stateEl.Masters,
                DefaultTypes = GetDictionary<Type>(stateEl.State.DefaultTypes),
                stateEl.State.Derived,
                stateEl.State.TrackCrumbTrail,
                stateEl.State.CheckPhysicalUrlAccess,
            };
        }

        private Dictionary<string, object> GetDictionary(NavigationData data)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (NavigationDataItem item in data.OrderBy(i => i.Key))
                dictionary[item.Key] = item.Value;
            return dictionary;
        }

        private Dictionary<string, object> GetDictionary<T>(StateInfoCollection<T> coll)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (string key in coll.Keys)
                dictionary[key] = coll[key];
            return dictionary;
        }
    }
}
