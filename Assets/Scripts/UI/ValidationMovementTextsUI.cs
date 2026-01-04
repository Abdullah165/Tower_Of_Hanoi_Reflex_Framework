using Reflex.Attributes;
using System.Collections;
using TMPro;
using UnityEngine;

public class ValidationMovementTextsUI : MonoBehaviour
{
    [Inject] private IValidationMovementUIService validationMovementUIService;

    [SerializeField] private TextMeshProUGUI validMoveText;
    [SerializeField] private TextMeshProUGUI wrongMoveText;


    private void Start()
    {
        wrongMoveText.gameObject.SetActive(false);
        validMoveText.gameObject.SetActive(false);

        validationMovementUIService.OnDiskMoveComplete += ValidationMovementUIService_OnDiskMoveComplete;
    }

    private void ValidationMovementUIService_OnDiskMoveComplete(bool isValidMove)
    {
        if (isValidMove == false)
        {
            wrongMoveText.gameObject.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(HideTextCoroutine(wrongMoveText));
        }
        else
        {
            validMoveText.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideTextCoroutine(validMoveText));
        }
    }

    private IEnumerator HideTextCoroutine(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
    }
}
