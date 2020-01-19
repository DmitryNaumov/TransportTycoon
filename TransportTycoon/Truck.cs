namespace TransportTycoon
{
    internal sealed class Truck : CargoCarrier
    {
        public Truck(IRouteMap routeMap, IFactory factory) : base(routeMap, factory)
        {
        }
    }
}