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
            int totalDamage = 0;
            Defense defense = defender.GetDefense();

            foreach (DamageSource source in attack.DamageSources)
            {
                int rolledDamage = Mathf.RoundToInt(Random.Range(source.damageMinAmount, source.damageMaxAmount));
                Debug.Log($"Damage Source [{source.damageType}, {source.damageSubType}] rolled {rolledDamage} damage");

                if (source.damageType == DamageType.True)
                {
                    totalDamage += rolledDamage;
                    Debug.Log($"\tTrue damage, {rolledDamage} damage applied");
                }
                else
                {
                    int temp = CalculateDamageReduction(source, defense.DamageSinks, rolledDamage);
                    Debug.Log($"\tReduced damage of {temp} applied");
                    totalDamage += temp;
                }
            }

            Debug.Log($"Total damage: {totalDamage}");
            return totalDamage;
        }

        private int CalculateDamageReduction(DamageSource source, List<DamageSink> sinks, int amount)
        {
            bool hasNoSubtype = false;
            DamageSink noSubtype = new DamageSink();
            bool hasMatchingSubtype = false;
            DamageSink matchingSubtype = new DamageSink();

            foreach (DamageSink sink in sinks)
            {
                if (sink.defenseType == source.damageType)
                {
                    if (sink.defenseSubType == DamageSubType.None)
                    {
                        Debug.Log($"\tMatching Type without a subtype: {sink.defensePercent}");
                        hasNoSubtype = true;
                        noSubtype = sink;
                    }
                    else if (sink.defenseSubType == source.damageSubType)
                    {
                        Debug.Log($"\tMatching Type and Subtype: {sink.defensePercent}");
                        hasMatchingSubtype = true;
                        matchingSubtype = sink;
                    }
                }
            }

            int modifiedDamage = amount;
            if (hasMatchingSubtype)
            {
                int modifier = Mathf.FloorToInt(modifiedDamage * matchingSubtype.defensePercent);
                modifiedDamage -= modifier;
            }

            if (hasNoSubtype)
            {
                int modifier = Mathf.FloorToInt(modifiedDamage * noSubtype.defensePercent);
                modifiedDamage -= modifier;
            }

            return modifiedDamage;
        }
    }
}