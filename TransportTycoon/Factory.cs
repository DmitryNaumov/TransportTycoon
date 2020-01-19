using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TransportTycoon
{
    [DebuggerDisplay("{Name}")]
    internal sealed class Factory : IFactory
    {
        private readonly Queue<Cargo> _storage = new Queue<Cargo>();

        public string Name => "Factory";

        public void ProduceCargo(Warehouse destination)
        {
            var cargo = new Cargo(destination);
            _storage.Enqueue(cargo);
        }

        public bool TakeCargo(out Cargo cargo)
        {
            return _storage.TryDequeue(out cargo);
        }
    }

    public interface IFactory : ISupplier
    {
    }

    public interface ISupplier : ILocation
    {
        bool TakeCargo(out Cargo cargo);
    }
}