using LocalAngle.Recruitment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LocalAngle.Test
{
    
    
    /// <summary>
    ///This is a test class for RemunerationRangeTest and is intended
    ///to contain all RemunerationRangeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RemunerationRangeTest
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
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ApproxToStringTest()
        {
            RemunerationRange target = new RemunerationRange();
            target.LowerBound = 35012m;
            target.Currency = "£";
            target.IsApproximate = true;
            string expected = "From £35K";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void DefiniteToStringTest()
        {
            RemunerationRange target = new RemunerationRange();
            target.LowerBound = 35000m;
            target.Currency = "£";
            target.IsApproximate = false;
            string expected = "From £35000.00";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void HourlyRateToStringTest()
        {
            RemunerationRange target = new RemunerationRange();
            target.LowerBound = 6.7m;
            target.UpperBound = 6.7m;
            target.Currency = "£";
            target.Period = Recurrence.Hour;
            target.IsApproximate = false;
            string expected = "£6.70 per Hour";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
