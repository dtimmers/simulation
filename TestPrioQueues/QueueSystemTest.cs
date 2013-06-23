using PrioQueues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MathNet.Numerics.Statistics;
using MathNet.Numerics;
using Troschuetz.Random;
using System.Collections.Generic;

namespace TestPrioQueues
{
    
    
    /// <summary>
    ///This is a test class for QueueSystemTest and is intended
    ///to contain all QueueSystemTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QueueSystemTest
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
        ///A test for QueueSystem Constructor without a seed
        ///</summary>
        [TestMethod()]
        public void QueueSystemConstructorTest()
        {
            int numWorkers = 6;
            Job tempJob = new Job(0);

            QueueSystem target = CreateSystem(numWorkers);

            Assert.AreEqual(3, target.NumJobTypes, "the number of types do not match up");
            Assert.AreEqual("Listings", target.QueueNames[0], "the first queuename does not match up");
            Assert.AreEqual(1, target.Queues[0].ArrTime.Mean);
            Assert.AreEqual(2, target.Queues[0].ProcTime.Mean);
            Assert.AreEqual("Q&A", target.QueueNames[1], "the second queuename does not match up");
            Assert.AreEqual("ReBlast", target.QueueNames[2], "the third queuename does not match up");
            Assert.AreEqual(6, target.Workers.Count, "wrong number of workers");
            for (int i = 0; i < numWorkers; i++)
            {
                Assert.AreEqual(0, target.Workers[i].EndTime, "wrong setup with workers");
            }
        }

        /// <summary>
        ///A test for QueueSystem Constructor with a seed
        ///</summary>
        [TestMethod()]
        public void QueueSystemConstructorTest1()
        {
            int numWorkers = 6;
            int seed = 1;
            Job tempJob = new Job(0);

            QueueSystem target = CreateSystemSeed(numWorkers, seed);
            ALFGenerator rand = new ALFGenerator(seed);
            ExponentialDistribution rv = new ExponentialDistribution(rand);

            Assert.AreEqual(3, target.NumJobTypes, "the number of types do not match up");
            Assert.AreEqual("Listings", target.QueueNames[0], "the first queuename does not match up");
            Assert.AreEqual(1, target.Queues[0].ArrTime.Mean);
            Assert.AreEqual(2, target.Queues[0].ProcTime.Mean);
            Assert.AreEqual("Q&A", target.QueueNames[1], "the second queuename does not match up");
            Assert.AreEqual("ReBlast", target.QueueNames[2], "the third queuename does not match up");

            for (int i = 0; i < target.NumJobTypes; i++)
            {
                tempJob.JobType = i;
                rv.Lambda = 1 / (target.Queues[i].ArrTime.Mean);
                tempJob.ArrTime = rv.NextDouble();
                rv.Lambda = 1 / (target.Queues[i].ProcTime.Mean);
                tempJob.ProcTime = rv.NextDouble();
                Assert.AreEqual(i, target.JobQueue[i][0].JobType, "jobtype");
                Assert.AreEqual(tempJob.ArrTime, target.JobQueue[i][0].ArrTime, "arrival time");
                Assert.AreEqual(tempJob.ProcTime, target.JobQueue[i][0].ProcTime, "proc time");
            }

            Assert.AreEqual(6, target.Workers.Count, "wrong number of workers");
            for (int i = 0; i < numWorkers; i++)
            {
                Assert.AreEqual(0, target.Workers[i].EndTime, "wrong setup with workers");
            }
        }


        /// <summary>
        ///A test for CreateRQueue
        ///</summary>
        [TestMethod()]
        public void CreateRQueueTest()
        {
            int numWorkers = 3;
            int seed = 0;
            QueueSystem target = CreateSystemSeed(numWorkers, seed);
            string name = "Listings";
            double[] parameters = new double[] { 10.0, 20.0 };
            ALFGenerator rand = new ALFGenerator(seed);
            RQueue actual = target.CreateRQueue(name, parameters, rand);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(10, actual.ArrTime.Mean);
            Assert.AreEqual(20, actual.ProcTime.Mean);
        }

