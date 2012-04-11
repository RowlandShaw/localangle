using System;
using LocalAngle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalAngle.Test
{
    
    
    /// <summary>
    ///This is a test class for PostCodeTest and is intended
    ///to contain all PostCodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PostCodeTest
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
        ///A test for Unit
        ///</summary>
        [TestMethod(), TestCategory("Postcode parsing")]
        public void FormattingTest()
        {
            string expected = "BH6 4LP"; // TODO: Initialize to an appropriate value
            Postcode target = new Postcode("BH64lp"); // TODO: Initialize to an appropriate value
            string actual = target.Unit;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        ///A test for Unit
        ///</summary>
        [TestMethod(), TestCategory("Postcode parsing")]
        public void MinorAreaTest()
        {
            string expected = "BH6 4LP"; // TODO: Initialize to an appropriate value
            Postcode target = new Postcode(expected); // TODO: Initialize to an appropriate value
            string actual = target.Unit;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Unit
        ///</summary>
        [TestMethod(), TestCategory("Postcode parsing")]
        public void MajorAreaTest()
        {
            string expected = "B33 8TH"; // TODO: Initialize to an appropriate value
            Postcode target = new Postcode(expected); // TODO: Initialize to an appropriate value
            string actual = target.Unit;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Unit
        ///</summary>
        [TestMethod(), TestCategory("Postcode parsing")]
        public void LondonValidTests()
        {
            string expected = "W10 4AA"; // TODO: Initialize to an appropriate value
            Postcode target = new Postcode(expected); // TODO: Initialize to an appropriate value
            string actual = target.Unit;
            Assert.AreEqual(expected, actual);

            expected = "EC1A 1BB"; // TODO: Initialize to an appropriate value
            target = new Postcode(expected); // TODO: Initialize to an appropriate value
            actual = target.Unit;
            Assert.AreEqual(expected, actual);

            expected = "WC1A 1AB"; // TODO: Initialize to an appropriate value
            target = new Postcode(expected); // TODO: Initialize to an appropriate value
            actual = target.Unit;
            Assert.AreEqual(expected, actual);

            expected = "W1B 3AW"; // TODO: Initialize to an appropriate value
            target = new Postcode(expected); // TODO: Initialize to an appropriate value
            actual = target.Unit;
            Assert.AreEqual(expected, actual);

            expected = "SW1Y 6YQ"; // TODO: Initialize to an appropriate value
            target = new Postcode(expected); // TODO: Initialize to an appropriate value
            actual = target.Unit;
            Assert.AreEqual(expected, actual);

            expected = "SW2 1AA"; // TODO: Initialize to an appropriate value
            target = new Postcode(expected); // TODO: Initialize to an appropriate value
            actual = target.Unit;
            Assert.AreEqual(expected, actual);

            expected = "EC50 9TY"; // TODO: Initialize to an appropriate value
            target = new Postcode(expected); // TODO: Initialize to an appropriate value
            actual = target.Unit;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Unit
        ///</summary>
        [TestMethod(), TestCategory("Postcode parsing"), ExpectedException(typeof(FormatException))]
        public void CentralLondonInvalidTest()
        {
            string expected = "WC1 1AB"; // TODO: Initialize to an appropriate value
            Postcode target = new Postcode(expected); // TODO: Initialize to an appropriate value
            Assert.Fail();
        }
    }
}
