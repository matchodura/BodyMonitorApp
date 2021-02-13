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


        public Queries()
        {

        }

        public Queries(int userId)
        {
            UserId = userId;
        }


        /// <summary>
        /// gets body values as list from db for current logged in user
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// creates user account based on credentials specified in viewmodel
        /// </summary>
        /// <returns></returns>
        public void CreateUserAccount(AccountModel account)
        {
           
            try
            {
                DateTime currentTime = DateTime.Now;

                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "INSERT INTO dbo.Users (UserLogin,UserPassword) values (@UserLogin,@UserPassword);" +
                    "INSERT INTO dbo.UserData(id,UserHeight,UserName,UserAge,UserMail,UserGender,AccountCreated,SecretQuestion,SecretAnswer) values ((SELECT SCOPE_IDENTITY()),@UserHeight,@UserName,@UserAge,@UserMail,@UserGender,@AccountCreated,@SecretQuestion,@SecretAnswer)";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = account.UserLogin;
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = account.UserPassword;
                cmd.Parameters.Add("@UserAge", SqlDbType.Date).Value = account.UserAge.Date;
                cmd.Parameters.Add("@UserHeight", SqlDbType.Int).Value = account.UserHeight;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = account.UserName;
                cmd.Parameters.Add("@UserMail", SqlDbType.VarChar).Value = account.UserMail;
                cmd.Parameters.Add("@UserGender", SqlDbType.Char).Value = account.UserGender;
                cmd.Parameters.Add("@AccountCreated", SqlDbType.DateTime).Value = currentTime;
                cmd.Parameters.Add("@SecretQuestion", SqlDbType.VarChar).Value = account.SecretQuestion;
                cmd.Parameters.Add("@SecretAnswer", SqlDbType.VarChar).Value = account.SecretAnswer;


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Account Created!");
                }

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
            }

        }


        //TODO: return profile info - ProfileInfoViewModel has base for it
        public AccountModel GetAccountModel(int userId)
        {
            var account = new AccountModel();

            try
            {
                DateTime currentTime = DateTime.Now;

                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

               
                string sqlAccount = "Select * FROM dbo.UserData WHERE @userId == Id";

                string sql = "INSERT INTO dbo.Users (UserLogin,UserPassword) values (@UserLogin,@UserPassword);" +
                    "INSERT INTO dbo.UserData(id,UserHeight,UserName,UserAge,UserMail,UserGender,AccountCreated,SecretQuestion,SecretAnswer) values ((SELECT SCOPE_IDENTITY()),@UserHeight,@UserName,@UserAge,@UserMail,@UserGender,@AccountCreated,@SecretQuestion,@SecretAnswer)";

                conn.Open();

               

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = account.UserLogin;
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = account.UserPassword;
                cmd.Parameters.Add("@UserAge", SqlDbType.Date).Value = account.UserAge.Date;
                cmd.Parameters.Add("@UserHeight", SqlDbType.Int).Value = account.UserHeight;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = account.UserName;
                cmd.Parameters.Add("@UserMail", SqlDbType.VarChar).Value = account.UserMail;
                cmd.Parameters.Add("@UserGender", SqlDbType.Char).Value = account.UserGender;
                cmd.Parameters.Add("@AccountCreated", SqlDbType.DateTime).Value = currentTime;
                cmd.Parameters.Add("@SecretQuestion", SqlDbType.VarChar).Value = account.SecretQuestion;
                cmd.Parameters.Add("@SecretAnswer", SqlDbType.VarChar).Value = account.SecretAnswer;


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Account Created!");
                  
                }

               
            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
               
            }


            return account;
        }


    }
    
}
