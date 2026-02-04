using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float nearMissDistance = 2.5f;
    public float nearMissPoints = 100f;
    public float destructionPoints = 250f;

    private Transform target;
    private PlayerController playerScript;
    private bool hasNearMissed = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            playerScript = playerObj.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (target == null) return;

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        CheckNearMiss();
    }

    void CheckNearMiss()
    {
        if (hasNearMissed || playerScript == null) return;

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= nearMissDistance)
        {
            playerScript.AddScore(nearMissPoints);
            hasNearMissed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerScript != null)
            {
                playerScript.TakeDamage();
            }
            Explode();
        }
        else if (collision.CompareTag("Missile"))
        {
            if (playerScript != null && gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                playerScript.AddScore(destructionPoints);
            }
            Explode();
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(fx, 1.5f);
        }

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayExplosion();
        }

        Destroy(gameObject);
    }
}