        /// <summary>
        ///A test for addJobs
        ///</summary>
        [TestMethod()]
        public void addJobsTest()
        {
            int seed = 1;
            int numWorkers = 3;
            QueueSystem target = CreateSystemSeed(numWorkers,seed);
            QueueSystem check = CreateSystemSeed(numWorkers,seed);

            //set the job times to appropriate times for both the target and check
            target.JobQueue[0][0].ArrTime = 9;
            target.JobQueue[1][0].ArrTime = 7;
            target.JobQueue[2][0].ArrTime = 3;
            target.NextWorkerTime = 12;

            check.JobQueue[0][0].ArrTime = 9;
            check.JobQueue[1][0].ArrTime = 7;
            check.JobQueue[2][0].ArrTime = 3;
            check.NextWorkerTime = 12;


            int jobType = 1;
            target.addJobs(jobType);
            //the jobs of type 0 are not updated
            Assert.AreEqual(1, target.JobQueue[0].Count);
            Assert.AreEqual(9, target.JobQueue[0][0].ArrTime = 9);
            
            //update the jobs of type 1
            double lastArrtime = 7;
            while (lastArrtime < 12)
            {
                check.JobQueue[jobType].Add(check.generateJob(jobType, lastArrtime));
                lastArrtime = check.JobQueue[jobType][check.JobQueue[jobType].Count - 1].ArrTime;
            }
            Assert.AreEqual(check.JobQueue[jobType].Count, target.JobQueue[jobType].Count);
            for (int i = 0; i < check.JobQueue[jobType].Count; i++)
            {
                Assert.AreEqual(check.JobQueue[jobType][i].ArrTime, target.JobQueue[jobType][i].ArrTime);
                Assert.AreEqual(check.JobQueue[jobType][i].ProcTime, target.JobQueue[jobType][i].ProcTime);
            }

            //update the jobs of type 2
            jobType = 2;
            lastArrtime = 3;
            while (lastArrtime < 12)
            {
                check.JobQueue[jobType].Add(check.generateJob(jobType, lastArrtime));
                lastArrtime = check.JobQueue[jobType][check.JobQueue[jobType].Count - 1].ArrTime;
            }
            Assert.AreEqual(check.JobQueue[jobType].Count, target.JobQueue[jobType].Count);
            for (int i = 0; i < check.JobQueue[jobType].Count; i++)
            {
                Assert.AreEqual(check.JobQueue[jobType][i].ArrTime, target.JobQueue[jobType][i].ArrTime);
                Assert.AreEqual(check.JobQueue[jobType][i].ProcTime, target.JobQueue[jobType][i].ProcTime);
            }
            
        }

        /// <summary>
        ///A test for generateJob
        ///</summary>
        [TestMethod()]
        public void generateJobTest()
        {
            int numWorkers = 3;
            int seed = 1;
            int type = 1;
            Job actual;

            QueueSystem target = CreateSystemSeed(numWorkers, seed);
            ALFGenerator rand = new ALFGenerator(seed);

            //to get random number generator at the same position as the one in target
            for (int i = 0; i < 6; i++)
            {
                rand.NextDouble();
            }

            Assert.AreEqual(rand.NextDouble(), target.Rand.NextDouble());

            //needs update if distributions of arrival times and processing times change
            double arrTime = RandomExp(rand, 1 / (target.Queues[type].ArrTime.Mean));
            double procTime = RandomExp(rand, 1 / (target.Queues[type].ProcTime.Mean));

            actual = target.generateJob(type, 0);
            Assert.AreEqual(actual.ArrTime, arrTime, "wrong arrival time");
            Assert.AreEqual(actual.ProcTime, procTime, "wrong processing time");

            arrTime = RandomExp(rand, 1 / (target.Queues[type].ArrTime.Mean));
            procTime = RandomExp(rand, 1 / (target.Queues[type].ProcTime.Mean));

            actual = target.generateJob(type, 5);
            Assert.AreEqual(actual.ArrTime, arrTime + 5, "wrong arrival time");
            Assert.AreEqual(actual.ProcTime, procTime, "wrong processing time");
        }

