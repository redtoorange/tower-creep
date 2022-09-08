using Godot;

namespace TowerCreep.Map.Portals
{
    public class PortalController : Node2D
    {
        private Portal spawnPortal;
        private Portal exitPortal;

        public override void _Ready()
        {
            spawnPortal = GetNode<Portal>("SpawnPortal");
            exitPortal = GetNode<Portal>("ExitPortal");
            exitPortal.ExitInitialize(spawnPortal);
        }
    }
}