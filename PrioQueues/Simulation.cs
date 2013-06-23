using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics.Statistics;
using Troschuetz.Random;

namespace PrioQueues
{
    public partial class Simulation : Form
    {
        #region Members
        
        int totQueues;
        int totRuns;
        double[][] parameters;
        int numWorkers;
        string[] names;
        string[] prioDatapoints;
        string[] regDatapoints;     

        #endregion

        #region Properties
        
        public int TotQueues
        {
            get { return totQueues; }
            set { totQueues = value; }
        }

        public int TotRuns
        {
            get { return totRuns; }
            set { totRuns = value; }
        }

        public string[] PrioDatapoints
        {
            get { return prioDatapoints; }
            set { prioDatapoints = value; }
        }

        public string[] RegDatapoints
        {
            get { return regDatapoints; }
            set { regDatapoints = value; }
        }  

        #endregion

        #region Constructors

        public Simulation(double[][] parameters, int numWorkers, int totQueues)
        {
            InitializeComponent();
            this.parameters = parameters;
            this.totQueues = totQueues;
            this.numWorkers = numWorkers;
            names = new string[totQueues];

            for (int i = 0; i < totQueues; i++)
            {
                names[i] = "Queue " + (i + 1);
            }
        }

        #endregion

        #region Methods

        private void startSimulationButton_Click(object sender, EventArgs e)
        {
            bool isInt = false;
            int totRuns;            

            isInt = int.TryParse(totalRunsBox.Text, out totRuns);
            if (isInt && totRuns > 0)
            {
                totalRunsBox.Enabled = false;
                startSimulationButton.Enabled = false;
                progressBar.Maximum = totRuns;
                this.runSimulations(totRuns);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a positive integer number");
            }
        }

        private void runSimulations(int totRuns)
        {
            double[][] prioResult = new double[totQueues][];
            double[][] regResult = new double[totQueues][];

            ALFGenerator rand = new ALFGenerator();
            int seed = rand.Next();

            QueueSystem prioQueue = new QueueSystem(names, parameters, numWorkers, seed);
            QueueSystem regQueue = new QueueSystem(names, parameters, numWorkers, seed);

            for (int i = 0; i < totRuns; i++)
            {
                prioQueue.singleRunPrio();
                regQueue.singleRun();
                progressBar.Value = progressBar.Value + 1;   
            }

            List<double[]> prioOutcomes = prioQueue.ExportData;
            List<double[]> regOutcomes = regQueue.ExportData;
            List<List<double>> prioDelay = new List<List<double>>();
            List<List<double>> regDelay = new List<List<double>>();

            for (int i = 0; i < totQueues; i++)
            {
                prioDelay.Add(new List<double>());
                regDelay.Add(new List<double>());
            }

            foreach (double[] run_info in prioOutcomes)
            {
                prioDelay[Convert.ToInt32(run_info[0])].Add(run_info[1]);
            }

            foreach (double[] run_info in regOutcomes)
            {
                regDelay[Convert.ToInt32(run_info[0])].Add(run_info[1]);
            }

            for (int i = 0; i < totQueues; i++)
            {
                prioResult[i] = new double[] { prioDelay[i].Mean(), prioDelay[i].StandardDeviation(), prioDelay[i].Count };
                regResult[i] = new double[] { regDelay[i].Mean(), regDelay[i].StandardDeviation(), regDelay[i].Count };
            }

            //Converting data to display the boxplot

            prioDatapoints = new string[totQueues];
            regDatapoints = new string[totQueues];
            double[] temp = new double[3];

            for (int i = 0; i < totQueues; i++)
            {
                temp[0] = prioResult[i][0] - (1.96*prioResult[i][1])/(Math.Sqrt(Convert.ToDouble(prioResult[i][2])));
                temp[1] = prioResult[i][0] + (1.96*prioResult[i][1])/(Math.Sqrt(Convert.ToDouble(prioResult[i][2])));
                temp[2] = prioResult[i][0];
                prioDatapoints[i] = temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[2] + "," + temp[2] + "," + temp[2];
                //lower whisker, upper whisker, lower box, upper box, mean, median
                temp[0] = regResult[i][0] - (1.96 * regResult[i][1]) / (Math.Sqrt(Convert.ToDouble(regResult[i][2])));
                temp[1] = regResult[i][0] + (1.96 * regResult[i][1]) / (Math.Sqrt(Convert.ToDouble(regResult[i][2])));
                temp[2] = regResult[i][0];
                regDatapoints[i] = temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[2] + "," + temp[2] + "," + temp[2];
                //lower whisker, upper whisker, lower box, upper box, mean, median
            }
        }

        #endregion
    }
}
