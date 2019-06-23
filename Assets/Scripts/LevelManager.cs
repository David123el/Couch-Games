using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Video;

public class LevelManager : MonoBehaviour
{
    private static int currentLevel = 2;

    private static List<int> listOfLevelsToBeCompleted = new List<int>();
    private int randNum;

    [SerializeField]
    private GameObject timeLeftBar;
    //[SerializeField]
    //private Text timeLeftText;
    [SerializeField]
    private float timeLeft = 10f;
    public static bool levelIsDone = false;

    [SerializeField]
    private GameObject whiteScreenVideoCamera;

    //[SerializeField]
    //private GameObject winVideoCamera;
    [SerializeField]
    private GameObject loseVideoCamera;
    [SerializeField]
    private float videoLength = 6f;

    [SerializeField]
    private AudioClip loseClip;

    [SerializeField]
    private GameObject snowAnim;
    [SerializeField]
    private AudioClip snowClip;

    private void OnEnable()
    {
        GameManager.LevelEnd += PlayLoseVideoAndDelay;

        GameManager.OnLevelComplete += UpdateCurrentLevel;
        GameManager.OnLevelComplete += IncreaseScore;
        //GameManager.OnLevelComplete += PlaySnowEffect;
        GameManager.OnLevelComplete += LoadStrikesScene;

        GameManager.OnLevelFailed += UpdateCurrentLevel;
        GameManager.OnLevelFailed += UpdateStrikes;
        GameManager.OnLevelFailed += LoadStrikesScene;
        //GameManager.OnTimeIsOver += TimeISOver;

        levelIsDone = false;
    }

    private void OnDisable()
    {
        GameManager.LevelEnd -= PlayLoseVideoAndDelay;

        GameManager.OnLevelComplete -= UpdateCurrentLevel;
        GameManager.OnLevelComplete -= IncreaseScore;
        //GameManager.OnLevelComplete -= PlaySnowEffect;
        GameManager.OnLevelComplete -= LoadStrikesScene;

        GameManager.OnLevelFailed -= UpdateCurrentLevel;
        GameManager.OnLevelFailed -= UpdateStrikes;
        GameManager.OnLevelFailed -= LoadStrikesScene;
        //GameManager.OnTimeIsOver -= TimeISOver;

        //listOfLevelsToBeCompleted.Remove(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        //only if level is done, dont play it again
        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().buildIndex != 1)
            listOfLevelsToBeCompleted.Add(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (!levelIsDone)
            StartCountDownTimer();
    }

