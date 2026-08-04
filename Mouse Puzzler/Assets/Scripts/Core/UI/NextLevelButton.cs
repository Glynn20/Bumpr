using TMPro;
using UnityEngine;
public class NextLevelButton : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;
    [SerializeField] private GameEvent OnRequestLoadNextScene;
    [SerializeField] private GameEvent OnPlaySound;
    [SerializeField] private GameEvent OnNextLevelButtonPressed;

    [Header("SFX")] 
    [SerializeField] private SoundData UIClickSound;

    [Header("Other")]
    [SerializeField] private SceneData sceneSwitchOnClick;


    public void OnClick()
    {
        OnNextLevelButtonPressed?.Raise(this, null);
    }
    
    public void SwitchToScene()
    {
        OnRequestLoadScene?.Raise(this, sceneSwitchOnClick);
    }

}
