using UnityEngine;
using System.Collections;

public class GameIntroManager : MonoBehaviour
{
    public PlayerController playerScript;
    public GameObject missileSpawner;
    public GameObject environmentSpawner;
    public CameraFollow cameraFollowScript;
    
    public float targetY = 1.4f;
    public float introSpeed = 5f;
    public float startDelay = 0.5f;

    void Start()
    {
        cameraFollowScript.enabled = false;
        playerScript.canMove = false;
        playerScript.transform.position = new Vector3(0, -6.5f, 0);
        
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        yield return new WaitForSeconds(startDelay);

        Vector3 targetPos = new Vector3(0, targetY, 0);
        Transform playerTransform = playerScript.transform;

        while (Vector3.Distance(playerTransform.position, targetPos) > 0.01f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPos, introSpeed * Time.deltaTime);
            yield return null;
        }

        playerTransform.position = targetPos;
        
        playerScript.canMove = true;
        cameraFollowScript.enabled = true;
        
        if (missileSpawner != null) missileSpawner.SetActive(true);
        if (environmentSpawner != null) environmentSpawner.SetActive(true);
    }
}