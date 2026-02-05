using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float _rotationSpeedDegreesPerSec = 90;
    
    public bool ClockwiseRotation = false;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        float direction = ClockwiseRotation ? -1f : 1f;

        transform.Rotate(0, 0, direction * _rotationSpeedDegreesPerSec * Time.deltaTime);
    }
}
