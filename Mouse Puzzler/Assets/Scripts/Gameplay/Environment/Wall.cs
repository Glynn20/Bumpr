using System.Security;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameEvent OnPlayWallBumpSound;
    [SerializeField] private SoundData _wallBumpSoundData;
    void OnCollisionEnter2D(Collision2D other)
    {
        // Must specifically be the Player
        IMovement playerMovement = other.gameObject.GetComponent<IMovement>();
        if ( playerMovement == null ) return;
        
        PlayerMoveState state = playerMovement.GetMoveState();
        if (state == PlayerMoveState.Running || state == PlayerMoveState.Overheating)
        {
            OnPlayWallBumpSound?.Raise(this, _wallBumpSoundData);  
        }
        
    }

}
