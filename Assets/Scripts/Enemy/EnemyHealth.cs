using System;
using System.Collections.Generic;
using TowerCreep.Damage;
using TowerCreep.Interface.DamagePopups;
using TowerCreep.Interface.HealthBar;
using TowerCreep.Towers;
using UnityEngine;

namespace TowerCreep.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public Action OnDie;

        [SerializeField] private EnemyHealthBar healthBar;

        private float maxHealth;
        private float health;
        private int experienceReward;

        private Dictionary<Attacker, float> allAttackerDamage;

        private void Start()
        {
            allAttackerDamage = new Dictionary<Attacker, float>();
        }

        public void Initialize(float maxHealth, int experienceReward)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            this.experienceReward = experienceReward;
        }

        public void TakeDamage(float damage, Attacker attacker)
        {
            // Prevent overkilling
            if (damage > health)
            {
                damage = health;
            }

            health -= damage;
            healthBar.SetFillPercent(health / maxHealth);

            if (!allAttackerDamage.ContainsKey(attacker))
            {
                allAttackerDamage.Add(attacker, damage);
            }
            else
            {
                allAttackerDamage[attacker] += damage;
            }


            DamagePopupController.S.CreatePopup(transform.position, damage.ToString("0"));

            if (health <= 0)
            {
                // All towers that attacked get experience based on how much damage that did
                foreach (KeyValuePair<Attacker, float> valuePair in allAttackerDamage)
                {
                    if (valuePair.Key.TryGetComponent(out Tower attackingTower))
                    {
                        float weightedExperience = experienceReward * (valuePair.Value / maxHealth);
                        attackingTower.RewardExperience(Mathf.RoundToInt(weightedExperience));
                    }
                }

                OnDie?.Invoke();
            }
        }
    }
}