        /// <summary>
        ///A test for resetData
        ///</summary>
        [TestMethod()]
        public void resetDataTest()
        {
            int numWorkers = 3;
            QueueSystem target = CreateSystem(numWorkers);
            target.singleRun();
            Assert.IsTrue(target.ExportData.Count >= 1, "number of entries should be positive");
            target.resetData();
            Assert.IsTrue(target.ExportData.Count == 0); 
        }

        /// <summary>
        ///A test for updateWorkers
        ///</summary>
        [TestMethod()]
        public void updateWorkersTest()
        {
            int numWorkers = 3;
            int seed = 1;
            QueueSystem target = CreateSystemSeed(numWorkers, seed);
            //******************************************
            //        Case without priority
            //******************************************
            //Test Case 1
            //Initially all workers endtimes are zero so job is handled by first worker
            //while second worker picks up next job
            target.updateWorkers(2, 4, false);

            Assert.AreEqual(1, target.NextWorkerIndex, "Case 1: job should be assigned to 1 but instead " + target.NextWorkerIndex);
            Assert.AreEqual(0, target.NextWorkerTime, "Case 1: next worker should be available at time 0 but instead at time" + target.NextWorkerTime);

            Assert.AreEqual(4, target.Workers[0].EndTime, "Case 1 worker 1 failed, expected 4 but received " + target.Workers[0].EndTime);
            Assert.AreEqual(0, target.Workers[1].EndTime, "Case 1 worker 2 failed, expected 0 but received " + target.Workers[1].EndTime);
            Assert.AreEqual(0, target.Workers[2].EndTime, "Case 1 worker 3 failed, expected 0 but received " + target.Workers[2].EndTime);

            //Test case 2
            target.Workers[0].EndTime = 4;
            target.Workers[1].EndTime = 1;
            target.Workers[2].EndTime = 2;
            target.NextWorkerIndex = 1;
            target.NextWorkerTime = 1;

            target.updateWorkers(3, 4, false);
            //Job is handled by worker 1 while worker 2 picks up next job
            Assert.AreEqual(2, target.NextWorkerIndex, "Case 2: job should be assigned to 2 but instead " + target.NextWorkerIndex);
            Assert.AreEqual(0, target.NextWorkerTime, "Case 2: next worker should be available at time 0 but instead at time " + target.NextWorkerTime);

            Assert.AreEqual(1, target.Workers[0].EndTime, "Case 2 worker 1 failed, expected 1 but received " + target.Workers[0].EndTime);
            Assert.AreEqual(4, target.Workers[1].EndTime, "Case 2 worker 2 failed, expected 4 but received " + target.Workers[1].EndTime);
            Assert.AreEqual(0, target.Workers[2].EndTime, "Case 2 worker 3 failed, expected 0 but received " + target.Workers[2].EndTime);

            //Test case 3
            target.Workers[0].EndTime = 8;
            target.Workers[1].EndTime = 7;
            target.Workers[2].EndTime = 4;
            target.NextWorkerIndex = 2;
            target.NextWorkerTime = 4;

            target.updateWorkers(4, 1, false);
            //Job is handled by worker 2 while worker 2 also picks up next job
            Assert.AreEqual(2, target.NextWorkerIndex, "Case 3: job should be assigned to 2 but instead " + target.NextWorkerIndex);
            Assert.AreEqual(1, target.NextWorkerTime, "Case 3: next worker should be available at time 1 but instead at time " + target.NextWorkerTime);

            Assert.AreEqual(4, target.Workers[0].EndTime, "Case 3 worker 1 failed, expected 4 but received " + target.Workers[0].EndTime);
            Assert.AreEqual(3, target.Workers[1].EndTime, "Case 3 worker 2 failed, expected 3 but received " + target.Workers[1].EndTime);
            Assert.AreEqual(1, target.Workers[2].EndTime, "Case 3 worker 3 failed, expected 1 but received " + target.Workers[2].EndTime);

            //******************************************
            //           Case with priority
            //******************************************
            //Test case 4
            target.Workers[0].EndTime = 4;
            target.Workers[1].EndTime = 3;
            target.Workers[2].EndTime = 2;
            target.NextWorkerIndex = 2;
            target.NextWorkerTime = 2;

            target.updateWorkers(1, 3, true);
            //Job is handled by worker 2 while worker 1 picks up next job
            Assert.AreEqual(1, target.NextWorkerIndex, "Case 4: job should be assigned to 1 but instead " + target.NextWorkerIndex);
            Assert.AreEqual(3, target.NextWorkerTime, "Case 4: next worker should be available at time 3 but instead at time " + target.NextWorkerTime);

            Assert.AreEqual(4, target.Workers[0].EndTime, "Case 4 worker 1 failed, expected 41 but received " + target.Workers[0].EndTime);
            Assert.AreEqual(3, target.Workers[1].EndTime, "Case 4 worker 2 failed, expected 3 but received " + target.Workers[1].EndTime);
            Assert.AreEqual(5, target.Workers[2].EndTime, "Case 4 worker 3 failed, expected 5 but received " + target.Workers[2].EndTime);
        }

