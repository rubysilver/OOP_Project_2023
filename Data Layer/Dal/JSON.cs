using Data_Layer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DAL
{
    internal class JSON : IData
    {
        private readonly string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public async Task<List<Match>> GetMatches(IData.Gender gender)
        {
            try
            {
                string content = await File.ReadAllTextAsync(Path.Combine(filePath, gender.ToString().ToLower(), "matches.json"));
                return JsonConvert.DeserializeObject<List<Match>>(content) ?? new List<Match>();
            }
            catch
            {
                return new List<Match>();
            }
        }
        public async Task<Match> GetMatch(int id, IData.Gender gender)
        {
            try
            {
                List<Match> matches = await GetMatches(gender);
                return matches.FirstOrDefault(e => e.Id == id);
            }
            catch 
            {
                return null;
            }
        }
        public async Task<List<Match>> GetMatches(string fifaCode, IData.Gender gender)
        {
            try
            {
                string content = await File.ReadAllTextAsync(Path.Combine(filePath, gender.ToString().ToLower(), $"matches-{fifaCode}.json"));
                return JsonConvert.DeserializeObject<List<Match>>(content) ?? new List<Match>();
            }
            catch
            {
                return new List<Match>();
            }
        }
        public async Task<Player> GetPlayer(int id, string fifaCode, IData.Gender gender)
        {
            try
            {
                List<Player> players = await GetPlayers(fifaCode, gender);
                return players.FirstOrDefault(p => p.Id == id) ?? new Player();
            }
            catch
            {
                return new Player();
            }
        }
        public async Task<List<Player>> GetPlayers(string fifaCode, IData.Gender gender)
        {
            try
            {
                List<Match> matches = await GetMatches(fifaCode, gender);
                if (matches.First().HomeStatistics.Code == fifaCode)
                {
                    return matches.First().GetHomeTeamPlayers();
                }
                else
                {
                    return matches.First().GetAwayTeamPlayers();
                }
            }
            catch
            {
                return new List<Player>();
            }
        }
        public async Task<Team> GetTeam(int id, IData.Gender gender)
        {
            try
            {
                List<Team> teams = await GetTeams(gender);
                return teams.FirstOrDefault(t => t.Id == id) ?? new Team();
            }
            catch 
            {
                return new Team();
            }
        }
        public async Task<List<Team>> GetTeams(IData.Gender gender)
        {
            try
            {
                string content = await File.ReadAllTextAsync(Path.Combine(filePath, gender.ToString().ToLower(), "teams.json"));
                return JsonConvert.DeserializeObject<List<Team>>(content) ?? new List<Team>();
            }
            catch 
            {
                return new List<Team>();
            }
        }

        public async Task<List<Player>> GetPlayersWithoutSubstitutes(string fifaCode, IData.Gender gender)
        {
            try
            {
                List<Match> matches = await GetMatches(fifaCode, gender);

                if (matches.First().HomeStatistics.Code == fifaCode)
                {
                    return matches.First().GetHomeTeamPlayersWithoutSubstitutes();
                }
                else
                {
                    return matches.First().GetAwayTeamPlayersWithoutSubstitutes();
                }
            }
            catch 
            {
                return new List<Player>();
            }
        }
    }
}
