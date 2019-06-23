using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> elevatorButtons;
    [SerializeField]
    private float loopSpeed = 1f;

    [SerializeField]
    private GameObject winVideoCamera;
    [SerializeField]
    private GameObject loseVideoCamera;

    [SerializeField]
    private AudioClip tutClip;
    [SerializeField]
    private AudioClip winClip;
    [SerializeField]
    private AudioClip loseClip;

    [SerializeField]
    private GameObject snowAnim;
    [SerializeField]
    private AudioClip snowClip;

    private void OnEnable()
    {
        StartCoroutine(LoopsThroughelevatorButtons());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        //PlaySnowEffect();

        SoundManager.Instance.Play(tutClip);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopAllCoroutines();

            LevelManager.levelIsDone = true;

            for (int i = 0; i < elevatorButtons.Count; i++)
            {
                if (elevatorButtons[i].gameObject.activeSelf)
                {
                    if (elevatorButtons[i].gameObject.tag == "Button Mondial")
                    {
                        StartCoroutine(PlayWinVideoAndDelay());
                    }
                    else
                    {
                        StartCoroutine(PlayLoseVideoAndDelay());
                    }
                }
            }
        }
    }

    private IEnumerator LoopsThroughelevatorButtons()
    {
        var index = Random.Range(0, elevatorButtons.Count);

        /*elevatorButtons[index].SetActive(true);
        yield return new WaitForSeconds(loopSpeed);
        elevatorButtons[index].SetActive(false);*/

        for (int i = 0; i < elevatorButtons.Count; i++)
        {
            elevatorButtons[i].SetActive(true);
            yield return new WaitForSeconds(loopSpeed);
            elevatorButtons[i].SetActive(false);
        }

        StartCoroutine(LoopsThroughelevatorButtons());

        yield return 0;
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

    private IEnumerator PlayLoseVideoAndDelay()
    {
        SoundManager.Instance.Play(loseClip);
        yield return new WaitForSeconds(1f);

        loseVideoCamera.SetActive(true);
        yield return new WaitForSeconds(6f);
        //LoseVideoCamera.SetActive(false);

        GameManager.OnLevelFailedFunc();
    }

    private void PlaySnowEffect()
    {
        snowAnim.SetActive(true);
        SoundManager.Instance.Play(snowClip);
    }
}
