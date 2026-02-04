using UnityEngine;

public class MenuCloud : MonoBehaviour
{
    public float speed = 3f;
    [Range(0f, 1f)] public float transparency = 0.4f;

    private float bottomBound;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = transparency;
        sr.color = c;

        bottomBound = -Camera.main.orthographicSize - 5f;

        float randomScale = Random.Range(4f, 8f);
        transform.localScale = Vector3.one * randomScale;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }
    }
}