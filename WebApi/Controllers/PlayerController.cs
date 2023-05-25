using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PlayerController : ApiController
    {
        List<PlayerModel> players = new List<PlayerModel>
        {
            new PlayerModel{ Id = 1, FirstName = "LeBron", LastName = "James", AllStar = true },
            new PlayerModel{ Id = 2, FirstName = "Khawi", LastName = "Leonard", AllStar = false },
            new PlayerModel{ Id = 3, FirstName = "Steph", LastName = "curry", AllStar = true },
            new PlayerModel{ Id = 4, FirstName = "Paul", LastName = "George", AllStar = true },
            new PlayerModel{ Id = 5, FirstName = "Pascal", LastName = "Siacam", AllStar = false },
        };
        //players.Add(PlayerModel () { ID = 1, FirstName = "LeBron", LastName = "James", AllStar = true });
        //players.Add(new Player { ID = 2, FirstName = "Stephen", LastName = "Curry", AllStar = true });
        //players.Add(new Player { ID = 3, FirstName = "Kevin", LastName = "Durant", AllStar = true });
        //players.Add(new Player { ID = 4, FirstName = "Giannis", LastName = "Antetokounmpo", AllStar = true });
        //players.Add(new Player { ID = 5, FirstName = "Kawhi", LastName = "Leonard", AllStar = true });

// GET api/<controller>
        public List<PlayerModel> Get()
        {
            return players;
        }

        // GET api/<controller>/5
        public List<PlayerModel> Get(int id )
        {
            return players;
        }

        // POST api/<controller>
        public void Post( PlayerModel player)
        {
           //nastavljam doma
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}