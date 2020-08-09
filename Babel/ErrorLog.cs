using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babel
{
    public partial class ErrorLog : Form
    {
        public ErrorLog()
        {
            InitializeComponent();
        }
        
        private void ErrorLog_Load(object sender, EventArgs e)
        {
            UpdateLog();
        }

        public void UpdateLog()
        {
            foreach(frmBabel.WorkerError WE in frmBabel.WorkerErrors)
            {
                dataGridView1.Rows.Clear();
                DataGridViewRow NewRow = new DataGridViewRow();
                NewRow.CreateCells(dataGridView1);
                NewRow.Cells[0].Value = WE.timestamp;
                NewRow.Cells[1].Value = WE.message;
                if (WE.url != "") NewRow.Cells[2].Value = WE.url;
                dataGridView1.Rows.Add(NewRow);
            }
        }
    }
}
