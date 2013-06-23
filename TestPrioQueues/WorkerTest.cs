using PrioQueues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestPrioQueues
{
    
    
    /// <summary>
    ///This is a test class for WorkerTest and is intended
    ///to contain all WorkerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WorkerTest
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
        ///A test for Worker Constructor
        ///</summary>
        [TestMethod()]
        public void WorkerConstructorTest()
        {
            Worker target = new Worker();
            Assert.AreEqual(target.EndTime, 0);
        }

        /// <summary>
        ///A test for EndTime
        ///</summary>
        [TestMethod()]
        public void EndTimeTest()
        {
            Worker target = new Worker();
            double expected = 0;
            double actual;
            target.EndTime = expected;
            actual = target.EndTime;
            Assert.AreEqual(expected, actual);
        }
    }
}
