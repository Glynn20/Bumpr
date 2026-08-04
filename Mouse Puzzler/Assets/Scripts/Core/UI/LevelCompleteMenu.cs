using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour, IMenu
{

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadNextLevel;
    [SerializeField] private GameEvent OnMenuOpen;
    [SerializeField] private GameEvent OnMenuClose;
    [SerializeField] private float _deathCountCoroutineLength = 1f;
    [SerializeField] private TextMeshProUGUI _deathCounterText;
    [SerializeField] private TextMeshProUGUI _levelRatingText;
    [SerializeField] private TextMeshProUGUI _levelTimeText;

    private Canvas _canvas;
    private Coroutine _levelCompleteCoroutine;
    private Animator _animator;
    private float _totalPlayerDeaths = 0f;
    private float _levelTime = 0f;
    private SceneData _currentSceneData;

    public void OnLevelBegin(Component sender, params object[] data)
    {
        if (data != null && data[0] is SceneData newSceneData)
        {
            _currentSceneData = newSceneData;
            ResetLevelTimer();
        }    
    }

    public void SetLevelTimeText()
    {
        _levelTimeText.text = ConvertLevelTimeToString();
    }
    public void StartDeathCountCoroutine()
    {
        _levelCompleteCoroutine = StartCoroutine(DeathCountCoroutine());
    }
    public void OnPlayerDeath()
    {
        ++_totalPlayerDeaths;
    }

    public void OpenMenu() // TODO   Called on Level Completed
    {
        if (_canvas.enabled) return;
        
        if (_levelCompleteCoroutine != null) Debug.LogWarning("Level Complete anim could not run. Coroutine not null.");
        _canvas.enabled = true;
        _animator.Play("LevelComplete");
        OnMenuOpen?.Raise(this, null);
    }

    public void CloseMenu()
    {
        if (!_canvas.enabled) return;

        OnMenuClose?.Raise(this, null);
        _canvas.enabled = false;
    }

    public void OnDisable()
    {
        CloseMenu();
    }

    public void OnLevelComplete(Component sender, params object[] data)
    {
        OpenMenu();
        SetLevelTimeText();
        GetLevelRating(); 
    }
    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _animator = GetComponent<Animator>();
        
        CloseMenu();
        _levelCompleteCoroutine = null;
    }
    private void ResetLevelTimer()
    {
        _levelTime = 0f;
    }
    private void Update()
    {
        _levelTime += Time.deltaTime;
    }
    private IEnumerator DeathCountCoroutine()
    {
        /*
        Enable Animation Object (contains crown, death counter, gravestone)
        Play Number tick coroutine
        Once it ends, enable screen-wide click to continue button
        */
        float t = 0;
        float initialt = 0;
        float displayNum = 0;
        while (t < _deathCountCoroutineLength)
        {
            if (_totalPlayerDeaths >= 1)
            {
                displayNum = Mathf.Lerp(initialt, _totalPlayerDeaths, t);
                _deathCounterText.text = Math.Round(displayNum, 0).ToString();
            }
            t += Time.deltaTime;
            yield return null;
        }
        _levelCompleteCoroutine = null;
        yield return null;
    }
    private void GetLevelRating()
    {
        if ( _levelTime < _currentSceneData.STime)
        {
            _levelRatingText.text = "S";
        }
        else if (_levelTime < _currentSceneData.ATime)
        {
            _levelRatingText.text = "A";
        }
        else if (_levelTime < _currentSceneData.BTime)
        {
            _levelRatingText.text = "B";
        }
        else if (_levelTime < _currentSceneData.CTime)
        {
            _levelRatingText.text = "C";
        }
        else { _levelRatingText.text = "D";}
    }
    private string ConvertLevelTimeToString()
    {
        if (_levelTime < 0) return "Invalid";

        int seconds = (int) _levelTime % 60;
        int minutes = 0;

        if (_levelTime < 60)
        {
            return seconds < 10 ? $"00:0{seconds}" : $"00:{seconds}";
        }
        
        minutes = ((int) _levelTime - seconds) / 60;

        return minutes < 10 ? $"0{minutes}:{seconds}" : $"{minutes}:{seconds}";

    }
}
