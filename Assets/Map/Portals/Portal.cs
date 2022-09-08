using System;
using Godot;
using TowerCreep.Player;

namespace TowerCreep.Map.Portals
{
    public enum PortalType
    {
        Entrance,
        Exit
    }

    public class Portal : Area2D
    {
        public static Action OnEnemyReachedExit;

        [Export] private PortalType portalType = PortalType.Entrance;

        private Portal spawnLocation;

        public void ExitInitialize(Portal spawnLocation)
        {
            this.spawnLocation = spawnLocation;
        }

        public override void _Ready()
        {
            if (portalType == PortalType.Exit)
            {
                Connect("body_entered", this, nameof(HandleBodyEntered));
            }
        }

        private void HandleBodyEntered(Node body)
        {
            if (body is Enemy.Enemy e)
            {
                OnEnemyReachedExit?.Invoke();
                e.TeleportToSpawn(spawnLocation);
            }
        }
    }
}