using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using BMS.Infra;
using BMS.Infra.DataTypes;
using NUnit.Framework;

namespace BMS.Tests
{
    [TestFixture]
    public class ParseAtmXmlTests
    {
        [Test]
        public void XmlSerializationTest()
        {
            ATMs atm = new ATMs {BankomatsList = new[] {new ATM(), new ATM()}};

            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(ATMs));
            using (TextWriter textWriter = new StringWriter(sb))
            {
                serializer.Serialize(textWriter, atm);
                Console.WriteLine(sb.ToString());
            }
        }

        [Test]
        public void ParseXmlAndGenerateMapMarkersTest()
        {
            const string inputXmlFile = @"..\..\..\..\..\modiin-atm.xml";
            string inputXmlPath = Path.GetFullPath(Path.Combine(AssemblyDirectory, inputXmlFile));
            Assert.That(File.Exists(inputXmlPath), Is.True);

            XmlSerializer serializer = new XmlSerializer(typeof(ATMs));
            using (FileStream fs = new FileStream(inputXmlPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ATMs bankomatsList = (ATMs) serializer.Deserialize(fs);

                Assert.That(bankomatsList.BankomatsList.Length, Is.EqualTo(31));
                Assert.That(bankomatsList.BankomatsList.Any(b=>b.X==0 || b.Y==0), Is.False);

                StringBuilder markers = new StringBuilder();
                JS_ITM_Adapter adapter = new JS_ITM_Adapter();
                bankomatsList.BankomatsList
                    .Select(bankoMat => new ITMPoint(bankoMat.X, bankoMat.Y))
                    .Select(itmPoint=>adapter.Convert_ITM_to_LatLon(itmPoint))
                .Each(point=> 
                {
                    markers.AppendLine("new google.maps.Marker({position: {lat: " + point.Latitude + ", lng: " + point.Longitude + "}, map: map});");
                });
                Console.WriteLine(markers.ToString());
            }
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }

    public class ATMs
    {
        [XmlElement(ElementName = "ATM")]
        public ATM[] BankomatsList;
    }

    public class ATM
    {
        [XmlElement(ElementName = "קוד_בנק")]
        public int BanksCode;
        [XmlElement(ElementName = "שם_בנק")]
        public string BanksName;
        [XmlElement(ElementName = "קוד_סניף")]
        public int BeanchsCode;
        [XmlElement(ElementName = "כתובת_ה-ATM")]
        public string Address;
        [XmlElement(ElementName = "ישוב")]
        public string Location;
        [XmlElement(ElementName = "קואורדינטת_X")]
        public int X;
        [XmlElement(ElementName = "קואורדינטת_Y")]
        public int Y;
    }

    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}