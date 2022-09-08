using Godot;

namespace TowerCreep.Towers
{
    public class TowerData : Resource
    {
        [Export] public Texture towerIcon;
        [Export] public Texture disabledTowerIcon;
        [Export] public string towerName = "Test Data";
        [Export] public int towerBaseCost = 100;
        [Export] public PackedScene towerPrefab;
        [Export(PropertyHint.MultilineText)] public string towerInformation = "";
    }
}