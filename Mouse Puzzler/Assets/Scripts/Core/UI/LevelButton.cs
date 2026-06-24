using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameEvent OnRequestLoadScene;
    [HideInInspector] public SceneData SceneOnClick;
    [HideInInspector] public TextMeshProUGUI Text;
    public void LoadLevel()
    {
        OnRequestLoadScene?.Raise(this, SceneOnClick);
    }
    private void Awake()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }
}
