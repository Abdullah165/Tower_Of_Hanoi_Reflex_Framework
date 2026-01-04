using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    [Inject] private IDiskSpawnerService diskSpawnerService;
    [Inject] private IValidationMovementUIService validationService;

    [SerializeField] private int spawnCount = 5;
    [SerializeField] private GameObject diskPrefab;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float scaleInterval = 0.05f;
    [SerializeField] private float diskHeight = 0.02f;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Peg pegA;

    private List<GameObject> spawnedDisks;

    private void Start()
    {
        spawnedDisks = diskSpawnerService.Spawn(spawnCount, diskPrefab, spawnPos.position, diskHeight);
        diskSpawnerService.SetInitialScale(spawnedDisks, scaleInterval);
        diskSpawnerService.SetInitialColor(spawnedDisks, gradient);

        foreach (var disk in spawnedDisks)
        {
            var diskComponent = disk.GetComponent<Disk>();
            if (diskComponent != null)
            {
                diskComponent.Initialize(validationService);
            }
        }

        pegA.Initialize(spawnedDisks);
    }
}