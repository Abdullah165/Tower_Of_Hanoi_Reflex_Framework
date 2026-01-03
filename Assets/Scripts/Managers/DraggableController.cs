using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float zCoord;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rigidbody.isKinematic = true;

        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = GetMouseWorldPos();

        currentPos.z = transform.position.z;

        transform.position = currentPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        rigidbody.isKinematic = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}