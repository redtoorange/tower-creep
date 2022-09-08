using System.Collections.Generic;
using Godot;
using TowerCreep.Towers;

namespace TowerCreep.Player.TowerCollection
{
    public class PlayerTowerCollection : Node
    {
        public static PlayerTowerCollection S;

        private List<TowerCollectionSlot> playerTowerCollection;

        public override void _EnterTree()
        {
            S = this;
        }

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