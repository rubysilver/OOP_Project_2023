using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Player : IComparable<Player>
    {
        private readonly static char Del = '/';

        [JsonProperty("shirt_number")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("position")]
        public string? Position { get; set; }

        public Player()
        {
        }

        public Player(long id, string name, bool captain, string position)
        {
            Id = id;
            Name = name;
            Captain = captain;
            Position = position;
        }

        public override string ToString() => $"{Id}: {(Captain ? "Captain " : "")}{Name}, {Position}";

        public override bool Equals(object? obj) => obj is Player player && Id == player.Id && Name == player.Name;

        public override int GetHashCode() => HashCode.Combine(Id, Name);

        public static string FormatForFile(Player player)
        {
            return $"{player.Id}{Del}{player.Name}{Del}{player.Captain}{Del}{player.Position}";
        }

        public static Player FormatFrom(string information)
        {
            try
            {
                string[] details = information.Split(Del);
                return new Player(int.Parse(details[0]), details[1], bool.Parse(details[2]), details[3]);
            }
            catch (Exception)
            {
                return new Player();
            }
        }

        public int CompareTo(Player? other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
