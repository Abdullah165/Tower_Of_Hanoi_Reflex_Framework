using System;
using UnityEngine;

public class ValidationMovementUIService : IValidationMovementUIService
{
    public event Action<bool> OnDiskMoveComplete;

    public void OnDiskMove(bool isValidMove)
    {
        OnDiskMoveComplete?.Invoke(isValidMove);
    }
}
