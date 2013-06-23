using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Troschuetz.Random;

namespace PrioQueues
{
    public partial class Input4 : Form
    {
        #region Members

        int numWorkers;
        double[][] parameters;

        #endregion

        #region Properties

        public int NumWorkers
        {
            get { return numWorkers; }
            set { numWorkers = value; }
        }

        public double[][] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        #endregion

        #region Constructor

        public Input4()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void submit_Click(object sender, EventArgs e)
        {
           int totQueues = 4;
           double[] meanArr = new double[totQueues];
           double[] meanProc = new double[totQueues];
           bool[] isNum = new bool[2*totQueues+1];
           int wrong;
           bool inputCorrect = true;
           parameters = new double[totQueues][];

           isNum[0] = double.TryParse(meanArr1.Text, out meanArr[0]);
           isNum[1] = double.TryParse(meanArr2.Text, out meanArr[1]);
           isNum[2] = double.TryParse(meanArr3.Text, out meanArr[2]);
           isNum[3] = double.TryParse(meanArr4.Text, out meanArr[3]);

           isNum[4] = double.TryParse(meanProc1.Text, out meanProc[0]);
           isNum[5] = double.TryParse(meanProc2.Text, out meanProc[1]);
           isNum[6] = double.TryParse(meanProc3.Text, out meanProc[2]);
           isNum[7] = double.TryParse(meanProc4.Text, out meanProc[3]);

           isNum[8] = int.TryParse(intNumWorkers.Text, out numWorkers);

           for (int i = 0; i < isNum.Length-1; i++)
           {
               if (!isNum[i] && i < totQueues)
               {
                   wrong = i + 1;
                   inputCorrect = false;
                   MessageBox.Show("Only enter numerical values\n" + "the " + wrong + "th arrival time is wrong.");
               }
               else if (!isNum[i])
               {
                   wrong = i + 1 - totQueues;
                   inputCorrect = false;
                   MessageBox.Show("Only enter numerical values\n" + "the " + wrong + "th processing time is wrong.");
               }
           }

            if ( !isNum[2*totQueues])
            {
                inputCorrect = false;
                MessageBox.Show("The number of workers must be an integer.");
            }

            if (inputCorrect)
            {
                for (int i = 0; i < totQueues; i++)
                {
                    this.parameters[i] = new double[] { meanArr[i], meanProc[i] };
                }
                if (numWorkers < this.MinWorkers(parameters))
                {
                    MessageBox.Show("Need at least " + this.MinWorkers(parameters) + " workers to handle the workload");
                }
                else
                {
                    this.Close();
                }
            }
        }

        //method to ensure there are enough workers to deal with the workload
        //see http://en.wikipedia.org/wiki/M/M/c_queue#Stationary_analysis
        public int MinWorkers(double[][] parameters)
        {
            double lambda = 0;
            double mu = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                lambda = lambda + 1 / (parameters[i][0]);
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                mu = mu + 1 / parameters[i][1] * 1 / (parameters[i][0]) * 1 / lambda;
            }

            return Convert.ToInt32(Math.Ceiling(lambda / mu));
        }

        #endregion
    }
}
