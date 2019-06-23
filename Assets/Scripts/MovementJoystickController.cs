using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystickController : MonoBehaviour
{
    [SerializeField]
    private List<BoxCollider2D> arrows = new List<BoxCollider2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButton("Fire1"))
        //{
        //    for (int i = 0; i < arrows.Count; i++)
        //    {
        //        if (arrows[i].gameObject.name == "Up Arrow")
        //        {
        //            Debug.Log("Up");
        //        }
        //        if (arrows[i].gameObject.name == "Up Arrow")
        //        {
        //            Debug.Log("Up");
        //        }
        //        if (arrows[i].gameObject.name == "Up Arrow")
        //        {
        //            Debug.Log("Up");
        //        }
        //        if (arrows[i].gameObject.name == "Up Arrow")
        //        {
        //            Debug.Log("Up");
        //        }
        //    }
        //}
    }
}
