using System;
using System.Collections.Generic;

namespace TransportTycoon
{
    internal sealed class RouteMap : IRouteMap
    {
        private readonly Dictionary<(ILocation, ILocation), int> _routes = new Dictionary<(ILocation, ILocation), int>();

        private IPort _port;

        public void RegisterRoute(ILocation from, ILocation to, int distance, bool bidirectional = true)
        {
            if (from is IPort port)
            {
                _port = port;
            }

            _routes.Add((from, to), distance);

            if (bidirectional)
            {
                _routes.Add((to, from), distance);
            }
        }

        public Route CreateRoute(ILocation from, ILocation to)
        {
            var route = FindRoute(from, to);
            if (route != null)
            {
                return route;
            }

            // trying keep routing simple for now...
            if (_port == null) throw new ArgumentException(FormatMessage());

            return FindRoute(from, _port) ?? throw new ArgumentException(FormatMessage());

            string FormatMessage() => $"Can't find route between '{from.Name}' and '{to.Name}'.";
        }

        public Route FromLocation(ILocation location)
        {
            return new Route(location);
        }

        private Route FindRoute(ILocation from, ILocation to)
        {
            var key = (from, to);
            if (!_routes.TryGetValue(key, out var distance))
            {
                return null;
            }

            return new Route(to, distance);
        }
    }

    public interface IRouteMap
    {
        Route CreateRoute(ILocation from, ILocation to);

        Route FromLocation(ILocation location);
    }

    public sealed class Route
    {
        private int _distance;

        public Route(ILocation destination, int distance)
        {
            if (distance <= 0) throw new ArgumentOutOfRangeException(nameof(distance));

            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            _distance = distance;
        }

        public Route(ILocation location)
        {
            Destination = location ?? throw new ArgumentNullException(nameof(location));
            _distance = 0;
        }

        public bool Completed => _distance == 0;

        public ILocation Destination { get; }

        public void Travel()
        {
            if (Completed) throw new InvalidOperationException();

            _distance--;
        }
    }
}