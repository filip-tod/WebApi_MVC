using Npgsql;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using BackUp_MVC.Models;

namespace BackUp_MVC.Controllers
{
    public class LeagueController : ApiController
    {
      private  static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";
        //bildabilno i radi  --> moram još provjere napraviti
        public HttpResponseMessage Get()
        {
            List<object> nbaLeagues = new List<object>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = $"SELECT  * FROM nba_league";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int Id = reader.GetInt32(reader.GetOrdinal("id"));
                                string Division = reader.GetString(reader.GetOrdinal("division"));
                                string Commissioner = reader.GetString(reader.GetOrdinal("commissioner"));

                                var NbaLeague = new {id = Id, division = Division, commissioner = Commissioner };
                                nbaLeagues.Add(NbaLeague);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, nbaLeagues);
        }
        //bildabilno i radi --> moram još provjere napraviti
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
        // bildabilno i radi --> moram još provjere napraviti
        public HttpResponseMessage Post(LeagueModel leagueData)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO nba_league  (division, commissioner) VALUES (@division, @commissioner)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@division", leagueData.Division);
                    command.Parameters.AddWithValue("@commissioner", leagueData.Commissioner);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
                return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }

        //bildabilno i valja --> moram još provjere napraviti
        public HttpResponseMessage Put(int id, LeagueModel leaguedata)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE nba_league SET division = @division, commissioner = @commissioner WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@division", leaguedata.Division);
                    command.Parameters.AddWithValue("@commissioner", leaguedata.Commissioner);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
                return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }
        // osim što nemogu zatvoriti konekciju radi i bildabilno je
        public HttpResponseMessage Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM nba_league WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                }
               // connection.Close();  --> javlja grešku
               
            }


        }

    }
     
}
