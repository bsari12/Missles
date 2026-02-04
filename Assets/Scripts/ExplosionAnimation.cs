using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 0.1f;
    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (frames.Length > 0)
        {
            sr.sprite = frames[0];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame++;

            if (currentFrame < frames.Length)
            {
                sr.sprite = frames[currentFrame];
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}