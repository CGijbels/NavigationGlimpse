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
                stateEl.Previous,
                stateEl.Crumb,
                Selected = stateEl.Crumb == 0
            };
        }
    }
}
