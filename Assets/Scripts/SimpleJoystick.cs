using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background;
    public RectTransform handle;
    
    [Range(0f, 2f)] 
    public float handleRange = 1f;

    public Vector2 InputVector { get; private set; }

    private Vector2 joystickPosition = Vector2.zero;
    private Camera cam;

    void Start()
    {
        if (CanvasUpdateRegistry.IsRebuildingLayout()) 
            Canvas.ForceUpdateCanvases();
            
        handleRange = 1f;
        cam = null; 
        
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera;

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, 
            eventData.position, 
            eventData.pressEventCamera, 
            out position))
        {
            position.x = (position.x / background.sizeDelta.x) * 2;
            position.y = (position.y / background.sizeDelta.y) * 2;

            InputVector = new Vector2(position.x, position.y);
            
            if (InputVector.magnitude > 1)
            {
                InputVector = InputVector.normalized;
            }

            handle.anchoredPosition = new Vector2(
                InputVector.x * (background.sizeDelta.x / 2) * handleRange, 
                InputVector.y * (background.sizeDelta.y / 2) * handleRange
            );
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}