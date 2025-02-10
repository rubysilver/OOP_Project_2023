using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Team : IComparable<Team>
    {
        private readonly static char Del = '|';

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public string? Name { get; set; }

        [JsonProperty("alternate_name")]
        public string? AlternateName { get; set; }

        [JsonProperty("fifa_code")]
        public string? FifaCode { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("group_letter")]
        public string? GroupLetter { get; set; }

        public Team()
        {
        }

        public Team(long id, string name, string? alternateName, string fifaCode, long groupId, string groupLetter)
        {
            Id = id;
            Name = name;
            AlternateName = alternateName;
            FifaCode = fifaCode;
            GroupId = groupId;
            GroupLetter = groupLetter;
        }

        public static string FormatForFile(Team team) {
            return $"{team.Id}{Del}{team.Name}{Del}{team.FifaCode}{Del}{(team.AlternateName is null ? "null" : team.AlternateName)}{Del}{team.GroupLetter}{Del}{team.GroupId}";
        }

        public static Team FormatFrom(string information)
        {
            try
            {
                string[] details = information.Split(Del);
                return new Team(int.Parse(details[0]), details[1], details[2], details[3], long.Parse(details[4]), details[5]);
            }
            catch (Exception)
            {
                return new Team();
            }
        }

        public override string ToString() => $"{Name} ({FifaCode})";

        public override bool Equals(object? obj) => obj is Team team && Id == team.Id;

        public override int GetHashCode() => HashCode.Combine(Id);

        public int CompareTo(Team? other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
