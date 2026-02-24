using UnityEngine;

public class WinFlag : MonoBehaviour
{
    [SerializeField] private GameEvent OnLevelComplete;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<IPlayer>() != null)
        {
            OnLevelComplete?.Raise(this, null);
        }
    }
}
