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
            int maxId = players.Max(p => p.Id);
            player.Id = maxId + 1;
            players.Add(player);
        }

        // PUT api/<controller>/5
        public void Put(int id, PlayerModel updatePlayer)
        {
            //Player model istanciram u novi objekt i tražim mu ID koji ću u konačnici korisiti za update
            PlayerModel playerToUpdate = players.FirstOrDefault(p => p.Id == id);
            if (playerToUpdate != null)
            {
                playerToUpdate.FirstName = updatePlayer.FirstName;
                playerToUpdate.LastName = updatePlayer.LastName;
                playerToUpdate.AllStar = updatePlayer.AllStar;
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            //isti princip ko na gornjem primjeru
            PlayerModel playerToDelete = players.Select(P => P.Id == id);
            //metoda brisanja
            if (playerToDelete != null)
            { 
             players.Remove(playerToDelete);
            }
        }
    }
}