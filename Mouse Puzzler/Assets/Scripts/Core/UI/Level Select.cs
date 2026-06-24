using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSelect : MonoBehaviour
{

    [SerializeField] private Canvas _levelSelectCanvas;
    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private TilemapRenderer[] _tileRenderers;

    public void OpenCanvas()
    {
        _levelSelectCanvas.enabled = true;
        _mainMenuCanvas.enabled = false;
        DisableRenderers();
    }
    public void CloseCanvas()
    {
        _levelSelectCanvas.enabled = false;
        _mainMenuCanvas.enabled = true;
        EnableRenderers();
    }

    public void Awake()
    {
        _levelSelectCanvas.enabled = false;
        _mainMenuCanvas.enabled = true; 
    }

    private void DisableRenderers()
    {
        foreach (TilemapRenderer r in _tileRenderers)
        {
            r.enabled = false;
        }
    }
        private void EnableRenderers()
    {
        foreach (TilemapRenderer r in _tileRenderers)
        {
            r.enabled = true;
        }
    }
}
