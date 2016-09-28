using BMS.Infra;
using BMS.Infra.DataTypes;
using BMS.Infra.ISR84LIB;
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
            Converters.itm2wgs84(x, y, out lat, out lon);
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
            Converters.ics2wgs84(x, y, out lat, out lon);
            Assert.That(lat, Is.EqualTo(32.1143873).Within(0.4));
            Assert.That(lon, Is.EqualTo(35.1858200).Within(0.4));
        }
        [Test, Explicit]
        public void X201724_Y645906_convert_to_lat_lon()
        {
            int x = 201724;
            int y = 645906;
            double lat;
            double lon;
            Converters.itm2wgs84(x, y, out lat, out lon);
            Assert.That(lat, Is.EqualTo(31.905831));
            Assert.That(lon, Is.EqualTo(35.0166872));
        }
    }

    [TestFixture]
    public class JS_ITM_Adapeter_Tests
    {
        [Test]
        public void X201724_Y645906_convert_to_lat_lon()
        {
            JS_ITM_Adapter adapter = new JS_ITM_Adapter();
            LatLonPoint latLonPoint = adapter.Convert_ITM_to_LatLon(new ITMPoint(201724, 645906));
            Assert.That(latLonPoint.Latitude, Is.EqualTo(31.905831).Within(0.005));
            Assert.That(latLonPoint.Longitude, Is.EqualTo(35.0166872).Within(0.005));
        }
    }
}
