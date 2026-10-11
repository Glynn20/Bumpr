using TMPro;
using UnityEngine;
public class NextLevelButton : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    [SerializeField] private GameEvent OnNextLevelButtonPressed;

    //[Header("SFX")] 
    //[SerializeField] private SoundData UIClickSound;


    public void OnClick()
    {
        OnNextLevelButtonPressed?.Raise(this, null);
        //OnPlaySound?.Raise(this, UIClickSound);
    }
    

}
