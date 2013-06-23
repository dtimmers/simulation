using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PrioQueues
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Output output = new Output();

            output.QueueNrDialog();

            if (output.NQueues == 2)
            {
                output.InputQueuesInfo2();
            }
            else if (output.NQueues == 3)
            {
                output.InputQueuesInfo3();
            }
            else if (output.NQueues == 4)
            {
                output.InputQueuesInfo4();
            }
            else if (output.NQueues == 5)
            {
                output.InputQueuesInfo5();
            }

            if (output.Parameters != null)
            {
                if( output.NumWorkers >= output.MinWorkers(output.Parameters) )
                {
                    output.SimulationDialog();
                }
            }

            if (output.RegDatapoints != null)
            {
                output.AddDataPoints();
                output.VisualSettings();
                Application.Run(output);
            }
        }
    }
}
