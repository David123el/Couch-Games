using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CleanTheStreetsHandController : MonoBehaviour
{
    public static bool isLevelStarted = false;

    [SerializeField]
    private Image cloud;

    [SerializeField]
    private GameObject winVideoCamera;

    [SerializeField]
    private AudioClip winClip;

    private void Start()
    {
        cloud.gameObject.SetActive(false);
        StartCoroutine(DelayCloud());
    }

    private void Update()
    {
        if (isLevelStarted)
        {
            if (Input.GetMouseButton(0))
            {
                var mousePos = Input.mousePosition;
                var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);

                cloud.color = new Color(cloud.color.r, cloud.color.g, cloud.color.b, cloud.color.a - 0.004f);

                if (cloud.color.a <= 0)
                {
                    LevelManager.levelIsDone = true;

                    StartCoroutine(PlayWinVideoAndDelay());
                }
            }
        }
    }

    private IEnumerator DelayCloud()
    {
        yield return new WaitForSeconds(4.5f);
        cloud.gameObject.SetActive(true);
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
