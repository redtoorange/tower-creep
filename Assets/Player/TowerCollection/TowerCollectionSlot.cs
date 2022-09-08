using TowerCreep.Towers;

namespace TowerCreep.Player.TowerCollection
{
    public class TowerCollectionSlot
    {
        // public TowerHotbarSlot CollectionHotBarSlot { get; set; }
        public TowerData CollectionTowerData { get; private set; }
        public TowerProgressionData TowerProgressionData { get; private set; }
        public bool IsPlaced { get; set; }

        public void Initialize(TowerData data)
        {
            CollectionTowerData = data;
            TowerProgressionData = new TowerProgressionData();
        }
    }
}