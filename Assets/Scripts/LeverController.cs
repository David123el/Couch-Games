using System.Collections;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField]
    private GraphicRaycasterRaycast raycaster;

    [SerializeField]
    private AnimationClip clip;
    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private IEnumerator DelayForClip()
    {
        yield return new WaitForSeconds(3.5f);

        GetComponent<Animator>().enabled = false;
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().SetBool("isPlaying", true);
        GetComponent<Animator>().Play("Rolls_Opening_Anim", 1);
        anim.clip = clip;
        anim.Play();

        if (raycaster != null)
        {
            raycaster.ReturnObjsFromRaycast();
            raycaster.CheckForVictory();
        }
    }

    private void OnMouseUp()
    {
        GetComponent<Animator>().SetBool("isPlaying", false);

        raycaster.ZeroTheCounter();
    }
}
