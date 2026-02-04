using UnityEngine;

public class MenuPlaneMover : MonoBehaviour
{
    public float speed = 5f;
    private float topBound;
    private float bottomBound;
    private float sideBound;
    
    private bool isFirstFlight = true;

    void Start()
    {
        UpdateBounds();
        transform.position = new Vector3(0, bottomBound, 0);
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        UpdateBounds();

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (IsOutOfBounds())
        {
            ResetToRandomPath();
        }
    }

    void UpdateBounds()
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;
        
        topBound = camHeight + 2f;
        bottomBound = -camHeight - 2f;
        sideBound = camWidth + 2f;
    }

    bool IsOutOfBounds()
    {
        Vector3 pos = transform.position;
        return pos.y > topBound || pos.y < bottomBound - 2f || Mathf.Abs(pos.x) > sideBound;
    }

    void ResetToRandomPath()
    {
        isFirstFlight = false;

        float randomX = Random.Range(-sideBound + 1f, sideBound - 1f);
        transform.position = new Vector3(randomX, bottomBound, 0);

        float randomAngle = Random.Range(-35f, 35f);
        transform.rotation = Quaternion.Euler(0, 0, -randomAngle);
    }
}