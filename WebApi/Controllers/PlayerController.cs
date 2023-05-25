﻿using System;
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
        //!TASK! :Editat svaku od metoda sa provjerama za ulazne parametre i ubacivanje HttpResponseMessage

        // GET api/<controller>
        public HttpResponseMessage List<PlayerModel> Get()
        {
            return Request.CreateResponse(HttpResponseMessage, Ok, players);
        }

        // GET api/<controller>/5
        public List<PlayerModel> Get(int id )
        {
            int PlayerId = players.IndexOf(players[id]);
            return PlayerId ;
        }

        // POST api/<controller>
        //
        public HttpResponseMessage Post( PlayerModel player)
        {
            int maxId = players.Max(c => c.Id);
            player.Id = maxId + 1;
            players.Add(player);

            return Request.CreateResponse(HttpStatusCode.Created, player);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, PlayerModel updatePlayer)
        {
            //Player model istanciram u novi objekt i tražim mu ID koji ću u konačnici korisiti za update
            PlayerModel playerToUpdate = players.FirstOrDefault(p => p.Id == id);
            if (playerToUpdate != null)
            {
                playerToUpdate.FirstName = updatePlayer.FirstName;
                playerToUpdate.LastName = updatePlayer.LastName;
                playerToUpdate.AllStar = updatePlayer.AllStar;
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Player was not found.");
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            //isti princip ko na gornjem primjeru
            PlayerModel playerToDelete = players.Select(P => P.Id == id);
            //metoda brisanja
            if (playerToDelete != null)
            { 
             players.Remove(playerToDelete);
                return Request.CreateResponse(HttpStatusCode.ok, playerToDelete);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Player not found");
            }
        }
    }
}