using UnityEngine;

namespace TowerCreep.Enemy.Resources.MonsterData
{
    [CreateAssetMenu(fileName = "Data", menuName = "TowerCreep/MonsterData", order = 1)]
    public class MonsterData : ScriptableObject
    {
        [SerializeField] public Sprite mobSprite;
        [SerializeField] public int mobHealth = 10;
        [SerializeField] public int mobSpeed = 50;
        [SerializeField] public int mobDamage = 1;
    }
}