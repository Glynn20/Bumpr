using UnityEngine;

public class EventCallButton : MonoBehaviour
{

    [SerializeField] private GameEvent OnClickEvent;
    public void OnClick()
    {
        OnClickEvent?.Raise(this, null);
    }

}
