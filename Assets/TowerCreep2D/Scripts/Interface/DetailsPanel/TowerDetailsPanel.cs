using TMPro;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.DetailsPanel
{
    public class TowerDetailsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject towerDetailsDisplay;
        [SerializeField] private Image iconDisplay;
        [SerializeField] private TMP_Text nameDisplay;
        [SerializeField] private TMP_Text informationDisplay;
        [SerializeField] private bool hideOnStart = true;
        private bool isShown;

        private void Start()
        {
            isShown = false;

            if (hideOnStart)
            {
                towerDetailsDisplay.SetActive(false);
            }
        }

        public void ShowTowerData(TowerData towerData)
        {
            if (!ReferenceEquals(towerData, null))
            {
                iconDisplay.sprite = towerData.towerIcon;
                nameDisplay.text = towerData.towerName;
                informationDisplay.text = towerData.towerInformation;
                isShown = true;
                towerDetailsDisplay.SetActive(true);
            }
        }

        public void OnCloseButtonPressed()
        {
            isShown = false;
            towerDetailsDisplay.SetActive(false);
        }
    }
}