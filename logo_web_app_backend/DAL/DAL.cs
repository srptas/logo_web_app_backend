using logo_web_app_backend.Common;
using logo_web_app_backend.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace logo_web_app_backend.Controllers
{
    public class DAL
    {

        public Response GetAllUsers(SqlConnection conn)
        {
            Response response = new Response();

            string query = "SELECT * FROM Registration";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();
                DataTable dt = new DataTable();

                List<User> users = new List<User>();

                

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            User user = new User();

                            user.Id = Convert.ToInt32(row["Id"]);
                            user.UserName = Convert.ToString(row["UserName"]);

                            // Encrypt the password using ConvertToEncrypt function
                            string encryptedPassword = CommonMethods.ConvertoEncrypt(Convert.ToString(row["Password"]));
                            user.Password = encryptedPassword;
                            user.Email = Convert.ToString(row["Email"]);
                            user.IsActive = Convert.ToInt32(row["IsActive"]);

                            users.Add(user);
                        }
                    }
                }

                if (users.Count > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Data Found";
                    response.users = users;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Data not found";
                    response.users = null;
                }
            }

            return response;


            /* OLD QUERY FOR GETUSERS
            Response response = new Response();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Registration", conn);

            DataTable dt = new DataTable();

            List<User> users = new List<User>();

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    User user = new User();

                    user.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    user.UserName = Convert.ToString(dt.Rows[i]["UserName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);

                    users.Add(user);

                }
            }

            if (users.Count > 0)
            {
                response.statusCode = 200;
                response.statusMessage = "Data Found";
                response.users = users;



            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "Data not found";
                response.users = null;
            }

            return response;
            */
        }

        public Response Login(SqlConnection conn, User user)
        {

            
            string query = @"SELECT [Username],[Password]
                             FROM Registration
                             WHERE UserName=@UserName AND Password=@Password";


            Response response = new Response();


            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();

                command.CommandType = CommandType.Text;

                // Set the parameter values
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = CommonMethods.ConvertoEncrypt(user.Password);

                // closes the connection automatically
                // garbage collector
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    if (reader.Read())
                    {
                        var recInf = new User
                        {
                            // GetOrdinal helps to avoid any issues that might arise due to column order changes in the SQL query.
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Password = reader.GetString(reader.GetOrdinal("Password"))

                        };
                        response.statusCode = 200;
                        response.statusMessage = "Login succesful";

                    }
                    else
                    {
                        response.statusCode = 100;
                        response.statusMessage = "Wrong username or password!";
                    }
                }
            }

            return response;
            

            

            /* OLD QUERY FOR LOGIN
            string query2 = "SELECT * FROM Registration WHERE UserName = '" + user.UserName + "' AND Password = '" + user.Password + "' ";
            SqlDataAdapter da = new SqlDataAdapter(query2, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);

            Response response = new Response();

            if (dt.Rows.Count > 0)
            {
                response.statusCode = 200;
                response.statusMessage = "Login succesful";
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "Login failed!";
            }
            
            return response;
            */
            
            
        }

      
        public Response RegistrationUser(SqlConnection conn, User user)
        {
            Response response = new Response();

            string query = "INSERT INTO Registration(UserName, Password, Email, IsActive) VALUES (@UserName, @Password, @Email, @IsActive)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = CommonMethods.ConvertoEncrypt(user.Password);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.Email;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;

                try
                {
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        response.statusCode = 200;
                        response.statusMessage = "Data Inserted!";
                    }
                    else
                    {
                        response.statusCode = 100;
                        response.statusMessage = "No data inserted.";
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception, e.g., log the error or return an error response.
                    response.statusCode = 500;
                    response.statusMessage = "An error occurred: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return response;



            /* OLD QUERY FOR REGISTRATION
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName, Password, Email, IsActive) VALUES ('" + user.UserName + "',' " + user.Password + "','" + user.Email + "','" + user.IsActive + "')", conn);

            conn.Open();

            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.statusCode = 200;
                response.statusMessage = "Data Inserted!";
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data inserted.";
            }

            return response;
            */
        }


        public Response UpdateUser(SqlConnection conn, User user, int id)
        {
            Response response = new Response();

            string query = "UPDATE Registration SET UserName = @UserName, Email = @Email, Password = @Password WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Set the parameter values
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = CommonMethods.ConvertoEncrypt(user.Password);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                try
                {
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        response.statusCode = 200;
                        response.statusMessage = "User updated!";
                    }
                    else
                    {
                        response.statusCode = 100;
                        response.statusMessage = "No data updated";
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception, e.g., log the error or return an error response.
                    response.statusCode = 500;
                    response.statusMessage = "An error occurred: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return response;

            /* OLD QUERY FOR UPDATE USER
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("UPDATE Registration SET UserName = '" + user.UserName + "', Email = '" + user.Email + "', Password = '" + user.Password + "' WHERE Id = '" + id + "' ", conn);

            conn.Open();

            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.statusCode = 200;
                response.statusMessage = "User updated!";
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data updated";
            }

            return response;
            */
        }


        public Response DeleteUser(SqlConnection conn, int id)
        {
            Response response = new Response();

            string query = "DELETE FROM Registration WHERE ID = @Id";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Set the parameter value
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                try
                {
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        response.statusCode = 200;
                        response.statusMessage = "User deleted";
                    }
                    else
                    {
                        response.statusCode = 100;
                        response.statusMessage = "No user deleted";
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception, e.g., log the error or return an error response.
                    response.statusCode = 500;
                    response.statusMessage = "An error occurred: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return response;

            /* OLD QUERY FOR DELETE USER
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("DELETE FROM Registration WHERE ID = '" + id + "'", conn);

            conn.Open();

            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.statusCode = 200;
                response.statusMessage = "User deleted";
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No user deleted";
            }

            return response;
            */
        }
    }
}