using UnityEngine;

public class MenuCloudSpawner : MonoBehaviour
{
    public GameObject[] menuCloudPrefabs; 
    public float spawnInterval = 1.5f;
    
    private float topSpawnY;
    private float screenRatioX;
    private float timer;

    void Start()
    {
        float camHeight = Camera.main.orthographicSize;
        topSpawnY = camHeight + 3f; 
        screenRatioX = camHeight * Camera.main.aspect; 
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCloud();
            timer = 0;
        }
    }

    void SpawnCloud()
    {
        if (menuCloudPrefabs.Length == 0) return;

        float randomX = Random.Range(-screenRatioX, screenRatioX);
        Vector3 spawnPos = new Vector3(randomX, topSpawnY, 0);

        int randomIndex = Random.Range(0, menuCloudPrefabs.Length);
        Instantiate(menuCloudPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}