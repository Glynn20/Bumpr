using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraPanTrigger : MonoBehaviour
{
    [SerializeField] private Transform _nextSnapPoint;
    [SerializeField] private Transform _queuedSnapPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;

        IPannableCamera cam = Camera.main.GetComponent<IPannableCamera>();

        if (cam == null) 
        {
            Debug.Log("Cannot Pan Camera. Main Camera has no GameCamera component.", this); 
            return;
        }

        cam.SnapToPoint(_nextSnapPoint);
        SwitchSnapPoints();
    }

    private void SwitchSnapPoints()
    {
        Transform temp = _nextSnapPoint;
        _nextSnapPoint = _queuedSnapPoint;
        _queuedSnapPoint = temp;
    }

}