        /// <summary>
        ///A test for singleRun
        ///</summary>
        [TestMethod()]
        public void singleRunTest()
        {
            string[] names = { "Listings", "Q&A", "ReBlast" };
            int numWorkers = 3;

            Job[] jobList = new Job[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                jobList[i] = new Job(i);
            }
            double[] arrTimes = new double[names.Length];
            double[] procTimes = new double[names.Length];
            double arrMin = 0;
            int minIndex = 0;
            int minIndex2 = 0;

            //Check with seed 1

            int seed = 1;
            QueueSystem target = CreateSystemSeed(numWorkers, seed);
            target.singleRun();

            //initialize the arrival and processing times
            ALFGenerator rand = new ALFGenerator(seed);
            for (int i = 0; i < names.Length; i++)
            {
                jobList[i].ArrTime = RandomExp(rand, 1 / (target.Queues[i].ArrTime.Mean));
                jobList[i].ProcTime = RandomExp(rand, 1 / (target.Queues[i].ProcTime.Mean));
                arrTimes[i] = jobList[i].ArrTime;
                procTimes[i] = jobList[i].ProcTime;
                if (i == 0)
                {
                    arrMin = arrTimes[i];
                    minIndex = 0;
                }
                else if (i > 0 && arrTimes[i] < arrMin)
                {
                    arrMin = arrTimes[i];
                    minIndex = i;
                }
            }

            //update the job with minimal arrival time
            for (int i = 0; i < names.Length; i++)
            {
                if (i == minIndex)
                {
                    jobList[i].ArrTime = RandomExp(rand, 1 / (target.Queues[i].ArrTime.Mean));
                    jobList[i].ProcTime = RandomExp(rand, 1 / (target.Queues[i].ProcTime.Mean));
                    arrTimes[i] = jobList[i].ArrTime;
                    procTimes[i] = jobList[i].ProcTime;
                }
                else
                {
                    arrTimes[i] = arrTimes[i] - arrMin;
                }
            }

            for (int i = 0; i < names.Length; i++)
            {
                Assert.AreEqual(1, target.JobQueue[i].Count, "jobqueue size");
                Assert.AreEqual(arrTimes[i], target.JobQueue[i][0].ArrTime, "arrival time");
                Assert.AreEqual(procTimes[i], target.JobQueue[i][0].ProcTime, "process time");
            }

            // check with seed 2 (other jobtype gets picked up first)
            seed = 2;
            target = CreateSystemSeed(numWorkers, seed);
            target.singleRun();

            //initialize the arrival and processing times
            rand = new ALFGenerator(seed);
            for (int i = 0; i < names.Length; i++)
            {
                jobList[i].ArrTime = RandomExp(rand, 1 / (target.Queues[i].ArrTime.Mean));
                jobList[i].ProcTime = RandomExp(rand, 1 / (target.Queues[i].ProcTime.Mean));
                arrTimes[i] = jobList[i].ArrTime;
                procTimes[i] = jobList[i].ProcTime;
                if (i == 0)
                {
                    arrMin = arrTimes[i];
                    minIndex2 = 0;
                }
                else if (i > 0 && arrTimes[i] < arrMin)
                {
                    arrMin = arrTimes[i];
                    minIndex2 = i;
                }
            }

            Assert.AreNotEqual(minIndex, minIndex2);

            //update the job with minimal arrival time
            for (int i = 0; i < names.Length; i++)
            {
                if (i == minIndex2)
                {
                    jobList[i].ArrTime = RandomExp(rand, 1 / (target.Queues[i].ArrTime.Mean));
                    jobList[i].ProcTime = RandomExp(rand, 1 / (target.Queues[i].ProcTime.Mean));
                    arrTimes[i] = jobList[i].ArrTime;
                    procTimes[i] = jobList[i].ProcTime;
                }
                else
                {
                    arrTimes[i] = arrTimes[i] - arrMin;
                }
            }

            for (int i = 0; i < names.Length; i++)
            {
                Assert.AreEqual(1, target.JobQueue[i].Count, "jobqueue size");
                Assert.IsTrue(Math.Abs(arrTimes[i] - target.JobQueue[i][0].ArrTime) < 0.00000001, "arrival time");
                Assert.AreEqual(procTimes[i], target.JobQueue[i][0].ProcTime, "process time");
            }

            //for a single M/M/c queue I can check numerical results against analytical results from literature,
            //see http://en.wikipedia.org/wiki/M/M/1_queue and http://en.wikipedia.org/wiki/M/M/c_queue
            //small problem because result is for queue in equilibrium, so for M/M/2 queue we first have 1000 initial runs and
            //throw those results away

            //the M/M/1 queue
            names = new string[] { "TestQueue" };
            double[][] parameters = new double[1][];
            double alpha = 1.96;
            int total_runs = 1000;
            double sigma;
            double mean;
            double lambda = 2;
            double mu = 5;
            double rho = lambda / mu;
            double actual_mean;
            parameters[0] = new double[] { 1 / lambda, 1 / mu };

            numWorkers = 1;
            seed = 10;

            QueueSystem queues = new QueueSystem(names, parameters, numWorkers, seed);

            for (int i = 0; i < total_runs; i++)
            {
                queues.singleRun();
            }

            List<double[]> result = queues.ExportData;
            List<double> delay = new List<double>();

            foreach (double[] list in result)
            {
                delay.Add(list[1]);
            }

            mean = delay.Mean();
            sigma = delay.StandardDeviation();
            actual_mean = rho / (mu - lambda);

            //the actual mean is as defined above, see whether the mean falls into the confidence interval for the statstical mean
            double right_endpoint = mean + (alpha * sigma) / (Math.Sqrt(total_runs));
            double left_endpoint = mean - (alpha * sigma) / (Math.Sqrt(total_runs));
            Assert.IsTrue(actual_mean < right_endpoint, "right endpoint:" + right_endpoint + " mean:" + actual_mean);
            Assert.IsTrue(actual_mean > left_endpoint, "left endpoint:" + left_endpoint + " mean:" + actual_mean);

            // M/M/2 queue           
            int startup = 1000;
            seed = 50;
            lambda = 6;
            mu = 5;
            numWorkers = 2;
            rho = lambda / (numWorkers * mu);
            parameters[0] = new double[] { 1 / lambda, 1 / mu };
            queues = new QueueSystem(names, parameters, numWorkers, seed);

            for (int i = 0; i < total_runs + startup; i++)
            {
                queues.singleRun();
            }

            result = queues.ExportData;
            delay.Clear();

            for (int i = 0; i < result.Count; i++)
            {
                if (i > startup - 1)
                {
                    delay.Add(result[i][1]);
                }
            }

            mean = delay.Mean();
            sigma = delay.StandardDeviation();

            double Lq = Pi_Zero(rho, numWorkers) * (Math.Pow(rho * numWorkers, numWorkers) / SpecialFunctions.Factorial(numWorkers)) * (rho / Math.Pow(1 - rho, 2));
            actual_mean = Lq / lambda;

            //the actual mean is as defined above, see whether the mean falls into the confidence interval for the statstical mean
            right_endpoint = mean + (alpha * sigma) / (Math.Sqrt(total_runs));
            left_endpoint = mean - (alpha * sigma) / (Math.Sqrt(total_runs));
            Assert.IsTrue(actual_mean < right_endpoint, "right endpoint:" + right_endpoint + " mean:" + actual_mean);
            Assert.IsTrue(actual_mean > left_endpoint, "left endpoint:" + left_endpoint + " mean:" + actual_mean);
        }

