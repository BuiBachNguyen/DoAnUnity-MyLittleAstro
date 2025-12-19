using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public GameObject dialoguePanel;
    public Transform choiceContainer; 

    [Header("Prefabs")]
    public GameObject choiceButtonPrefab; 

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public float autoPlayDelay = 2f;

    private DialogueNode currentNode;
    private int lineIndex; 
    private bool isTyping = false;
    private bool isAutoPlay = false;
    private bool isShowingChoices = false;
    private Coroutine typingCoroutine;

    [Header("Input System")]
    public InputActionProperty nextAction; 

    private void OnEnable()
    {
        nextAction.action.Enable();
    }

    private void OnDisable()
    {
        nextAction.action.Disable();
    }

    void Update()
    {
        if (!isShowingChoices && nextAction.action.WasPressedThisFrame())
        {
            HandleInput();
        }
    }

    public void HandleInput()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentNode.dialogueLines[lineIndex];
            isTyping = false;

            if (isAutoPlay) StartCoroutine(AutoPlayNextAfterSkip());
        }
        else
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueNode startNode)
    {
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        DisplayNode(startNode);
    }

    public void DisplayNode(DialogueNode node)
    {
        currentNode = node;
        lineIndex = 0;
        isShowingChoices = false;
        node.onNodeEnter?.Invoke();

        UpdateCharacterUI();
        ClearChoices();
        StartLine();
    }

    void StartLine()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(currentNode.dialogueLines[lineIndex]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (isAutoPlay)
        {
            yield return new WaitForSeconds(autoPlayDelay);
            NextLine();
        }
    }

    public void NextLine()
    {
        if (lineIndex < currentNode.dialogueLines.Length - 1)
        {
            lineIndex++;
            StartLine();
        }
        else
        {
            if (currentNode.choices.Count > 0)
            {
                isShowingChoices = true;
                ShowChoices();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void ShowChoices()
    {
        ClearChoices();

        foreach (DialogueChoice choice in currentNode.choices)
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }

    IEnumerator AutoPlayNextAfterSkip()
    {
        yield return new WaitForSeconds(autoPlayDelay);
        if (!isTyping) NextLine();
    }

    public void ToggleAutoPlay()
    {
        isAutoPlay = !isAutoPlay;
        if (isAutoPlay && !isTyping) NextLine();
    }

    void OnChoiceSelected(DialogueChoice choice)
    {
        currentNode.onNodeExit?.Invoke();
        isShowingChoices = false;

        if (choice.nextNode != null)
        {
            DisplayNode(choice.nextNode);
        }
        else
        {
            EndDialogue();
        }
    }

    void UpdateCharacterUI()
    {
        if (currentNode.character != null)
        {
            if (nameText != null)
            {
                nameText.text = currentNode.character.characterName;
                nameText.color = currentNode.character.nameColor;
            }

            if (portraitImage != null)
            {
                portraitImage.sprite = currentNode.character.portrait;
                portraitImage.gameObject.SetActive(currentNode.character.portrait != null);
            }
        }
        else
        {
            if (nameText != null) nameText.text = "";
            if (portraitImage != null) portraitImage.gameObject.SetActive(false);
        }
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}