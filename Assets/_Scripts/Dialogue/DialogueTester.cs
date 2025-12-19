using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    public DialogueManager manager;
    public DialogueNode startNode;

    void Start()
    {
        manager.StartDialogue(startNode);
    }
}