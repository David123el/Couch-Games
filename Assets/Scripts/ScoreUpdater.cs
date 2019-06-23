using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = ScoreHolder.score.ToString();
    }
}
