using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DAL
{
    public interface IData
    { 
        public enum Gender { Men, Women }
        public Task<List<Player>> GetPlayers(string fifaCode, Gender gender);
        public Task<List<Player>> GetPlayersWithoutSubstitutes(string fifaCode, Gender gender);
        public Task<Player> GetPlayer(int id, string fifaCode, Gender gender);
        public Task<List<Match>> GetMatches(Gender gender);
        public Task<List<Match>> GetMatches(string fifaCode, Gender gender);
        public Task<List<Team>> GetTeams(Gender gender);
        public Task<Team> GetTeam(int id, Gender gender);
        public Task<Match> GetMatch(int id, Gender gender); 
    }
}
