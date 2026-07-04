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
    public void LoadLevel(int levelNumber)
    {
        if (_isLoading) return;

        // Load scene where its number matches the levelnumber given
        SceneData sceneData = registry.GetLevelByNumber(levelNumber);
        StartCoroutine(TransitionScene(sceneData));

        _currentScene = sceneData;
    }

    public void LoadLevel(Component sender, params object[] data)
    {
        if (_isLoading) return;
        /*
        if (data.Length > 0 && data[0] is int levelNumber)
        {
            SceneData sceneData = registry.GetLevelByNumber(levelNumber);
            StartCoroutine(TransitionScene(sceneData));
            _currentScene = sceneData;
        }
        else
        {
            Debug.Log("LevelLoad failed. Bad data passed via event.");
        }
        */
        if (data.Length > 0 && data[0] is SceneData nextSceneData)
        {
            if (registry.AllLevels.Contains(nextSceneData))
            {
                _currentScene = nextSceneData;
                StartCoroutine(TransitionScene(nextSceneData));
            }
        }
    }

    public void LoadNextLevel()
    {
        if (_isLoading) return;
        
        int nextSceneIndex = 0; // Main Menu by default

        if (_currentScene != null && _currentScene.LevelNumber < registry.AllLevels.Count - 1)
        {
            nextSceneIndex = _currentScene.LevelNumber + 1;
        }

        SceneData nextSceneData = registry.GetLevelByNumber(nextSceneIndex);
        _currentScene = nextSceneData;

        StartCoroutine(TransitionScene(nextSceneData));
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
