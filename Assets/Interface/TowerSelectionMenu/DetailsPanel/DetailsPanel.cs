using TMPro;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu.DetailsPanel
{
    public class DetailsPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameDisplay;
        [SerializeField] Image iconDisplay;
        [SerializeField] TMP_Text informationDisplay;
        private bool isShown = false;

        

        // public override void _Input(InputEvent @event)
        // {
        //     if (isShown)
        //     {
        //         if (@event.IsActionPressed("CloseDetails"))
        //         {
        //             OnCloseButtonPressed();
        //         }
        //     }
        // }

        public void ShowTowerData(TowerData towerData)
        {
            nameDisplay.text = towerData.towerName;
            iconDisplay.sprite = towerData.towerIcon;
            informationDisplay.text = towerData.towerInformation;
            isShown = true;
        }

        public void OnCloseButtonPressed()
        {
            isShown = false;
        }
    }
}