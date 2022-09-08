using System.Collections.Generic;
using TowerCreep.Towers;
using UnityEngine;

namespace TowerCreep.Player.TowerCollection
{
    public class PlayerTowerCollection : MonoBehaviour
    {
        private List<TowerCollectionSlot> playerTowerCollection;

        public void SetTowerCollection(List<TowerData> selectedTowers)
        {
            playerTowerCollection = new List<TowerCollectionSlot>();
            for (int i = 0; i < selectedTowers.Count; i++)
            {
                TowerCollectionSlot newSlot = new TowerCollectionSlot();
                newSlot.Initialize(selectedTowers[i]);
                playerTowerCollection.Add(newSlot);
            }
        }

        public List<TowerCollectionSlot> GetTowerCollection()
        {
            return playerTowerCollection;
        }
    }
}