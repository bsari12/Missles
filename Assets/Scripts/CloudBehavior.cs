using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    private Transform player;
    public float cleanUpDistance = 20f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = Random.Range(0.2f, 0.4f);
        sr.color = c;
        sr.sortingOrder = -10;
    }

    void Update()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.position) > cleanUpDistance)
        {
            Destroy(gameObject);
        }
    }
}