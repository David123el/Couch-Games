using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CookingHandController : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Image tearsBar;
    [SerializeField]
    private float tearsFactor = 1f;
    [SerializeField]
    private GameObject onion;
    [SerializeField]
    private GameObject cutPhase1;
    [SerializeField]
    private GameObject cutPhase2;
    [SerializeField]
    private GameObject cutPhase3;
    [SerializeField]
    private GameObject cutPhase4;

    [SerializeField]
    private GameObject winVideoCamera;

    [SerializeField]
    private AudioClip winClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CutOnion();
    }

    private void CutOnion()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Cut");

            tearsBar.rectTransform.sizeDelta = new Vector2(Mathf.Clamp(tearsBar.rectTransform.sizeDelta.x + tearsFactor, 78f, 850f), tearsBar.rectTransform.sizeDelta.y);
            
            if (tearsBar.rectTransform.sizeDelta.x >= 100f)
            {
                onion.SetActive(false);
                cutPhase1.SetActive(true);
            }
            if (tearsBar.rectTransform.sizeDelta.x >= 300f)
            {
                cutPhase1.SetActive(false);
                cutPhase2.SetActive(true);
            }
            if (tearsBar.rectTransform.sizeDelta.x >= 550f)
            {
                cutPhase2.SetActive(false);
                cutPhase3.SetActive(true);
            }
            if (tearsBar.rectTransform.sizeDelta.x >= 850f)
            {
                cutPhase3.SetActive(false);
                cutPhase4.SetActive(true);

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
