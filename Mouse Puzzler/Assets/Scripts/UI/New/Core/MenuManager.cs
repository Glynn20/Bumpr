using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private Menu _initialMenu;
    private readonly Stack<Menu> menuStack = new Stack<Menu>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (_initialMenu != null)
        {
            OpenMenu(_initialMenu);
        }
    }

    private void Update()
    {
        // TO CHANGE to update with unity event driven input 
    }

    public void OpenMenu(Menu menu)
    {
        if (menuStack.Count > 0)
        {
            Menu topMenu = menuStack.Peek();

            if (topMenu == menu) return;

            topMenu.OnPause();
        }

        menuStack.Push(menu);
        menu.OnOpen();
    }

    public void CloseMenu(Menu menu)
    {
        if (menuStack.Count == 0) return;

        if (menuStack.Peek() != menu)
        {
            Debug.LogWarning($"Trying to close menu '{menu.name}' that is not on top of the stack.");
            return;
        }

        Menu topMenu = menuStack.Pop();
        topMenu.OnClose();

        if (menuStack.Count > 0)
        {
            Menu previousMenu = menuStack.Peek();
            previousMenu.OnResume();
        }
    }

    public void CloseAll()
    {
        while (menuStack.Count > 0)
        {
            Menu menu = menuStack.Pop();
            menu.OnClose();
        }
    }
}
