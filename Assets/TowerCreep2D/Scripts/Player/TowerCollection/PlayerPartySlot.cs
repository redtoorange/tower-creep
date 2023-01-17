using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Interface.HotBar;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using TowerCreep.TowerCreep2D.Scripts.Towers.TowerClassProgression;

namespace TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection
{
    public class PlayerPartySlot
    {
        public TowerHotBarSlot CollectionHotBarSlot { get; set; }
        public TowerData CollectionTowerData { get; private set; }
        public TowerInstanceProgressionData TowerInstanceProgressionData { get; private set; }
        public TowerClassProgressionData TowerClassProgressionData { get; private set; }
        public bool IsPlaced { get; set; }

        public void Initialize(TowerData data)
        {
            if (data)
            {
                CollectionTowerData = data;
                TowerInstanceProgressionData = new TowerInstanceProgressionData();
                TowerClassProgressionData = TowerClassProgressionDataManager.S.GetLevelData(data);
            }
        }

        public List<TowerClassProgressionDataRecord> GetCurrentLevelRecordData()
        {
            return TowerClassProgressionData.GetData(TowerInstanceProgressionData.CurrentLevel);
        }
    }
}