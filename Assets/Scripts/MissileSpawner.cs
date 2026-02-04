using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missilePrefab;
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
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        float spawnDistance = camHeight * 1.8f;
        float horizontalSpread = camWidth * 1.5f;

        float randomSide = Random.Range(-horizontalSpread, horizontalSpread);
        Vector3 spawnPos = player.position + player.up * spawnDistance + player.right * randomSide;

        Instantiate(missilePrefab, spawnPos, Quaternion.identity);
    }
}