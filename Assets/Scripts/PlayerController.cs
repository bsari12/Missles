using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SimpleJoystick joystick; 
    public float rotateSpeed = 400f;

    public float moveSpeed = 5f; 

    void Update()
    {
        Vector2 direction = joystick.InputVector;
        if (direction.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}