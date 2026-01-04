using System;
using UnityEngine;

public interface IValidationMovementUIService 
{
    public event Action<bool> OnDiskMoveComplete;

    public void OnDiskMove(bool isValidMove);
}
