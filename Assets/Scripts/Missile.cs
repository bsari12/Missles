using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    private Transform target;
    private bool nearMissTriggered = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (nearMissTriggered) return;

        float distance = Vector2.Distance(transform.position, target.position);
        
        if (distance < 2.5f)
        {
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddNearMissScore();
                nearMissTriggered = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Missile"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}