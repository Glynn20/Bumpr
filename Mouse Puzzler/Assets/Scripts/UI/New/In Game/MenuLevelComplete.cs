using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;
using System;
public class MenuLevelComplete : Menu<MenuLevelComplete>
{

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;

    [Header("Config")]
    [SerializeField] private SceneData _sceneToLoad;
    [SerializeField] private float _deathCountCoroutineLength = 1f;
    [SerializeField] private string _levelCompleteAnimName;
    [SerializeField] private TextMeshProUGUI _deathCounterText;
    [SerializeField] private TextMeshProUGUI _levelRatingText;
    [SerializeField] private TextMeshProUGUI _levelTimeText;

    private Coroutine _levelCompleteCoroutine;
    private Animator _animator;
    private float _totalPlayerDeaths = 0f;
    private float _levelTime = 0f;
    private SceneData _currentSceneData;

    public override void OnBackPressed()
    {
        Hide();
    }
    public void OnSceneTransitionInAnimEnd()
    {
        OnRequestLoadScene?.Raise(this, _sceneToLoad);
    }
    
    public void OnLevelBegin(Component sender, params object[] data)
    {
        if (data != null && data[0] is SceneData newSceneData)
        {
            _currentSceneData = newSceneData;
            ResetLevelTimer();
        }    
    }
    public void OnPlayerDeath()
    {
        ++_totalPlayerDeaths;
    }
    public void OnLevelComplete(Component sender, params object[] data)
    {
        Show();

        if (_levelCompleteCoroutine != null) Debug.LogWarning("Level Complete anim could not run. Coroutine not null.");

        _animator.Play(_levelCompleteAnimName);
        _levelTimeText.text = ConvertLevelTimeToString();
        _levelRatingText.text = GetLevelRating(); 
    }
    public void StartDeathCountCoroutine()
    {
        _levelCompleteCoroutine = StartCoroutine(DeathCountCoroutine());
    }
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _levelCompleteCoroutine = null;
    }
    private void Update()
    {
        _levelTime += Time.deltaTime;
    }
    private void ResetLevelTimer()
    {
        _levelTime = 0f;
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
    private string GetLevelRating()
    {
        if ( _levelTime < _currentSceneData.STime)
        {
            return "S";
        }
        else if (_levelTime < _currentSceneData.ATime)
        {
            return "A";
        }
        else if (_levelTime < _currentSceneData.BTime)
        {
            return "B";
        }
        else if (_levelTime < _currentSceneData.CTime)
        {
            return "C";
        }
        return "D";
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
