using Glimpse.Core.Extensibility;

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
                stateEl.State.Defaults,
                stateEl.State.DefaultTypes,
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
    }
}
