using System;
using Godot;

namespace TowerCreep.Enemy.Resources.Waves
{
    [Serializable]
    public class EnemyWaveData : Resource
    {
        [Export] public PackedScene enemyBaseScene;
        [Export] public MonsterData.MonsterData monsterData;
        [Export] public int spawnCount = 10;
        [Export] public float spawnInterval = 1.0f;
    }
}