using TMPro;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers;
using TowerCreep.Towers.Selection;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerInfoDisplay
{
    public class TowerInfoDisplayController : MonoBehaviour
    {
        private TowerSelectionController towerSelectionController;

        [SerializeField] private GameObject towerDetailsDisplay;
        [SerializeField] private Image towerImage;
        [SerializeField] private TMP_Text towerName;
        [SerializeField] private Slider experienceSlider;
        [SerializeField] private TMP_Text levelDisplay;

        private void Awake()
        {
            towerSelectionController = FindObjectOfType<TowerSelectionController>();
            towerSelectionController.OnTowerSelected += HandleTowerSelected;
            towerSelectionController.OnTowerDeSelected += HandleTowerDeSelected;
        }

        private void Start()
        {
            towerDetailsDisplay.SetActive(false);
        }

        private void HandleTowerDeSelected(Tower tower)
        {
            towerDetailsDisplay.SetActive(false);
        }

        private void HandleTowerSelected(Tower tower)
        {
            if (!ReferenceEquals(tower, null))
            {
                TowerCollectionSlot towerCollectionData = tower.GetCollectionSlotData();
                towerImage.sprite = towerCollectionData.CollectionTowerData.towerIcon;
                towerName.text = towerCollectionData.CollectionTowerData.towerName;
                experienceSlider.value = towerCollectionData.TowerProgressionData.GetExperiencePercent();
                levelDisplay.text = $"Level {towerCollectionData.TowerProgressionData.CurrentLevel}";
                towerDetailsDisplay.SetActive(true);
            }
            else
            {
                towerDetailsDisplay.SetActive(false);
            }
        }
    }
}