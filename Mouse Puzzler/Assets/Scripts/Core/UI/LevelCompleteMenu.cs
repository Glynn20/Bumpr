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

    private Canvas _canvas;
    private Coroutine _levelCompleteCoroutine;
    private Animator _animator;
    private float _totalPlayerDeaths = 0f;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _animator = GetComponent<Animator>();
        
        CloseMenu();
        _levelCompleteCoroutine = null;
    }

    public void OnPlayerDeath()
    {
        ++_totalPlayerDeaths;
    }

    public void OpenMenu() // TODO   Called on Level Completed
    {
        if (_levelCompleteCoroutine != null) Debug.LogWarning("Level Complete anim could not run. Coroutine not null.");
        _canvas.enabled = true;
        _animator.Play("LevelComplete");
        OnMenuOpen?.Raise(this, null);
    }

    public void CloseMenu() //TODO
    {
        OnMenuClose?.Raise(this, null);
        _canvas.enabled = false;
    }

    public void OnLevelComplete(Component sender, params object[] data)
    {
        OpenMenu();
    }
    public void OnNextLevelButtonClicked()
    {
        CloseMenu();
        OnRequestLoadNextLevel?.Raise(this, null);
    }

/*
enable canvas
Start Coroutine
Finish Coroutine

when finished, crown should be visible, death counter visible, overall score calculated and displayed
*/
    public void StartDeathCountCoroutine()
    {
        _levelCompleteCoroutine = StartCoroutine(DeathCountCoroutine());
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

    
}
