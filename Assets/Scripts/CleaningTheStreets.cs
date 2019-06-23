using System.Collections;
using UnityEngine;

public class CleaningTheStreets : MonoBehaviour
{
    [SerializeField]
    private GameObject videoCamera;
    [SerializeField]
    private float timeToVideo = 2f;
    private float currentTime;

    [SerializeField]
    private AudioClip tutClip;

    private void Start()
    {
        currentTime = Time.time;
        CleanTheStreetsHandController.isLevelStarted = false;

        StartCoroutine(DelayForVideo());

        SoundManager.Instance.Play(tutClip);
    }

    private void Update()
    {
        //if (Time.timeSinceLevelLoad - currentTime >= timeToVideo)
        //{
        //    CleanTheStreetsHandController.isLevelStarted = true;
        //    Debug.Log(CleanTheStreetsHandController.isLevelStarted);
        //    Debug.Log("video is over");
        //    videoCamera.SetActive(false);
        //}
    }

    private IEnumerator DelayForVideo()
    {
        yield return new WaitForSeconds(3.5f);
        videoCamera.SetActive(true);
        yield return new WaitForSeconds(2f);
        CleanTheStreetsHandController.isLevelStarted = true;
        Debug.Log(CleanTheStreetsHandController.isLevelStarted);
        Debug.Log("video is over");
        videoCamera.SetActive(false);
    }
}
