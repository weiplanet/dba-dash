﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBADashGUI.Changes
{
    public partial class AzureServiceObjectivesHistory : UserControl
    {
        public AzureServiceObjectivesHistory()
        {
            InitializeComponent();
        }

        public List<Int32> InstanceIDs;

        public void RefreshData()
        {
            refreshDB();
            refreshPool();
        }

        private void refreshDB()
        {
            using (var cn = new SqlConnection(Common.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.AzureServiceObjectivesHistory_Get", cn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("InstanceIDs", string.Join(",", InstanceIDs));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Common.ConvertUTCToLocal(ref dt);
                    dgv.AutoGenerateColumns = false;
                    dgv.DataSource = dt;
                }
            }
        }

        private void refreshPool()
        {
            using (var cn = new SqlConnection(Common.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.AzureDBElasticPoolHistory_Get", cn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("InstanceIDs", string.Join(",", InstanceIDs));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Common.ConvertUTCToLocal(ref dt);
                    dgvPool.AutoGenerateColumns = false;
                    dgvPool.DataSource = dt;
                }
            }
        }

        private void tsRefresh_Click(object sender, EventArgs e)
        {
            refreshDB();
        }

        private void tsCopy_Click(object sender, EventArgs e)
        {
            Common.CopyDataGridViewToClipboard(dgv);
        }

        private void tsRefreshPool_Click(object sender, EventArgs e)
        {
            refreshPool();
        }

        private void tsCopyPool_Click(object sender, EventArgs e)
        {
            Common.CopyDataGridViewToClipboard(dgvPool);
        }
    }
}
