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
        //bildabilno i radi
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
             //radi 
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
        }
    }
