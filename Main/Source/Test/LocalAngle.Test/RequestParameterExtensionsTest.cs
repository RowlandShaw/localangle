using LocalAngle.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LocalAngle.Net.Test
{
    
    
    /// <summary>
    ///This is a test class for RequestParameterExtensionsTest and is intended
    ///to contain all RequestParameterExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RequestParameterExtensionsTest
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
        ///A test for GetParameters
        ///</summary>
        [TestMethod()]
        public void GetParametersTest()
        {
            Uri uri = new Uri("http://term.ie/oauth/example/echo_api.php?method=test");
            int expectedCount = 1;
            string expectedName = "method";
            string expectedValue = "test";

            IList<RequestParameter> actual;
            actual = RequestParameterExtensions.GetParameters(uri);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCount, actual.Count);
            Assert.AreEqual(expectedName, actual[0].Name);
            Assert.AreEqual(expectedValue, actual[0].Value);
        }
    }
}
