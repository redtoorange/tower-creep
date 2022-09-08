using System.Collections.Generic;
using Godot;
using TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList;
using TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public class TowerSelectionMenu : Control
    {
        [Export] private int minimumTowers = 9;

        [Export] private NodePath detailsPanelPath;
        private DetailsPanel.DetailsPanel detailsPanel;

        [Export] private NodePath selectedTowerContainerPath;
        private SelectedTowerContainer selectedTowerContainer;

        [Export] private NodePath readyButtonPath;
        private Button readyButton;


        public override void _Ready()
        {
            detailsPanel = GetNode<DetailsPanel.DetailsPanel>(detailsPanelPath);
            selectedTowerContainer = GetNode<SelectedTowerContainer>(selectedTowerContainerPath);
            selectedTowerContainer.OnReadyCountChanged += HandleOnReadyCountChanged;
            readyButton = GetNode<Button>(readyButtonPath);
            readyButton.Connect("pressed", this, nameof(HandleReadyPressed));
        }

        private void HandleOnReadyCountChanged(int newCount)
        {
            readyButton.Disabled = newCount < minimumTowers;
        }

        public override void _EnterTree()
        {
            AvailableTowerSlot.OnOpenManual += HandleOpenManual;
            AvailableTowerSlot.OnAddToSelected += HandleAddToSelected;

            SelectedTowerSlot.OnOpenManual += HandleOpenManual;
            SelectedTowerSlot.OnRemoveFromSelected += HandleRemoveFromSelected;
        }

        public override void _ExitTree()
        {
            AvailableTowerSlot.OnOpenManual -= HandleOpenManual;
            AvailableTowerSlot.OnAddToSelected -= HandleAddToSelected;

            SelectedTowerSlot.OnOpenManual -= HandleOpenManual;
            SelectedTowerSlot.OnRemoveFromSelected -= HandleRemoveFromSelected;
        }

        private void HandleOpenManual(SelectedTowerSlot slot)
        {
            detailsPanel.ShowTowerData(slot.GetTowerData());
        }

        private void HandleOpenManual(AvailableTowerSlot slot)
        {
            detailsPanel.ShowTowerData(slot.GetTowerData());
        }

        private void HandleRemoveFromSelected(SelectedTowerSlot slot)
        {
            selectedTowerContainer.RemoveTower(slot);
        }

        private void HandleAddToSelected(AvailableTowerSlot slot)
        {
            selectedTowerContainer.AddTower(slot.GetTowerData());
        }

        private void HandleReadyPressed()
        {
            List<TowerData> selectedTowers = selectedTowerContainer.GetSelectedTowers();
            PlayerTowerCollection.S.SetTowerCollection(selectedTowers);

            GetTree().ChangeScene("res://Main/MainGame.tscn");
        }
    }
}