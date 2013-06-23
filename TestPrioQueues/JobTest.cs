using PrioQueues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestPrioQueues
{
    
    
    /// <summary>
    ///This is a test class for JobTest and is intended
    ///to contain all JobTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JobTest
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
        ///A test for ProcTime
        ///</summary>
        [TestMethod()]
        public void ProcTimeTest()
        {
            int type = 0;
            Job target = new Job(type);
            double expected = 0;
            double actual;
            target.ProcTime = expected;
            actual = target.ProcTime;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JobType
        ///</summary>
        [TestMethod()]
        public void JobTypeTest()
        {
            int type = 0;
            Job target = new Job(type);
            int expected = 0;
            int actual;
            target.JobType = expected;
            actual = target.JobType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ArrTime
        ///</summary>
        [TestMethod()]
        public void ArrTimeTest()
        {
            int type = 0;
            Job target = new Job(type);
            double expected = 0;
            double actual;
            target.ArrTime = expected;
            actual = target.ArrTime;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Job Constructor
        ///</summary>
        [TestMethod()]
        public void JobConstructorTest()
        {
            int type = 0;
            Job target = new Job(type);
            Assert.AreEqual(target.ArrTime,0);
            Assert.AreEqual(target.ProcTime, 0);
        }

        /// <summary>
        ///A test for Job Constructor
        ///</summary>
        [TestMethod()]
        public void JobConstructorTest1()
        {
            int type = 0;
            double arrTime = 3;
            double procTime = 4;
            Job target = new Job(type, arrTime, procTime);
            Assert.AreEqual(target.ArrTime, 3);
            Assert.AreEqual(target.ProcTime, 4);
        }
    }
}
