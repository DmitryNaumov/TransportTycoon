using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    public sealed class World
    {
        private World()
        {
            var factory = new Factory();
            var port = new Port();

            var warehouseA = new Warehouse("A");
            var warehouseB = new Warehouse("B");

            var routeMap = new RouteMap();
            routeMap.RegisterRoute(factory, port, 1);
            routeMap.RegisterRoute(port, warehouseA, 4);
            routeMap.RegisterRoute(factory, warehouseB, 5);

            Factory = factory;
            Port = port;
            Warehouses = new[] {warehouseA, warehouseB};
            Actors = new CargoCarrier[]
            {
                new Truck(routeMap, factory),
                new Truck(routeMap, factory),
                new Ship(routeMap, port)
            };
        }

        internal Factory Factory { get; }

        internal Port Port { get; }

        internal IReadOnlyCollection<Warehouse> Warehouses { get; }

        internal IReadOnlyCollection<CargoCarrier> Actors { get; }

        public static World CreateWorld(IEnumerable<char> shipments)
        {
            var world = new World();

            foreach (var destination in shipments)
            {
                var warehouse = world.Warehouses.First(w => w.Name == destination.ToString());
                world.Factory.ProduceCargo(warehouse);
            }

            return world;
        }

        public int CargosDelivered()
        {
            return Warehouses.Sum(w => w.CargoInStock);
        }

        public void NextTurn()
        {
            foreach (var actor in Actors)
            {
                actor.Travel();
            }

            // two-phase turn, so that unloading truck shouldn't allow ship to pick up the cargo and travel in the same turn
            foreach (var actor in Actors)
            {
                actor.UnloadCargo();
            }
        }
    }

    public interface ILocation
    {
        string Name { get; }
    }
}
