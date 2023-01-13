using TMPro;
using TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using TowerCreep.TowerCreep2D.Scripts.Towers.Selection;
using TowerCreep.TowerCreep2D.Scripts.Towers.TowerLevelData;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerInfoDisplay
{
    public class TowerInfoDisplayController : MonoBehaviour
    {
        private TowerSelectionController towerSelectionController;

        [SerializeField] private GameObject towerDetailsDisplay;
        [SerializeField] private Image towerImage;
        [SerializeField] private TMP_Text towerName;
        [SerializeField] private Slider experienceSlider;
        [SerializeField] private TMP_Text levelDisplay;
        [SerializeField] private TMP_Text experienceNumbersDisplay;
        [SerializeField] private GameObject levelUpButton;

        [Header("Stat Blocks")]
        [SerializeField] private TMP_Text damageTextDisplay;
        [SerializeField] private TMP_Text rangeTextDisplay;
        [SerializeField] private TMP_Text speedTextDisplay;
        [SerializeField] private TMP_Text aoeTextDisplay;

        private TowerCollectionSlot towerCollectionData;
        private TowerProgressionData currentProgressData;

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

            if (!ReferenceEquals(currentProgressData, null))
            {
                currentProgressData.OnDataProgressionChange -= UpdateProgressData;
                currentProgressData = null;
            }
        }

        private void HandleTowerSelected(Tower tower)
        {
            if (!ReferenceEquals(tower, null))
            {
                towerCollectionData = tower.GetCollectionSlotData();
                towerImage.sprite = towerCollectionData.CollectionTowerData.towerIcon;
                towerName.text = towerCollectionData.CollectionTowerData.towerName;

                currentProgressData = towerCollectionData.TowerProgressionData;
                currentProgressData.OnDataProgressionChange += UpdateProgressData;
                UpdateProgressData();
                towerDetailsDisplay.SetActive(true);
            }
            else
            {
                towerDetailsDisplay.SetActive(false);
                if (!ReferenceEquals(currentProgressData, null))
                {
                    currentProgressData.OnDataProgressionChange -= UpdateProgressData;
                    currentProgressData = null;
                }
            }
        }

        private void UpdateProgressData()
        {
            experienceSlider.value = currentProgressData.GetExperiencePercent();
            levelDisplay.text = $"Level {currentProgressData.CurrentLevel}";
            experienceNumbersDisplay.text =
                $"{currentProgressData.CurrentExperience}/{currentProgressData.RequiredExperience}";

            TowerLevelDataRecord record = towerCollectionData.GetCurrentLevelRecordData()[0];
            damageTextDisplay.text = ((record.MaxDamage + record.MinDamage) / 2.0f).ToString("0");
            rangeTextDisplay.text = record.Range.ToString("0");
            speedTextDisplay.text = (record.Speed / 60.0f).ToString("N2");
            aoeTextDisplay.text = record.AOE.ToString("0");
        }
    }
}