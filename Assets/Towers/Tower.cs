
using System;
using TowerCreep.Player.TowerCollection;
using UnityEngine;

namespace TowerCreep.Towers
{
    public class Tower : MonoBehaviour
    {
        public Action OnTowerPlaced;
        public Action OnTowerRemoved;
        
        [SerializeField] private SpriteRenderer towerSprite;
        private TowerCollectionSlot collectionSlot;

        public void PlaceTower(TowerCollectionSlot collectionSlot)
        {
            this.collectionSlot = collectionSlot;
            this.collectionSlot.IsPlaced = true;

            towerSprite.sprite = this.collectionSlot.CollectionTowerData.towerIcon;
            
            OnTowerPlaced?.Invoke();
        }

        public void RemoveTower()
        {
            collectionSlot.IsPlaced = false;
            OnTowerRemoved?.Invoke();
        }

        public TowerCollectionSlot GetCollectionSlotData()
        {
            return collectionSlot;
        }
    }
}