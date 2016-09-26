using NUnit.Framework;

namespace BMS.Tests
{
    [TestFixture]
    public class CoordinatesConverterTests
    {
        [Test]
        public void SanityTest_ITM_to_WGS84()
        {
            int x = 678000;
            int y = 230000;
            double lat;
            double lon;
            Coordinates.Converters.itm2wgs84(x, y, out lat, out lon);
            Assert.That(lat, Is.EqualTo(32.1143945).Within(0.4));
            Assert.That(lon, Is.EqualTo(35.1858782).Within(0.4));
        }
        [Test]
        public void SanityTest_ICS_to_WGS84()
        {
            int x = 1178000;
            int y = 180000;
            double lat;
            double lon;
            Coordinates.Converters.ics2wgs84(x, y, out lat, out lon);
            Assert.That(lat, Is.EqualTo(32.1143873).Within(0.4));
            Assert.That(lon, Is.EqualTo(35.1858200).Within(0.4));
        }
        [Test]
        public void X201724_Y645906_convert_to_lat_long()
        {
            int x = 201724;
            int y = 645906;
            double lat;
            double lon;
            Coordinates.Converters.itm2wgs84(x, y, out lat, out lon);
            Assert.That(lat, Is.EqualTo(31.905831));
            Assert.That(lon, Is.EqualTo(35.0166872));
        }
    }
}
