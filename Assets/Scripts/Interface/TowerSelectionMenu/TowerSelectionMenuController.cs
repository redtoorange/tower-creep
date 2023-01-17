using System.Collections.Generic;
using TowerCreep.Interface.DetailsPanel;
using TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList;
using TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList;
using TowerCreep.Towers;
using TowerCreep.Utility;
using UnityEngine;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public class TowerSelectionMenuController : MonoBehaviour
    {
        public static TowerSelectionMenuController S;

        [SerializeField] private List<AvailableTowerSlot> availableTowerSlots;
        private List<TowerData> availableTowers;
        [SerializeField] private List<SelectedTowerSlot> selectedTowerSlots;
        [SerializeField] private TowerSelectionButtonController towerSelectionButtonController;
        [SerializeField] private TowerDetailsPanel towerDetailsPanel;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
            }
            else
            {
                Debug.LogError("Two TowerSelectionMenuController's detected");
                enabled = false;
                Destroy(this);
            }
        }

        private void Start()
        {
            availableTowers = new List<TowerData>();
            for (int i = 0; i < availableTowerSlots.Count; i++)
            {
                if (availableTowerSlots[i].IsAvailable())
                {
                    availableTowers.Add(availableTowerSlots[i].GetTowerData());
                }
            }
        }

        private void OnEnable()
        {
            SelectedTowerSlot.OnSelectedSlotDataChanged += HandleOnSelectedSlotDataChanged;
            SelectedTowerSlot.OnSelectedSlotDoubleClicked += HandleOnSelectedSlotDoubleClicked;

            AvailableTowerSlot.OnAvailableSlotRightClicked += HandleOnAvailableSlotRightClicked;
            AvailableTowerSlot.OnAvailableSlotDoubleClicked += HandleOnAvailableSlotDoubleClicked;

            towerSelectionButtonController.OnResetPressed += ClearSelectedTowers;
            towerSelectionButtonController.OnReadyPressed += HandleReadyPressed;
            towerSelectionButtonController.OnRandomPressed += HandleRandomPressed;
        }


        private void OnDisable()
        {
            SelectedTowerSlot.OnSelectedSlotDataChanged -= HandleOnSelectedSlotDataChanged;
            SelectedTowerSlot.OnSelectedSlotDoubleClicked -= HandleOnSelectedSlotDoubleClicked;

            AvailableTowerSlot.OnAvailableSlotRightClicked -= HandleOnAvailableSlotRightClicked;
            AvailableTowerSlot.OnAvailableSlotDoubleClicked -= HandleOnAvailableSlotDoubleClicked;

            towerSelectionButtonController.OnResetPressed -= ClearSelectedTowers;
            towerSelectionButtonController.OnReadyPressed -= HandleReadyPressed;
            towerSelectionButtonController.OnRandomPressed -= HandleRandomPressed;
        }

        private void HandleOnAvailableSlotRightClicked(AvailableTowerSlot slot)
        {
            if (!ReferenceEquals(slot.GetTowerData(), null))
            {
                AddTowerToNextSlot(slot.GetTowerData());
            }
        }

        private void HandleOnAvailableSlotDoubleClicked(AvailableTowerSlot slot)
        {
            if (!ReferenceEquals(slot.GetTowerData(), null))
            {
                towerDetailsPanel.ShowTowerData(slot.GetTowerData());
            }
        }

        private void HandleOnSelectedSlotDoubleClicked(SelectedTowerSlot slot)
        {
            if (!ReferenceEquals(slot.GetTowerData(), null))
            {
                towerDetailsPanel.ShowTowerData(slot.GetTowerData());
            }
        }

        private void HandleOnSelectedSlotDataChanged(SelectedTowerSlot slot)
        {
            int selectedTowers = GetSelectedCount();
            towerSelectionButtonController.SetReadyEnabled(selectedTowers > 0);
            towerSelectionButtonController.SetResetEnabled(selectedTowers > 0);
        }

        private void AddTowerToNextSlot(TowerData data)
        {
            for (int i = 0; i < selectedTowerSlots.Count; i++)
            {
                if (selectedTowerSlots[i].GetTowerData() == null)
                {
                    selectedTowerSlots[i].SetTowerData(data);
                    return;
                }
            }
        }

        private int GetSelectedCount()
        {
            int count = 0;
            for (int i = 0; i < selectedTowerSlots.Count; i++)
            {
                if (!ReferenceEquals(selectedTowerSlots[i].GetTowerData(), null))
                {
                    count++;
                }
            }

            return count;
        }

        private void ClearSelectedTowers()
        {
            for (int i = 0; i < selectedTowerSlots.Count; i++)
            {
                selectedTowerSlots[i].SetTowerData(null);
            }
        }

        private void HandleReadyPressed()
        {
            List<TowerData> selectedTowers = new List<TowerData>();
            for (int i = 0; i < selectedTowerSlots.Count; i++)
            {
                selectedTowers.Add(selectedTowerSlots[i].GetTowerData());
            }

            GameManager.S.SetPlayerParty(selectedTowers);
            GameManager.S.ChangeToGame();
        }

        private void HandleRandomPressed()
        {
            for (int i = 0; i < selectedTowerSlots.Count; i++)
            {
                if (selectedTowerSlots[i].GetTowerData() == null)
                {
                    selectedTowerSlots[i].SetTowerData(
                        availableTowers[Random.Range(0, availableTowers.Count)]
                    );
                }
            }
        }
    }
}