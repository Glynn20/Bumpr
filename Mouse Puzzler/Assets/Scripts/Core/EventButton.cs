using TMPro;
using UnityEngine;
public class SceneSwitchButton : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;
    [SerializeField] private GameEvent OnRequestLoadNextScene;
    
    [Header("Other")]
    [SerializeField] private int sceneSwitchIndex;

    public void SwitchToSceneByNumber()
    {
        OnRequestLoadScene?.Raise(this, sceneSwitchIndex);
    }
    public void SwitchToNextScene()
    {
        OnRequestLoadNextScene?.Raise(this, null);
    }
}
