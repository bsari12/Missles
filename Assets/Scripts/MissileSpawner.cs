using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject[] missilePrefabs;
    public Transform player;
    public float spawnInterval = 2.5f;
    
    private float timer;

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMissile();
            timer = 0;
        }
    }

    void SpawnMissile()
    {
        if (missilePrefabs.Length == 0) return;

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        float spawnDistance = camHeight * 1.8f;
        float horizontalSpread = camWidth * 1.5f;

        float randomSide = Random.Range(-horizontalSpread, horizontalSpread);
        Vector3 spawnPos = player.position + player.up * spawnDistance + player.right * randomSide;

        int randomIndex = Random.Range(0, missilePrefabs.Length);
        Instantiate(missilePrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}