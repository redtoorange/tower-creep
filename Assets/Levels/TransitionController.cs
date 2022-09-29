using TowerCreep.Thirdparty.LeanTween.Framework;
using UnityEngine;

namespace TowerCreep.Levels
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private float transitionTime;
        [SerializeField] private RectTransform shroudCanvas;

        public delegate void FadeOutCompleteCallback();

        public static TransitionController S;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
            }
            else
            {
                enabled = false;
                Destroy(this);
            }
        }

        private void Start()
        {
            FadeIn();
        }

        public void FadeIn()
        {
            shroudCanvas.gameObject.SetActive(true);
            LeanTween.alpha(shroudCanvas, 0.0f, transitionTime)
                .setOnComplete(() =>
                {
                    shroudCanvas.gameObject.SetActive(false);
                });
        }

        public void FadeOut(FadeOutCompleteCallback completeCallback)
        {
            shroudCanvas.gameObject.SetActive(true);
            LeanTween.alpha(shroudCanvas, 1.0f, transitionTime)
                .setOnComplete(() =>
                {
                    shroudCanvas.gameObject.SetActive(false);
                    completeCallback.Invoke();
                });
        }
    }
}