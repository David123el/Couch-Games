using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class StrikeSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] psilot;
    [SerializeField]
    private GameObject remoteVideoCamera;
    [SerializeField]
    private GameObject lastChanceVideoCamera;

    //[SerializeField]
    //private VideoPlayer secondVideo;

    [SerializeField]
    private GameObject whiteScreenVideoCamera;

    [SerializeField]
    private GameObject snowAnim;
    [SerializeField]
    private AudioClip snowClip;

    private void Start()
    {
        PlaySnowEffect();

        int psilotLeft = StrikesManager.strikes;

        switch (psilotLeft)
        {
            case 0:
                psilot[0].SetActive(true);
                break;
            case 1:
                if (StrikesManager.isItTheLastChance)
                {
                    //StartCoroutine(PlayRemoteVideoAndDelay());
                    StartCoroutine(PlayLastChanceVideoAndDelay());
                    StrikesManager.isItTheLastChance = false;
                }
                else psilot[1].SetActive(true);
                break;
            case 2:
                psilot[2].SetActive(true);
                break;
            case 3:
                psilot[3].SetActive(true);
                break;
            default:
                break;
        }
    }

    //private void Update()
    //{
    //    if (StrikesManager.isItTheLastChance)
    //    {
    //        if (Input.GetButtonDown("Fire1"))
    //            StopCoroutine(PlayLastChanceVideoAndDelay());
    //    }
    //}

    private void OnDisable()
    {
        //PlaySnowEffect();

        if (psilot != null)
        {
            for (int i = 0; i < psilot.Length; i++)
            {
                if (psilot[i] != null)
                    psilot[i].SetActive(false);
            }
        }
    }

    private IEnumerator PlayRemoteVideoAndDelay()
    {
        remoteVideoCamera.SetActive(true);
        yield return new WaitForSeconds(11f);
        remoteVideoCamera.SetActive(false);
    }

    private IEnumerator PlayLastChanceVideoAndDelay()
    {
        remoteVideoCamera.SetActive(true);
        yield return new WaitForSeconds(2f);
        remoteVideoCamera.SetActive(false);

        lastChanceVideoCamera.SetActive(true);
        yield return new WaitForSeconds(5f);
        lastChanceVideoCamera.SetActive(false);

        psilot[1].SetActive(true);
    }

    //private IEnumerator PlayLastChanceVideoAndDelay()
    //{
    //    remoteVideoCamera.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    //remoteVideoCamera.GetComponentInChildren<VideoPlayer>() = lastChanceVideoCamera.GetComponentInChildren<VideoPlayer>();
    //    yield return new WaitForSeconds(5f);
    //    lastChanceVideoCamera.SetActive(false);

    //    psilot[1].SetActive(true);
    //}

    private IEnumerator PlayWhiteScreen()
    {
        yield return new WaitForSeconds(2f);
        whiteScreenVideoCamera.SetActive(true);
        yield return new WaitForSeconds(1f);
        whiteScreenVideoCamera.SetActive(false);
    }

    private void PlaySnowEffect()
    {
        snowAnim.SetActive(true);
        SoundManager.Instance.Play(snowClip);
    }
}
