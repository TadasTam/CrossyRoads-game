using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Player
{
    public class Score
    {
        private double score { get; set; }

        public Score(double score = 0) { this.score = score;  }

        public double Get() { return score; }
        public void Set(double newPosition) 
        {
            score = newPosition > score ? newPosition : score;
        }
    }
}
