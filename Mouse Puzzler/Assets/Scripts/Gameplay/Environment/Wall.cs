using System.Security;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameEvent OnPlayWallBumpSound;
    [SerializeField] private GameEvent OnPlayWallBumpParticle;
    [SerializeField] private SoundData _wallBumpSoundData;
    [SerializeField] private ParticleData _wallHitFX; 
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Must specifically be the Player
        IMovement playerMovement = other.gameObject.GetComponent<IMovement>();
        if ( playerMovement == null ) return;
        
        PlayerMoveState state = playerMovement.GetMoveState();
        if (state == PlayerMoveState.Running || state == PlayerMoveState.Overheating)
        {
            OnPlayWallBumpSound?.Raise(this, _wallBumpSoundData);
            
            if (_wallHitFX == null) return;  

            OnPlayWallBumpParticle?.Raise(this, _wallHitFX, other.contacts[0].point);
            
        }
        
    }

}
