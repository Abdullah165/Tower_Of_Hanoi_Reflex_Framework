using Reflex.Core;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawnerService : IDiskSpawnerService
{
    public int DiskCount { get; set; }

    public List<GameObject> Spawn(int count, GameObject prefab, Vector3 position, float diskHeight)
    {
        List<GameObject> disks = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = position + new Vector3(0, i * diskHeight, 0);
            var disk = Object.Instantiate(prefab, spawnPos, Quaternion.identity);

            disks.Add(disk);

            if(disk.TryGetComponent<Disk>(out var currentDisk))
            {
                currentDisk.Size = count - i;
            }
        }

        return disks;
    }

    public void SetInitialScale(List<GameObject> disks, float scaleInterval)
    {
        if (disks.Count == 0) return;

        GameObject previousDisk = disks[0];

        for (int i = 1; i < disks.Count; i++)
        {
            Vector3 newScale = new Vector3(
                previousDisk.transform.localScale.x - scaleInterval,
                disks[i].transform.localScale.y,
                previousDisk.transform.localScale.z - scaleInterval
            );

            disks[i].transform.localScale = newScale;
            previousDisk = disks[i];
        }
    }

    public void SetInitialColor(List<GameObject> disks, Gradient gradient)
    {
        for (int i = 0; i < disks.Count; i++)
        {
            if (disks[i].TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                float t = (float)i / (disks.Count - 1); 
                meshRenderer.material.color = gradient.Evaluate(t);
            }
        }
    }
}