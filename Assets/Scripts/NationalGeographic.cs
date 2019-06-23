using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationalGeographic : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer drone;
    [SerializeField]
    private SpriteRenderer net;
    private Vector3 netPos;
    [SerializeField]
    private float netTimer = 1f;
    private float currentTime;
    public static bool isNetThrown = false;

    [SerializeField]
    private AudioClip tutClip;

    void Start()
    {
        netPos = net.gameObject.transform.localPosition;

        SoundManager.Instance.Play(tutClip);
    }

    void Update()
    {
        Fetch();
    }

    private void Fetch()
    {
        if (net.gameObject.transform.parent != drone.gameObject.transform)
        {
            if (Time.time - currentTime >= netTimer)
            {
                isNetThrown = false;

                net.gameObject.transform.SetParent(drone.gameObject.transform);
                net.gameObject.transform.localPosition = netPos;
            }
        }
    }

    public void Throw()
    {
        currentTime = Time.time;
        isNetThrown = true;

        if (drone.gameObject.transform.childCount > 0)
        {
            net.gameObject.transform.SetParent(net.gameObject.transform.parent.parent);
            net.gameObject.transform.position = Vector3.Lerp(net.transform.position, new Vector3(net.gameObject.transform.position.x, net.gameObject.transform.position.y - 1f, net.gameObject.transform.position.z), 5.0f);
        }
    }

    public void MoveUp()
    {
        drone.transform.position = new Vector3(drone.transform.position.x, Mathf.Clamp(drone.transform.position.y + 1f, -3.8f, 4.5f), drone.transform.position.z);
    }

    public void MoveDown()
    {
        drone.transform.position = new Vector3(drone.transform.position.x, Mathf.Clamp(drone.transform.position.y - 1f, -3.8f, 4.5f), drone.transform.position.z);
    }

    public void MoveRight()
    {
        drone.transform.position = new Vector3(Mathf.Clamp(drone.transform.position.x + 1f, -7.4f, 7.4f), drone.transform.position.y, drone.transform.position.z);
    }

    public void MoveLeft()
    {
        drone.transform.position = new Vector3(Mathf.Clamp(drone.transform.position.x - 1f, -7.4f, 7.4f), drone.transform.position.y, drone.transform.position.z);
    }
}
