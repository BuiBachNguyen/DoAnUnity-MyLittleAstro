using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "DialogSystem/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName; 
    public Sprite portrait;      
    public Color nameColor = Color.white; 
}