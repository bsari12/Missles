using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1f);
        fadeImage.raycastTarget = true;
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0f);
        fadeImage.raycastTarget = false;
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeImage.raycastTarget = true;
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}