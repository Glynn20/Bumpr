using NUnit.Framework;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private int _openRequests = 0; // Disables cursor at 0
    private bool _cursorEnabled;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetCursorEnabled(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is bool enable)
        {
            if (enable)
            {
                RequestEnableCursor();
            }
            else
            {
                RemoveRequestEnableCursor();
            }
        }
    }  

    public void RequestEnableCursor()
    {
        ++_openRequests;
        EvaluateCursorState();
    }
    public void RemoveRequestEnableCursor()
    {
        --_openRequests;
        Debug.Log(_openRequests);
        if (_openRequests < 0) _openRequests = 0;

        EvaluateCursorState();
    }
    private void EvaluateCursorState()
    {
        if (_openRequests > 0)
        {
            _cursorEnabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;  
        }   
        else
        {
            _cursorEnabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
