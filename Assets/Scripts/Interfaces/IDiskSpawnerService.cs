using System.Collections.Generic;
using UnityEngine;

public interface IDiskSpawnerService
{
    int DiskCount { get; set; }

    List<GameObject> Spawn(int count, GameObject prefab, Vector3 position, float diskHeight);
    void SetInitialScale(List<GameObject> disks, float scaleInterval);
    void SetInitialColor(List<GameObject> disks, Gradient gradient);
}