using System;
using System.Collections.Generic;
using Godot;
using Portal = TowerCreep.Map.Portals.Portal;

namespace TowerCreep.Player
{
    /// <summary>
    /// Generic manager for different resources
    /// </summary>
    public class PlayerResourceManager : Node
    {
        public static Action OnPlayerDie;

        public delegate void ResourceChangeHandler(PlayerResourceType resourceType, int oldAmount, int newAmount);

        public static event ResourceChangeHandler OnResourceChange;

        [Export] private int startingHealth = 100;
        [Export] private int startingMana = 400;
        [Export] private int startingGold = 0;
        [Export] private int startingEnergy = 0;
        [Export] private int startingExperience = 0;

        private Dictionary<PlayerResourceType, PlayerResource> playerResources;

        public override void _Ready()
        {
            GD.Print("Creating new resource manager");
            playerResources = new Dictionary<PlayerResourceType, PlayerResource>();

            playerResources[PlayerResourceType.Health] = new PlayerResource(startingHealth);
            playerResources[PlayerResourceType.Mana] = new PlayerResource(startingMana);
            playerResources[PlayerResourceType.Gold] = new PlayerResource(startingGold);
            playerResources[PlayerResourceType.Energy] = new PlayerResource(startingEnergy);
            playerResources[PlayerResourceType.Experience] = new PlayerResource(startingExperience);

            OnResourceChange?.Invoke(PlayerResourceType.Mana, 0, startingMana);
            OnResourceChange?.Invoke(PlayerResourceType.Health, 100, startingHealth);

            Portal.OnEnemyReachedExit += PlayerTakeDamage;
            
            GD.Print("Player Health: ", playerResources[PlayerResourceType.Health]);
        }

        public override void _ExitTree()
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