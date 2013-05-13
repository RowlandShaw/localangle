using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Eatndrink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalAngle.Test
{
    /// <summary>
    ///This is a test class for EstablishmentTest and is intended
    ///to contain all EstablishmentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EstablishmentTest
    {
        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private IOAuthCredentials uatCredentials = new OAuthCredentials("ngcA6zb9mcLwj15Hyjy0vA", "jmOYyX43N3W5S0gHsX2lP7OzUPavVUEigTrsdvyirG4", "86389544-grD6t7NnRyVPyOFNwkH4ciL3rhdT2ayN1RmLMg1Q", "6h45JPBeXaMq0TngKolfku6M2tB6gmTmIQshBZgL8i8"); // TODO: Initialize to an appropriate value

        /// <summary>
        ///A test for SpecialEvent Constructor
        ///</summary>
        [TestMethod()]
        public void EstablishmentConstructorTest()
        {
            List<Establishment> col = new List<Establishment>();

            Establishment target = new Establishment();
            target.Name = "Test establishment";
            target.Description = "Little bit of info about the place";
            target.Location = new Postcode("IP3 9GG");
            target.DeliveryPoint = "15";
            target.ContactNumbers.Add(new ContactNumber() { ContactType = ContactType.Twitter, Detail = "LocalAngle" });
            col.Add(target);

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Establishment>));
            ser.WriteObject(stream1, col);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            System.Diagnostics.Debug.WriteLine(sr.ReadToEnd());
        }

        /// <summary>
        ///A test for LoadJson
        ///</summary>
        [TestMethod()]
        public void LoadJsonTest()
        {
            string data = @"[{""SiteId"":690,""Tag"":""plough"",""Name"":""The Plough"",""DeliveryPoint"":"""",""Postcode"":""IP41AD"",""CategoryKey"":""pubs"",""SubCategoryKey"":""aipswich"",""SubCategoryName"":""Ipswich"",""Status"":""live"",""Flags"":""wifi"",""Website"":null,""OrderUrl"":null,""FoodSafetyId"":169982,""FoodSafetyRatingKey"":""fhrs_5_en-GB"",""Hygene"":5,""Structural"":5,""Management"":0,""FoodSafetyInspectionDate"":""\/Date(1315350000000)\/""},
{""SiteId"":690,""Tag"":""pizzaexp"",""Name"":""Pizza Express"",""DeliveryPoint"":24,""Postcode"":""IP13HD"",""CategoryKey"":""restaurants"",""SubCategoryKey"":""italian"",""SubCategoryName"":""Italian"",""Status"":""live"",""Flags"":""takeaway"",""Website"":null,""OrderUrl"":null,""FoodSafetyId"":175219,""FoodSafetyRatingKey"":""fhrs_5_en-GB"",""Hygene"":0,""Structural"":0,""Management"":0,""FoodSafetyInspectionDate"":""\/Date(1303858800000)\/""},
{""SiteId"":690,""Tag"":""oyster"",""Name"":""The Oyster Reach (Beefeater)"",""DeliveryPoint"":"""",""Postcode"":""IP28ND"",""CategoryKey"":""restaurants"",""SubCategoryKey"":""european"",""SubCategoryName"":""European"",""Status"":""live"",""Flags"":""nosmoking,parking"",""Website"":null,""OrderUrl"":null,""FoodSafetyId"":null,""FoodSafetyRatingKey"":null,""Hygene"":null,""Structural"":null,""Management"":null,""FoodSafetyInspectionDate"":null}]";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)); // TODO: Initialize to an appropriate value
            IEnumerable<Establishment> actual;
            int expectedCount = 3;

            actual = Establishment.LoadJson(stream);

            int actualCount = actual.Count();
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        ///A test for SearchNear
        ///</summary>
        ///<remarks>
        ///Rubbish test - just checks we don't blow up
        ///</remarks>
        [TestMethod()]
        public void SearchNearTest()
        {
            Postcode location = new Postcode("IP3 9SJ"); // TODO: Initialize to an appropriate value
            double range = 10F; // TODO: Initialize to an appropriate value
            IEnumerable<Establishment> actual;
            actual = Establishment.SearchNear(location, range, uatCredentials);
        }
    }
}
