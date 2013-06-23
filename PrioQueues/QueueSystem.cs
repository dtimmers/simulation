using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MathNet.Numerics.Distributions;
using Troschuetz.Random;

namespace PrioQueues
{
    class QueueSystem
    {
        #region Members
        //random variables
        Generator rand;

        //queue variables
        private List<RQueue> queues = new List<RQueue>();
        private string[] queueNames;

        //worker variables
        private List<Worker> workers = new List<Worker>();
        private double nextWorkerTime = 0;
        private int nextWorkerIndex = 0;

        //job variables
        private int numJobTypes;
        private List<List<Job>> jobQueue = new List<List<Job>>();

        //variables for recording output
        private List<double[]> exportData = new List<double[]>();

        #endregion

        #region Properties

        public List<double[]> ExportData
        {
            get { return exportData; }
            set { exportData = value; }
        }

        public Generator Rand
        {
            get { return rand; }
            set { rand = value; }
        }

        public double NextWorkerTime
        {
            get { return nextWorkerTime; }
            set { nextWorkerTime = value; }
        }
        public int NextWorkerIndex
        {
            get { return nextWorkerIndex; }
            set { nextWorkerIndex = value; }
        }

        public int NumJobTypes
        {
            get { return numJobTypes; }
            set { numJobTypes = value; } //TODO check whether set is necessary
        }

        public string[] QueueNames
        {
            get { return queueNames; }
            set { queueNames = value; } //TODO check whether set is necessary
        }

        internal List<List<Job>> JobQueue
        {
            get { return jobQueue; }
            set { jobQueue = value; } //TODO check whether set is necessary
        }

        internal List<Worker> Workers
        {
            get { return workers; }
            set { workers = value; }
        }

        internal List<RQueue> Queues
        {
            get { return queues; }
            set { queues = value; }
        }

        #endregion

        #region Constructors

        public QueueSystem(string[] names, double[][] parameters, int numWorkers)
        {
            this.numJobTypes = names.Length;
            this.queueNames = names;
            this.rand = new ALFGenerator();

            for (int i = 0; i < this.numJobTypes; i++)
            {
                this.queues.Add(CreateRQueue(this.queueNames[i], parameters[i], this.rand));
                this.jobQueue.Add(new List<Job>());
                this.jobQueue[i].Add(generateJob(i, 0));
            }

            for (int i = 0; i < numWorkers; i++)
            {
                this.workers.Add(new Worker());
            }
        }

        public QueueSystem(string[] names, double[][] parameters, int numWorkers, int seed)
        {
            this.numJobTypes = names.Length;
            this.queueNames = names;
            this.rand = new ALFGenerator(seed);

            for (int i = 0; i < this.numJobTypes; i++)
            {
                this.queues.Add(CreateRQueue(this.queueNames[i], parameters[i], this.rand));
                this.jobQueue.Add(new List<Job>());
                this.jobQueue[i].Add(generateJob(i, 0));
            }

            for (int i = 0; i < numWorkers; i++)
            {
                this.workers.Add(new Worker());
            }
        }

        #endregion

        #region Methods

        public RQueue CreateRQueue(string name, double[] parameters, Generator rand)
        {
            ExponentialDistribution arrTime = new ExponentialDistribution(rand);
            ExponentialDistribution procTime = new ExponentialDistribution(rand);

            arrTime.Lambda = 1 / parameters[0];
            procTime.Lambda = 1 / parameters[1];

            return new RQueue(name, arrTime, procTime);
        }

        public void singleRunPrio()
        {
            int i = 0;
            bool assignedJob = false;
            int minIndex = 0;
            double minArrTime = this.jobQueue[minIndex][0].ArrTime;
            double[] runData = new double[this.numJobTypes + 2];

            //If necessary use the priority rule to assign the job
            while ((i < this.numJobTypes) && !assignedJob)
            {
                //see whether there is someone waiting in the queue
                if (jobQueue[i][0].ArrTime < nextWorkerTime)
                {
                    //add jobs
                    this.addJobs(i);
                    //check the queuesizes and read data
                    runData[0] = i;
                    runData[1] = nextWorkerTime - jobQueue[i][0].ArrTime;
                    //update the workers
                    this.updateWorkers(jobQueue[i][0].ArrTime, jobQueue[i][0].ProcTime, true);
                    this.jobQueue[i].RemoveAt(0);
                    for (int j = 0; j < this.numJobTypes; j++)
                    {
                        runData[j + 2] = this.jobQueue[j].Count - 1;
                    }
                    //write data to list
                    this.exportData.Add(runData);
                    assignedJob = true;
                }
                else
                {
                    //keep track of the job which arrives earliest
                    if (jobQueue[i][0].ArrTime < minArrTime)
                    {
                        minIndex = i;
                        minArrTime = jobQueue[i][0].ArrTime;
                    }
                    i++;
                }
            }

            //If job was not assigned yet, then all queues are empty and first job which arrives gets immediately processed.
            if (!assignedJob)
            {
                //there was no prioDelay and there were no jobs waiting in line
                this.updateWorkers(minArrTime, jobQueue[minIndex][0].ProcTime, false);
                if (this.jobQueue[minIndex].Count == 1)
                {
                    this.jobQueue[minIndex][0] = generateJob(minIndex, 0);
                }
                else
                {
                    this.jobQueue[minIndex].RemoveAt(0);
                }

                for (int j = 0; j < this.numJobTypes; j++)
                {
                    if (j != minIndex)
                    {
                        this.jobQueue[j][0].ArrTime = this.jobQueue[j][0].ArrTime - minArrTime;
                    }
                }

                runData[0] = minIndex;
                runData[1] = 0;
                for (int j = 0; j < this.numJobTypes; j++)
                {
                    runData[j + 2] = 0;
                }
                //write data to list

                this.exportData.Add(runData);
            }
        }

