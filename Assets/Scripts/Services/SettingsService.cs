using System.Collections.Generic;
using UnityEngine;
using static ISettingsService;

public class SettingsService : ISettingsService
{
    private Stack<ISettingsService.MoveHistory> undos = new Stack<ISettingsService.MoveHistory>();
    public Stack<ISettingsService.MoveHistory> Undos => undos;


    private Stack<ISettingsService.MoveHistory> redos = new Stack<ISettingsService.MoveHistory>();
    public Stack<ISettingsService.MoveHistory> Redos => redos;

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
            MoveHistory move = redos.Pop();

            move.fromPeg.DiskSizes.Pop();
            move.toPeg.DiskSizes.Push(move.disk.Size);

            Vector3 currentPos = move.disk.transform.position;

            move.disk.transform.position = move.previousPos;
           // move.disk.SetCurrentPeg(move.toPeg);

            move.previousPos = currentPos;
            undos.Push(move);
        }
    }

    public void Undo()
    {
        if (undos.Count > 0)
        {
            var move = undos.Pop();

            move.toPeg.DiskSizes.Pop();
            move.fromPeg.DiskSizes.Push(move.disk.Size);

            Vector3 currentPos = move.disk.transform.position;

            move.disk.transform.position = move.previousPos;

            move.previousPos = currentPos;
            redos.Push(move);
        }
    }
}
