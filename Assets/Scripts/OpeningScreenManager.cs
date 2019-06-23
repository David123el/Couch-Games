using System.Collections;
using UnityEngine;

public class OpeningScreenManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip openingClip;
    [SerializeField]
    private AudioClip gameClip;

    [SerializeField]
    private GameObject openingSequenceVideo;

    private void OnEnable()
    {
        if (OpeningSequenceController.isSequenceShown)
        {
            openingSequenceVideo.SetActive(false);
        }
        else StartCoroutine(DelayForVideo());
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic(openingClip);
    }

    private void OnDisable()
    {
        SoundManager.Instance.PlayMusic(gameClip);
    }

    private IEnumerator DelayForVideo()
    {
        yield return new WaitForSeconds(103.0f);
        openingSequenceVideo.SetActive(false);
    }

    public void SkipVideo()
    {
        openingSequenceVideo.SetActive(false);
    }
}
