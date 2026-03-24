using UnityEngine;

public interface IActivatable
{
    bool IsActivatedOnStart {get; set;}
    bool IsActivated {get; set;}

    public void Activate();
    public void Deactivate();

}
