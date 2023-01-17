using UnityEngine;

namespace TowerCreep.Interface.DamagePopups
{
    public class DamagePopupController : MonoBehaviour
    {
        public static DamagePopupController S;

        [SerializeField] private DamagePopupText damagePopupPrefab;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
            }
            else
            {
                Destroy(this);
                enabled = false;
            }
        }

        public void CreatePopup(Vector2 position, string text)
        {
            DamagePopupText popup = Instantiate(damagePopupPrefab, position, Quaternion.identity, transform);
            popup.Setup(text);
        }
    }
}