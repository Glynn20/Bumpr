using UnityEngine;

public class LevelListCreator : MonoBehaviour
{
    [SerializeField] private SceneData[] _levelList;
    [SerializeField] private GameObject _listItemPrefab;
    

    private void Start()
    {
        if (_levelList == null) return;
        if (_levelList.Length < 1) return;

        for (int i = 0; i < _levelList.Length; ++i)
        {
            AddListItem(_levelList[i]);
        }
    }

    private void AddListItem(SceneData scene)
    {
        GameObject newListItem = Instantiate(_listItemPrefab, this.transform);
        LevelButton button = newListItem.GetComponent<LevelButton>();

        button.SceneOnClick = scene;
        button.Text.text = scene.SceneName; 
    }

}
