using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = false;
    public SimpleJoystick joystick;
    public float rotateSpeed = 200f;
    public float moveSpeed = 8f;
    public int health = 3;
    public float score = 0f;
    public float timeScoreMultiplier = 10f;
    public UIManager uiManager;

    void Update()
    {
        if (!canMove) return;

        score += Time.deltaTime * timeScoreMultiplier;
        uiManager.UpdateScoreUI(score);

        Vector2 direction = joystick.InputVector;
        if (direction.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public void AddScore(float points) => score += points;

    public void TakeDamage()
    {
        health--;
        uiManager.UpdateHealthUI(health);
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayExplosion();
        }

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        uiManager.TriggerGameOver(score);
        gameObject.SetActive(false);
    }
}