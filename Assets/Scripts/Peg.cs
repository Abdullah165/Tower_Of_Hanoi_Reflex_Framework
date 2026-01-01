using Reflex.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    private Stack<int> diskSizes = new Stack<int>();
    public void Initialize(List<GameObject> initialDisksSize)
    {
        for (int i = initialDisksSize.Count; i > 0; i--)
        {
            AddDisk(i);
            Debug.Log("Disk Size = " + diskSizes.Peek());
        }
    }

    private void AddDisk(int diskSize)
    {
        diskSizes.Push(diskSize);
    }
}
