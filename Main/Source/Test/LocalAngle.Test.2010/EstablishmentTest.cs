using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using LocalAngle.Eatndrink;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalAngle;
using LocalAngle.Net;

namespace LocalAngle.Test
{
    /// <summary>
    ///This is a test class for SpecialEventTest and is intended
    ///to contain all SpecialEventTest Unit Tests
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
            target.ContactNumbers.Add(new ContactNumber() { ContactType = ContactType.Twitter, Detail = "LocalAngle" } );
            col.Add(target);

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Establishment>));
            ser.WriteObject(stream1, col);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            System.Diagnostics.Debug.WriteLine(sr.ReadToEnd());
        }

    }
}
