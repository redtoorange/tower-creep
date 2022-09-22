using System.Collections.Generic;
using TowerCreep.Towers;
using TowerCreep.Utility;
using UnityEngine;

namespace TowerCreep.Player.TowerCollection
{
    public class PlayerTowerCollectionManager : MonoBehaviour
    {
        public static PlayerTowerCollectionManager S;

        private List<TowerCollectionSlot> playerTowerCollection;
        [SerializeField] private List<TowerData> debuggingInitialTowerData;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogError("Multiple PlayerTowerCollectionManager detected");
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

            List<TowerData> towerCollection = GameManager.S.GetTowerCollectionData();

            if (towerCollection != null)
            {
                SetTowerCollection(towerCollection);
            }
            else if (debuggingInitialTowerData != null && debuggingInitialTowerData.Count > 0)
            {
                Debug.Log("Using debugging tower data");
                SetTowerCollection(debuggingInitialTowerData);
            }
        }

        public void SetTowerCollection(List<TowerData> selectedTowers)
        {
            playerTowerCollection = new List<TowerCollectionSlot>();
            for (int i = 0; i < selectedTowers.Count; i++)
            {
                TowerCollectionSlot newSlot = new TowerCollectionSlot();
                newSlot.Initialize(selectedTowers[i]);
                playerTowerCollection.Add(newSlot);
            }
        }

        public List<TowerCollectionSlot> GetTowerCollection()
        {
            return playerTowerCollection;
        }
    }
}