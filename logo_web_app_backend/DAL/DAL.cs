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
        }

        public Response Login(SqlConnection conn, User user)
        {/*
            string query = @" INSERT INTO [dbo].[InvoiceDesignAnaliseResult] 
                                ([Created],[InvoiceDesignId],[ScriptElement],[FirmaImzasi],[GibLogo],[AltName1],[AltName2],[AltName3],[AltName4],[Status],[Description])
                                VALUES(GETDATE(),@InvoiceDesignId,@ScriptElement,@FirmaImzasi,@GibLogo,@AltName1,@AltName2,@AltName3,@AltName4,@Status,@Description) "
            ;

            using (SqlConnection connection = new SqlConnection(Setting.MainConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                command.CommandType = CommandType.Text;

                command.Parameters.Add("@InvoiceDesignId", SqlDbType.Int).Value = recInf.id;
                command.Parameters.Add("@GibLogo", SqlDbType.Int).Value = analizer.ScriptElement == null ? 0 : 1;
                command.Parameters.Add("@GibLogo", SqlDbType.Int).Value = analizer.GibLogoElement == null ? 0 : 1;

                command.ExecuteNonQuery();
            }
            */

            /*
            string query = @"SELECT [Username],[Password]
                             FROM Registration
                             WHERE UserName=@UserName AND Password=@Password";


            Response response = new Response();


            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();

                command.CommandType = CommandType.Text;

                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = "";
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = "";


                // connection ı otomatik kapatır
                // garbage collector
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    if (reader.Read())
                    {
                        var recInf = new User
                        {
                            // kolon sırasına göre
                            UserName = reader.GetString("UserName"),
                            Password = reader.GetString("Password")

                        };
                        response.statusCode = 200;
                        response.statusMessage = "Data Inserted!";

                    }
                    else
                    {
                        response.statusCode = 100;
                        response.statusMessage = "Login failed!";
                    }
                }
            }

            return response;
            */



            
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
            
        }


        public Response RegistrationUser(SqlConnection conn, User user)
        {
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
        }


        public Response UpdateUser(SqlConnection conn, User user, int id)
        {

            Response response = new Response();

            SqlCommand cmd = new SqlCommand("UPDATE Registration SET UserName = '" + user.UserName + "', Email = '" + user.Email + "' WHERE Id = '" + id + "' ", conn);

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
        }


        public Response DeleteUser(SqlConnection conn, int id)
        {

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
        }


    }
}