        public void singleRun()
        {
            int minIndex = 0;
            double minArrTime = this.jobQueue[minIndex][0].ArrTime;
            double[] runData = new double[this.numJobTypes + 2];

            //search for earliest job
            for (int j = 1; j < this.numJobTypes; j++)
            {
                if (jobQueue[j][0].ArrTime < minArrTime)
                {
                    minIndex = j;
                    minArrTime = jobQueue[j][0].ArrTime;
                }
            }

            //update the jobs
            runData[0] = minIndex;
            runData[1] = Math.Max(nextWorkerTime - minArrTime, 0);
            this.updateWorkers(minArrTime, jobQueue[minIndex][0].ProcTime, false);
            this.jobQueue[minIndex][0] = generateJob(minIndex, 0);

            for (int i = 0; i < this.numJobTypes; i++)
            {
                if (i != minIndex)
                {
                    this.jobQueue[i][0].ArrTime = this.jobQueue[i][0].ArrTime - minArrTime;
                }
            }

            for (int j = 0; j < this.numJobTypes; j++)
            {
                runData[j + 2] = this.jobQueue[j].Count - 1;
            }
            //write data to list
            this.exportData.Add(runData);
        }

        //Method to add Jobs when using priority
        public void addJobs(int jobType)
        {
            double lastArrTime;

            for (int i = jobType; i < this.numJobTypes; i++)
            {
                lastArrTime = this.jobQueue[i][this.jobQueue[i].Count - 1].ArrTime;
                while (lastArrTime < this.nextWorkerTime)
                {
                    //generate job but take the prioDelay into account
                    this.jobQueue[i].Add(generateJob(i, lastArrTime));
                    lastArrTime = this.jobQueue[i][this.jobQueue[i].Count - 1].ArrTime;
                }
            }
        }

        public void updateWorkers(double arrTime, double procTime, bool usePriority)
        {
            //avoid updating the same worker twice
            int avoidWorker = nextWorkerIndex;

            if (!usePriority)
            {
                //first update the worker who is assigned the job
                this.workers[nextWorkerIndex].EndTime = Math.Max(this.workers[nextWorkerIndex].EndTime - arrTime, 0) + procTime;
                nextWorkerTime = this.workers[nextWorkerIndex].EndTime;

                //next we search if there is a worker who is available before the currently assigned worker
                for (int i = 0; i < this.workers.Count; i++)
                {
                    if (i != avoidWorker)
                    {
                        this.workers[i].EndTime = Math.Max(this.workers[i].EndTime - arrTime, 0);
                        if (this.workers[i].EndTime < nextWorkerTime)
                        {
                            nextWorkerTime = this.workers[i].EndTime;
                            nextWorkerIndex = i;
                        }
                    }
                }
            }
            else
            {
                //only update the worker who is assigned the job
                this.workers[nextWorkerIndex].EndTime = this.workers[nextWorkerIndex].EndTime + procTime;
                nextWorkerTime = this.workers[nextWorkerIndex].EndTime;

                //next we search if there is a worker who is available before the currently assigned worker
                for (int i = 0; i < this.workers.Count; i++)
                {
                    if (this.workers[i].EndTime < nextWorkerTime)
                    {
                        nextWorkerTime = this.workers[i].EndTime;
                        nextWorkerIndex = i;
                    }
                }
            }
        }

        public Job generateJob(int type, double wait)
        {
            //here is a sample method where the arrival times and processing times are exponentially distributed
            //with different means for each jobtype
            return new Job(type, queues[type].ArrTime.NextDouble() + wait, queues[type].ProcTime.NextDouble());
        }

        public void resetData()
        {
            exportData.Clear();
        }

        #endregion
    }
}
