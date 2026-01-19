using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    [SerializeField] DialogueManager manager;
    [SerializeField] DialogueNode startNode;
    [SerializeField] GameObject MainUIPanel;

    void Start()
    {
        manager.StartDialogue(startNode);
        MainUIPanel.SetActive(false);
    }
}