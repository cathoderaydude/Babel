﻿using System;
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
            SafeUpdateLog = UpdateLog;
            UpdateLog();
        }

        private Action SafeUpdateLog;
        public void UpdateLog()
        {
            if (InvokeRequired)
            {
                Invoke(SafeUpdateLog);
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (frmBabel.WorkerError WE in frmBabel.WorkerErrors)
                {
                    DataGridViewRow NewRow = new DataGridViewRow();
                    NewRow.CreateCells(dataGridView1);
                    NewRow.Cells[0].Value = WE.timestamp;
                    NewRow.Cells[1].Value = WE.message;
                    if (WE.url != "")
                    {
                        NewRow.Cells[2].Value = "Link";
                        NewRow.Cells[2].Tag = WE.url;
                    }
                    dataGridView1.Rows.Add(NewRow);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string URL = dataGridView1.Rows[e.RowIndex].Cells[2].Tag.ToString();
            if (e.ColumnIndex == 2 && URL != "") System.Diagnostics.Process.Start(URL);
        }
    }
}
