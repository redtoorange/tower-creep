using System;
using System.Collections.Generic;
using TowerCreep.Levels.DungeonLevels;
using TowerCreep.Player.TowerCollection;
using UnityEngine;

namespace TowerCreep.Towers.Placement
{
    public class TowerController : MonoBehaviour
    {
        public static Action<TowerCollectionSlot> OnSetTowerAsAvailable;
        
        private List<Tower> controlledTowers;
        [SerializeField] private DungeonLevel currentDungeonLevel;

        private void Start()
        {
            controlledTowers = new List<Tower>();
            currentDungeonLevel.OnPlayerExitedLevel += HandlePlayerExitedLevel;
        }

        public void AddTower( Tower tower)
        {
            controlledTowers.Add(tower);
        }
        
        private void HandlePlayerExitedLevel(DungeonLevel level)
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
            TowerCollectionSlot collectionSlot = which.GetCollectionSlotData();
            collectionSlot.IsPlaced = false;
            OnSetTowerAsAvailable?.Invoke(collectionSlot);
        }
    }
}