    private void StartCountDownTimer()
    {
        //StartCoroutine(PlayWhiteScreen());

        if (timeLeftBar != null)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                //timeLeftText.text = "תוינש " + Mathf.Round(timeLeft);
                timeLeftBar.transform.localScale = new Vector3((timeLeft / 10), timeLeftBar.transform.localScale.y);
            }
            else if (timeLeft <= 0)
            {
                TimeISOver();
            }
        }
    }

    private void TimeISOver()
    {
        //if (SceneManager.GetActiveScene().name == "National_Geographic_Scene")
        //{
        //    StartCoroutine(PlayLoseVideoAndDelay());
        //}
        //if (SceneManager.GetActiveScene().name == "Cooking_Program_Scene")
        //{
        //    StartCoroutine(PlayLoseVideoAndDelay());
        //}

        //else GameManager.OnLevelFailedFunc();

        StartCoroutine(PlayLoseVideoAndDelay());
        //GameManager.OnLevelEnd();
    }

    private void StopCountDownTimer()
    {
        levelIsDone = true;
    }

    public void UpdateStrikes()
    {
        if (StrikesManager.strikes > 1)
        {
            StrikesManager.strikes--;
        }
        else if (StrikesManager.strikes == 1 && !StrikesManager.isGrantedFinalChance)
        {
            StrikesManager.isGrantedFinalChance = true;
            StrikesManager.isItTheLastChance = true;
        }
        else if (StrikesManager.strikes == 1 && StrikesManager.isGrantedFinalChance)
        {
            StrikesManager.strikes--;
        }
        //else if (StrikesManager.strikes == 0 && !StrikesManager.isGrantedFinalChance)
        //{
        //    StrikesManager.strikes++;
        //    StrikesManager.isGrantedFinalChance = true;
        //}
        else if (StrikesManager.strikes == 0)
        {
            Debug.Log("Game Over!");
            //start over..
            //StrikesManager.strikes = 3;
            //StrikesManager.isGrantedFinalChance = false;
            //LoadMainScene();
        }
    }

    public void IncreaseScore()
    {
        if (timeLeft >= 3)
            ScoreHolder.score += 10;
        else if (timeLeft > 1 && timeLeft < 3)
            ScoreHolder.score += 5;
        else if (timeLeft <= 1)
            ScoreHolder.score += 1;
    }

    public void LoadStrikesScene()
    {
        SceneManager.LoadScene("Strike_Out_Scene");
    }

    public void LoadFirstLevel()
    {
        if (!OpeningSequenceController.isSequenceShown)
            OpeningSequenceController.isSequenceShown = true;

        SceneManager.LoadScene(2);
    }

    public void LoadLeaderboardLevel()
    {
        if (!OpeningSequenceController.isSequenceShown)
            OpeningSequenceController.isSequenceShown = true;

        SceneManager.LoadScene(7);
    }

    public void UpdateCurrentLevel()
    {
        currentLevel++;
    }

    public void LoadNextLevel()
    {
        //StartCoroutine(PlayWhiteScreen());
        //PlayWhiteScreenCamera();

        //PlaySnowEffect();

        if (currentLevel <= 7 && StrikesManager.strikes > 0)
        {
            //PlaySnowEffect();
            SceneManager.LoadScene(currentLevel);
        }
        else if (currentLevel > 7)
        {
            StrikesManager.strikes = 3;
            StrikesManager.isGrantedFinalChance = false;
            currentLevel = 2;
            SceneManager.LoadScene(0);
        }
        else if (StrikesManager.strikes == 0)
        {
            StrikesManager.strikes = 3;
            StrikesManager.isGrantedFinalChance = false;
            currentLevel = 2;
            SceneManager.LoadScene(7);
        }
    }

    public void LoadWonLevel()
    {
        listOfLevelsToBeCompleted.Remove(SceneManager.GetActiveScene().buildIndex);

        randNum = listOfLevelsToBeCompleted[Random.Range(2, listOfLevelsToBeCompleted.Count)];

        //foreach (var sceneNum in listOfLevelsToBeCompleted)
        //{
        //    if (randNum == sceneNum)
        //        LoadRandomGameScene();
        //}

        Debug.Log("Level Won");

        //StartCoroutine(PlayWhiteScreen());
        SceneManager.LoadScene(randNum);       
    }

    public void LoadRandomGameScene()
    {
        randNum = Random.Range(2, 6);
        Debug.Log("Load Random Level");
        //StartCoroutine(PlayWhiteScreen());
        SceneManager.LoadScene(randNum);
    }

    public void LoadFailedLevel()
    {
        Debug.Log("level failed");
        LoadRandomGameScene();
    }

    public void LoadMainScene()
    {
        currentLevel = 2;
        SceneManager.LoadScene(0);
    }

    public void LoadWhiteScreenScene()
    {
        SceneManager.LoadScene(1);
    }

    //private void PlayWhiteScreenCamera()
    //{
    //    whiteScreenVideoCamera.Play();
    //}

    private void PlaySnowEffect()
    {
        snowAnim.SetActive(true);
        SoundManager.Instance.Play(snowClip);
    }

    private IEnumerator PlayWhiteScreen()
    {
        whiteScreenVideoCamera.SetActive(true);
        yield return new WaitForSeconds(1f);
        whiteScreenVideoCamera.SetActive(false);
    }

    //private IEnumerator PlayWinVideoAndDelay()
    //{
    //    winVideoCamera.SetActive(true);
    //    yield return new WaitForSeconds(6f);
    //    //winVideoCamera.SetActive(false);

    //    GameManager.OnlevelCompleteFunc();
    //}

    private IEnumerator PlayLoseVideoAndDelay()
    {
        SoundManager.Instance.Play(loseClip);
        yield return new WaitForSeconds(1f);

        //GameManager.OnTimeIsOver -= StartCountDownTimer;

        loseVideoCamera.SetActive(true);
        yield return new WaitForSeconds(videoLength);
        //LoseVideoCamera.SetActive(false);

        GameManager.OnLevelFailedFunc();
    }
}
