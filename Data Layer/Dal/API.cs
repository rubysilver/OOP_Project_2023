using Data_Layer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data_Layer.DAL.IData;

namespace Data_Layer.DAL
{
    internal class API : IData
    {
        public async Task<Match> GetMatch(int id, Gender gender)
        {
            try
            {
                List<Match> team = await GetMatches(gender) as List<Match>;
                return team.FirstOrDefault(t => t.Id == id);
            }
            catch 
            {
                return null;
            }
        }
        public async Task<List<Match>> GetMatches(Gender gender)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://worldcup-vua.nullbit.hr/{gender.ToString().ToLower()}/matches");

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Match>>(content) ?? new List<Match>();
                    }

                }
                return new List<Match>();
            }
            catch 
            {
                return new List<Match>();
            }
        }
        public async Task<List<Match>> GetMatches(string fifaCode, Gender gender)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://worldcup-vua.nullbit.hr/{gender.ToString().ToLower()}/matches/country?fifa_code={fifaCode.ToUpper()}");

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Match>>(content) ?? new List<Match>();
                    }

                }
                return new List<Match>();
            }
            catch 
            {
                return new List<Match>();
            }
        }
        public async Task<Player> GetPlayer(int id, string fifaCode, Gender gender)
        {
            try
            {
                List<Player> players = await GetPlayers(fifaCode, gender) as List<Player>;
                return players.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Player>> GetPlayers(string fifaCode, Gender gender)
        {
            try
            {
                List<Match> matches = await GetMatches(fifaCode, gender);

                if (matches.First().HomeStatistics.Code == fifaCode)
                {
                    return matches.First().GetHomeTeamPlayers();
                } else
                {
                    return matches.First().GetAwayTeamPlayers();
                }
            }
            catch 
            {
                return new List<Player>();
            }
        }

        public async Task<List<Player>> GetPlayersWithoutSubstitutes(string fifaCode, Gender gender)
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

        public async Task<Team> GetTeam(int id, Gender gender)
        {
            try
            {
                List<Team> team = await GetTeams(gender) as List<Team>;
                return team.FirstOrDefault(t => t.Id == id) ?? new Team();
            }
            catch 
            {
                return new Team();
            }
        }
        public async Task<List<Team>> GetTeams(Gender gender)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://worldcup-vua.nullbit.hr/{gender.ToString().ToLower()}/teams");

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Team>>(content) ?? new List<Team>();
                    }

                }
                return new List<Team>();
            }
            catch 
            {
                return new List<Team>();
            }
        }
    }
}
