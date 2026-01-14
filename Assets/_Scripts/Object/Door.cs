using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Door : MonoBehaviour
{
    [SerializeField] bool isOpening = false;
    [SerializeField] private List<Plate> plates = new();

    private Animator anim;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (plates == null || plates.Count == 0)
        {
            Debug.LogWarning($"{name}: No plates assigned");
            return;
        }

        foreach (var plate in plates)
        {
            plate.OnHoldingChanged += OnPlateChanged;
        }

        CheckDoor();
    }

    private void OnDisable()
    {
        if (plates == null) return;

        foreach (var plate in plates)
        {
            plate.OnHoldingChanged -= OnPlateChanged;
        }
    }

    private void OnPlateChanged(bool _)
    {
        CheckDoor();
    }

    private void CheckDoor()
    {

        bool allHolding = plates.All(p => p.IsHolding);
        if (isOpening == allHolding) return;

        isOpening = allHolding;
        anim.SetBool("isOpening", isOpening);
        col.enabled = !allHolding;
        //Debug.Log(allHolding ? "open" : "close");
        AudioManager.Instance.PlaySFX(AudioClipNames.Door);
    }
}
