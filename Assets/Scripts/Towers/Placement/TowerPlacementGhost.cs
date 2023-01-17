using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers.Shooting;
using TowerCreep.Towers.TowerClassProgression;
using UnityEngine;

namespace TowerCreep.Towers.Placement
{
    public class TowerPlacementGhost : MonoBehaviour
    {
        [SerializeField] private Color validColor = Color.green;
        [SerializeField] private Color invalidColor = Color.red;

        private SpriteRenderer spriteRenderer;
        private TowerRangePreview towerRangePreview;
        private bool isShowing;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            towerRangePreview = GetComponentInChildren<TowerRangePreview>();

            SetShown(false);
        }

        public void SetData(PlayerPartySlot slotData)
        {
            spriteRenderer.sprite = slotData.CollectionTowerData.towerIcon;
            TowerClassProgressionDataRecord record = slotData.GetCurrentLevelRecordData()[0];
            towerRangePreview.SetRange(record.Range);
        }

        public void SetShown(bool isShown)
        {
            isShowing = isShown;
            spriteRenderer.enabled = isShowing;
            towerRangePreview.SetShow(isShowing);
        }

        public void SetValid(bool isValid)
        {
            if (isValid)
            {
                spriteRenderer.color = validColor;
                towerRangePreview.SetShow(true);
            }
            else
            {
                spriteRenderer.color = invalidColor;
                towerRangePreview.SetShow(false);
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void Update()
        {
            if (isShowing)
            {
                towerRangePreview.UpdateCircle();
            }
        }
    }
}