using Babel.StateMachines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MState = Babel.StateMachines.State;

namespace Babel
{
    public partial class StateTest : Form
    {
        public StateTest()
        {
            InitializeComponent();
        }

        #region State definitions

        #region State A

        private MState stateA => new MState(EnterA, LeaveA);

        private void EnterA()
        {
            textBox1.Text = "State A";
            button1.Enabled = false;
        }

        private void LeaveA()
        {
            button1.Enabled = true;
        }

        #endregion

        #region State B

        private MState stateB => new MState(EnterB, LeaveB);

        private void EnterB()
        {
            textBox1.Text = "State B";
            button2.Enabled = false;
        }

        private void LeaveB()
        {
            button2.Enabled = true;
        }

        #endregion

        #region State C

        private MState stateC => new MState(EnterC, LeaveC);

        private void EnterC()
        {
            textBox1.Text = "State C";
            button3.Enabled = false;
        }

        private void LeaveC()
        {
            button3.Enabled = true;
        }

        #endregion

        #endregion

        StateMachine machine = new StateMachine();

        private void button1_Click(object sender, EventArgs e)
        {
            machine.Change(stateA);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            machine.Change(stateB);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            machine.Change(stateC);
        }

        private void StateTest_Load(object sender, EventArgs e)
        {
            textBox1.Text = "null state";
        }
    }
}
