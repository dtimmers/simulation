using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrioQueues
{
    class Job
    {
        #region Members

        private double arrTime;
        private double procTime;
        private int jobType;

        #endregion

        #region Properties

        public double ArrTime
        {
            get
            {
                return this.arrTime;
            }
            set
            {
                this.arrTime = value;
            }
        }

        public double ProcTime
        {
            get
            {
                return this.procTime;
            }
            set
            {
                this.procTime = value;
            }
        }

        public int JobType
        {
            get
            {
                return this.jobType;
            }
            set
            {
                //
            }
        }        

        #endregion

        #region Constructors

        public Job(int type)
        {
            this.arrTime = 0;
            this.procTime = 0;
            this.jobType = type;
        }

        public Job(int type, double arrTime, double procTime)
        {
            this.arrTime = arrTime;
            this.procTime = procTime;
            this.jobType = type;
        }

        #endregion

    }
}
