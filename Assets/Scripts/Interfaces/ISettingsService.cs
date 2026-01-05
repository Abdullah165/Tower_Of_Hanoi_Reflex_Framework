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

    public Stack<MoveHistory> Undos { get; }
    public Stack<MoveHistory> Redos { get; }

    void RecordMove(Disk disk, Peg from, Peg to, Vector3 previousePos);

    void Undo();

    void Redo();
}
