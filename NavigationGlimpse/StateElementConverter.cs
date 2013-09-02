using Glimpse.Core.Extensibility;
using Navigation;
using System;
using System.Collections.Generic;

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
                stateEl.State.Page,
                stateEl.State.Title,
                stateEl.State.Route,
                Data = GetDictionary(stateEl.Data),
                DefaultTypes = GetDictionary<Type>(stateEl.State.DefaultTypes),
                stateEl.State.Derived,
                stateEl.State.TrackCrumbTrail,
                stateEl.State.CheckPhysicalUrlAccess,
                stateEl.State.Theme,
                stateEl.State.Masters,
                stateEl.State.MobilePage,
                stateEl.State.MobileRoute,
                stateEl.State.MobileTheme,
                stateEl.State.MobileMasters
            };
        }

        private Dictionary<string, object> GetDictionary(NavigationData data)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (NavigationDataItem item in data)
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
