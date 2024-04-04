using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace FlappyBird_WebAPI.Models
{
    public class Score
    {
       
        public int id { get; set; }
        public string? pseudo { get; set; }
        public string? date { get; set; }
        public string timeInSeconds { get; set; }
        public bool isPublic { get; set; }
        public int scoreValue { get; set; }
        [JsonIgnore]
        public virtual User? _user { get; set; }



    }
}
