using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Transform player;
    
    [Header("Frequency and Limit")]
    public float spawnInterval = 0.1f;
    public int cloudsPerCycle = 2;
    public int maxClouds = 60;
    
    [Header("Distance Settings")]
    public float minRadius = 7f;
    public float maxRadius = 14f;
    public float overlapCheckRadius = 2f;
    public LayerMask cloudLayer;

    private float timer;

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            if (GameObject.FindGameObjectsWithTag("Environment").Length < maxClouds)
            {
                for (int i = 0; i < cloudsPerCycle; i++)
                {
                    TrySpawnCloud();
                }
            }
            timer = 0;
        }
    }

    void TrySpawnCloud()
    {
        float randomAngle = Random.Range(-90f, 90f);
        Vector3 spawnDirection = Quaternion.Euler(0, 0, randomAngle) * player.up;
        float distance = Random.Range(minRadius, maxRadius);
        
        Vector3 spawnPos = player.position + spawnDirection * distance;

        Collider2D overlap = Physics2D.OverlapCircle(spawnPos, overlapCheckRadius, cloudLayer);

        if (overlap == null)
        {
            GameObject cloud = Instantiate(cloudPrefab, spawnPos, Quaternion.identity);
            float randomScale = Random.Range(0.8f, 2.2f);
            cloud.transform.localScale = Vector3.one * randomScale;
        }
    }
}