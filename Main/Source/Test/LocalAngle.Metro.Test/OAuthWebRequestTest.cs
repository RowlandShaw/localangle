using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using LocalAngle.Net;

namespace LocalAngle.Net.Test
{
    
    
    /// <summary>
    ///This is a test class for OAuthWebRequestTest and is intended
    ///to contain all OAuthWebRequestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OAuthWebRequestTest
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
        /// Test using an echo service
        /// </summary>
        [TestMethod()]
        public void GetLocalAngleTest()
        {
            Uri uri = new Uri("http://api.angle.uk.com/oauth/1.0/events/nearby");
            string consumerKey = "ngcA6zb9mcLwj15Hyjy0vA";
            string consumerSecret = "jmOYyX43N3W5S0gHsX2lP7OzUPavVUEigTrsdvyirG4";
            string token = "86389544-grD6t7NnRyVPyOFNwkH4ciL3rhdT2ayN1RmLMg1Q";
            string tokenSecret = "6h45JPBeXaMq0TngKolfku6M2tB6gmTmIQshBZgL8i8";
            string expected = @"[{""Description"":""Little bit of info about the event"",""EndTime"":""\/Date(1322046065000+0000)\/"",""Name"":""Test event name"",""Postcode"":""IP1 5PH"",""StartTime"":""\/Date(1322046000000+0000)\/""},{""Description"":""Little bit of info about the event"",""EndTime"":""\/Date(1322046065000+0000)\/"",""Name"":""Test event name"",""Postcode"":""IP1 5PH"",""StartTime"":""\/Date(1322046000000+0000)\/""}]";
            OAuthWebRequest req = OAuthWebRequest.Create(uri, new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret));
            req.RequestParameters.Add(new RequestParameter("location", "IP1 5PH"));
            req.RequestParameters.Add(new RequestParameter("range", 0f.ToString()));

            // First check the constructor did "the right thing"
            Assert.AreEqual(consumerKey, req.OAuthCredentials.ConsumerKey);
            Assert.AreEqual(consumerSecret, req.OAuthCredentials.ConsumerSecret);
            Assert.AreEqual(token, req.OAuthCredentials.Token);
            Assert.AreEqual(tokenSecret, req.OAuthCredentials.TokenSecret);

            // Get the response
            HttpWebResponse actualResponse = req.GetResponse() as HttpWebResponse;
            Assert.IsNotNull(actualResponse);

            // Read the message
            StreamReader reader = new StreamReader(actualResponse.GetResponseStream());
            string actual = reader.ReadToEnd();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test using an echo service
        /// </summary>
        /// <remarks>Caution, this server appears to URI decode twice, which can hide bugs in our code</remarks>
        [TestMethod()]
        public void GetEchoMadGexTest()
        {
            Uri uri = new Uri("http://echo.lab.madgex.com/echo.ashx?method=test");
            string consumerKey = "key";
            string consumerSecret = "secret";
            string token = "accesskey";
            string tokenSecret = "accesssecret";
            string expected = "method=test";
            OAuthCredentials creds = new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret);
            OAuthWebRequest req = OAuthWebRequest.Create(uri, creds);

            // First check the constructor did "the right thing"
            Assert.AreEqual(consumerKey, req.OAuthCredentials.ConsumerKey);
            Assert.AreEqual(consumerSecret, req.OAuthCredentials.ConsumerSecret);
            Assert.AreEqual(token, req.OAuthCredentials.Token);
            Assert.AreEqual(tokenSecret, req.OAuthCredentials.TokenSecret);

            // Get the response
            HttpWebResponse actualResponse = req.GetResponse() as HttpWebResponse;
            Assert.IsNotNull(actualResponse);

            // Read the message
            StreamReader reader = new StreamReader(actualResponse.GetResponseStream());
            string actual = reader.ReadToEnd();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test using an echo service
        /// </summary>
        /// <remarks>This test server appears to be a bit pernickety, and has been double checked with Perl's Net::OAuth (which in turn, Twitter works with)</remarks>
        [TestMethod()]
        public void GetEchoTermIeTest()
        {
            Uri uri = new Uri("http://term.ie/oauth/example/echo_api.php?method=test");
            string consumerKey = "key";
            string consumerSecret = "secret";
            string token = "accesskey";
            string tokenSecret = "accesssecret";
            string expected = "method=test";
            OAuthCredentials creds = new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret);
            OAuthWebRequest req = OAuthWebRequest.Create(uri, creds);

            // First check the constructor did "the right thing"
            Assert.AreEqual(consumerKey, req.OAuthCredentials.ConsumerKey);
            Assert.AreEqual(consumerSecret, req.OAuthCredentials.ConsumerSecret);
            Assert.AreEqual(token, req.OAuthCredentials.Token);
            Assert.AreEqual(tokenSecret, req.OAuthCredentials.TokenSecret);

            // Get the response
            HttpWebResponse actualResponse = req.GetResponse() as HttpWebResponse;
            Assert.IsNotNull(actualResponse);

            // Read the message
            StreamReader reader = new StreamReader(actualResponse.GetResponseStream());
            string actual = reader.ReadToEnd();

            Assert.AreEqual(expected, actual);
        }

        #region URI normalization test cases

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void UriNormalizationTest()
        {
            Uri uri = new Uri("HTTP://Example.com:80/resource?id=123");
            string expected = "http://example.com/resource";
            OAuthWebRequest req = OAuthWebRequest.Create(uri, new OAuthCredentials("", "", "", ""));

            string actual = req.RequestUri.AbsoluteUri;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}