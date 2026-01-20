using System.Data.Common;
using System.Net.Sockets;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool _canAttach = true;
    private Transform _transform;
    private ICanAttachBomb _currentSocket;

    void Awake()
    {
        _transform = transform;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        ICanAttachBomb socket = other.gameObject.GetComponent<ICanAttachBomb>();
        
        if (socket == null || _canAttach == false || socket == _currentSocket) return;
        
        _transform.SetParent(socket.BombAttachPoint);
        _transform.position = socket.BombAttachPoint.position;
        _currentSocket = socket;

        if (socket.IsPermanentAttachPoint) {_canAttach = false;}
    }
}
