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
    [SerializeField] private SceneData sceneSwitchOnClick;

    

    public void OnClick()
    {
        SwitchToScene();
        OnPlaySound?.Raise(this, UIClickSound);
    }
    public void SwitchToScene()
    {
        OnRequestLoadScene?.Raise(this, sceneSwitchOnClick);
    }
    public void SwitchToNextScene()
    {
        OnRequestLoadNextScene?.Raise(this, null);
    }
}
