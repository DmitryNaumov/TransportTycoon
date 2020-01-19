using Xunit;

namespace TransportTycoon.Tests
{
    public class SoftwareParkFixture
    {
        [Theory]
        [InlineData("A", 5)]
        [InlineData("AB", 5)]
        [InlineData("BB", 5)]
        [InlineData("ABB", 7)]
        public void Exercise1(string destinations, int expectedHours)
        {
            var world = World.CreateWorld(destinations);

            var turns = 0;
            while (world.CargosDelivered() < destinations.Length)
            {
                world.NextTurn();
                turns++;
            }

            Assert.Equal(expectedHours, turns);
        }
    }
}
