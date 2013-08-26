using Glimpse.Core.Extensibility;

namespace NavigationGlimpse
{
    public class TransitionElementConverter : SerializationConverter<TransitionElement>
    {
        public override object Convert(TransitionElement transEl)
        {
            return new
            {
                transEl.Transition.Key,
                transEl.X1,
                transEl.X2,
                transEl.Y,
                transEl.H
            };
        }
    }
}
