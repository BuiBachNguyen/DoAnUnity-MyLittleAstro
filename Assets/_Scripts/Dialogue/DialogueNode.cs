using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class DialogueChoice
{
    [TextArea(2, 4)]
    public string choiceText;
    public DialogueNode nextNode;
}

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "DialogSystem/Node")]
public class DialogueNode : ScriptableObject
{
    [Header("Character Info")]
    public CharacterData character; 

    [Header("Content")]
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Branching")]
    public List<DialogueChoice> choices;

    [Header("Events")]
    public UnityEvent onNodeEnter;
    public UnityEvent onNodeExit;
}