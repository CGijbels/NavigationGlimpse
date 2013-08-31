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
                Selected = stateEl.Current
            };
        }
    }
}
