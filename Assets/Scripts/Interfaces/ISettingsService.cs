using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface ISettingsService
{
    public struct MoveHistory
    {
        public Disk disk;
        public Peg fromPeg;
        public Peg toPeg;
        public Vector3 previousPos;
    }

    public struct AutoSolverStep
    {
        public Peg fromPeg;
        public Peg toPeg;
    }

    public Stack<MoveHistory> Undos { get; }
    public Stack<MoveHistory> Redos { get; }

    public Queue<AutoSolverStep> AutoSolverSteps { get; }

    void RecordMove(Disk disk, Peg from, Peg to, Vector3 previousePos);

    void Undo();

    void Redo();

    void StartAutoSolve(int diskCount, Peg a, Peg b, Peg c);

    void DoAutomaticSolve(int diskCount, Peg a, Peg b, Peg c);

    IEnumerator PlaySolutionCoroutine();
}
