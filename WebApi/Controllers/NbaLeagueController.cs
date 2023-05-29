using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Npgsql;
using System.Data.SqlClient;
using player_api.Models;

namespace player_api.Controllers
{
    public class NbaLeagueController : ApiController
    {
        static readonly string connectionString = "Server=localhost;User Id=postgres;Password=root;Database=playerdb;";

        //Get by ID
        public HttpResponseMessage GetElementById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM nba_league WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("id"));
                            string Division = reader.GetString(reader.GetOrdinal("division"));
                            string Commissioner = reader.GetString(reader.GetOrdinal("commissioner"));

                            var NbaLeagueModel = new { Id = id, division = Division, commissioner = Commissioner };
                            return Request.CreateResponse(HttpStatusCode.OK, NbaLeagueModel);

                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //GET
        public HttpResponseMessage Get()
        {
            var nbaLeague = new List<NbaLeagueModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = "select * from nba_league";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Division = reader.GetString(reader.GetOrdinal("division"));
                                string Commissioner = reader.GetString(reader.GetOrdinal("commissioner"));

                                var NbaLeagueModel = new { division = Division, commissioner = Commissioner };
                                return Request.CreateResponse(HttpStatusCode.OK, nbaLeague);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        //put //select
      public HttpResponseMessage Put(int id, NbaLeagueModel UpdateNba_league)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                {
                    string checkQuery = "SELECT * FROM nba_league WHERE Id = @id";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@id", id);
                        int leagueCount = (int)checkCommand.ExecuteScalar();

                        if (leagueCount == 1)
                        {
                            string updateQuery = "UPDATE nba_league SET Division = @division, Commissioner = @commissioner WHERE Id = @id";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@id", id);
                                updateCommand.Parameters.AddWithValue("@division", UpdateNba_league.Division);
                                updateCommand.Parameters.AddWithValue("@commissioner", UpdateNba_league.Commissioner);
                        

                                updateCommand.ExecuteNonQuery();
                            }
                            using (SqlDataReader reader = checkCommand.ExecuteReader())
                            {
                                NbaLeagueModel league = new NbaLeagueModel();
                                while (reader.Read())
                                {
                                    league.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                    league.Division = reader.GetString(reader.GetOrdinal("Division"));
                                    league.Commissioner = reader.GetString(reader.GetOrdinal("Commissioner"));


                                   
                                    return Request.CreateResponse(HttpStatusCode.OK, UpdateNba_league);
                                }
                            }
                        }
                        
                    }
                    connection.Close();
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }

        }

        //delete 
        public HttpResponseMessage Delete(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
             connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM nba_league WHERE Id = @id";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@id", id);
                    int leagueCount = checkCommand.ExecuteNonQuery();
                    if(leagueCount == 1)
                    {
                        string deleteQuery = "Delete * from nba_league WHERE Id = @id";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@id", id);
                            deleteCommand.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    return new HttpResponseMessage(HttpStatusCode.NoContent);   
                }
            }
        }
    }
}