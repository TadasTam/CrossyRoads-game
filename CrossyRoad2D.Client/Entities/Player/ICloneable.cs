using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Player
{
    internal interface ICloneable<T>
    {
        public T CloneDeep();
        public T CloneShallow();
    }
}
