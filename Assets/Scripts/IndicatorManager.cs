using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;
    public Transform player;
    private Camera mainCamera;
    private Dictionary<Transform, GameObject> indicators = new Dictionary<Transform, GameObject>();

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");

        foreach (GameObject missile in missiles)
        {
            if (!indicators.ContainsKey(missile.transform))
            {
                GameObject indicator = Instantiate(indicatorPrefab, transform);
                indicators.Add(missile.transform, indicator);
            }
        }

        List<Transform> toRemove = new List<Transform>();
        foreach (var pair in indicators)
        {
            if (pair.Key == null)
            {
                Destroy(pair.Value);
                toRemove.Add(pair.Key);
                continue;
            }

            UpdateIndicator(pair.Key, pair.Value);
        }

        foreach (var key in toRemove)
        {
            indicators.Remove(key);
        }
    }

    void UpdateIndicator(Transform target, GameObject indicator)
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(target.position);
        bool isOffScreen = screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;

        indicator.SetActive(isOffScreen);

        if (isOffScreen)
        {
            screenPos.x = Mathf.Clamp(screenPos.x, 0.05f, 0.95f);
            screenPos.y = Mathf.Clamp(screenPos.y, 0.05f, 0.95f);

            Vector3 worldPos = mainCamera.ViewportToScreenPoint(screenPos);
            indicator.transform.position = worldPos;

            Vector2 direction = target.position - player.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}