using System.Collections.Generic;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers;
using TowerCreep.Towers.TowerClassProgression;
using UnityEngine;

namespace TowerCreep.Damage
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private List<DamageSource> damageSources;

        private Tower tower;
        private PlayerPartySlot collectionSlot;
        private TowerInstanceProgressionData towerInstanceProgressionData;
        private TowerClassProgressionData towerClassProgressionData;

        private void Start()
        {
            tower = GetComponent<Tower>();
            collectionSlot = tower.GetCollectionSlotData();

            towerInstanceProgressionData = collectionSlot.TowerInstanceProgressionData;
            towerInstanceProgressionData.OnTowerInstanceLevelChange += ParseAttacks;
            towerClassProgressionData = collectionSlot.TowerClassProgressionData;

            ParseAttacks();
        }

        private void ParseAttacks()
        {
            damageSources = new List<DamageSource>();
            List<TowerClassProgressionDataRecord> records = towerClassProgressionData.GetData(
                towerInstanceProgressionData.CurrentLevel
            );
            foreach (TowerClassProgressionDataRecord record in records)
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