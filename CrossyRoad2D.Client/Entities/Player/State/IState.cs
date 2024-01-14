using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Player.State
{
    public interface IState
    {
        void ConsumeHeart();
        void CheckCollisionsWithLethalCollidables();
        void UpdatePlayerState();
        string GetTimeRemainingString();
    }
}
