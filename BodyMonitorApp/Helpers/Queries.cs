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
        /// <summary>
        /// gets user credientals - hash and salt from table
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public static LoginModel GetUser(string userLogin)
        {
            LoginModel user = new LoginModel()
            {

                Hash = "0",
                Salt = "0"
            };

            try
            {               
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);
                string sql = $"SELECT UserId,UserLogin,Hash,Salt FROM dbo.Users WHERE UserLogin=@UserLogin";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = userLogin;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.UserId = reader.GetInt32(0);
                            user.UserName = reader.GetString(1);
                            user.Hash = reader.GetString(2);
                            user.Salt = reader.GetString(3);
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

            return user;
        }
              
        public static int GetUserId(string userLogin)
        {
            int userId = 0;

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
                            userId = reader.GetInt32(0);                          
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

            return userId;
        }

        public static void UpdatePassword(string userLogin, string newUserPassword, HashSalt hashSalt)
        {
            int userId = GetUserId(userLogin);

            try
            {

                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "UPDATE dbo.Users " +
                   "SET UserPassword=@UserPassword, Hash=@Hash, Salt=@Salt" +
                   " WHERE UserId=@UserId";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = newUserPassword;
                cmd.Parameters.Add("@Hash", SqlDbType.VarChar).Value = hashSalt.Hash;
                cmd.Parameters.Add("@Salt", SqlDbType.VarChar).Value = hashSalt.Salt;
               
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

        public static double GetHeight(string userLogin)
        {
            int userId = GetUserId(userLogin);
            int height = 0;

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = $"SELECT UserHeight FROM dbo.UserData WHERE Id=@UserId";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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

        /// <summary>
        /// gets value of selected body part
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="bodyPart"></param>
        /// <returns></returns>
        public static double GetBodyPart(string userLogin, string bodyPart)
        {
            int userId = GetUserId(userLogin);
            decimal bodyValue = 0;

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = $"SELECT " + bodyPart + " FROM dbo.UserBodyValues WHERE UserId=@UserId";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
             

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            bodyValue = reader.GetDecimal(0);
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

            return (double)bodyValue;
        }

        /// <summary>
        /// gets last inserted weight
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public static double GetLastWeight(string userLogin)
        {

            int userId = GetUserId(userLogin);
            decimal weight = 0;

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = $"SELECT TOP (1) Weight FROM dbo.UserBodyValues WHERE UserId=@UserId ORDER BY DateAdded DESC";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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
            
        /// <summary>
        /// Selectes secret question
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public static SecretQuestion GetSecretQuestion(string userLogin)
        {

            int userId = GetUserId(userLogin);
            var secrets = new SecretQuestion();

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = $"SELECT SecretQuestion, SecretAnswer FROM dbo.UserData WHERE Id=@UserId";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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

        /// <summary>
        /// Updates data of body user values based on selected date
        /// </summary>
        public static bool UpdateRecord(ProgressModel progressModel, DateTime calendarDate)
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
        public static ChartModel GetUserValues(int userId)
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
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

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
        public static bool CreateUserAccount(AccountModel account)
        {           
            try
            {
                DateTime currentTime = DateTime.Now;

                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "INSERT INTO dbo.Users (UserLogin,UserPassword,Hash,Salt) values (@UserLogin,@UserPassword,@Hash,@Salt);" +
                    "INSERT INTO dbo.UserData(id,UserHeight,UserName,UserBirthday,UserMail,UserGender,AccountCreated,SecretQuestion,SecretAnswer) values ((SELECT SCOPE_IDENTITY()),@UserHeight,@UserName,@UserBirthday,@UserMail,@UserGender,@AccountCreated,@SecretQuestion,@SecretAnswer)";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = account.UserLogin;
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = account.UserPassword;
                cmd.Parameters.Add("@Hash", SqlDbType.VarChar).Value = account.HashSalt.Hash;
                cmd.Parameters.Add("@Salt", SqlDbType.VarChar).Value = account.HashSalt.Salt;

                cmd.Parameters.Add("@UserBirthday", SqlDbType.Date).Value = account.UserBirthday.Date;
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
                return false;
            }

        }
               
        /// <summary>
        ///fFills user profile data 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static AccountModel GetProfileData(int userId)
        {
            AccountModel currentAccount = new AccountModel();

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "SELECT * FROM dbo.UserData WHERE Id=@UserId";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // if the result set is not NULL
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {                                                
                            currentAccount.UserBirthday = reader.GetDateTime(1);
                            currentAccount.UserHeight = reader.GetInt32(2);
                            currentAccount.UserName = reader.GetString(3);
                            currentAccount.UserMail = reader.GetString(4);
                            currentAccount.UserGender = reader.GetString(5);
                            currentAccount.AccountCreated = reader.GetDateTime(6);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Wrong Data!");
                    }

                }
            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);

            }

            return currentAccount;
        }

        public static void UpdateProfileData(AccountModel account, int userId)
        {
         

            try
            {
                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "UPDATE dbo.UserData " +
                   "SET UserName=@UserName,UserBirthday=@UserBirthday,UserHeight=@UserHeight, UserMail=@UserMail, UserGender=@UserGender" +
                   " WHERE Id=@UserId";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@UserBirthday", SqlDbType.DateTime).Value = account.UserBirthday;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = account.UserName;
                cmd.Parameters.Add("@UserHeight", SqlDbType.Int).Value = account.UserHeight;
                cmd.Parameters.Add("@UserMail", SqlDbType.VarChar).Value = account.UserMail;
                cmd.Parameters.Add("@UserGender", SqlDbType.Char).Value = account.UserGender;


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

        public static bool AddNewRecord(ProgressModel progress)
        {
            try
            {
                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "INSERT INTO dbo.UserBodyValues (UserId,DateAdded,Weight,Neck,Chest,Stomach,Waist,Hips,Thigh,Calf,Biceps) values (@UserId,@DateAdded,@Weight,@Neck,@Chest,@Stomach,@Waist,@Hips,@Thigh,@Calf,@Biceps)";


                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = progress.UserId;
                cmd.Parameters.Add("@Weight", SqlDbType.Decimal).Value = progress.Weight;
                cmd.Parameters.Add("@Neck", SqlDbType.Decimal).Value = progress.Neck;
                cmd.Parameters.Add("@Chest", SqlDbType.Decimal).Value = progress.Chest;
                cmd.Parameters.Add("@Stomach", SqlDbType.Decimal).Value = progress.Stomach;
                cmd.Parameters.Add("@Waist", SqlDbType.Decimal).Value = progress.Waist;
                cmd.Parameters.Add("@Hips", SqlDbType.Decimal).Value = progress.Hips;
                cmd.Parameters.Add("@Thigh", SqlDbType.Decimal).Value = progress.Thigh;
                cmd.Parameters.Add("@Calf", SqlDbType.Decimal).Value = progress.Calf;
                cmd.Parameters.Add("@Biceps", SqlDbType.Decimal).Value = progress.Biceps;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = progress.DateAdded;


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;

                }

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
                return false;
            }

            return false;
        }


        public static bool CheckIfRecordExists(int userId, DateTime calendarDate)
        {

            bool recordExists = false;

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);
                string sql = "SELECT * FROM dbo.UserBodyValues WHERE UserId=@UserId AND DateAdded=@DateAdded";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = calendarDate;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // if the result set is not NULL
                    if (reader.HasRows)
                    {       
                        recordExists = true;
                    }

                    else
                    {
                        recordExists = false;
                        MessageBox.Show("Record doesn't exist!");
                    }

                }

                conn.Close();

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);

            }

            return recordExists;
        }

        public static ProgressModel GetUserProgress(int userId, DateTime calendarDate)
        {
            var progress = new ProgressModel();

            try
            {
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);
                string sql = "SELECT * FROM dbo.UserBodyValues WHERE UserId=@UserId AND DateAdded=@DateAdded";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = calendarDate;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // if the result set is not NULL
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            progress.Weight = reader.GetDecimal(4);
                            progress.Neck = reader.GetDecimal(5);
                            progress.Chest = reader.GetDecimal(6);
                            progress.Stomach = reader.GetDecimal(7);
                            progress.Waist = reader.GetDecimal(8);
                            progress.Hips = reader.GetDecimal(9);
                            progress.Thigh = reader.GetDecimal(10);
                            progress.Calf = reader.GetDecimal(11);
                            progress.Biceps = reader.GetDecimal(12);
                        }                     
                    }
                 

                }

                conn.Close();
            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);

            }

            return progress;
        }
    
    }
    
}
