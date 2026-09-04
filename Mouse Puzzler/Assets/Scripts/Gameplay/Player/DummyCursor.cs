using UnityEngine;

public class DummyCursor : PlayerMovement
{
    [Header("Bumpr Model")]
    [SerializeField] private GameObject BumprModel;
    
    [Header("Sensitivity Settings")]
    [SerializeField] private float _minMouseSensMultiplier = 0.4f;
    private float _maxMouseSensMultiplier = 1.0f;

    [Header("Distance Thresholds")]
    [SerializeField] private float _minMouseSensRange = 1.0f;  
    [SerializeField] private float _maxMouseSensRange = 5.0f;  
    
    private float _distanceToBumpr;
    private float _originalBaseSens; // Stores the starting value of _mouseSens

    private void GetDistanceToBumpr()
    {
        _distanceToBumpr = Vector2.Distance(transform.position, BumprModel.transform.position);
    }

    private float CalculateSensMultiplier()
    {
        if (_distanceToBumpr >= _maxMouseSensRange)
        {
            return _maxMouseSensMultiplier;
        }
        if (_distanceToBumpr <= _minMouseSensRange)
        {
            return _minMouseSensMultiplier;
        }

        float t = Mathf.InverseLerp(_minMouseSensRange, _maxMouseSensRange, _distanceToBumpr);
        return Mathf.Lerp(_minMouseSensMultiplier, _maxMouseSensMultiplier, t);
    }
}