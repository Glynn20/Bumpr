using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WinFlag : MonoBehaviour
{
    [SerializeField] private GameEvent OnLevelComplete;
    [SerializeField] private GameEvent OnPlayLevelCompleteSound;
    [SerializeField] private SoundData _levelCompleteSoundData;

    [SerializeField] private TextMeshProUGUI _levelCompleteText;

    void Start()
    {
        _levelCompleteText.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<IPlayer>() != null)
        {
            OnLevelComplete?.Raise(this, null);
            OnPlayLevelCompleteSound?.Raise(this, _levelCompleteSoundData);
            _levelCompleteText.enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
