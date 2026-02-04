using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Sprite[] cloudSprites;
    public Transform player;

    [Header("Sıklık ve Limit")]
    public float spawnInterval = 0.1f;
    public int cloudsPerCycle = 2;
    public int maxClouds = 60;

    [Header("Mesafe Ayarları")]
    public float minRadius = 10f;
    public float maxRadius = 20f;
    public float overlapCheckRadius = 4f;
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

            if (cloudSprites.Length > 0)
            {
                SpriteRenderer sr = cloud.GetComponent<SpriteRenderer>();
                sr.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
            }

            float randomScale = Random.Range(4.0f, 8.0f);
            cloud.transform.localScale = Vector3.one * randomScale;
        }
    }
}