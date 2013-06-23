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
    public partial class Output : Form
    {
        #region Members

        int nQueues = -1;
        int numWorkers;
        double[][] parameters;
        int totRuns;
        string[] prioDatapoints;
        string[] regDatapoints;

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

        public int NQueues
        {
            get { return nQueues; }
            set { nQueues = value; }
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

        #region Constructor

        public Output()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public void QueueNrDialog()
        {
            SetupA setup = new SetupA();
            setup.ShowDialog();
            this.nQueues = setup.NQueues;
        }

        public void InputQueuesInfo2()
        {
           Input2 input = new Input2();
           input.ShowDialog();
           this.numWorkers = input.NumWorkers;
           this.parameters = input.Parameters;
        }

        public void InputQueuesInfo3()
        {
            Input3 input = new Input3();
            input.ShowDialog();
            this.numWorkers = input.NumWorkers;
            this.parameters = input.Parameters;
        }

        public void InputQueuesInfo4()
        {
            Input4 input = new Input4();
            input.ShowDialog();
            this.numWorkers = input.NumWorkers;
            this.parameters = input.Parameters;
        }

        public void InputQueuesInfo5()
        {
            Input5 input = new Input5();
            input.ShowDialog();
            this.numWorkers = input.NumWorkers;
            this.parameters = input.Parameters;
        }

        public void SimulationDialog()
        {
            Simulation simWindow = new Simulation(this.parameters, this.numWorkers, this.nQueues);
            simWindow.ShowDialog();
            this.prioDatapoints = simWindow.PrioDatapoints;
            this.regDatapoints = simWindow.RegDatapoints;
        }

        public void AddDataPoints()
        {
            System.Windows.Forms.DataVisualization.Charting.DataPoint datapoint;

            foreach (string item in this.prioDatapoints)
	        {
                datapoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, item);
                this.chart1.Series[0].Points.Add(datapoint);
	        }

            foreach (string item in this.regDatapoints)
            {
                datapoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, item);
                this.chart1.Series[1].Points.Add(datapoint);
            }
        }

        public void VisualSettings()
        {
            this.chart1.ChartAreas[0].AxisX.Maximum = this.nQueues + .99;
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
