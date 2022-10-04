using System.Collections.Generic;
using UnityEngine;

namespace TowerCreep.Damage
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private List<DamageSource> damageSources;

        public Attack GetAttack()
        {
            return new Attack(this, damageSources);
        }
    }
}