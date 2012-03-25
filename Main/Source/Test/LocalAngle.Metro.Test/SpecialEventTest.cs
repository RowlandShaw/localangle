using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using LocalAngle.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LocalAngle.Entities.Test
{
    /// <summary>
    ///This is a test class for SpecialEventTest and is intended
    ///to contain all SpecialEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpecialEventTest
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


        /// <summary>
        ///A test for SpecialEvent Constructor
        ///</summary>
        [TestMethod()]
        public void SpecialEventConstructorTest()
        {
            List<SpecialEvent> col = new List<SpecialEvent>();

            SpecialEvent target = new SpecialEvent();
            target.Name = "Test event name";
            target.Description = "Little bit of info about the event";
            target.Location = new Postcode("IP1 5PH");
            target.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            target.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            col.Add(target);

            SpecialEvent target2 = new SpecialEvent();
            target2.Name = "Test event name";
            target2.Description = "Little bit of info about the event";
            target2.Location = new Postcode("IP1 5PH");
            target2.StartTime = new DateTime(2011, 11, 23, 11, 02, 00); // 1322046000000
            target2.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            col.Add(target2);

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));
            ser.WriteObject(stream1, col);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            System.Diagnostics.Debug.WriteLine(sr.ReadToEnd());
        }

        /// <summary>
        ///A test for SearchNear
        ///</summary>
        [TestMethod()]
        public void SearchNearTest()
        {
            Postcode location = new Postcode("IP3 9SJ"); // TODO: Initialize to an appropriate value
            double range = 0F; // TODO: Initialize to an appropriate value
            IOAuthCredentials credentials = new OAuthCredentials("ngcA6zb9mcLwj15Hyjy0vA", "jmOYyX43N3W5S0gHsX2lP7OzUPavVUEigTrsdvyirG4", "86389544-grD6t7NnRyVPyOFNwkH4ciL3rhdT2ayN1RmLMg1Q", "6h45JPBeXaMq0TngKolfku6M2tB6gmTmIQshBZgL8i8"); // TODO: Initialize to an appropriate value
            IEnumerable<SpecialEvent> actual;
            actual = SpecialEvent.SearchNear(location, range, credentials);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