        /// <summary>
        ///A test for singleRunPrio
        ///</summary>
        [TestMethod()]
        public void singleRunPrioTest()
        {
            string[] names = { "Listings", "Q&A", "ReBlast" };
            int numWorkers = 3;

            Job[] jobList = new Job[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                jobList[i] = new Job(i);
            }
            double[] arrTimes = new double[names.Length];
            double[] procTimes = new double[names.Length];
            //double arrMin = 0;TODO remove
            //int minIndex = 0;TODO remove
            //int minIndex2 = 0;TODO remove
            //Assign the job where the first job is the first one to be picked up.
            int seed = 1;
            QueueSystem target = CreateSystemSeed(numWorkers, seed);
            ALFGenerator rand = new ALFGenerator(seed);
            //initialize the processing times
            for (int i = 0; i < names.Length; i++)
            {
                jobList[i].ArrTime = RandomExp(rand, 1 / (target.Queues[i].ArrTime.Mean));
                jobList[i].ProcTime = RandomExp(rand, 1 / (target.Queues[i].ProcTime.Mean));
                procTimes[i] = jobList[i].ProcTime;
            }

            for (int i = 0; i < numWorkers; i++)
            {
                target.Workers[i].EndTime = 20;
            }
            target.NextWorkerTime = 20;

            target.JobQueue[0][0].ArrTime = 8;
            target.JobQueue[1][0].ArrTime = 9;
            target.JobQueue[2][0].ArrTime = 10;

            Assert.AreEqual(procTimes[0], target.JobQueue[0][0].ProcTime, "proc time 0");
            Assert.AreEqual(procTimes[1], target.JobQueue[1][0].ProcTime, "proc time 1");
            Assert.AreEqual(procTimes[2], target.JobQueue[2][0].ProcTime, "proc time 2");

            target.singleRunPrio();

            Assert.AreEqual(0, target.ExportData[0][0], "type");
            Assert.AreEqual(12, target.ExportData[0][1], "delay");

            Assert.AreNotEqual(8, target.JobQueue[0][0].ArrTime, "arrival time 0");
            Assert.AreEqual(9, target.JobQueue[1][0].ArrTime, "arrival time 1");
            Assert.AreEqual(10, target.JobQueue[2][0].ArrTime, "arrival time 2");

            Assert.AreNotEqual(procTimes[0], target.JobQueue[0][0].ProcTime, "proc time 0");
            Assert.AreEqual(procTimes[1], target.JobQueue[1][0].ProcTime, "proc time 1");
            Assert.AreEqual(procTimes[2], target.JobQueue[2][0].ProcTime, "proc time 2");

            //let's whether the last arrival times are correct
            int count = 0;
            for (int i = 0; i < names.Length; i++)
            {
                count = target.JobQueue[i].Count;
                Assert.IsTrue(target.JobQueue[i][count - 2].ArrTime < 20, "last arrival times");
            }

            for (int i = 0; i < names.Length; i++)
            {
                Assert.IsTrue(target.JobQueue[i].Count >= 1, "jobqueue size");
            }

            //Assign the job where the second job is the first to be picked up.

            target = CreateSystemSeed(numWorkers, seed);
            rand = new ALFGenerator(seed);
            for (int i = 0; i < names.Length; i++)
            {
                jobList[i].ArrTime = RandomExp(rand, 1 / (target.Queues[i].ArrTime.Mean));
                jobList[i].ProcTime = RandomExp(rand, 1 / (target.Queues[i].ProcTime.Mean));
                procTimes[i] = jobList[i].ProcTime;
            }

            for (int i = 0; i < numWorkers; i++)
            {
                target.Workers[i].EndTime = 20;
            }
            target.NextWorkerTime = 20;

            target.JobQueue[0][0].ArrTime = 22;
            target.JobQueue[1][0].ArrTime = 9;
            target.JobQueue[2][0].ArrTime = 10;

            Assert.AreEqual(procTimes[0], target.JobQueue[0][0].ProcTime, "proc time 0");
            Assert.AreEqual(procTimes[1], target.JobQueue[1][0].ProcTime, "proc time 1");
            Assert.AreEqual(procTimes[2], target.JobQueue[2][0].ProcTime, "proc time 2");

            target.singleRunPrio();

            //depends on exportData
            Assert.AreEqual(1, target.ExportData[0][0], "type");
            Assert.AreEqual(11, target.ExportData[0][1], "delay");

            Assert.AreEqual(22, target.JobQueue[0][0].ArrTime, "arrival time 0");
            Assert.AreNotEqual(9, target.JobQueue[1][0].ArrTime, "arrival time 1");
            Assert.AreEqual(10, target.JobQueue[2][0].ArrTime, "arrival time 2");

            Assert.AreEqual(procTimes[0], target.JobQueue[0][0].ProcTime, "proc time 0");
            Assert.AreNotEqual(procTimes[1], target.JobQueue[1][0].ProcTime, "proc time 1");
            Assert.AreEqual(procTimes[2], target.JobQueue[2][0].ProcTime, "proc time 2");

            Assert.IsTrue(target.JobQueue[0].Count == 1, "jobqueue size 0");
            Assert.IsTrue(target.JobQueue[1].Count >= 1, "jobqueue size 1");
            Assert.IsTrue(target.JobQueue[2].Count >= 1, "jobqueue size 2");

            //let's whether the last arrival times are correct
            count = 0;
            for (int i = 0; i < names.Length; i++)
            {
                count = target.JobQueue[i].Count;
                if (count > 1)
                {
                    Assert.IsTrue(target.JobQueue[i][count - 2].ArrTime < 20, "last arrival times");
                }
            }
        }

