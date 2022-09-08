using System;
using System.Collections.Generic;
using Godot;
using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerContainer : Control
    {
        public Action<int> OnReadyCountChanged;

        [Export] private NodePath slotContainerPath;

        private List<SelectedTowerSlot> selectedSlots;
        private int readyCount = 0;
        private bool readyCountDirty = false;

        public override void _Ready()
        {
            selectedSlots = new List<SelectedTowerSlot>(9);
            Node container = GetNode<Node>(slotContainerPath);
            for (int i = 0; i < container.GetChildCount(); i++)
            {
                if (container.GetChild(i) is SelectedTowerSlot sts)
                {
                    selectedSlots.Add(sts);
                    sts.OnTowerDataChanged += HandleOnTowerDataChanged;
                }
            }
        }

        public override void _Process(float delta)
        {
            if (readyCountDirty)
            {
                readyCountDirty = false;
                int tempReady = 0;
                for (int i = 0; i < selectedSlots.Count; i++)
                {
                    if (selectedSlots[i].GetTowerData() != null)
                    {
                        tempReady++;
                    }
                }

                if (tempReady != readyCount)
                {
                    readyCount = tempReady;
                    OnReadyCountChanged?.Invoke(readyCount);
                }
            }
        }

        private void HandleOnTowerDataChanged()
        {
            readyCountDirty = true;
        }

        public void AddTower(TowerData data)
        {
            SelectedTowerSlot slot = GetEmptySlot();
            if (slot != null)
            {
                slot.SetTowerData(data);
            }
        }

        private SelectedTowerSlot GetEmptySlot()
        {
            for (int i = 0; i < selectedSlots.Count; i++)
            {
                if (selectedSlots[i].GetTowerData() == null)
                {
                    return selectedSlots[i];
                }
            }

            return null;
        }

        public void RemoveTower(SelectedTowerSlot slot)
        {
            slot.ClearTowerData();
        }

        public List<TowerData> GetSelectedTowers()
        {
            List<TowerData> selectedTowers = new List<TowerData>(selectedSlots.Count);
            for (int i = 0; i < selectedSlots.Count; i++)
            {
                TowerData td = selectedSlots[i].GetTowerData();
                if (td != null)
                {
                    selectedTowers.Add(td);
                }
            }

            return selectedTowers;
        }
    }
}