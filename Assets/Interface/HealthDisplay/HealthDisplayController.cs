using TMPro;
using TowerCreep.Player;
using UnityEngine;

namespace TowerCreep.Interface.HealthDisplay
{
    public class HealthDisplayController : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthLabel;
        private int currentHealth = 0;

        private void OnEnable()
        {
            PlayerResourceManager.OnResourceChange += HandlePlayerResourceChange;
        }

        private void OnDisable()
        {
            PlayerResourceManager.OnResourceChange -= HandlePlayerResourceChange;
        }

        private void HandlePlayerResourceChange(PlayerResourceType type, int oldValue, int newValue)
        {
            if (type == PlayerResourceType.Health)
            {
                SetHealth(newValue);
            }
        }

        public void SetHealth(int newHealth)
        {
            currentHealth = newHealth;
            healthLabel.SetText(currentHealth.ToString());
        }
    }
}