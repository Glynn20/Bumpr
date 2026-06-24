using NUnit.Framework;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
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
                EnableCursor();
            }
            else
            {
                DisableCursor();
            }
        }
    }  

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
