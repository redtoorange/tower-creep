using System;
using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Utility;
using UnityEngine;
using Portal = TowerCreep.TowerCreep2D.Scripts.Map.Portals.Portal;

namespace TowerCreep.TowerCreep2D.Scripts.Player
{
    /// <summary>
    /// Generic manager for different resources
    /// </summary>
    public class PlayerResourceManager : MonoBehaviour
    {
        public static Action OnPlayerDie;

        public delegate void ResourceChangeHandler(PlayerResourceType resourceType, int oldAmount, int newAmount);

        public static event ResourceChangeHandler OnResourceChange;

        [SerializeField] private int startingHealth = 100;
        [SerializeField] private int startingMana = 400;
        [SerializeField] private int startingGold = 0;
        [SerializeField] private int startingEnergy = 0;
        [SerializeField] private int startingExperience = 0;

        private Dictionary<PlayerResourceType, PlayerResource> playerResources;

        private void Start()
        {
            playerResources = new Dictionary<PlayerResourceType, PlayerResource>();

            playerResources[PlayerResourceType.Health] = new PlayerResource(startingHealth);
            playerResources[PlayerResourceType.Mana] = new PlayerResource(startingMana);
            playerResources[PlayerResourceType.Gold] = new PlayerResource(startingGold);
            playerResources[PlayerResourceType.Energy] = new PlayerResource(startingEnergy);
            playerResources[PlayerResourceType.Experience] = new PlayerResource(startingExperience);

            OnResourceChange?.Invoke(PlayerResourceType.Mana, 0, startingMana);
            OnResourceChange?.Invoke(PlayerResourceType.Health, 100, startingHealth);

            Portal.OnEnemyReachedExit += PlayerTakeDamage;
        }

        private void OnDisable()
        {
            Portal.OnEnemyReachedExit -= PlayerTakeDamage;
        }

        public bool HasRequiredAmount(int requiredAmount, PlayerResourceType type)
        {
            return playerResources[type].currentValue >= requiredAmount;
        }

        public void RemoveResource(int amount, PlayerResourceType type)
        {
            PlayerResource resource = playerResources[type];
            OnResourceChange?.Invoke(
                type,
                resource.currentValue,
                resource.currentValue - amount
            );
            resource.currentValue -= amount;

            if (type == PlayerResourceType.Health && resource.currentValue <= 0)
            {
                OnPlayerDie?.Invoke();
                GameManager.S.ChangeToLoseScreen();
            }
        }

        public void AddResource(int amount, PlayerResourceType type)
        {
            PlayerResource resource = playerResources[type];
            OnResourceChange?.Invoke(
                type,
                resource.currentValue,
                resource.currentValue + amount
            );
            resource.currentValue += amount;
        }

        private void PlayerTakeDamage()
        {
            RemoveResource(10, PlayerResourceType.Health);
        }
    }
}