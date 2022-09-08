using Godot;

namespace TowerCreep.Enemy.Resources.MonsterData
{
    public class MonsterData : Resource
    {
        [Export] public Texture mobSprite;
        [Export] public int mobHealth = 10;
        [Export] public int mobSpeed = 50;
        [Export] public int mobDamage = 1;
    }
}