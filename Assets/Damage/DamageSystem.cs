using System.Collections.Generic;
using UnityEngine;

namespace TowerCreep.Damage
{
    public class DamageSystem : MonoBehaviour
    {
        public static DamageSystem S;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
            }
            else
            {
                Destroy(this);
                enabled = false;
            }
        }

        public float ProcessAttack(Attack attack, Defender defender)
        {
            float totalDamage = 0.0f;
            Defense defense = defender.GetDefense();

            foreach (DamageSource source in attack.DamageSources)
            {
                float rolledDamage = Random.Range(source.damageMinAmount, source.damageMaxAmount);

                if (source.damageType == DamageType.True)
                {
                    totalDamage += rolledDamage;
                }
                else
                {
                    totalDamage += CalculateDamageReduction(source, defense.DamageSinks, rolledDamage);
                }
            }

            return totalDamage;
        }

        private float CalculateDamageReduction(DamageSource source, List<DamageSink> sinks, float amount)
        {
            float totalPercent = 0.0f;
            foreach (DamageSink sink in sinks)
            {
                if (sink.defenseType == source.damageType && (sink.defenseSubType == DamageSubType.None ||
                                                              sink.defenseSubType == source.damageSubType))
                {
                    totalPercent += sink.defensePercent;
                }
            }

            float damageModifier = amount * totalPercent;
            return amount - damageModifier;
        }
    }
}