using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraPanTrigger : MonoBehaviour
{
    [SerializeField] private Transform _nextSnapPoint;
    [SerializeField] private bool _lerpTransition;
    private IPannableCamera cam;


    private void Start()
    {
        cam = Camera.main.GetComponent<IPannableCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;

        if (cam == null) 
        {
            Debug.Log("Cannot Pan Camera. Main Camera has no GameCamera component.", this); 
            return;
        }

        if (_lerpTransition)
        {
            cam.SnapToPoint(_nextSnapPoint);    
        }
        else
        {
            cam.SnapToPointInstant(_nextSnapPoint);
        }
        
    }



}
