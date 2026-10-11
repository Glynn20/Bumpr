using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _rendererToDisplayOnDeath;

    private bool _rendererChanged = false;
    private int _originalLayer = 0;

    public void OnPlayerContinue()
    {
        if (!_rendererChanged) return;
        _rendererToDisplayOnDeath.sortingOrder = _originalLayer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;
        
        // Display object that killed player over the black overlay
        _rendererChanged = true;
        _originalLayer = _rendererToDisplayOnDeath.sortingOrder;
        _rendererToDisplayOnDeath.sortingOrder = 10;

        IHealth health = other.gameObject.GetComponentInChildren<IHealth>();
        health.TakeDamage();
    }
}
