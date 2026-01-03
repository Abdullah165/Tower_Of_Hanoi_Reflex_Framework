using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    private Stack<int> diskSizes = new Stack<int>();

    public Stack<int> DiskSizes {  get { return diskSizes; } }

    public void Initialize(List<GameObject> initialDisksSize)
    {
        for (int i = initialDisksSize.Count; i > 0; i--)
        {
            AddDisk(i);
        }
    }

    private void AddDisk(int diskSize)
    {
        diskSizes.Push(diskSize);
    }
}
