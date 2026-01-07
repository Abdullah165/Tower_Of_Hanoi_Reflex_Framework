using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    [Inject] private readonly IDiskSpawnerService diskSpawnerService;
    [Inject] private readonly IValidationMovementUIService validationService;
    [Inject] private readonly ISettingsService settingsService;

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
        diskSpawnerService.DiskCount = spawnedDisks.Count;


        foreach (var disk in spawnedDisks)
        {
            if (disk.TryGetComponent<Disk>(out var diskComponent))
            {
                diskComponent.Initialize(validationService, settingsService);
            }
        }

        pegA.Initialize(spawnedDisks);
    }
}