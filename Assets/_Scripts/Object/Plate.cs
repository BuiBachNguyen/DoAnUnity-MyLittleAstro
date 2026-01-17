using UnityEngine;
using System;

public class Plate : MonoBehaviour
{

    [SerializeField] private bool isHolding;
    Animator anim;

    public event Action<bool> OnHoldingChanged;
    public bool IsHolding => isHolding;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void SetHolding(bool value)
    {
        if (isHolding == value) return;

        isHolding = value;
        anim.SetBool("isPressing", isHolding);
        OnHoldingChanged?.Invoke(isHolding);
        AudioManager.Instance.PlaySFX(AudioClipNames.Switch);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetHolding(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        SetHolding(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetHolding(false);
    }
}
