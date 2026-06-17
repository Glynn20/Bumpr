using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "New FallingSpriteData", menuName = "Data/FallingSpriteData")]

public class FallingSpriteData : ScriptableObject
{
    public int OrderInLayer = 4;
    public float MinFallSpeed = 0.8f;
    public float MaxFallSpeed = 2.2f;
    public float MinSize = 2.5f;
    public float MaxSize = 11.5f;
    public float MinAlpha = 2;
    public float MaxAlpha = 12;
    public Vector2 ColliderSize = new Vector2(.35f,.35f);
    public Sprite[] Sprites;
    
    public float GetRandomScaleAndSpeedProportion()
    {
        return Random.Range(0,1f);
    }

    public float GetAlphaSpread() => MaxAlpha - MinAlpha;
    public float GetFallSpeedSpread() => MaxFallSpeed - MinFallSpeed;
    public float GetScaleSpread() => MaxSize - MinSize;    
}
