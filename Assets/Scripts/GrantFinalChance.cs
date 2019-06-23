using UnityEngine;

public class GrantFinalChance : MonoBehaviour
{
    private void Awake()
    {
        if (StrikesManager.isItTheLastChance)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                gameObject.SetActive(false);
                StrikesManager.isItTheLastChance = false;
            }
        }
    }
}
