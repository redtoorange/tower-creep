using System.Collections.Generic;
using TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList;
using TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public class TowerSelectionMenu : MonoBehaviour
    {
        [SerializeField] private int minimumTowers = 9;
        [SerializeField] private DetailsPanel.DetailsPanel detailsPanel;
        [SerializeField] private SelectedTowerContainer selectedTowerContainer;
        [SerializeField] private Button readyButton;

        private void Start()
        {
            selectedTowerContainer.OnReadyCountChanged += HandleOnReadyCountChanged;
            readyButton.onClick.AddListener(HandleReadyPressed);
        }

        private void HandleOnReadyCountChanged(int newCount)
        {
            readyButton.enabled = newCount < minimumTowers;
        }

        private void OnEnable()
        {
            AvailableTowerSlot.OnOpenManual += HandleOpenManual;
            AvailableTowerSlot.OnAddToSelected += HandleAddToSelected;

            SelectedTowerSlot.OnOpenManual += HandleOpenManual;
            SelectedTowerSlot.OnRemoveFromSelected += HandleRemoveFromSelected;
        }


        private void OnDisable()
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
            PlayerTowerCollectionManager.S.SetTowerCollection(selectedTowers);

            SceneManager.LoadScene(0);
        }
    }
}