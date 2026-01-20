using UnityEngine;

public class BombAttachPad : MonoBehaviour, ICanAttachBomb
{
    [SerializeField] private bool _isPermanentAttachPoint = false;
    [SerializeField] private Transform _bombAttachPoint;

    public Transform BombAttachPoint => _bombAttachPoint;
    public bool IsPermanentAttachPoint => _isPermanentAttachPoint;

}
