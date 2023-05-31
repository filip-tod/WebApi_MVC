using Npgsql;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using BackUp_MVC.Models;

namespace WebApi.Controllers
{
    public class LeagueController : ApiController
    {
      private  static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";
        
        public HttpResponseMessage Get()
        {
            List<LeagueTeamModel> leagueTeams = new List<LeagueTeamModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = $"select nl.id, nl.division, nl.commissioner, t.teamname from nba_league nl right join team t on nl.id = t.fk_team_nba_league;";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int Id = reader.GetInt32(reader.GetOrdinal("id"));
                                string division = reader.GetString(reader.GetOrdinal("division"));
                                string commissioner = reader.GetString(reader.GetOrdinal("commissioner"));
                                string teamName = reader.GetString(reader.GetOrdinal("teamname"));

                                var leagueTeam = new LeagueTeamModel

                                {
                                    Id = Id,
                                    Division = division,
                                    Commissioner = commissioner,
                                    Teamname = teamName
                                };
                                

                                leagueTeams.Add(leagueTeam); 
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, leagueTeams);
        }
       
        public HttpResponseMessage GetElementById(int id)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT  a.division, b.teamname, a.commissioner,  a.id   FROM nba_league a  JOIN team b on a.id = b.fk_team_nba_league  WHERE a.id = @id ";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                            int Id = reader.GetInt32(reader.GetOrdinal("id"));
                            string division = reader.GetString(reader.GetOrdinal("division"));
                            string commissioner = reader.GetString(reader.GetOrdinal("commissioner"));
                            string teamName = reader.GetString(reader.GetOrdinal("teamname"));

                            var NbaLeagueModel = new { Id = id, Division = division, Commissioner = commissioner, TeamName=teamName };
                                return Request.CreateResponse(HttpStatusCode.OK, NbaLeagueModel);

                            }
                        }
                    }
                    connection.Close();
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            
       
        public HttpResponseMessage Post(LeagueModel leagueData)
        {
            if (string.IsNullOrWhiteSpace(leagueData.Division) || string.IsNullOrWhiteSpace(leagueData.Commissioner))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Division and Commissioner fields are required!!!");
            }
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

        public HttpResponseMessage Put(int id, LeagueModel leaguedata)
        {
            if (string.IsNullOrWhiteSpace(leaguedata.Division) || string.IsNullOrWhiteSpace(leaguedata.Commissioner))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Division and Commissioner fields are required!!!");
            }
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE nba_league SET ";
                List<string> updateFields = new List<string>();

                if (leaguedata.Division != "")
                {
                    updateFields.Add("division = @division");
                }

                if (leaguedata.Commissioner != "")
                {
                    updateFields.Add("commissioner = @commissioner");
                }

              

                query += string.Join(", ", updateFields);
                query += " WHERE id = @id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    if (leaguedata.Division != "")
                    {
                        command.Parameters.AddWithValue("@pricediscount", leaguedata.Division);
                    }

                    if (leaguedata.Commissioner != "")
                    {
                        command.Parameters.AddWithValue("@couponvalidation", leaguedata.Division);
                    }

                  

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
                return Request.CreateResponse(HttpStatusCode.Created, "you have inserted data successfully!");
        }

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
                }
                 connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "League was deleted!");
            }
        }

    }
     
}
