using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using TowerCreep.TowerCreep2D.Scripts.Towers.TowerLevelData;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Damage
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private List<DamageSource> damageSources;

        private Tower tower;
        private TowerCollectionSlot collectionSlot;
        private TowerProgressionData towerProgressionData;
        private TowerLevelData towerLevelData;

        private void Start()
        {
            tower = GetComponent<Tower>();
            collectionSlot = tower.GetCollectionSlotData();

            towerProgressionData = collectionSlot.TowerProgressionData;
            towerProgressionData.OnTowerLevelChangeChange += ParseAttacks;
            towerLevelData = collectionSlot.TowerLevelData;

            ParseAttacks();
        }

        private void ParseAttacks()
        {
            damageSources = new List<DamageSource>();
            List<TowerLevelDataRecord> records = towerLevelData.GetData(
                towerProgressionData.CurrentLevel
            );
            foreach (TowerLevelDataRecord record in records)
            {
                damageSources.Add(DamageSource.FromData(record));
            }
        }

        public Attack GetAttack()
        {
            return new Attack(this, damageSources);
        }
    }
}