using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TransportTycoon
{
    [DebuggerDisplay("{Name}")]
    internal sealed class Port : IPort
    {
        private readonly Queue<Cargo> _storage = new Queue<Cargo>();

        public string Name => "Port";
        
        public bool TakeCargo(out Cargo cargo)
        {
            return _storage.TryDequeue(out cargo);
        }

        public void Store(Cargo cargo)
        {
            if (cargo == null) throw new ArgumentNullException(nameof(cargo));

            _storage.Enqueue(cargo);
        }
    }

    public interface IPort : ISupplier, IWarehouse
    {
    }
}