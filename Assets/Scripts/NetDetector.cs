using System.Collections;
using UnityEngine;

public class NetDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject winVideoCamera;

    [SerializeField]
    private AudioClip winClip;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (NationalGeographic.isNetThrown)
        {
            if (collision.gameObject.tag == "Zebra")
            {
                LevelManager.levelIsDone = true;

                StartCoroutine(PlayWinVideoAndDelay());
            }
        }
    }

    private IEnumerator PlayWinVideoAndDelay()
    {
        SoundManager.Instance.Play(winClip);
        yield return new WaitForSeconds(1f);

        winVideoCamera.SetActive(true);
        yield return new WaitForSeconds(6f);
        //winVideoCamera.SetActive(false);

        GameManager.OnlevelCompleteFunc();
    }
}
