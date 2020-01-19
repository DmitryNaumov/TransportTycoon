namespace TransportTycoon
{
    internal sealed class Ship : CargoCarrier
    {
        public Ship(IRouteMap routeMap, IPort port) : base(routeMap, port)
        {
        }
    }
}