using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelBlock : MonoBehaviour, IPointerClickHandler
{
    public static event Action<int> OnLevelClicked;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite locking;
    [SerializeField] Sprite unlocking;

    [SerializeField] int levelIndex;
    [SerializeField] bool isLocking = false;

    public bool IsLocking
    {
        get { return isLocking; }
        set { isLocking = value; }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        if (spriteRenderer == null)
            Debug.Log("NULL ref at Level Block");
    }
    void Start()
    {
        spriteRenderer.sprite = (isLocking) ? locking : unlocking;
    }    

    public void ChangeStatus(bool value)
    {
        isLocking = value;
        spriteRenderer.sprite = (isLocking) ? locking : unlocking;
    }    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocking) return;
        Debug.Log("LevelBlock clicked: " + levelIndex);
        OnLevelClicked?.Invoke(levelIndex);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLocking) return;
        if(collision.gameObject.CompareTag(Tags.Player))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                if (normal.y > 0.5f)
                {
                    Debug.Log("LevelBlock kicked: " + levelIndex);
                    OnLevelClicked?.Invoke(levelIndex);
                }    
            }
        }    
    }
}
