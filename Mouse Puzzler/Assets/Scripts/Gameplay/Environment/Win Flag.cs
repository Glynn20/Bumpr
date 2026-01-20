using UnityEngine;

public class WinFlag : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<IPlayer>() != null)
        {
            Debug.Log("Level Complete!");
        }
    }
}
