using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    private Transform target;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Missile"))
        {
            Destroy(gameObject);
            
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}