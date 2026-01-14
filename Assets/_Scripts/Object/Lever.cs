using System;
using UnityEngine;

public class Lever : MonoBehaviour
{

    [SerializeField] private bool isActivated;
    Animator anim;

    public event Action<bool> OnActivatedChanged;
    public bool IsActivated => isActivated;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void SetActivated(bool value)
    {
        if (isActivated == value) return;

        isActivated = value;
        anim.SetBool("isActivated", isActivated);
        OnActivatedChanged?.Invoke(isActivated);

        AudioManager.Instance.PlaySFX(AudioClipNames.Switch);
    }

}
