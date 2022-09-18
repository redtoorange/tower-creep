using UnityEngine;

namespace TowerCreep.Levels
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private float transitionTime;
        [SerializeField] private RectTransform shroudCanvas;

        private void Start()
        {
            PlayTransition();
        }

        public void PlayTransition()
        {
            shroudCanvas.gameObject.SetActive(true);
            LeanTween.alpha(shroudCanvas, 0.0f, transitionTime).setOnComplete(() => shroudCanvas.gameObject.SetActive(false));
        }
    }
}