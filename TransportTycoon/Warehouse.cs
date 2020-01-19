using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TransportTycoon
{
    [DebuggerDisplay("{Name}")]
    internal sealed class Warehouse : IWarehouse
    {
        private readonly Queue<Cargo> _storage = new Queue<Cargo>();

        public Warehouse(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public int CargoInStock => _storage.Count;

        public void Store(Cargo cargo)
        {
            if (cargo == null) throw new ArgumentNullException(nameof(cargo));

            _storage.Enqueue(cargo);
        }
    }

    public interface IWarehouse : ILocation
    {
        void Store(Cargo cargo);
    }
}