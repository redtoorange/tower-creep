using System.Collections.Generic;
using UnityEngine;

namespace TowerCreep.Damage
{
    public class Defender : MonoBehaviour
    {
        [SerializeField] private List<DamageSink> damageSinks;

        public Defense GetDefense()
        {
            return new Defense { DamageSinks = damageSinks };
        }
    }
}