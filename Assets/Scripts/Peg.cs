using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    private readonly Stack<Disk> disks = new();
    public Stack<Disk> Disks { get { return disks; } }

    public void Initialize(List<GameObject> initialDisks)
    {
        for (int i = 0; i < initialDisks.Count; i++)
        {
            if (initialDisks[i].TryGetComponent<Disk>(out var disk))
            {
                AddDisk(disk);
            }
        }
    }

    private void AddDisk(Disk disk)
    {
        disks.Push(disk);
    }
}
