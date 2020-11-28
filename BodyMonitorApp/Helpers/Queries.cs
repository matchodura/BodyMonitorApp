using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BodyMonitorApp
{
    class Queries
    {
        public int UserId { get; set; }

        public Queries(int userId)
        {
            UserId = userId;
        }



        public ChartModel GetUserValues()
        {

            ChartModel results = new ChartModel() { DateAdded = new List<DateTime>(), Weight = new List<decimal>(),
                Neck = new List<decimal>(), Chest = new List<decimal>(),
                Stomach = new List<decimal>(), Waist = new List<decimal>(),
                Hips = new List<decimal>(), Thigh = new List<decimal>(),
                Calf = new List<decimal>(), Biceps = new List<decimal>()};
                     
            try
            {

                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = $"SELECT DateAdded, Weight, Neck, Chest, Stomach, Waist, Hips, Thigh, Calf, Biceps  FROM dbo.UserBodyValues WHERE UserId=@UserId ORDER BY DateAdded ASC";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                                      
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                          
                            results.DateAdded.Add(reader.GetDateTime(0));
                            results.Weight.Add(reader.GetDecimal(1));
                            results.Neck.Add(reader.GetDecimal(2));
                            results.Chest.Add(reader.GetDecimal(3));
                            results.Stomach.Add(reader.GetDecimal(4));
                            results.Waist.Add(reader.GetDecimal(5));
                            results.Hips.Add(reader.GetDecimal(6));
                            results.Thigh.Add(reader.GetDecimal(7));
                            results.Calf.Add(reader.GetDecimal(8));
                            results.Biceps.Add(reader.GetDecimal(9));
                          
                        }
                                               
                    }
                    else
                    {
                        MessageBox.Show("Record doesn't exist!");

                    }

                }

            }


            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);

            }

            return results;

        }

    }
}
