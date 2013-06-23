using System;
using System.Collections.Generic;
using Troschuetz.Random;
using System.Linq;
using System.Text;

namespace PrioQueues
{
    class RQueue
    {
        #region Members

        private string name;
        private Distribution arrTime;
        private Distribution procTime;

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Distribution ArrTime
        {
            get { return arrTime; }
            set { arrTime = value; }
        }


        public Distribution ProcTime
        {
            get { return procTime; }
            set { procTime = value; }
        }

        #endregion

        #region Constructors

        public RQueue()
        {
        }

        public RQueue(string name, Distribution arrTime, Distribution procTime)
        {
            this.name = name;
            this.arrTime = arrTime;
            this.procTime = procTime;
        }

        #endregion


    }
}