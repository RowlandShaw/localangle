using LocalAngle.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

        #region Echo Tests

        /// <summary>
        /// Test using an echo service
        /// </summary>
        [TestMethod(), TestCategory("Echo tests")]
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
        [TestMethod(), TestCategory("Echo tests")]
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
        [TestMethod(), TestCategory("Echo tests")]
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

        /// <summary>
        /// Test using an echo service
        /// </summary>
        [TestMethod(), TestCategory("Echo tests")]
        public void PostLocalAngleTest()
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
            req.Method = "POST";

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
        [TestMethod(), TestCategory("Echo tests")]
        public void PostEchoTermIeTest()
        {
            Uri uri = new Uri("http://term.ie/oauth/example/echo_api.php?method=test");
            string consumerKey = "key";
            string consumerSecret = "secret";
            string token = "accesskey";
            string tokenSecret = "accesssecret";
            string expected = "method=test";
            OAuthCredentials creds = new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret);
            OAuthWebRequest req = OAuthWebRequest.Create(uri, creds);
            req.Method = "POST";

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

        #endregion

        #region Escaping test cases

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest1()
        {
            string value = "abcABC123";
            string expected = "abcABC123";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest2()
        {
            string value = "-._~";
            string expected = "-._~";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest3()
        {
            string value = "%";
            string expected = "%25";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest4()
        {
            string value = "+";
            string expected = "%2B";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest5()
        {
            string value = "&=*";
            string expected = "%26%3D%2A";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest6()
        {
            string value = "\n";
            string expected = "%0A";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString (space)
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest7()
        {
            string value = " ";
            string expected = "%20";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest8()
        {
            string value = "\u007f";
            string expected = "%7F";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString (2 byte UTF-8 character)
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest9()
        {
            string value = "\u0080";
            string expected = "%C2%80";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EscapeDataString (3 byte UTF-8 character)
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void EscapeDataStringTest10()
        {
            string value = "\u3001";
            string expected = "%E3%80%81";
            string actual;
            actual = OAuthWebRequest_Accessor.EscapeDataString(value);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Signature test cases

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GenerateHmacSignatureTest1()
        {
            string consumerSecret = "cs";
            string tokenSecret = string.Empty;
            string baseString = "bs";
            OAuthSignatureMethod method = OAuthSignatureMethod.HmacSha1;
            string expected = "egQqG5AJep5sJ7anhXju1unge2I=";
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateSignature(consumerSecret, tokenSecret, baseString, method);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GenerateHmacSignatureTest2()
        {
            string consumerSecret = "cs";
            string tokenSecret = "ts";
            string baseString = "bs";
            OAuthSignatureMethod method = OAuthSignatureMethod.HmacSha1;
            string expected = "VZVjXceV7JgPq/dOTnNmEfO0Fv8=";
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateSignature(consumerSecret, tokenSecret, baseString, method);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GenerateHmacSignatureTest3()
        {
            string consumerSecret = "cs";
            string tokenSecret = "ts";
            string baseString = "GET&http%3A%2F%2Fphotos.example.net%2Fphotos&file%3Dvacation.jpg%26oauth_consumer_key%3Ddpf43f3p2l4k3l03%26oauth_nonce%3Dkllo9940pd9333jh%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1191242096%26oauth_token%3Dnnch734d00sl2jdk%26oauth_version%3D1.0%26size%3Doriginal";
            OAuthSignatureMethod method = OAuthSignatureMethod.HmacSha1;
            string expected = "ZAcjW2U/wg+uve2N4scMMgTwZ8g="; // expected, per http://oauth.googlecode.com/svn/code/javascript/example/signature.html
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateSignature(consumerSecret, tokenSecret, baseString, method);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GeneratePlainSignatureTest1()
        {
            string consumerSecret = "djr9rjt0jd78jf88";
            string tokenSecret = "jjd999tj88uiths3";
            string baseString = "bs";
            OAuthSignatureMethod method = OAuthSignatureMethod.Plaintext;
            string expected = "djr9rjt0jd78jf88&jjd999tj88uiths3";
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateSignature(consumerSecret, tokenSecret, baseString, method);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GeneratePlainSignatureTest2()
        {
            string consumerSecret = "djr9rjt0jd78jf88";
            string tokenSecret = "jjd99$tj88uiths3";
            string baseString = "bs";
            OAuthSignatureMethod method = OAuthSignatureMethod.Plaintext;
            string expected = "djr9rjt0jd78jf88&jjd99%24tj88uiths3";
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateSignature(consumerSecret, tokenSecret, baseString, method);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GeneratePlainSignatureTest3()
        {
            string consumerSecret = "djr9rjt0jd78jf88";
            string tokenSecret = "";
            string baseString = "bs";
            OAuthSignatureMethod method = OAuthSignatureMethod.Plaintext;
            string expected = "djr9rjt0jd78jf88&";
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateSignature(consumerSecret, tokenSecret, baseString, method);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Base string test cases

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void GenerateBaseStringTest()
        {
            string method  = "GET";
            Uri uri = new Uri("http://photos.example.net/photos");
            IList<RequestParameter> source = new List<RequestParameter>();
            source.Add(new RequestParameter("file", "vacation.jpg"));
            source.Add(new RequestParameter("size", "original"));

            // these would normally be generated as part of the signing process
            source.Add(new RequestParameter("oauth_consumer_key", "dpf43f3p2l4k3l03"));
            source.Add(new RequestParameter("oauth_token", "nnch734d00sl2jdk"));
            source.Add(new RequestParameter("oauth_signature_method", "HMAC-SHA1"));
            source.Add(new RequestParameter("oauth_timestamp", "1191242096"));
            source.Add(new RequestParameter("oauth_nonce", "kllo9940pd9333jh"));
            source.Add(new RequestParameter("oauth_version", "1.0"));

            string expected = "GET&http%3A%2F%2Fphotos.example.net%2Fphotos&file%3Dvacation.jpg%26oauth_consumer_key%3Ddpf43f3p2l4k3l03%26oauth_nonce%3Dkllo9940pd9333jh%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1191242096%26oauth_token%3Dnnch734d00sl2jdk%26oauth_version%3D1.0%26size%3Doriginal";
            string actual;
            actual = OAuthWebRequest_Accessor.GenerateBaseString( method, uri, source);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Parameter normalization test cases

        /// <summary>
        ///A test for GenerateSignature
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LocalAngle.dll")]
        public void NormalizeParametersTest()
        {
            IList<RequestParameter> source = new List<RequestParameter>();
            source.Add(new RequestParameter("c", "hi there"));
            source.Add(new RequestParameter("z", "t"));
            source.Add(new RequestParameter("f", "25"));
            source.Add(new RequestParameter("a", "1"));
            source.Add(new RequestParameter("f", "50"));
            source.Add(new RequestParameter("f", "a"));
            source.Add(new RequestParameter("z", "p"));
            string expected = "a=1&c=hi%20there&f=25&f=50&f=a&z=p&z=t";
            string actual;
            actual = OAuthWebRequest_Accessor.NormalizeParameters(source);
            Assert.AreEqual(expected, actual);
        }

        #endregion

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