using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.HealthBar
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Gradient colorGradient;

        private float healthPercent;
        private Image healthBarFill;

        private void Start()
        {
            healthBarFill = GetComponentInChildren<Image>();
            gameObject.SetActive(false);
        }

        public void SetFillPercent(float amount)
        {
            healthPercent = Mathf.Clamp(amount, 0.0f, 1.0f);
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBarFill.fillAmount = healthPercent;
            healthBarFill.color = colorGradient.Evaluate(healthPercent);

            if (healthPercent >= 1.0f)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}