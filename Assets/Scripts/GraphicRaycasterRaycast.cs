//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class GraphicRaycasterRaycast : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    [SerializeField]
    private GameObject[] targets;

    private int counter = 0;

    [SerializeField]
    private GameObject winVideoCamera;
    [SerializeField]
    private GameObject LoseVideoCamera;

    [SerializeField]
    private AudioClip winClip;
    [SerializeField]
    private AudioClip loseClip;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    public void ReturnObjsFromRaycast()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);

        for (int i = 0; i < targets.Length; i++)
        {
            var screenPos = Camera.main.WorldToScreenPoint(targets[i].transform.position);

            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = screenPos;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Snow")
                {
                    counter++;
                }
            }
        }
    }

    public void CheckForVictory()
    {
        LevelManager.levelIsDone = true;

        if (counter >= 5)
        {
            StartCoroutine(PlayWinVideoAndDelay());
            //GameManager.OnlevelCompleteFunc();
        }
        else
        {
            StartCoroutine(PlayLoseVideoAndDelay());
            //GameManager.OnLevelFailedFunc();
        }
    }

    public void ZeroTheCounter()
    {
        counter = 0;
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

        LoseVideoCamera.SetActive(true);
        yield return new WaitForSeconds(6f);
        //LoseVideoCamera.SetActive(false);

        GameManager.OnLevelFailedFunc();
    }
}
