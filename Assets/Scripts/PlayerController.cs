using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SimpleJoystick joystick;
    public float rotateSpeed = 200f;
    public float moveSpeed = 8f;
    public int health = 3;
    
    public float score = 0;
    public float nearMissDistance = 2.5f;

    void Update()
    {
        score += Time.deltaTime * 10f;

        Vector2 direction = joystick.InputVector;
        if (direction.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0) Die();
    }

    public void AddNearMissScore()
    {
        score += 50f;
        Debug.Log("Near Miss! Score: " + (int)score);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}