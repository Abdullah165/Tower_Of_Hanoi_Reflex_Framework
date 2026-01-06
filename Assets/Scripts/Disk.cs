using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class Disk : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public int Size { get; set; }

    private Vector3 previousePos;

    private Peg currentPeg;

    private IValidationMovementUIService validationService;
    private ISettingsService settingsService;

    public void Initialize(IValidationMovementUIService validationMovementService, ISettingsService settingsService)
    {
        this.validationService = validationMovementService;
        this.settingsService = settingsService;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousePos = transform.position;

        Vector3 startPoint = transform.position + Vector3.up * 5f;

        if (Physics.Raycast(startPoint, Vector3.down, out var hitInfo, 5))
        {
            if (hitInfo.transform.TryGetComponent<Peg>(out var Peg))
            {
                currentPeg = Peg;
                Debug.Log("Current Peg = " + currentPeg.name);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 10))
        {
            if (hitInfo.transform.TryGetComponent<Peg>(out var newPeg))
            {
                Debug.Log("Name" + hitInfo.transform.name);
                bool isValidMove = newPeg.Disks.Count == 0 || Size < newPeg.Disks.Peek().Size;

                validationService.OnDiskMove(isValidMove);


                if (isValidMove)
                {
                    currentPeg.Disks.Pop();
                    newPeg.Disks.Push(this);

                    transform.position = newPeg.transform.position; // snap the disk to correct Peg pos

                    settingsService.RecordMove(this, currentPeg, newPeg, previousePos);
                }
                else
                {
                    transform.position = previousePos;
                }
            }
            else
            {
                transform.position = previousePos;
            }
        }
    }
}
