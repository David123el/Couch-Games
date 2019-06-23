using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 initalPos;
    private Vector3 finalPos;
    private float nextYPos;
    private float deltaStep = 135.8f;

    private Vector3 newPos;

    private bool isDragging = false;
    private bool allowDragging = false;

    private Direction direction;

    [SerializeField]
    private float dragFactor = 50f;

    private void Start()
    {
        StartCoroutine(DelayForClip());
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        newPos = new Vector3(transform.position.x, objPos.y, 10f);
        newPos.Normalize();
        Vector3 normalizedPos = newPos.normalized;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + normalizedPos.y * dragFactor, transform.localPosition.z);

        finalPos = Input.mousePosition;

        if (initalPos.y < finalPos.y)
        {
            direction = Direction.positive;
        }
        else
        {
            direction = Direction.negative;
        }

        if (transform.localPosition.y >= 1350f || transform.localPosition.y <= -1350f)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        initalPos = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        allowDragging = false;
    }

    private IEnumerator Roll()
    {
        switch (direction)
        {
            case Direction.positive:
                transform.localPosition = Vector3.Lerp(transform.position, new Vector3(transform.localPosition.x, transform.localPosition.y + deltaStep, transform.localPosition.z), 100f);
                break;
            case Direction.negative:
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (deltaStep * -1f), transform.localPosition.z);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator DelayForClip()
    {
        yield return new WaitForSeconds(3.5f);

        GetComponent<Animator>().enabled = false;
    }
}

public enum Direction
{
    positive,
    negative
}
