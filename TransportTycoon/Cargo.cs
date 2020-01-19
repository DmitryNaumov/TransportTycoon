using System;
using System.Diagnostics;

namespace TransportTycoon
{
    [DebuggerDisplay("To {Destination.Name}")]
    public sealed class Cargo
    {
        public Cargo(ILocation destination)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public ILocation Destination { get; }
    }
}