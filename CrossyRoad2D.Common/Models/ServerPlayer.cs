using CrossyRoad2D.Common.Models;
using System.Security.Cryptography.X509Certificates;

namespace CrossyRoad2D.Server.Models
{
    public class ServerPlayer
    {
        public string PlayerServerId { get; set; }
        public Color Color { get; set; }
        public Position Position { get; set; }
        public int PlayerCopyId { get; set; }
        public bool IsBadCopy { get; set; }
    }
}
