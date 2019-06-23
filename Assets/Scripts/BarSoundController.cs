using UnityEngine;

public class BarSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip tutClip;

    private void Start()
    {
        SoundManager.Instance.Play(tutClip);
    }
}
