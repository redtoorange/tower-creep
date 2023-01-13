using System.Collections.Generic;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Damage
{
    public class Defender : MonoBehaviour
    {
        [SerializeField] private List<DamageSink> damageSinks;

        public Defense GetDefense()
        {
            return new Defense { DamageSinks = damageSinks };
        }

        public void SetDamageSinks(List<DamageSink> enemyDataDamageSinks)
        {
            damageSinks = enemyDataDamageSinks;
        }
    }
}