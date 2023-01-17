using System;
using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Map.DungeonLevels;
using TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.Placement
{
    public class TowerController : MonoBehaviour
    {
        public static Action<PlayerPartySlot> OnSetTowerAsAvailable;
        public static Action<Tower> OnTowerRemoved;

        private List<Tower> controlledTowers;
        [SerializeField] private DungeonLevel currentDungeonLevel;

        private void Start()
        {
            controlledTowers = new List<Tower>();
            currentDungeonLevel.OnPlayerExitedLevel += HandlePlayerExitedLevel;
        }

        public void AddTower(Tower tower)
        {
            controlledTowers.Add(tower);
        }

        private void HandlePlayerExitedLevel()
        {
            for (int i = 0; i < controlledTowers.Count; i++)
            {
                DeconstructTower(controlledTowers[i]);
                Destroy(controlledTowers[i].gameObject);
            }

            controlledTowers.Clear();
        }

        private void DeconstructTower(Tower which)
        {
            which.SetHovered(false);
            which.SetSelected(false);
            OnTowerRemoved?.Invoke(which);
            PlayerPartySlot collectionSlot = which.GetCollectionSlotData();
            collectionSlot.IsPlaced = false;
            OnSetTowerAsAvailable?.Invoke(collectionSlot);
        }

        public void GiveExperienceToAll(int amount)
        {
            for (int i = 0; i < controlledTowers.Count; i++)
            {
                controlledTowers[i].RewardExperience(amount);
            }
        }
    }
}