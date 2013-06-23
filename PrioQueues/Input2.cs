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
    public partial class Input2 : Form
    {
        #region Members

        double[][] parameters;
        int numWorkers;

        #endregion

        #region Properties

        public double[][] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public int NumWorkers
        {
            get { return numWorkers; }
            set { numWorkers = value; }
        }

        #endregion

        #region Constructor

        public Input2()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void submit_Click(object sender, EventArgs e)
        {
            bool inputCorrect = true;
            int totQueues = 2;
            parameters = new double[totQueues][];
            double[] meanArr = new double[totQueues];
            double[] meanProc = new double[totQueues];
            bool[] isNum = new bool[2 * totQueues + 1];
            int wrong;

            isNum[0] = double.TryParse(meanArr1.Text, out meanArr[0]);
            isNum[1] = double.TryParse(meanArr2.Text, out meanArr[1]);

            isNum[2] = double.TryParse(meanProc1.Text, out meanProc[0]);
            isNum[3] = double.TryParse(meanProc2.Text, out meanProc[1]);

            isNum[2 * totQueues] = int.TryParse(intNumWorkers.Text, out numWorkers);

            for (int i = 0; i < isNum.Length - 1; i++)
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

            if (!isNum[2 * totQueues])
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
            else
            {
                MessageBox.Show("try restarting the program");
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
