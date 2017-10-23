using NUnit.Framework;

namespace MarsRover
{
    [TestFixture]
    public class SurfaceAreaTest
    {
        private readonly SurfaceArea _area = new SurfaceArea();

        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 101)]
        [TestCase(50, 1, 4901)]
        [TestCase(100, 1, 9901)]
        [TestCase(47, 24, 4624)]
        [TestCase(1, 100, 100)]
        public void GridReferenceFromPosition(int top, int left, int expected)
        {
            var position = new Position(top, left);

            var actual = _area.GridReferenceFromPosition(position);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 101)]
        [TestCase(50, 1, 4901)]
        [TestCase(100, 1, 9901)]
        [TestCase(47, 24, 4624)]
        [TestCase(1, 100, 100)]
        public void PositionFromGridReference(int top, int left, int reference)
        {
            var position = _area.PositionFromGridReference(reference);

            Assert.AreEqual(left, position.Left);
            Assert.AreEqual(top, position.Top);
        }
    }
}
