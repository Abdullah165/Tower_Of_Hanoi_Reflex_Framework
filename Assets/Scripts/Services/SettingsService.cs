using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsService : ISettingsService
{
    private readonly Stack<ISettingsService.MoveHistory> undos = new();
    public Stack<ISettingsService.MoveHistory> Undos => undos;


    private readonly Stack<ISettingsService.MoveHistory> redos = new();
    public Stack<ISettingsService.MoveHistory> Redos => redos;

    private readonly Queue<ISettingsService.AutoSolverStep> solverSteps = new();
    public Queue<ISettingsService.AutoSolverStep> AutoSolverSteps => solverSteps;


    public void RecordMove(Disk disk, Peg from, Peg to, Vector3 previousePos)
    {
        var move = new ISettingsService.MoveHistory()
        {
            disk = disk,
            fromPeg = from,
            toPeg = to,
            previousPos = previousePos
        };

        undos.Push(move);

        redos.Clear();
    }

    public void Redo()
    {
        if (redos.Count > 0)
        {
            var move = redos.Pop();

            move.fromPeg.Disks.Pop();
            move.toPeg.Disks.Push(move.disk);

            Vector3 currentPos = move.disk.transform.position;

            move.disk.transform.position = move.previousPos;

            move.previousPos = currentPos;
            undos.Push(move);
        }
    }

    public void Undo()
    {
        if (undos.Count > 0)
        {
            var move = undos.Pop();

            move.toPeg.Disks.Pop();
            move.fromPeg.Disks.Push(move.disk);

            Vector3 currentPos = move.disk.transform.position;

            move.disk.transform.position = move.previousPos;

            move.previousPos = currentPos;
            redos.Push(move);
        }
    }

    public void StartAutoSolve(int diskCount, Peg a, Peg b, Peg c)
    {
        CoroutineRunner.Instance.StopAllCoroutines();
        solverSteps.Clear();

        DoAutomaticSolve(diskCount, a, c, b);

        CoroutineRunner.Instance.Run(PlaySolutionCoroutine());
    }

    public void DoAutomaticSolve(int diskCount, Peg a, Peg b, Peg c)
    {
        if (diskCount == 0) return;

        DoAutomaticSolve(diskCount - 1, a, c, b);

        solverSteps.Enqueue(new ISettingsService.AutoSolverStep { fromPeg = a, toPeg = b });

        DoAutomaticSolve(diskCount - 1, c, b, a);
    }

    public IEnumerator PlaySolutionCoroutine()
    {
        while (solverSteps.Count > 0)
        {
            var step = solverSteps.Dequeue();

            if (step.fromPeg.Disks.Count > 0)
            {
                Disk diskToMove = step.fromPeg.Disks.Peek();

                step.fromPeg.Disks.Pop();

                step.toPeg.Disks.Push(diskToMove);

                RecordMove(diskToMove, step.fromPeg, step.toPeg, diskToMove.transform.position);

                //Stop physics of the disk
                if (diskToMove.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.isKinematic = true;
                }

                //Move Up
                Vector3 upPos = new Vector3(diskToMove.transform.position.x, 1, diskToMove.transform.position.z);
                diskToMove.transform.position = upPos;
                yield return new WaitForSeconds(0.3f);

                //Move over
                Vector3 overPos = new Vector3(step.toPeg.transform.position.x, 1, step.toPeg.transform.position.z);
                diskToMove.transform.position = overPos;
                yield return new WaitForSeconds(0.3f);

                // enable physics
                if (rb != null)
                {
                    rb.isKinematic = false;
                }

                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
