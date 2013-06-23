using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrioQueues
{
    class Worker
    {
        #region Members

        private double endTime;

        #endregion

        #region Properties

        public double EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        #endregion

        #region Constructor

        public Worker()
        {
            this.endTime = 0;
        }

        #endregion
    }
}
