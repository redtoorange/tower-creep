using TMPro;
using UnityEngine;

namespace TowerCreep.Interface.DamagePopups
{
    public class DamagePopupText : MonoBehaviour
    {
        [SerializeField] private Vector2 offSetRange = new Vector2(0.25f, 0.25f);
        [SerializeField] private float moveUpSpeedMin = 1.0f;
        [SerializeField] private float moveUpSpeedMax = 3.0f;
        private float moveUpSpeed;

        [SerializeField] private float lifetime = 1.0f;
        private float elapsedLifetime;

        [SerializeField] private float fadeSpeed = 0.5f;
        private Color textColor;

        [SerializeField] private TMP_Text textDisplay;

        public void Setup(string text)
        {
            textDisplay.text = text;

            moveUpSpeed = Random.Range(moveUpSpeedMin, moveUpSpeedMax);
            elapsedLifetime = lifetime;

            textColor = textDisplay.color;

            transform.Translate(
                Random.Range(-offSetRange.x, offSetRange.x),
                Random.Range(-offSetRange.y, offSetRange.y),
                0
            );
        }

        private void Update()
        {
            transform.Translate(0, moveUpSpeed * Time.deltaTime, 0);

            elapsedLifetime -= Time.deltaTime;
            if (elapsedLifetime <= 0)
            {
                textColor.a -= fadeSpeed * Time.deltaTime;
                textDisplay.color = textColor;
                if (textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}