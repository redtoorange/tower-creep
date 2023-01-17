using TMPro;
using TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using TowerCreep.TowerCreep2D.Scripts.Towers.Selection;
using TowerCreep.TowerCreep2D.Scripts.Towers.TowerClassProgression;
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

        private PlayerPartySlot playerPartyData;
        private TowerInstanceProgressionData currentProgressData;

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
                currentProgressData.OnTowerInstanceProgressionChange -= UpdateProgressData;
                currentProgressData = null;
            }
        }

        private void HandleTowerSelected(Tower tower)
        {
            if (!ReferenceEquals(tower, null))
            {
                playerPartyData = tower.GetCollectionSlotData();
                towerImage.sprite = playerPartyData.CollectionTowerData.towerIcon;
                towerName.text = playerPartyData.CollectionTowerData.towerName;

                currentProgressData = playerPartyData.TowerInstanceProgressionData;
                currentProgressData.OnTowerInstanceProgressionChange += UpdateProgressData;
                UpdateProgressData();
                towerDetailsDisplay.SetActive(true);
            }
            else
            {
                towerDetailsDisplay.SetActive(false);
                if (!ReferenceEquals(currentProgressData, null))
                {
                    currentProgressData.OnTowerInstanceProgressionChange -= UpdateProgressData;
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

            TowerClassProgressionDataRecord record = playerPartyData.GetCurrentLevelRecordData()[0];
            damageTextDisplay.text = ((record.MaxDamage + record.MinDamage) / 2.0f).ToString("0");
            rangeTextDisplay.text = record.Range.ToString("0");
            speedTextDisplay.text = (record.Speed / 60.0f).ToString("N2");
            aoeTextDisplay.text = record.AOE.ToString("0");
        }
    }
}