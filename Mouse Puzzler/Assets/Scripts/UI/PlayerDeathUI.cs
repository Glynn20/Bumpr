using TMPro;
using UnityEngine;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _blackOverlay;
    [SerializeField] private TextMeshProUGUI _deathText;

    public void OnPlayerDeath()
    {
        EnableOverlay();
    }
    public void OnPlayerContinue()
    {
        DisableOverlay();
    }

    private void Awake()
    {
        DisableOverlay();
    }

    private void DisableOverlay()
    {
        _blackOverlay.enabled = false;
        _deathText.enabled = false;  
    }

    private void EnableOverlay()
    {
        _blackOverlay.enabled = true;
        _deathText.enabled = true; 
    }
}
