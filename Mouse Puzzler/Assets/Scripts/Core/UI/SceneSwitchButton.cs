using TMPro;
using UnityEngine;
public class SceneSwitchButton : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;
    [SerializeField] private GameEvent OnRequestLoadNextScene;
    [SerializeField] private GameEvent OnPlaySound;

    [Header("SFX")] 
    [SerializeField] private SoundData UIClickSound;

    [Header("Other")]
    [SerializeField] private int sceneSwitchIndex;

    

    public void OnClick()
    {
        SwitchToSceneByNumber();
        OnPlaySound?.Raise(this, UIClickSound);
    }
    public void SwitchToSceneByNumber()
    {
        OnRequestLoadScene?.Raise(this, sceneSwitchIndex);
    }
    public void SwitchToNextScene()
    {
        OnRequestLoadNextScene?.Raise(this, null);
    }
}
