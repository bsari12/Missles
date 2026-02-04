using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 2f;

    void Start() => Destroy(gameObject, lifeTime);

    void Update() => transform.Translate(Vector3.up * speed * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}