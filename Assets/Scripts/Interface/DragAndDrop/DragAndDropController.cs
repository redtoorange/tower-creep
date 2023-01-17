using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TowerCreep.Interface.DragAndDrop
{
    public class DragAndDropController : MonoBehaviour
    {
        [SerializeField] private RectTransform dragAndDropImage;

        private RectTransform rect;
        private Camera camera;
        private bool dragging = false;
        private bool validDropSink = false;
        private GameInputActions gameInputActions;

        private DragAndDropSource hoveredSource;
        private DragAndDropSink hoveredSink;

        private object dragAndDropData;


        private void Start()
        {
            rect = GetComponent<RectTransform>();
            camera = Camera.main;

            gameInputActions = new GameInputActions();
            gameInputActions.TowerSelection.TowerDrag.performed += HandleDragTower;
            gameInputActions.TowerSelection.TowerDrag.canceled += HandleStopDragTower;
            gameInputActions.Enable();

            dragAndDropImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            DragAndDropSource.OnDnDSourceMouseEnter += HandleOnSourceMouseEnter;
            DragAndDropSource.OnDnDSourceMouseExit += HandleOnSourceMouseExit;

            DragAndDropSink.OnDnDSinkMouseEnter += HandleOnSinkMouseEnter;
            DragAndDropSink.OnDnDSinkMouseExit += HandleOnSinkMouseExit;
        }

        private void OnDisable()
        {
            DragAndDropSource.OnDnDSourceMouseEnter -= HandleOnSourceMouseEnter;
            DragAndDropSource.OnDnDSourceMouseExit -= HandleOnSourceMouseExit;

            DragAndDropSink.OnDnDSinkMouseEnter -= HandleOnSinkMouseEnter;
            DragAndDropSink.OnDnDSinkMouseExit -= HandleOnSinkMouseExit;
        }


        private void HandleOnSourceMouseEnter(DragAndDropSource source)
        {
            hoveredSource = source;
        }

        private void HandleOnSourceMouseExit(DragAndDropSource source)
        {
            hoveredSource = null;
        }

        private void HandleOnSinkMouseExit(DragAndDropSink sink)
        {
            hoveredSink = null;
        }

        private void HandleOnSinkMouseEnter(DragAndDropSink sink)
        {
            if (dragging && sink.CanDropData(dragAndDropData))
            {
                Debug.Log("Mouse Entered while dragging");
                validDropSink = true;
                hoveredSink = sink;
            }
            else
            {
                hoveredSink = null;
            }
        }

        private void HandleDragTower(InputAction.CallbackContext context)
        {
            if (!ReferenceEquals(hoveredSource, null) && hoveredSource.CanStartDragging())
            {
                dragAndDropImage.GetComponentInChildren<Image>().sprite = hoveredSource.GetDragAndDropSprite();
                dragAndDropData = hoveredSource.GetDragAndDropData();
                dragAndDropImage.gameObject.SetActive(true);
                dragging = true;
                hoveredSink = null;
            }
        }

        private void HandleStopDragTower(InputAction.CallbackContext context)
        {
            if (dragging)
            {
                dragAndDropImage.gameObject.SetActive(false);
                dragging = false;
                if (!ReferenceEquals(hoveredSink, null))
                {
                    hoveredSink.DropData(dragAndDropData);
                }

                hoveredSink = null;
                dragAndDropData = null;
            }
        }


        private void Update()
        {
            if (dragging)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rect,
                    Mouse.current.position.ReadValue(),
                    camera,
                    out Vector2 pos
                );
                dragAndDropImage.anchoredPosition = pos;
            }
        }
    }
}