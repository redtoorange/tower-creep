using Godot;
using TowerCreep.Player.TowerCollection;

namespace TowerCreep.Towers
{
    public class Tower : StaticBody2D
    {
        public TowerCollectionSlot CollectionSlotData { get; set; }
    }
}