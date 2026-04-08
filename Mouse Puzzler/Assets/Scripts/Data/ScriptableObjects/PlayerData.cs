using UnityEngine;
[CreateAssetMenu(fileName ="New PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    
    [Range(1,100)]public int MaxHealth;
}
