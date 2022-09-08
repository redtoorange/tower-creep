using Godot;
using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu.DetailsPanel
{
    public class DetailsPanel : PanelContainer
    {
        private Label nameDisplay;
        private TextureRect iconDisplay;
        private RichTextLabel informationDisplay;
        private bool isShown = false;


        public override void _Ready()
        {
            nameDisplay = GetNode<Label>("VBoxContainer/HBoxContainer/NameDisplay");
            iconDisplay = GetNode<TextureRect>("VBoxContainer/HBoxContainer/IconDisplay");
            informationDisplay =
                GetNode<RichTextLabel>("VBoxContainer/Panel/MarginContainer/ScrollContainer/InformationDisplay");
        }

        public override void _Input(InputEvent @event)
        {
            if (isShown)
            {
                if (@event.IsActionPressed("CloseDetails"))
                {
                    OnCloseButtonPressed();
                }
            }
        }

        public void ShowTowerData(TowerData towerData)
        {
            nameDisplay.Text = towerData.towerName;
            iconDisplay.Texture = towerData.towerIcon;
            informationDisplay.BbcodeText = towerData.towerInformation;
            isShown = true;

            Show();
        }

        public void OnCloseButtonPressed()
        {
            Hide();
            isShown = false;
        }
    }
}