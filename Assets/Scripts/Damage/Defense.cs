using System;
using System.Collections.Generic;

namespace TowerCreep.Damage
{
    [Serializable]
    public struct Defense
    {
        public List<DamageSink> DamageSinks;
    }
}