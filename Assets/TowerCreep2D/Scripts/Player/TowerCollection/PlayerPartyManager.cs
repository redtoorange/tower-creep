using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Interface.HotBar;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using TowerCreep.TowerCreep2D.Scripts.Utility;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection
{
    public class PlayerPartyManager : MonoBehaviour
    {
        private List<PlayerPartySlot> playerParty;
        [SerializeField] private List<TowerData> debuggingInitialTowerData;
        private TowerHotBarController hotbar;

        private void Start()
        {
            List<TowerData> towerCollection = GameManager.S.GetSelectedPlayerParty();

            if (!ReferenceEquals(towerCollection, null))
            {
                SetTowerCollection(towerCollection);
            }
            else if (!ReferenceEquals(debuggingInitialTowerData, null) && debuggingInitialTowerData.Count > 0)
            {
                Debug.Log("Using debugging tower data");
                SetTowerCollection(debuggingInitialTowerData);
            }

            hotbar = FindObjectOfType<TowerHotBarController>();
            if (hotbar)
            {
                hotbar.Initialize(playerParty);
            }
        }

        public void SetTowerCollection(List<TowerData> selectedTowers)
        {
            playerParty = new List<PlayerPartySlot>();
            for (int i = 0; i < selectedTowers.Count; i++)
            {
                PlayerPartySlot newSlot = new PlayerPartySlot();
                if (!ReferenceEquals(selectedTowers[i], null))
                {
                    newSlot.Initialize(selectedTowers[i]);
                }

                playerParty.Add(newSlot);
            }
        }
    }
}