using UnityEngine;

public class UIFloatingEffect : MonoBehaviour
{
    public float verticalAmplitude = 20f;
    public float horizontalAmplitude = 10f;
    public float frequency = 1f;
    public float rotationAmplitude = 2f;
    
    private float timeOffset;
    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        timeOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        float t = (Time.time + timeOffset) * frequency;

        float newY = startPosition.y + Mathf.Sin(t) * verticalAmplitude;
        float newX = startPosition.x + Mathf.Cos(t * 0.5f) * horizontalAmplitude;
        
        rectTransform.anchoredPosition = new Vector2(newX, newY);

        float rotationZ = Mathf.Sin(t * 0.5f) * rotationAmplitude;
        rectTransform.localRotation = Quaternion.Euler(0, 0, rotationZ);
    }
}