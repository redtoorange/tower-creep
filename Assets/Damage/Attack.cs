using System;
using System.Collections.Generic;

namespace TowerCreep.Damage
{
    [Serializable]
    public struct Attack
    {
        public List<DamageSource> DamageSources;
    }
}