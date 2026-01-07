using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button restart;
    [SerializeField] private Button undo;
    [SerializeField] private Button redo;
    [SerializeField] private Button autoSolver;

    [SerializeField] private Peg pegA;
    [SerializeField] private Peg pegB;
    [SerializeField] private Peg pegC;

    [Inject] private readonly ISettingsService settingsService;
    [Inject] private readonly IDiskSpawnerService diskSpawnerService;

    private void Start()
    {
        restart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        undo.onClick.AddListener(() =>
        {
            settingsService.Undo();
        });

        redo.onClick.AddListener(() =>
        {
            settingsService.Redo();
        });

        autoSolver.onClick.AddListener(() =>
        {
            settingsService.StartAutoSolve(diskSpawnerService.DiskCount, pegA, pegB, pegC);
        });
    }
}