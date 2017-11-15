using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using LocalAngle.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalAngle;
using LocalAngle.Net;

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

        private IOAuthCredentials uatCredentials = new OAuthCredentials("ngcA6zb9mcLwj15Hyjy0vA", "jmOYyX43N3W5S0gHsX2lP7OzUPavVUEigTrsdvyirG4", "86389544-grD6t7NnRyVPyOFNwkH4ciL3rhdT2ayN1RmLMg1Q", "6h45JPBeXaMq0TngKolfku6M2tB6gmTmIQshBZgL8i8"); // TODO: Initialize to an appropriate value

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
            target.Tags = "gig-jazz";
            target.PublishStatus = PublishStatus.Active;
            col.Add(target);

            SpecialEvent target2 = new SpecialEvent();
            target2.Name = "Test event name";
            target2.Description = "Little bit of info about the event";
            target2.Location = new Postcode("IP1 5PH");
            target2.StartTime = new DateTime(2011, 11, 23, 11, 02, 00); // 1322046000000
            target2.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            target2.PublishStatus = PublishStatus.SoldOut;
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
            double range = 10F; // TODO: Initialize to an appropriate value
            IEnumerable<SpecialEvent> actual;
            actual = SpecialEvent.SearchNear(location, range, uatCredentials);
        }

        /// <summary>
        ///A test to verify trust level
        ///</summary>
        [TestMethod()]
        public void VerifyTrustTest()
        {
            Uri uri = new Uri(ApiHelper.BaseUri, "auth/verifytrust");
            string expected = "0"; // Expect the user token / app key associated to UAT to report no access

            OAuthWebRequest req = OAuthWebRequest.Create(uri, uatCredentials);

            // Get the response
            HttpWebResponse actualResponse = req.GetResponse() as HttpWebResponse;
            Assert.IsNotNull(actualResponse);

            // Read the message
            StreamReader reader = new StreamReader(actualResponse.GetResponseStream());
            string actual = reader.ReadToEnd();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            SpecialEvent target = new SpecialEvent(); // TODO: Initialize to an appropriate value
            target.StartTime = new DateTime(2012, 03, 31, 13, 45, 0);
            target.EndTime = new DateTime(2012, 03, 31, 13, 45, 59);
            target.VenueName = "UAT Land";
            target.Location = new Postcode("IP1 3RL"); // Completely made up postcode, but should be syntactically valid (heck, it might even be a real one)
            target.Tags = "gig"; // Pretend to be a live music event
            target.Save(uatCredentials);
            Assert.IsTrue(target.EventId > 0);
        }

        #region Comparison tests

        /// <summary>
        ///A test for op_LessThanOrEqual
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_LessThanOrEqualTestWhenEqual()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            right.Tags = "gig";

            bool expected = true;
            bool actual;
            actual = (left <= right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_LessThan
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_LessThanTestWhenEqual()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            right.Tags = "gig";

            bool expected = false;
            bool actual;
            actual = (left < right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_InequalityTestWhenEqual()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            right.Tags = "gig";

            bool expected = false;
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_EqualityTestWhenEqual()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            right.Tags = "gig";

            bool expected = true;
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_GreaterThanOrEqual
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_GreaterThanOrEqualTestWhenEqual()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            right.Tags = "gig";

            bool expected = true;
            bool actual;
            actual = (left >= right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_GreaterThan
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_GreaterThanTestWhenEqual()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            right.Tags = "gig";

            bool expected = false;
            bool actual;
            actual = (left > right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_LessThanOrEqual
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_LessThanOrEqualTestWhenRightGreater()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 24, 10, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 24, 10, 01, 02); //   1322046065000
            right.Tags = "gig";

            bool expected = true;
            bool actual;
            actual = (left <= right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_LessThan
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_LessThanTestWhenRightGreater()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 24, 10, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 24, 10, 01, 02); //   1322046065000
            right.Tags = "gig";

            bool expected = true;
            bool actual;
            actual = (left < right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_InequalityTestWhenRightGreater()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 24, 10, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 24, 10, 01, 02); //   1322046065000
            right.Tags = "gig";

            bool expected = true;
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_EqualityTestWhenRightGreater()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 24, 10, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 24, 10, 01, 02); //   1322046065000
            right.Tags = "gig";

            bool expected = false;
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_GreaterThanOrEqual
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_GreaterThanOrEqualTestWhenRightGreater()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 24, 10, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 24, 10, 01, 02); //   1322046065000
            right.Tags = "gig";

            bool expected = false;
            bool actual;
            actual = (left >= right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_GreaterThan
        ///</summary>
        [TestMethod(), TestCategory("Comparison tests")]
        public void op_GreaterThanTestWhenRightGreater()
        {
            SpecialEvent left = new SpecialEvent();
            left.Name = "Test event name";
            left.Description = "Little bit of info about the event";
            left.Location = new Postcode("IP1 5PH");
            left.StartTime = new DateTime(2011, 11, 23, 11, 00, 00); // 1322046000000
            left.EndTime = new DateTime(2011, 11, 23, 11, 01, 05); //   1322046065000
            left.Tags = "gig";

            SpecialEvent right = new SpecialEvent();
            right.Name = "Test event name";
            right.Description = "Little bit of info about the event";
            right.Location = new Postcode("IP1 5PH");
            right.StartTime = new DateTime(2011, 11, 24, 10, 00, 00); // 1322046000000
            right.EndTime = new DateTime(2011, 11, 24, 10, 01, 02); //   1322046065000
            right.Tags = "gig";

            bool expected = false;
            bool actual;
            actual = (left > right);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
