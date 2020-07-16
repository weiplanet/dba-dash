﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAChecksGUI.AgentJobs
{
    public class AgentJobThreshold
    {
        public bool IsInherited;
        public Int32 InstanceID { get; set; }
        public Guid JobID { get; set; } = Guid.Empty;
        public Int32? TimeSinceLastFailureWarning { get; set; }
        public Int32? TimeSinceLastFailureCritical { get; set; }
        public Int32? TimeSinceLastSucceededCritical { get; set; }
        public Int32? TimeSinceLastSucceededWarning { get; set; }
        public Int32? FailCount24HrsWarning { get; set; }
        public Int32? FailCount24HrsCritical{ get; set; }
        public Int32? FailCount7DaysWarning { get; set; }
        public Int32? FailCount7DaysCritical { get; set; }

        public Int32? JobStepFails24HrsWarning { get; set; }
        public Int32? JobStepFails24HrsCritical { get; set; }

        public Int32? JobStepFails7DaysWarning { get; set; }
        public Int32? JobStepFails7DaysCritical { get; set; }

        public bool LastFailIsCritical { get; set; }
        public bool LastFailIsWarning { get; set; }

        public static AgentJobThreshold GetAgentJobThreshold(Int32 InstanceID, Guid JobID,string connectionString)
        {
            var threshold = new AgentJobThreshold();
            threshold.InstanceID = InstanceID;
            threshold.JobID = JobID;
            SqlConnection cn = new SqlConnection(connectionString);
            using (cn)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("AgentJobThresholds_Get", cn);
                cmd.Parameters.AddWithValue("InstanceID", InstanceID);
                cmd.Parameters.AddWithValue("JobID", JobID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    threshold.TimeSinceLastFailureCritical = columnToNullableInt32(rdr,"TimeSinceLastFailureCritical");
                    threshold.TimeSinceLastFailureWarning = columnToNullableInt32(rdr, "TimeSinceLastFailureWarning");
                    threshold.TimeSinceLastSucceededCritical = columnToNullableInt32(rdr, "TimeSinceLastSucceededCritical");
                    threshold.TimeSinceLastSucceededWarning = columnToNullableInt32(rdr, "TimeSinceLastSucceededWarning");
                    threshold.FailCount24HrsCritical = columnToNullableInt32(rdr, "FailCount24HrsCritical");
                    threshold.FailCount24HrsWarning = columnToNullableInt32(rdr, "FailCount24HrsWarning");
                    threshold.FailCount7DaysCritical = columnToNullableInt32(rdr, "FailCount7DaysCritical");
                    threshold.FailCount7DaysWarning = columnToNullableInt32(rdr, "FailCount7DaysWarning");
                    threshold.JobStepFails24HrsCritical = columnToNullableInt32(rdr, "JobStepFails24HrsCritical");
                    threshold.JobStepFails24HrsWarning = columnToNullableInt32(rdr, "JobStepFails24HrsWarning");
                    threshold.JobStepFails7DaysCritical = columnToNullableInt32(rdr, "JobStepFails7DaysCritical");
                    threshold.JobStepFails7DaysWarning = columnToNullableInt32(rdr, "JobStepFails7DaysWarning");
                    threshold.LastFailIsCritical = rdr["LastFailIsCritical"] == DBNull.Value ? false : (bool)rdr["LastFailIsCritical"];
                    threshold.LastFailIsWarning = rdr["LastFailIsWarning"] == DBNull.Value ? false : (bool)rdr["LastFailIsWarning"];
                }
                else
                {
                    threshold.IsInherited = true;
                }
                return threshold;

            }
        }

        public void Save(string connectionString)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            using (cn)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("AgentJobThresholds_Upd", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("InstanceID", InstanceID);
                cmd.Parameters.AddWithValue("job_id", JobID);
                if (TimeSinceLastFailureWarning != null) { cmd.Parameters.AddWithValue("TimeSinceLastFailureWarning", TimeSinceLastFailureWarning); }
                if (TimeSinceLastFailureCritical != null) { cmd.Parameters.AddWithValue("TimeSinceLastFailureCritical", TimeSinceLastFailureCritical); }
                if (TimeSinceLastSucceededWarning != null) { cmd.Parameters.AddWithValue("TimeSinceLastSucceededWarning", TimeSinceLastSucceededWarning); }
                if (TimeSinceLastSucceededCritical != null) { cmd.Parameters.AddWithValue("TimeSinceLastSucceededCritical", TimeSinceLastSucceededCritical); }
                if (FailCount24HrsWarning != null) { cmd.Parameters.AddWithValue("FailCount24HrsWarning", FailCount24HrsWarning); }
                if (FailCount24HrsCritical != null) { cmd.Parameters.AddWithValue("FailCount24HrsCritical", FailCount24HrsCritical); }
                if (FailCount7DaysCritical != null) { cmd.Parameters.AddWithValue("FailCount7DaysCritical", FailCount7DaysCritical); }
                if (FailCount7DaysWarning != null) { cmd.Parameters.AddWithValue("FailCount7DaysWarning", FailCount7DaysWarning); }
                if (JobStepFails24HrsWarning != null) { cmd.Parameters.AddWithValue("JobStepFails24HrsWarning", JobStepFails24HrsWarning); }
                if (JobStepFails24HrsCritical != null) { cmd.Parameters.AddWithValue("JobStepFails24HrsCritical", JobStepFails24HrsCritical); }
                if (JobStepFails7DaysWarning != null) { cmd.Parameters.AddWithValue("JobStepFails7DaysWarning", JobStepFails7DaysWarning); }
                if (JobStepFails7DaysCritical != null) { cmd.Parameters.AddWithValue("JobStepFails7DaysCritical", JobStepFails7DaysCritical); }
                cmd.Parameters.AddWithValue("LastFailIsCritical", LastFailIsCritical); 
                cmd.Parameters.AddWithValue("LastFailIsWarning", LastFailIsWarning);
                cmd.Parameters.AddWithValue("Inherit", IsInherited);
                cmd.ExecuteNonQuery();

            }
        }

        private static Int32? columnToNullableInt32(SqlDataReader rdr,string columnName)
        {
            return rdr[columnName] == DBNull.Value ? null : (Int32?)rdr[columnName];
        }

    }
}
