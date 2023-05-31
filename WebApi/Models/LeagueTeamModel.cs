using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class LeagueTeamModel
    {
        public int Id { get; set; }
        public string Division { get; set;}
        public string Commissioner { get; set; }
        public string Teamname { get; set; }
        
    }
}