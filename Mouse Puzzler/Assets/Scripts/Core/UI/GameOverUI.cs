using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _continueButton;

    [Header("Event")]
    [SerializeField] private GameEvent _onContinue; 

    void Start()
    {
        _continueButton.gameObject.SetActive(false);

    }
    public void _OnContinueButtonClicked()
    {
        _onContinue?.Raise(this, null);
        _continueButton.gameObject.SetActive(false);
    }

    public void OnPlayerDeath()
    {
        _continueButton.gameObject.SetActive(true);
    }

}
