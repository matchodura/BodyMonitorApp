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

        
        public void GetUserId(string userLogin)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = $"SELECT UserId FROM dbo.Users WHERE UserLogin=@UserLogin";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = userLogin;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            UserId = reader.GetInt32(0);
                          

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


        }

        public void UpdatePassword(string userLogin, string newUserPassword)
        {

            GetUserId(userLogin);
            try
            {

                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "UPDATE dbo.Users " +
                   "SET UserPassword=@UserPassword" +
                   " WHERE UserId=@UserId";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = newUserPassword;
               


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data Updated!");
                }

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
            }

        }


        public double GetHeight(string userLogin)
        {

            GetUserId(userLogin);
            int height = 0;

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = $"SELECT UserHeight FROM dbo.UserData WHERE Id=@UserId";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            height = reader.GetInt32(0);


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

            return (double)height;
        }


        public double GetLastWeight(string userLogin)
        {

            GetUserId(userLogin);
            decimal weight = 0;

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = $"SELECT TOP (1) Weight FROM dbo.UserBodyValues WHERE UserId=@UserId ORDER BY DateAdded DESC";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            weight = reader.GetDecimal(0);
                           
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

            return (double)weight;
        }
            

        public SecretQuestion GetSecretQuestion(string userLogin)
        {

            GetUserId(userLogin);
            var secrets = new SecretQuestion();

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = $"SELECT SecretQuestion, SecretAnswer FROM dbo.UserData WHERE Id=@UserId";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            secrets.Question = reader.GetString(0);
                            secrets.Answer = reader.GetString(1);

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

            return secrets;
        }


        //public bool CheckIfRecordExists()
        //{


            
        //}


        /// <summary>
        /// Updates data of body user values based on selected date
        /// </summary>
        public bool UpdateRecord(ProgressModel progressModel, DateTime calendarDate)
        {

            try
            {


                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "UPDATE dbo.UserBodyValues " +
                   "SET Weight=@Weight,Neck=@Neck, Chest=@Chest, Stomach=@Stomach, Waist=@Waist, Hips=@Hips, Thigh=@Thigh, Calf=@Calf, Biceps=@Biceps" +
                   " WHERE UserId=@UserId AND DateAdded=@DateAdded";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = progressModel.UserId;
                cmd.Parameters.Add("@Weight", SqlDbType.Decimal).Value = progressModel.Weight;           
                cmd.Parameters.Add("@Neck", SqlDbType.Decimal).Value = progressModel.Neck;           
                cmd.Parameters.Add("@Chest", SqlDbType.Decimal).Value = progressModel.Chest;
                cmd.Parameters.Add("@Stomach", SqlDbType.Decimal).Value = progressModel.Stomach;
                cmd.Parameters.Add("@Waist", SqlDbType.Decimal).Value = progressModel.Waist;
                cmd.Parameters.Add("@Hips", SqlDbType.Decimal).Value = progressModel.Hips;
                cmd.Parameters.Add("@Thigh", SqlDbType.Decimal).Value = progressModel.Thigh;
                cmd.Parameters.Add("@Calf", SqlDbType.Decimal).Value = progressModel.Calf;
                cmd.Parameters.Add("@Biceps", SqlDbType.Decimal).Value = progressModel.Biceps;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = calendarDate;
      


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data Updated!");
                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
                return true;
            }

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


        /// <summary>
        /// Checks for secret password and it's answer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        



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
