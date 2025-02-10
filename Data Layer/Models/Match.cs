using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    
    public class Match : IComparable<Match>
    {
        public static int counter = 0;
        public int Id { get; set; }

        [JsonProperty("venue")]
        public string? Venue { get; set; }

        [JsonProperty("location")]
        public string? Location { get; set; }

        [JsonProperty("fifa_id")]
        public long? FifaId { get; set; }

        [JsonProperty("attendance")]
        public int Attendance { get; set; }

        [JsonProperty("officials")]
        public List<string>? Officials { get; set; }

        [JsonProperty("stage_name")]
        public string? StageName { get; set; }

        [JsonProperty("home_team_country")]
        public string? HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string? AwayTeamCountry { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset? Datetime { get; set; }

        // Winner of the match
        [JsonProperty("winner")]
        public string? Winner { get; set; }

        [JsonProperty("winner_code")]
        public string? WinnerCode { get; set; }

        // Main statistics on goals and penalties for home and away team.
        [JsonProperty("home_team")]
        public MatchStatistics HomeStatistics { get; set; }

        [JsonProperty("away_team")]
        public MatchStatistics AwayStatistics { get; set; }

        // Information on events during the match. Goals, penalties, red cards, etc. 

        [JsonProperty("home_team_events")]
        public List<MatchEvent> HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public List<MatchEvent> AwayTeamEvents { get; set; }

        // Detailed statistics on everything but goals and penalties
        [JsonProperty("home_team_statistics")]
        public DetailedMatchStatistics HomeExtraStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public DetailedMatchStatistics AwayExtraStatistics { get; set; }

        public Match(string? venue, string? location, long? fifaId, int attendance, List<string> officials, string? stageName, string? homeTeamCountry, string? awayTeamCountry, DateTimeOffset datetime, string? winner, string? winnerCode, MatchStatistics homeStatistics, MatchStatistics awayStatistics, List<MatchEvent> homeTeamEvents, List<MatchEvent> awayTeamEvents, DetailedMatchStatistics homeExtraStatistics, DetailedMatchStatistics awayExtraStatistics)
        {
            Id = ++counter - 7;
            Venue = venue;
            Location = location;
            FifaId = fifaId;
            Attendance = attendance;
            Officials = officials;
            StageName = stageName;
            HomeTeamCountry = homeTeamCountry;
            AwayTeamCountry = awayTeamCountry;
            Datetime = datetime;
            Winner = winner;
            WinnerCode = winnerCode;
            HomeStatistics = homeStatistics;
            AwayStatistics = awayStatistics;
            HomeTeamEvents = homeTeamEvents;
            AwayTeamEvents = awayTeamEvents;
            HomeExtraStatistics = homeExtraStatistics;
            AwayExtraStatistics = awayExtraStatistics;
        }

        public override string ToString() => $"{Id}:{HomeTeamCountry} vs {AwayTeamCountry}, {Winner}({WinnerCode}) at {(HomeStatistics.Goals > AwayStatistics.Goals ? HomeStatistics.Goals : AwayStatistics.Goals)} to {(HomeStatistics.Goals < AwayStatistics.Goals ? HomeStatistics.Goals : AwayStatistics.Goals)}.";

        public override bool Equals(object? obj) => obj is Match match && FifaId == match.FifaId;

        public override int GetHashCode() => HashCode.Combine(FifaId);

        public List<Player> GetHomeTeamPlayers()
        {
            List<Player> list = new List<Player>();

            foreach (var player in HomeExtraStatistics.MatchPlayers)
            {
                list.Add(player);
            }

            foreach (var player in HomeExtraStatistics.SubstitutePlayers)
            {
                list.Add(player);
            }

            return list;
        }

        public List<Player> GetAwayTeamPlayers()
        {
            List<Player> list = new List<Player>();

            foreach (var player in AwayExtraStatistics.MatchPlayers)
            {
                list.Add(player);
            }

            foreach (var player in AwayExtraStatistics.SubstitutePlayers)
            {
                list.Add(player);
            }

            return list;
        }

        public List<Player> GetHomeTeamPlayersWithoutSubstitutes()
        {
            List<Player> list = new List<Player>();

            foreach (var player in HomeExtraStatistics.MatchPlayers)
            {
                list.Add(player);
            }

            return list;
        }

        public List<Player> GetAwayTeamPlayersWithoutSubstitutes()
        {
            List<Player> list = new List<Player>();

            foreach (var player in AwayExtraStatistics.MatchPlayers)
            {
                list.Add(player);
            }

            return list;
        }

        public int CompareTo(Match? other)
        {
            return -Attendance.CompareTo(other.Attendance);
        }
    }

    public class MatchEvent
    {

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("type_of_event")]
        public string? TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string? Player { get; set; }

        [JsonProperty("time")]
        public string? Time { get; set; }

        public MatchEvent(long? id, string? typeOfEvent, string? player, string? time)
        {
            Id = id;
            TypeOfEvent = typeOfEvent;
            Player = player;
            Time = time;
        }

        public override string ToString() => $"{Player} - {TypeOfEvent} at {Time}";

        public override bool Equals(object? obj)
        {
            return obj is MatchEvent @event &&
                   Id == @event.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }

    public class MatchStatistics
    {

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("goals")]
        public long? Goals { get; set; }

        [JsonProperty("penalties")]
        public long? Penalties { get; set; }

        public MatchStatistics(string? country, string? code, long? goals, long? penalties)
        {
            Country = country;
            Code = code;
            Goals = goals;
            Penalties = penalties;
        }

        public override string ToString() => $"{Country}({Code}) - {Goals} goals, {Penalties} penalties.";
    }

    public class DetailedMatchStatistics
    {

        [JsonProperty("country")]
        public string? Name { get; set; }

        [JsonProperty("attempts_on_goal")]
        public long? AttemptsOnGoal { get; set; }

        [JsonProperty("on_target")]
        public long? OnTarget { get; set; }

        [JsonProperty("off_target")]
        public long? OffTarget { get; set; }

        [JsonProperty("blocked")]
        public long? Blocked { get; set; }

        [JsonProperty("woodwork")]
        public long? Woodwork { get; set; }

        [JsonProperty("corners")]
        public long? Corners { get; set; }

        [JsonProperty("offsides")]
        public long? Offsides { get; set; }

        [JsonProperty("ball_possession")]
        public long? BallPossession { get; set; }

        [JsonProperty("pass_accuracy")]
        public long? PassAccuracy { get; set; }

        [JsonProperty("num_passes")]
        public long? NumPasses { get; set; }

        [JsonProperty("passes_completed")]
        public long? PassesCompleted { get; set; }

        [JsonProperty("distance_covered")]
        public long? DistanceCovered { get; set; }

        [JsonProperty("balls_recovered")]
        public long? BallsRecovered { get; set; }

        [JsonProperty("tackles")]
        public long? Tackles { get; set; }

        [JsonProperty("clearances")]
        public long? Clearances { get; set; }

        [JsonProperty("yellow_cards")]
        public long? YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public long? RedCards { get; set; }

        [JsonProperty("fouls_committed")]
        public long? FoulsCommitted { get; set; }

        [JsonProperty("tactics")]
        public string? Tactics { get; set; }

        [JsonProperty("starting_eleven")]
        public List<Player> MatchPlayers { get; set; }

        [JsonProperty("substitutes")]
        public List<Player> SubstitutePlayers { get; set; }

        public DetailedMatchStatistics(string? name, long? attemptsOnGoal, long? onTarget, long? offTarget, long? blocked, long? woodwork, long? corners, long? offsides, long? ballPossession, long? passAccuracy, long? numPasses, long? passesCompleted, long? distanceCovered, long? ballsRecovered, long? tackles, long? clearances, long? yellowCards, long? redCards, long? foulsCommitted, string? tactics, List<Player> matchPlayers, List<Player> substitutePlayers)
        {
            Name = name;
            AttemptsOnGoal = attemptsOnGoal;
            OnTarget = onTarget;
            OffTarget = offTarget;
            Blocked = blocked;
            Woodwork = woodwork;
            Corners = corners;
            Offsides = offsides;
            BallPossession = ballPossession;
            PassAccuracy = passAccuracy;
            NumPasses = numPasses;
            PassesCompleted = passesCompleted;
            DistanceCovered = distanceCovered;
            BallsRecovered = ballsRecovered;
            Tackles = tackles;
            Clearances = clearances;
            YellowCards = yellowCards;
            RedCards = redCards;
            FoulsCommitted = foulsCommitted;
            Tactics = tactics;
            MatchPlayers = matchPlayers;
            SubstitutePlayers = substitutePlayers;
        }

        public override bool Equals(object? obj)
        {
            return obj is DetailedMatchStatistics statistics &&
                   Name == statistics.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
