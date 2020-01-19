using System;

namespace TransportTycoon
{
    internal class CargoCarrier
    {
        private readonly IRouteMap _routeMap;
        private readonly ISupplier _supplier;

        private Route _route;
        private Cargo _cargo;

        protected CargoCarrier(IRouteMap routeMap, ISupplier supplier)
        {
            _routeMap = routeMap ?? throw new ArgumentNullException(nameof(routeMap));
            _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));

            _route = routeMap.FromLocation(supplier);
        }

        public void Travel()
        {
            TryLoadCargo();

            TryTravel();
        }

        public void UnloadCargo()
        {
            TryUnloadCargo();
        }

        private bool TryLoadCargo()
        {
            if (!_route.Completed || _route.Destination != _supplier)
                return false;

            if (!_supplier.TakeCargo(out _cargo))
            {
                // no more cargo at factory, idling
                return false;
            }

            var newRoute = _routeMap.CreateRoute(_supplier, _cargo.Destination);
            _route = newRoute;

            return true;
        }

        private bool TryTravel()
        {
            if (_route.Completed)
            {
                return false;
            }

            _route.Travel();

            return true;
        }

        private bool TryUnloadCargo()
        {
            if (!_route.Completed || _route.Destination == _supplier)
            {
                return false;
            }

            var warehouse = (IWarehouse)_route.Destination;

            warehouse.Store(_cargo);
            _cargo = null;

            var returnRoute = _routeMap.CreateRoute(_route.Destination, _supplier);
            _route = returnRoute;

            return true;
        }
    }
}