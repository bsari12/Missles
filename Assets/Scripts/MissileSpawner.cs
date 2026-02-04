using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject[] missilePrefabs;
    public PlayerController player;
    
    public float maxInterval = 3.5f;
    public float minInterval = 1.5f;
    public float decreaseAmount = 0.25f;
    public float scoreStep = 500f;

    private float timer;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        timer = maxInterval;
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        float currentInterval = maxInterval - (Mathf.Floor(player.score / scoreStep) * decreaseAmount);
        
        if (currentInterval < minInterval)
        {
            currentInterval = minInterval;
        }

        if (timer >= currentInterval)
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
        Vector3 spawnPos = player.transform.position + player.transform.up * spawnDistance + player.transform.right * randomSide;

        int randomIndex = Random.Range(0, missilePrefabs.Length);
        Instantiate(missilePrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}