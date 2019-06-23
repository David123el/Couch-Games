using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnLevelComplete = delegate { };
    public static event Action OnLevelFailed = delegate { };
    public static event Action OnTimeIsOver = delegate { };

    public delegate IEnumerator LevelEndHandler();
    public static event LevelEndHandler LevelEnd;

    public static void OnlevelCompleteFunc()
    {
        OnLevelComplete();
    }

    public static void OnLevelFailedFunc()
    {
        OnLevelFailed();
    }

    public static void OnTimeIsOverFunc()
    {
        OnLevelFailed();
    }

    public void OnLevelEnd()
    {
        LevelEndHandler levelEnd = LevelEnd;
        if (levelEnd != null)
            StartCoroutine(LevelEnd());
    }
}
