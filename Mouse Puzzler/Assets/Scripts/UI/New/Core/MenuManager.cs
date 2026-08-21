using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    [SerializeField] private Menu _initialMenu;
    private readonly Stack<Menu> menuStack = new Stack<Menu>();
    private bool _isClosingPauseMenus = false;

    public void OnPauseStateChanged(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is bool isNowPaused)
        {
            if (!isNowPaused)
            {
                ClosePauseMenus();
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        if (menuStack.Count > 0 && menuStack.Peek() == menu) return;
        
        if (menuStack.Count > 0)
        {
            menuStack.Peek().OnPause();
        }

        if (menuStack.Contains(menu))
        {
            RemoveFromStack(menu);
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

    public void ClosePauseMenus()
    {
        if (_isClosingPauseMenus) return;
        _isClosingPauseMenus = true; 

        while (menuStack.Count > 0)
        {
            CleanupDeadReferences();

            Menu topMenu = menuStack.Peek();

            if (topMenu.IsPartOfPauseMenu == false)
            {
                break;
            }

            topMenu = menuStack.Pop();
            topMenu.OnClose();
        }

        _isClosingPauseMenus  = false;
        CleanupDeadReferences();
        if (menuStack.Count > 0)
        {
            menuStack.Peek().OnResume();
        }
        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Deleting Duplicate");
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

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void CleanupDeadReferences()
    {
        if (menuStack.Count == 0) return;
        while (menuStack.Count > 0 && menuStack.Peek() == null)
        {
            menuStack.Pop();
        }
    }

    private void RemoveFromStack(Menu targetMenu)
    {
        Stack<Menu> tempStack = new Stack<Menu>();

        while (menuStack.Count > 0)
        {
            Menu popped = menuStack.Pop();

            if (popped != targetMenu)
            {
                tempStack.Push(popped);
            }
            else
            {
                popped.OnClose();
            }
        }

        while (tempStack.Count > 0)
        {
            menuStack.Push(tempStack.Pop());
        }
    }
}
