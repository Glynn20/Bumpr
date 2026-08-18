using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private SceneRegistry registry;
    private SceneData _currentScene;
    bool _isLoading = false;
    
    [Header("Events")]
    [SerializeField] public GameEvent OnLevelLoadComplete;

    protected override void Awake()
    {
        base.Awake();
        string currentName = SceneManager.GetActiveScene().name;
        _currentScene = registry.GetLevelByName(currentName);

    }

    public void LoadLevel(Component sender, params object[] data)
    {
        if (_isLoading) return;

        if (data.Length > 0 && data[0] is SceneData nextSceneData)
        {
            if (registry.AllLevels.Contains(nextSceneData))
            {
                _currentScene = nextSceneData;
                StartCoroutine(TransitionScene(nextSceneData));
            }
        }
    }

    private IEnumerator TransitionScene(SceneData newScene) // TO DO
    {
        _isLoading = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene.SceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        _isLoading = false;
        OnLevelLoadComplete?.Raise(this, newScene);
        yield return null;
    }
}