        //Supplemenatary methods
        private static QueueSystem CreateSystem(int numWorkers)
        {
            string[] names = new string[] { "Listings", "Q&A", "ReBlast" };
            double[][] parameters = new double[3][];

            parameters[0] = new double[] { 1, 2 };
            parameters[1] = new double[] { 2, 4 };
            parameters[2] = new double[] { 3, 6 };

            return new QueueSystem(names, parameters, numWorkers);
        }

        private static QueueSystem CreateSystemSeed(int numWorkers, int seed)
        {
            string[] names = new string[] { "Listings", "Q&A", "ReBlast" };
            double[][] parameters = new double[3][];

            parameters[0] = new double[] { 1, 2 };
            parameters[1] = new double[] { 2, 4 };
            parameters[2] = new double[] { 3, 6 };

            return new QueueSystem(names, parameters, numWorkers, seed);
        }

        private static double RandomExp(Generator rand, double lambda)
        {
            return -Math.Log(1 - rand.NextDouble()) / lambda;
        }

        private static double Pi_Zero(double rho, int c)
        {
            double result = Math.Pow(c * rho, c) / (SpecialFunctions.Factorial(c) * (1 - rho));

            for (int k = 0; k < c; k++)
            {
                result = result + Math.Pow(c * rho, k) / (SpecialFunctions.Factorial(k));
            }

            result = 1 / result;

            return result;
        }
    }
}
