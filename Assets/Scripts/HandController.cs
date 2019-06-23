using UnityEngine;

public class HandController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        }
    }
}
