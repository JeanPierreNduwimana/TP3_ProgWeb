using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace FlappyBird_WebAPI.Models
{
    public class Score
    {
       
        public int Id { get; set; }
        public double Temps { get; set; }
        public bool Visibilité { get; set; }
        public int Score_Joueur { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }



    }
}
