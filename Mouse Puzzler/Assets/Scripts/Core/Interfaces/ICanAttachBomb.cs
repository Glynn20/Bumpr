using Unity.VisualScripting;
using UnityEngine;

public interface ICanAttachBomb
{
    public Transform BombAttachPoint { get; }
    public bool IsPermanentAttachPoint { get; }
}
