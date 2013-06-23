using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrioQueues
{
    public partial class SetupA : Form
    {
        #region Members

        int nQueues = -1;

        #endregion

        #region Properties

        public int NQueues
        {
            get { return nQueues; }
            set { nQueues = value; }
        }

        #endregion

        #region Constructors

        public SetupA()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void submit_Click(object sender, EventArgs e)
        {

            if (nrQueuesBox.SelectedItem != null)
            {
                nQueues = Convert.ToInt32(nrQueuesBox.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select the number of queues anywhere between 2 and 5 queues");
            }
        }

        #endregion
    }
}
