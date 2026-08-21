using System.Collections.Generic;
using UnityEngine;

public class LevelListCreator : MonoBehaviour
{
    [SerializeField] private SceneRegistry _sceneRegistry;
    private List<SceneData> _levelList = new List<SceneData>();
    [SerializeField] private GameObject _listItemPrefab;
    
    private void Awake()
    {
        if (_sceneRegistry != null)
        _levelList = _sceneRegistry.AllGameplayLevels;
    }
    private void Start()
    {
        if (_levelList == null || _levelList.Count < 1) return;

        for (int i = 0; i < _levelList.Count; ++i)
        {
            AddListItem(_levelList[i]);
        }
    }

    private void AddListItem(SceneData scene)
    {
        GameObject newListItem = Instantiate(_listItemPrefab, this.transform);
        LevelButton button = newListItem.GetComponent<LevelButton>();

        button.Initialize(scene);
    }

}
