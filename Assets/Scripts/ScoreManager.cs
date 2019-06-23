using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text[] scoreTexts;

    private void OnEnable()
    {
        for (int k = 0; k < scoreTexts.Length; k++)
        {
            if (!PlayerPrefs.GetInt(scoreTexts[k].rectTransform.parent.GetChild(0).name).Equals(0))
            {
                scoreTexts[k].text = PlayerPrefs.GetInt(scoreTexts[k].rectTransform.parent.GetChild(0).GetComponent<Text>().text).ToString();
                Debug.Log(scoreTexts[k].rectTransform.parent.GetChild(0).GetComponent<Text>().text + scoreTexts[k].text);
            }        
        }
    }

    private void Start()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (scoreTexts[i].text == "0")
            {
                if (ScoreHolder.score != 0)
                {
                    scoreTexts[i].text = ScoreHolder.score.ToString();
                    //Debug.Log(scoreTexts[i].rectTransform.parent.GetChild(0).name);
                    PlayerPrefs.SetInt(scoreTexts[i].rectTransform.parent.GetChild(0).GetComponent<Text>().text.ToString(), ScoreHolder.score);
                    //PlayerPrefs.Save();
                    //Debug.Log(PlayerPrefs.GetInt(scoreTexts[i].rectTransform.parent.GetChild(0).name.ToString()));
                    break;
                }
            }
            if (scoreTexts[scoreTexts.Length - 1].text != "0")
            {
                for (int j = 0; j < scoreTexts.Length; j++)
                {
                    PlayerPrefs.SetInt(scoreTexts[j].rectTransform.parent.GetChild(0).GetComponent<Text>().text.ToString(), 0);
                    scoreTexts[j].text = "0";
                }
            }
            //else
            //{
            //    for (int j = 0; j < scoreTexts.Length; j++)
            //    {
            //        scoreTexts[j].text = "0";
            //    }
            //}
        }
    }

    private void OnDisable()
    {
        ScoreHolder.score = 0;
    }
}
